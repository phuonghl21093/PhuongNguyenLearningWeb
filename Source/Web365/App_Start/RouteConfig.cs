using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web365
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "AjaxDefault",
                url: "ajax/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //TuNT  15/04/2017 hard code introduction news
            routes.MapRoute(
                name: "GioiThieu",
                url: "gioi-thieu",
                defaults: new { controller = "Article", action = "Introduction" }
            );
            routes.MapRoute(
                name: "ThuNgo",
                url: "thu-ngo",
                defaults: new { controller = "Article", action = "Introduction" }
                );
            routes.MapRoute(
                name: "Introduction",
                url: "en/introduce",
                defaults: new { controller = "Article", action = "IntroductionEn" }
            );
            //10/8/2021 Phuong tao bai viet vechungtoi
            routes.MapRoute(
              name: "Vechungtoi",
              url: "ve-chung-toi",
              defaults: new { controller = "Article", action = "Vechungtoi" }
            );
            //10/8/2021 Phuong tao bai viet vechungtoi
            routes.MapRoute(
              name: "Ungdung",
              url: "ung-dung",
              defaults: new { controller = "Article", action = "Ungdung" }
            );
            //TuNT  15/04/2017 hard code introduction news

            routes.MapRoute(
                name: "NotFound",
                url: "not-found",
                defaults: new { controller = "Layout", action = "NotFound" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem/{s}",
                defaults: new { controller = "Article", action = "Search", s = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LocalizedSearch",
                url: "{lang}/search/{s}",
                constraints: new { lang = @"(\w{2})" },
                defaults: new { controller = "Article", action = "Search", s = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "LocalizedLibrary",
              url: "{lang}/library",
              constraints: new { lang = @"(\w{2})" },
              defaults: new { controller = "Library", action = "Index", lang = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Library",
              url: "thu-vien",
              defaults: new { controller = "Library", action = "Index" }
            );


            routes.MapRoute(
              name: "LocalizedLibraryCategory",
              url: "{lang}/library/{libraryCate}",
              constraints: new { lang = @"(\w{2})" },
              defaults: new { controller = "Library", action = "LibraryCategory", lang = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "LibraryCategory",
              url: "thu-vien/{libraryCate}",
              defaults: new { controller = "Library", action = "LibraryCategory" }
            );

            routes.MapRoute(
              name: "ArticleDetail",
              url: "{category}/{article}",
              constraints: new { category = @"(^[a-zA-Z0-9_-]{3,})" },
              defaults: new { controller = "Article", action = "CommonDetail" }
            );

            routes.MapRoute(
              name: "LocalizedArticleDetail",
              url: "{lang}/{category}/{article}",
              constraints: new { lang = @"(\w{2})", category = @"(^[a-zA-Z0-9_-]{3,})" },
              defaults: new { controller = "Article", action = "CommonDetail", lang = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Article",
              url: "{category}",
              constraints: new { category = @"(^[a-zA-Z0-9_-]{3,})" },
              defaults: new { controller = "Article", action = "Index" }
            );

            routes.MapRoute(
              name: "LocalizedArticle",
              url: "{lang}/{category}",
              constraints: new { lang = @"(\w{2})" },
              defaults: new { controller = "Article", action = "Index", lang = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "DefaultLocalized",
              url: "{lang}/{controller}/{action}/{id}",
              constraints: new { lang = @"(\w{2})" },
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
