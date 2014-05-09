using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Website.Models
{
    public class ResultModel
    { 
        public ResultModel(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
    }
}