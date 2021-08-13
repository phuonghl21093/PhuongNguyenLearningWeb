using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Web365Base;
using Web365Utility;
using System.Web;

namespace Web365DA.RDBMS.Front_End
{
    public abstract class BaseFE
    {

        protected Entities365 web365db = new Entities365();
        public Entities365 Web365DB
        {
            get
            {
                return web365db;
            }
        }

        //public static int? LanguageId { get; set; }
        //public static string LanguageName { get; set; }

        //protected BaseFE()
        //{
        //    try
        //    {
        //        if (LanguageId == null || LanguageId == 0 ||
        //            Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName != LanguageName)
        //        {
        //            LanguageName = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        //            var lang =
        //                Web365DB.tblLanguage.FirstOrDefault(
        //                    l => l.Code == LanguageName);
        //            LanguageId = lang != null ? lang.ID : 1;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        LanguageId = 1;
        //        LanguageName = "vi";
        //    }
        //}
    }
}
