﻿using System.Web;
using System.Web.Mvc;
using Web365Utility;

namespace Web365
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
