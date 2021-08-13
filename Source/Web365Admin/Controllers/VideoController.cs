using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using System;
using Web365Domain.Language;

namespace Web365Admin.Controllers
{
    public class VideoController : BaseController
    {

        private readonly IVideoRepositoryBE _videoRepositoryBe;
        private readonly IVideoTypeRepositoryBE _videoTypeRepositoryBe;
        private readonly ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public VideoController(IVideoRepositoryBE videoRepositoryBe, ILanguageRepositoryBE _languageRepository,
            IVideoTypeRepositoryBE videoTypeRepositoryBe)
        {
            this.baseRepository = videoRepositoryBe;
            this._videoRepositoryBe = videoRepositoryBe;
            this.languageRepository = _languageRepository;
            this._videoTypeRepositoryBe = videoTypeRepositoryBe;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, string typeId, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var listType = string.IsNullOrEmpty(typeId) ? new int[] { } : typeId.Split(',').Select(int.Parse).ToArray();

            var total = 0;

            var list = _videoRepositoryBe.GetList(out total, name, listType, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPropertyFilter()
        {
            var listType = _videoTypeRepositoryBe.GetListForTree<object>();

            return Json(new
            {
                listType = listType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListByArrayID(int[] arrID)
        {
            var list = _videoRepositoryBe.GetListByArrayID(arrID);

            return Json(new
            {
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(int? id, int? language)
        {
            var obj = new VideoItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name                
            };
            if (id.HasValue)
            {
                if (language.HasValue && language.Value != 0)
                {
                    var defaultLanguage = _videoRepositoryBe.GetItemById<VideoItem>(id.Value);
                    var typeInThisLanguage = _videoTypeRepositoryBe.GetSameTypeByLanguage(defaultLanguage.TypeID ?? 0,
                        language.Value);
                    if (typeInThisLanguage != null)
                    {
                        obj.TypeID = typeInThisLanguage.ID;
                    }

                    obj.Number = defaultLanguage.Number;
                    obj.PictureID = defaultLanguage.PictureID;
                    obj.RootId = id;
                }
                else
                {
                    obj = _videoRepositoryBe.GetItemById<VideoItem>(id.Value);
                }
            }

            var listProductType = _videoTypeRepositoryBe.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = listProductType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblVideo objSubmit)
        {
            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _videoRepositoryBe.Add(objSubmit);
            }
            else
            {
                var obj = _videoRepositoryBe.GetById<tblVideo>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                _videoRepositoryBe.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
