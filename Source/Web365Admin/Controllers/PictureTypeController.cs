using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using Web365Domain.Language;

namespace Web365Admin.Controllers
{
    public class PictureTypeController : BaseController
    {

        private IPictureTypeRepositoryBE pictureTypeRepository;
        private ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public PictureTypeController(IPictureTypeRepositoryBE _pictureTypeRepository, ILanguageRepositoryBE _languageRepository)
        {
            this.baseRepository = _pictureTypeRepository;
            this.pictureTypeRepository = _pictureTypeRepository;
            this.languageRepository = _languageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = pictureTypeRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new PictureTypeItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            if (id.HasValue)
            {
                if (language.HasValue && language.Value != 0)
                {
                    var defaultLanguage = pictureTypeRepository.GetItemById<PictureTypeItem>(id.Value);
                    var typeInThisLanguage = pictureTypeRepository.GetSameTypeByLanguage(defaultLanguage.Parent ?? 0,
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
                    obj = pictureTypeRepository.GetItemById<PictureTypeItem>(id.Value);
                }
            }

            var listPictureType = pictureTypeRepository.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = listPictureType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblTypePicture objSubmit)
        {            

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                pictureTypeRepository.Add(objSubmit);
            }
            else
            {
                var obj = pictureTypeRepository.GetById<tblTypePicture>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                pictureTypeRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
