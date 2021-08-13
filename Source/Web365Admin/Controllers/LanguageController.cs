using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain.Language;

namespace Web365Admin.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageRepositoryBE _languageRepositoryBe;
        public LanguageController(ILanguageRepositoryBE languageRepositoryBe)
        {
            this._languageRepositoryBe = languageRepositoryBe;
            this.baseRepository = languageRepositoryBe;
        }

        // GET: Languages
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            int total;
            var list = _languageRepositoryBe.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total, list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListExpectDefault()
        {
            var data = _languageRepositoryBe.GetListExpectDefault();

            return Json(new
            {
                data
            },
            JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EditForm(int? id)
        {
            var obj = new LanguageItem();

            var listLayoutGroup = _languageRepositoryBe.GetListForTree<object>();

            if (id.HasValue)
                obj = _languageRepositoryBe.GetItemById<LanguageItem>(id.Value);

            return Json(new
            {
                data = obj,
                listLayoutGroup
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblLanguage objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _languageRepositoryBe.Add(objSubmit);
            }
            else
            {
                var obj = _languageRepositoryBe.GetById<tblLanguage>(objSubmit.ID);

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                _languageRepositoryBe.Update(obj);
            }

            //languageRepositoryBe.ResetListPicture(objSubmit.ID, Request["listPictureId"]);

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
