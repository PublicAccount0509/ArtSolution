using Art.Website.Common.Config;
using Art.Website.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Art.Website.Handlers
{
    public class FileUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var model = new FileUploadModel();

            HttpPostedFile file;
            try
            {
                file = context.Request.Files.Get(0);
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "请选择jpg,jpeg,png,gif类型的文件";
                context.Response.Write(ex.Message);
                return;
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".jpg" && Path.GetExtension(file.FileName).ToLower() != ".png" && Path.GetExtension(file.FileName).ToLower() != ".gif" && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
            {
                model.IsSuccess = false;
                model.Message = "请选择jpg,jpeg,png,gif类型的文件";
                var jsonString = WebExpress.Website.Serialization.JavaScriptJsonSerializer.Instance.Serialize(model);
                context.Response.Write(jsonString);
                return;
            }

            var folderName = ConfigSettings.Instance.UploadedFileFolder;
            var path = context.Server.MapPath(folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = string.Format("{0}_{1}.jpg", "avatart", DateTime.Now.ToString("yyyyMMddhhmmss"));
            var fullFileName = Path.Combine(path, fileName);
            file.SaveAs(fullFileName);

            model.IsSuccess = true;
            model.UploadedFileName = folderName + "\\" + fileName;
            var json = WebExpress.Website.Serialization.JavaScriptJsonSerializer.Instance.Serialize(model);
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
         
    }
}