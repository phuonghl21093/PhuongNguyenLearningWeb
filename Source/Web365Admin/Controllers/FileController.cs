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
    public class FileController : BaseController
    {

        private readonly IFileRepositoryBE fileRepository;
        private readonly IFileTypeRepositoryBE fileTypeRepository;
        private readonly ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public FileController(IFileRepositoryBE _fileRepository, ILanguageRepositoryBE _languageRepository,
            IFileTypeRepositoryBE _fileTypeRepository)
        {
            this.baseRepository = _fileRepository;
            this.fileRepository = _fileRepository;
            this.languageRepository = _languageRepository;
            this.fileTypeRepository = _fileTypeRepository;
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

            var list = fileRepository.GetList(out total, name, listType, currentRecord, numberRecord, propertyNameSort, descending);

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
            var listType = fileTypeRepository.GetListForTree<object>();

            return Json(new
            {
                listType = listType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListByArrayID(int[] arrID)
        {
            var list = fileRepository.GetListByArrayID(arrID);

            return Json(new
            {
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(int? id, int? language)
        {
            var obj = new FileItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            if (id.HasValue)
            {
                if (language.HasValue && language.Value != 0)
                {
                    var defaultLanguage = fileRepository.GetItemById<FileItem>(id.Value);
                    var typeInThisLanguage = fileTypeRepository.GetSameTypeByLanguage(defaultLanguage.TypeID ?? 0,
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
                    obj = fileRepository.GetItemById<FileItem>(id.Value);
                }
            }

            var filtTypes = fileTypeRepository.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = filtTypes
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblFile objSubmit)
        {

            if (System.IO.File.Exists(Server.MapPath(ConfigWeb.TempPath + objSubmit.FileName)))
            {
                objSubmit.Size = new System.IO.FileInfo(Server.MapPath(ConfigWeb.TempPath + objSubmit.FileName)).Length;

                FileUtility.MoveFile(StaticEnum.FileType.File, objSubmit.FileName);
            }

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                fileRepository.Add(objSubmit);
            }
            else
            {
                var obj = fileRepository.GetById<tblFile>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                fileRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
