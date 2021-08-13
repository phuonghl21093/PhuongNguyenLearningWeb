using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;
using Web365Utility;

namespace Web365
{
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
    public class BundleConfig
    {

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.IgnoreList.Clear();
            //AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new StyleBundle("~/csslibs").Include(
                                      "~/Content/owl-carousel/owl.carousel.css",
                      "~/Content/owl-carousel/owl.theme.css",
                      "~/Content/owl-carousel/owl.transitions.css",


                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/Content/css/animate.css",
                      "~/Content/ion-icons/css/ionicons.min.css",
                      "~/Content/construction-fonts/flaticon.css",

                      "~/Content/css/fotorama.css",
                      "~/Content/masterslider/style/masterslider.css",
                      "~/Content/masterslider/skins/default/style.css",
                      "~/Content/cubeportfolio/css/cubeportfolio.min.css",
                      "~/Content/dzsparallaxer/dzsparallaxer.css",
                      "~/Content/css/fotorama.css",
                      "~/Content/css/jquery.mb.YTPlayer.css",
                      "~/Content/css/style.css"
                      ));


            var bundle = new ScriptBundle("~/bundles/javascriptlibs").Include(
                        "~/Content/js/jquery-compress.js",
                        "~/Content/js/jquery.lazyload.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Content/js/jquery-migrate.min.js",
                        "~/Content/bootstrap/js/bootstrap.min.js",
                        "~/Content/js/bootstrap-hover-dropdown.min.js",
                        "~/Content/js/wow.min.js",
                        "~/Content/dzsparallaxer/dzsparallaxer.js",
                        "~/Content/js/bootstrap-hover-dropdown.min.js",
                        "~/Content/owl-carousel/owl.carousel.min.js",
                        "~/Content/js/custom.js",
                        "~/Content/masterslider/masterslider.min.js",
                        "~/Content/js/master-custom.js",
                        "~/Content/cubeportfolio/js/jquery.cubeportfolio.min.js",
                        "~/Content/js/masonry.pkgd.min.js",
                        "~/Content/js/jquery.popupoverlay.js",
                        "~/Content/js/fotorama.js"
                        );

            bundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundle);

            BundleTable.EnableOptimizations = ConfigWeb.EnableOptimizations;
        }
    }
}
