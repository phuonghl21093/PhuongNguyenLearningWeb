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
    public class VideoTypeController : BaseController
    {

        private readonly IVideoTypeRepositoryBE _videoTypeRepository;
        private readonly ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public VideoTypeController(IVideoTypeRepositoryBE videoTypeRepository, ILanguageRepositoryBE _languageRepository)
        {
            this.baseRepository = videoTypeRepository;
            this.languageRepository = _languageRepository;
            this._videoTypeRepository = videoTypeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = _videoTypeRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(int? id, int? language)
        {
            var obj = new VideoTypeItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            if (id.HasValue)
            {
                if (language.HasValue && language.Value != 0)
                {
                    var defaultLanguage = _videoTypeRepository.GetItemById<VideoTypeItem>(id.Value);
                    var typeInThisLanguage = _videoTypeRepository.GetSameTypeByLanguage(defaultLanguage.Parent ?? 0,
                        language.Value);
                    if (typeInThisLanguage != null)
                    {
                        obj.Parent = typeInThisLanguage.ID;
                    }

                    obj.Number = defaultLanguage.Number;
                    obj.PictureID = defaultLanguage.PictureID;
                    obj.RootId = id;
                }
                else
                {
                    obj = _videoTypeRepository.GetItemById<VideoTypeItem>(id.Value);
                }
            }

            var videoTypes = _videoTypeRepository.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = videoTypes
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblTypeVideo objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _videoTypeRepository.Add(objSubmit);
            }
            else
            {
                var obj = _videoTypeRepository.GetById<tblTypeVideo>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                _videoTypeRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
