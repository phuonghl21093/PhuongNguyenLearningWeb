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
    public class FileTypeController : BaseController
    {

        private IFileTypeRepositoryBE fileTypeRepository;
        private ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public FileTypeController(IFileTypeRepositoryBE _fileTypeRepository, ILanguageRepositoryBE _languageRepository)
        {
            this.baseRepository = _fileTypeRepository;
            this.languageRepository = _languageRepository;
            this.fileTypeRepository = _fileTypeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = fileTypeRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new FileTypeItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };
            if (id.HasValue)
            {
                if (language.HasValue && language.Value != 0)
                {
                    var defaultLanguage = fileTypeRepository.GetItemById<FileTypeItem>(id.Value);
                    var typeInThisLanguage = fileTypeRepository.GetSameTypeByLanguage(defaultLanguage.Parent ?? 0,
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
                    obj = fileTypeRepository.GetItemById<FileTypeItem>(id.Value);
                }  
            }

            var fileTypes = fileTypeRepository.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = fileTypes
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblTypeFile objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                fileTypeRepository.Add(objSubmit);
            }
            else
            {
                var obj = fileTypeRepository.GetById<tblTypeFile>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                fileTypeRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
