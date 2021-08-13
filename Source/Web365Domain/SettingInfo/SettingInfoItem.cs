using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web365Domain.SettingInfo
{
    public class SettingInfoItem : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public string LanguageName { get; set; }
        public int? LanguageId { get; set; }
        public int? RootId { get; set; }
        public List<SettingInfoItem> ListOtherLanguage { get; set; }
    }
}
