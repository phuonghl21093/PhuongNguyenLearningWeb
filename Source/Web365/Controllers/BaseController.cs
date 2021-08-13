using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Web365Business.Front_End.IRepository;
using Web365Business.Front_End.Repository;
using Web365DA.RDBMS.Front_End.Repository;
using Web365Utility;

namespace Web365.Controllers
{
    public abstract class BaseController : Controller
    {
        public string LanguageCode { get; set; }
        public static int LanguageId { get; set; }

        private static ILanguageRepositoryFE _languageRepositoryInstance;
        public static ILanguageRepositoryFE LanguageRepository
        {
            get {
                return _languageRepositoryInstance ??
                       (_languageRepositoryInstance = new LanguageRepositoryFE(new LanguageDAFERepository()));
            }
        }


        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                LanguageCode = (string)requestContext.RouteData.Values["lang"];
            }

            try
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageCode ?? ConfigWeb.DefaultLanguage);
                var currentLang = LanguageRepository.GetItemByCode(LanguageCode ?? ConfigWeb.DefaultLanguage);
                LanguageId = currentLang != null ? currentLang.ID : (int)StaticEnum.LanguageId.Vietnamese;

            }
            catch (Exception e)
            {
                //string.Format("Invalid language code '{0}'.", LanguageCode)
            }

            base.Initialize(requestContext);
        }
    }
}