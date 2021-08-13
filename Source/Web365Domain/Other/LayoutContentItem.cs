using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web365Domain
{
    public class LayoutContentItem : BaseModel
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public int? PictureID { get; set; }
        public int? LayoutGroupId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeyword { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public string UrlPicture { get; set; }
        public string IconClassCss { get; set; }
        public int[] ListPictureID { get; set; }
        public List<PictureItem> ListPicture { get; set; }
        public int? LanguageId { get; set; }
        public int? RootId { get; set; }
        public string LanguageName { get; set; } 
        public int? Number { get; set; }
        public int? GroupNumber { get; set; }

        public List<LayoutContentItem> ListOtherLanguage { get; set; }
    }
}
