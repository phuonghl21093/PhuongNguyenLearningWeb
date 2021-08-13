using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain.SettingInfo;
using Web365Utility;

namespace Web365.Controllers
{
    public class LayoutController : BaseController
    {
        private readonly IMenuRepositoryFE _menuFeRepository;
        private readonly ISettingRepositoryFE _settingDafeRepository;
        private readonly ILanguageRepositoryFE _languageFeRepository;
        private readonly IArticleRepositoryFE _articleRepositoryFe;
        private readonly IArticleTypeRepositoryFE _articleTypeRepository;
        private readonly ILayoutContentRepositoryFE _layoutContentFe;
        public LayoutController(IMenuRepositoryFE menuFeRepository, ISettingRepositoryFE settingDafeRepository, ILanguageRepositoryFE languageFeRepository, IArticleRepositoryFE articleRepositoryFe, IArticleTypeRepositoryFE articleTypeRepository, ILayoutContentRepositoryFE layoutContentFe)
        {
            this._menuFeRepository = menuFeRepository;
            this._settingDafeRepository = settingDafeRepository;
            this._languageFeRepository = languageFeRepository;
            this._articleRepositoryFe = articleRepositoryFe;
            this._articleTypeRepository = articleTypeRepository;
            this._layoutContentFe = layoutContentFe;
        }

        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            try
            {
                ViewBag.Address = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Address, LanguageId);
                ViewBag.Phone = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.PhoneNumber, LanguageId);
                ViewBag.Fax = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Fax, LanguageId);
                ViewBag.Email = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Email, LanguageId);
                ViewBag.Facebook = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Facebook, LanguageId);
                ViewBag.Twitter = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Twitter, LanguageId);
                ViewBag.Youtube = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Youtube, LanguageId);
                ViewBag.Instagram = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Instagram, LanguageId);

                ViewData.Model = _menuFeRepository.GetListByParent(ConfigWeb.MenuParentId, true, false, LanguageId);
                ViewBag.PhoneNumber = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.PhoneNumber,
                    LanguageId);
                ViewBag.Languages = _languageFeRepository.GetAll();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult Footer()
        {
            try
            {
                ViewBag.IntroduceType =
                    _articleTypeRepository.GetItemById(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Introduce));
                ViewBag.ServiceType =
                    _articleTypeRepository.GetItemById(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Service));

                ViewBag.Introduces =
                    _articleRepositoryFe.GetListByArrType(
                        new[] { GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Introduce) }, 0, 10);
                ViewBag.TopServices =
                    _articleRepositoryFe.GetListByType(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Service),
                        string.Empty, 0, 10);

                ViewBag.Address = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Address, LanguageId);
                ViewBag.Phone = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.PhoneNumber, LanguageId);
                ViewBag.Fax = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Fax, LanguageId);
                ViewBag.Email = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Email, LanguageId);
                ViewBag.Facebook = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Facebook, LanguageId);
                ViewBag.Twitter = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Twitter, LanguageId);
                ViewBag.Youtube = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Youtube, LanguageId);
                ViewBag.Instagram = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Instagram, LanguageId);
                //ViewBag.AddressPrimary = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.AddressPrimary, LanguageId);
                //ViewBag.PhoneNumberPrimary = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.PhoneNumberPrimary, LanguageId);

                ViewData.Model =
                    _layoutContentFe.GetListByGroupId(
                        GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Certificate))
                        .LayoutContents.FirstOrDefault();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }


        public ActionResult NotFound()
        {
            return View("~/Views/Shared/NotFound.cshtml");
        }


        public int GetArticleTypeIdByLanguage(int groupId)
        {
            try
            {
                return LanguageId == (int)StaticEnum.LanguageId.Vietnamese
                    ? groupId
                    : _articleTypeRepository.GetTypeInOtherLang(groupId, LanguageId).ID;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        public int GetLayoutGroupIdByLanguage(int groupId)
        {
            try
            {
                return LanguageId == (int)StaticEnum.LanguageId.Vietnamese
                    ? groupId
                    : _layoutContentFe.GetGroupInOtherLang(groupId, LanguageId).ID;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        public int GetGroupIdByLanguage(int groupId)
        {
            try
            {
                return LanguageId == (int)StaticEnum.LanguageId.Vietnamese
                    ? groupId
                    : _articleRepositoryFe.GetGroupInOtherLang(groupId, LanguageId).ID;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }
    }
}