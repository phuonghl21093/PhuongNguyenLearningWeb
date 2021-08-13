using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Web365Utility
{
    public static class ConfigWeb
    {
        public static readonly string TempPath = ConfigurationManager.AppSettings["TempUpload"];

        public static readonly string ImageThumpPath = ConfigurationManager.AppSettings["UploadImageThumb"];

        public static readonly string ImagePath = ConfigurationManager.AppSettings["UploadImage"];

        public static readonly string FilePath = ConfigurationManager.AppSettings["UploadFile"];

        public static readonly bool UseCache = Convert.ToBoolean(ConfigurationManager.AppSettings["UseCache"]);

        public static readonly bool UseOutputCache = Convert.ToBoolean(ConfigurationManager.AppSettings["UseOutputCache"]);

        public static readonly int MinOnline = Convert.ToInt32(ConfigurationManager.AppSettings["MinOnline"]);

        public static readonly int PageSizeNews = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeNews"]);

        public static readonly int PageSizeProduct = Convert.ToInt32(ConfigurationManager.AppSettings["PageSizeProduct"]);

        public static readonly int FeaturedProject = Convert.ToInt32(ConfigurationManager.AppSettings["FeaturedProject"]);

        public static readonly int FeaturedNews = Convert.ToInt32(ConfigurationManager.AppSettings["FeaturedNews"]);
        //TuNT    15/04/2017  fix group Introduction
        public static readonly int Introduction = Convert.ToInt32(ConfigurationManager.AppSettings["Introduction"]);
        public static readonly int IntroductionEn = Convert.ToInt32(ConfigurationManager.AppSettings["IntroductionEn"]);

        public static readonly int FeaturedEventInLeft = Convert.ToInt32(ConfigurationManager.AppSettings["FeaturedEventInLeft"]);

        public static readonly int TypicalFaceInLeft = Convert.ToInt32(ConfigurationManager.AppSettings["TypicalFaceInLeft"]);

        public static readonly int FeaturedService = Convert.ToInt32(ConfigurationManager.AppSettings["FeaturedService"]);

        public static readonly int OtherNewsHomepageInCategory = Convert.ToInt32(ConfigurationManager.AppSettings["LastestNew"]);

        public static readonly int OtherNewsRightBarInCategory = Convert.ToInt32(ConfigurationManager.AppSettings["OtherNewsRightBarInCategory"]);

        public static readonly int OtherNewsRightBarInCategoryQuantity = Convert.ToInt32(ConfigurationManager.AppSettings["OtherNewsRightBarInCategoryQuantity"]);

        public static readonly int AchievementHighlight = Convert.ToInt32(ConfigurationManager.AppSettings["AchievementHighlight"]);

        public static readonly int AchievementHighlightQuantity = Convert.ToInt32(ConfigurationManager.AppSettings["AchievementHighlightQuantity"]);

        public static readonly int ListNewsInCategory = Convert.ToInt32(ConfigurationManager.AppSettings["ListNewsInCategory"]);

        public static readonly string MenuParentId = ConfigurationManager.AppSettings["MenuParentId"];

        public static readonly string SpecialArticle = ConfigurationManager.AppSettings["SpecialArticle"];

        public static readonly string OtherArticle = ConfigurationManager.AppSettings["OtherArticle"];

        //phuong Manhinhapp
        public static readonly int Manhinhapp = Convert.ToInt32(ConfigurationManager.AppSettings["Manhinhapp"].ToString());
        //phuong tinhnangdinhvivaphanloaivuottroi
        public static readonly int tinhnangdinhvivaphanloaivuottroi = Convert.ToInt32(ConfigurationManager.AppSettings["tinhnangdinhvivaphanloaivuottroi"].ToString());       
        //phuong taiungdung
        public static readonly int taiungdung = Convert.ToInt32(ConfigurationManager.AppSettings["taiungdung"].ToString());
        //phuong vechungtoi
        public static readonly int vechungtoi = Convert.ToInt32(ConfigurationManager.AppSettings["vechungtoi"].ToString());

        public static readonly bool EnableOptimizations = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableOptimizations"]);

        public static readonly string DefaultLanguage = ConfigurationManager.AppSettings["DefaultLanguage"];

        public static readonly int ListNewsInRootCate = Convert.ToInt32(ConfigurationManager.AppSettings["ListNewsInRootCate"]);

        public static readonly int ListNewsInCate = Convert.ToInt32(ConfigurationManager.AppSettings["ListNewsInCate"]);

        public static readonly int ListCertificate = Convert.ToInt32(ConfigurationManager.AppSettings["ListCertificate"]);

        public static readonly int ListOtherProjects = Convert.ToInt32(ConfigurationManager.AppSettings["ListOtherProjects"]);

        public static readonly int ListProject = Convert.ToInt32(ConfigurationManager.AppSettings["ListProject"]);

        public static readonly int FeatureImages = Convert.ToInt32(ConfigurationManager.AppSettings["FeatureImages"]);

        public static readonly int FeatureVideos = Convert.ToInt32(ConfigurationManager.AppSettings["FeatureVideos"]);

        public static readonly int FeatureFiles = Convert.ToInt32(ConfigurationManager.AppSettings["FeatureFiles"]);
        public static readonly int ArticleProjectTypeId = Convert.ToInt32(ConfigurationManager.AppSettings["ArticleProjectTypeId"]);

        //tunt 14/09/2020   them config fix loi
        public static readonly int TopHighlightInNewsEvents = Convert.ToInt32(ConfigurationManager.AppSettings["TopHighlightInNewsEvents"]);
        public static readonly int TopOtherInNewsEvents = Convert.ToInt32(ConfigurationManager.AppSettings["TopOtherInNewsEvents"]);
        //tunt 14/09/2020   them config fix loi

        public static readonly List<int> ListNewsRootCategoryId =
            !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ListNewsRootCategoryId"])
            ? ConfigurationManager.AppSettings["ListNewsRootCategoryId"].Split(',').Select(c => Convert.ToInt32(c)).ToList()
            : new List<int>();

        //Config 1 số loại bài viết
        public static readonly int ArticleTypeOutstandingDesigns = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ArticleTypeOutstandingDesigns"]) ? Convert.ToInt32(ConfigurationManager.AppSettings["ArticleTypeOutstandingDesigns"]) : 0;
        public static readonly int ArticleTypeLeadership = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ArticleTypeLeadership"]) ? Convert.ToInt32(ConfigurationManager.AppSettings["ArticleTypeLeadership"]) : 0;
        public static readonly int OurService = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["OurService"]) ? Convert.ToInt32(ConfigurationManager.AppSettings["OurService"]) : 0;
        public static readonly int OpenLetter = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["OpenLetter"]) ? Convert.ToInt32(ConfigurationManager.AppSettings["OpenLetter"]) : 0;
        public static readonly int Capacity = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["Capacity"]) ? Convert.ToInt32(ConfigurationManager.AppSettings["Capacity"]) : 0;
        
    }
}
