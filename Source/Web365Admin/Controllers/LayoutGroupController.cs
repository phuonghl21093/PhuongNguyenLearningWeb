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
using Web365Domain.Other;

namespace Web365Admin.Controllers
{
    public class LayoutGroupController : BaseController
    {

        private IGroupLayoutContentRepositoryBE layoutContentRepository;
        private ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public LayoutGroupController(IGroupLayoutContentRepositoryBE _layoutContentRepository, ILanguageRepositoryBE _languageRepository)
        {
            this.layoutContentRepository = _layoutContentRepository;
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
            var list = layoutContentRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new LayoutGroupItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            if (id.HasValue)
            {
                if (!language.HasValue || language.Value == 0)
                {
                    obj = layoutContentRepository.GetItemById<LayoutGroupItem>(id.Value);
                }
                else
                {
                    obj.RootId = id;
                }
            }

            return Json(new
            {
                data = obj
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblLayoutGroup objSubmit)
        {
            try
            {
                if (objSubmit.ID == 0)
                {
                    objSubmit.DateCreated = DateTime.Now;
                    objSubmit.DateUpdated = DateTime.Now;
                    objSubmit.IsDeleted = false;
                    objSubmit.IsShow = true;
                    layoutContentRepository.Add(objSubmit);
                }
                else
                {
                    var obj = layoutContentRepository.GetById<tblLayoutGroup>(objSubmit.ID);

                    UpdateModel(obj);

                    objSubmit.DateUpdated = DateTime.Now;

                    layoutContentRepository.Update(obj);
                }
            }
            catch (Exception e)
            {
                
            }

            //layoutContentRepository.ResetListPicture(objSubmit.Id, Request["listPictureId"]);

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
