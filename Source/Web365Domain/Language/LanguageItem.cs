using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web365Domain.Language
{
    public class LanguageItem : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
