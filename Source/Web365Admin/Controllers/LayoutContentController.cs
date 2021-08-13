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
    public class LayoutContentController : BaseController
    {

        private readonly ILayoutContentRepositoryBE _layoutContentRepository;
        private readonly IGroupLayoutContentRepositoryBE _groupLayoutContentRepository;
        private readonly ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public LayoutContentController(ILayoutContentRepositoryBE _layoutContentRepository, IGroupLayoutContentRepositoryBE _groupLayoutContentRepository, ILanguageRepositoryBE _languageRepository)
        {
            this._layoutContentRepository = _layoutContentRepository;
            this._groupLayoutContentRepository = _groupLayoutContentRepository;
            this.languageRepository = _languageRepository;
            this.baseRepository = _layoutContentRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = _layoutContentRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new LayoutContentItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            if (id.HasValue)
            {
                if (!language.HasValue || language.Value == 0)
                {
                    obj = _layoutContentRepository.GetItemById<LayoutContentItem>(id.Value);
                }
                else
                {
                    var defaultLanguage = _layoutContentRepository.GetItemById<LayoutContentItem>(id.Value);
                    var groupInThisLanguage = _groupLayoutContentRepository.GetSameGroupInOtherLang(defaultLanguage.LayoutGroupId ?? 0, language.Value);
                    if (groupInThisLanguage != null)
                    {
                        obj.LayoutGroupId = groupInThisLanguage.ID;
                    }

                    obj.IconClassCss = defaultLanguage.IconClassCss;
                    obj.PictureID = defaultLanguage.PictureID;
                    obj.RootId = id;
                }
            }

            var listLayoutGroup = _groupLayoutContentRepository.GetListForTree<object>(obj.LanguageId.Value);
            return Json(new
            {
                data = obj,
                listLayoutGroup
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblLayoutContent objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _layoutContentRepository.Add(objSubmit);
            }
            else
            {             
                var obj = _layoutContentRepository.GetById<tblLayoutContent>(objSubmit.ID);
                
                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                _layoutContentRepository.Update(obj);
            }

            _layoutContentRepository.ResetListPicture(objSubmit.ID, Request["listPictureId"]);

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
