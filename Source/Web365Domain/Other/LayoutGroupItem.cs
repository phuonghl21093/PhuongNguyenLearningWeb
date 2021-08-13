using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web365Domain.Other
{
    public class LayoutGroupItem : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public int? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? RootId { get; set; }
        public List<LayoutGroupItem> ListOtherLanguage { get; set; }
        public List<LayoutContentItem> LayoutContents { get; set; }
    }
}
