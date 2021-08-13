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
    public class MenuController : BaseController
    {

        private IMenuRepositoryBE menuRepository;
        private ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public MenuController(IMenuRepositoryBE _menuRepository, ILanguageRepositoryBE _languageRepository)
        {
            this.baseRepository = _menuRepository;
            this.menuRepository = _menuRepository;
            this.languageRepository = _languageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPropertyFilter()
        {
            var listType = menuRepository.GetListForTree<object>();

            return Json(new
            {
                listType = listType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetList(string name, int? parentId, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = menuRepository.GetList(out total, name, parentId, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new MenuItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };


            if (id.HasValue)
            {
                if (!language.HasValue || language.Value == 0)
                {
                    obj = menuRepository.GetItemById<MenuItem>(id.Value);
                }
                else
                {
                    var defaultLanguage = menuRepository.GetItemById<MenuItem>(id.Value);
                    var parentInThisLanguage = menuRepository.GetSameMenuInOtherLanguage(defaultLanguage.Parent ?? 0, language.Value);
                    if (parentInThisLanguage != null)
                    {
                        obj.Parent = parentInThisLanguage.ID;
                    }

                    obj.DisplayOrder = defaultLanguage.DisplayOrder;
                    obj.CssClass = defaultLanguage.CssClass;
                    obj.RootId = id;
                }
            }

            var listType = menuRepository.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = listType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblMenu objSubmit)
        {
            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.CreatedBy = User.Identity.Name;
                objSubmit.UpdatedBy = User.Identity.Name;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                menuRepository.Add(objSubmit);
            }
            else
            {
                var obj = menuRepository.GetById<tblMenu>(objSubmit.ID);

                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.UpdatedBy = User.Identity.Name;

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                menuRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
