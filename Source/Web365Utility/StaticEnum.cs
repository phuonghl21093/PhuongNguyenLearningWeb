using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web365Utility
{
    public static class StaticEnum
    {
        /// <summary>
        /// Enum about type file when upload of download
        /// </summary>
        public enum FileType
        {
            //type file jpg, jpeg, png, gif...
            Image = 0,
            //thumb type file jpg, jpeg, png, gif...
            ImageThumb = 1,
            //type file doc, docx, xls, xlsx, pdf...
            Document = 2,
            //type file mp3, mp4, avi, mkv...
            Multimedia = 3,
            //type file rar, zip, dll, exe...
            File = 4
        }

        public enum TypeCache : int
        {
            Caching = 1,
            Redis = 2,
            Memcached = 3
        }

        public enum NewsChildCategory
        {
            Caching = 1,
            Redis = 2,
            Memcached = 3
        }

        public enum ArticleType
        {
            All = 0,
            Introduce = 1,
            Ability = 2,
            Service = 3,
            Project = 4,
            Document = 5,
            CSR = 6,
            News = 7,
            Contact = 8,
            Banner = 55
        }

        public enum ProjectChildCate
        {
            OnGoing = 12,
            Completed = 13
        }

        public enum SettingInfo
        {
            PhoneNumber = 1,
            Address = 2,
            Facebook = 3,
            Twitter = 4,
            LinkedIn = 5,
            Instagram = 6,
            Email = 7,
            Fax = 8,
            Youtube = 9,
            IsHaveLanding = 14,
        }

        public enum LayoutContent
        {
            BannerSlider = 1,
            OverviewContent = 2,
            Statistical = 3,
            Service = 4,
            Feedback = 5,
            Message = 6,
            Partner = 7,
            Certificate = 15
        }

        public enum ArticleGroup
        {
            Events = 1,
            TypicalFace = 2,
            FeaturedProject = 3,
            FeaturedNews = 9,
            Director = 11,
            Administrator = 12,
            Contruction = 13,
            Introduction = 26 //TuNT    15/04/2017  fix group Introduction
        }

        public enum LanguageId
        {
            Vietnamese = 1,
            English = 2
        }

        public enum AbilityChildCate
        {
            Human = 45,
            Equipment = 47,
            Certificate = 49,
        }

        public enum Library
        {
            Image = 8,
            Video = 1,
            Document = 1
        }

        public enum FeatureLibrary
        {
            Image = 9,
            Video = 2,
            Document = 2
        }
    }
}
