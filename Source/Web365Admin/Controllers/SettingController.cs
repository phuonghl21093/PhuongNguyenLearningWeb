using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using Web365Domain.Language;
using Web365Domain.SettingInfo;
using Web365Utility;

namespace Web365Admin.Controllers
{
    public class SettingController : BaseController
    {
        private readonly ISettingRepositoryBE _settingContentRepository;
        private readonly ILanguageRepositoryBE languageRepository;
        // GET: Setiing

        public SettingController(ISettingRepositoryBE settingContentRepository, ILanguageRepositoryBE _languageRepository)
        {
            _settingContentRepository = settingContentRepository;
            languageRepository = _languageRepository;
            this.baseRepository = settingContentRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = _settingContentRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

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
            var obj = new SettingInfoItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            if (id.HasValue)
            {
                if (!language.HasValue || language.Value == 0)
                {
                    obj = _settingContentRepository.GetItemById<SettingInfoItem>(id.Value);
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
        public ActionResult Action(tblSetting objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                _settingContentRepository.Add(objSubmit);
            }
            else
            {
                var obj = _settingContentRepository.GetById<tblSetting>(objSubmit.ID);

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                _settingContentRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}