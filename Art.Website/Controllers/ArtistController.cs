using Art.BussinessLogic;
using Art.Data.Domain;
using Art.Website.Models;
using Art.Website.Models.Views;
using Art.Website.Models.Views.AristManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebExpress.Core;
using WebExpress.Core.Guards;
using WebExpress.Core.TypeExtensions;
using WebExpress.Website.Exceptions;

namespace Art.Website.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Edit Page
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var artist = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetArtist(id);
            Guard.IsNotNull<DataNotFoundException>(artist);

            var model = GetArtistEditModel(artist);

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var artist = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetArtist(id);
            Guard.IsNotNull<DataNotFoundException>(artist);

            var model = ArtistDetailModelTranslator.Instance.Translate(artist);

            return PartialView("_Detail", model);
        }

        public JsonResult Delete(int id)
        {
            var artist = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetArtist(id);
            Art.BussinessLogic.ArtistBussinessLogic.Instance.Delete(artist);
            var model = new ResultModel(true, string.Empty);
            return Json(model);
        }

        public JsonResult Publish(int id)
        {
            var artist = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetArtist(id);
            artist.IsPublic = true;
            Art.BussinessLogic.ArtistBussinessLogic.Instance.Update(artist);
            var model = new ResultModel(true, string.Empty);
            return Json(model);
        }

        public JsonResult UnPublish(int id)
        {
            var artist = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetArtist(id);
            List<string> reasons;
            var model = new ResultModel(true, string.Empty);
            if (Art.BussinessLogic.ArtistBussinessLogic.Instance.CanUnpublish(artist, out reasons))
            {
                artist.IsPublic = false;
                Art.BussinessLogic.ArtistBussinessLogic.Instance.Update(artist);
            }
            else
            {
                model = new ResultModel(false, string.Join(",", reasons));
            }
            return Json(model);
        }


        public ViewResult Add()
        {
            var artist = new Artist();
            var model = GetArtistEditModel(artist);
            return View("Edit", model);
        }

        [HttpPost]
        public JsonResult Add(ArtistModel model)
        {
            var artist = ArtistTranslator.Instance.Translate(model);


            ArtistBussinessLogic.Instance.Add(artist);

            var result = new ResultModel(true, "add successfully!");
            return Json(result);
        }

        [HttpPost]
        public JsonResult Update(ArtistModel model)
        {
            var artist = ArtistTranslator.Instance.Translate(model);

            ArtistBussinessLogic.Instance.Update(artist);

            var result = new ResultModel(true, "update successfully!");

            return Json(result);

        }
        #endregion

        public ActionResult List(SearchCriteria criteria)
        {
            var model = GetPagedArtistModel(criteria);
            return PartialView("_List",model);
        }

        private PagedArtistModel GetPagedArtistModel(SearchCriteria criteria)
        {
            var artistItems = new List<ArtistItem>();
            var pagedArtists = ArtistBussinessLogic.Instance.SearchArtists(criteria.NamePart, criteria.ProfessionId, criteria.PagingRequest);
            foreach (var item in pagedArtists)
            {
                var artist = ArtistItemTranslator.Instance.Translate(item);
                artist.CanUnPublish = Art.BussinessLogic.ArtistBussinessLogic.Instance.CanUnpublish(item);
                artistItems.Add(artist);
            }

            var model = new PagedArtistModel(artistItems, pagedArtists.PagingResult);
            return model;
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = new ArtistManageModel();

            var defaultCriteria = new SearchCriteria(10);
            model.PagedArtists = GetPagedArtistModel(defaultCriteria);

            var professions = ArtistBussinessLogic.Instance.GetProfessions();
            model.Professions = ProfessionTranslator.Instance.Translate(professions).ToList();

            return View(model);
        }


        private ArtistEditModel GetArtistEditModel(Artist artist)
        {
            var professions = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetProfessions();
            var genres = Art.BussinessLogic.ArtistBussinessLogic.Instance.GetGenres();
            var model = new ArtistEditModel
            {
                Artist = ArtistTranslator.Instance.Translate(artist),
                SourceProfessions = ProfessionTranslator.Instance.Translate(professions).ToArray(),
                SourceGenres = GenreTranslator.Instance.Translate(genres).ToArray(),
                Degrees = EnumExtenstion.GetValueTexts<Degree>()
            };
            return model;
        }

    }
}
