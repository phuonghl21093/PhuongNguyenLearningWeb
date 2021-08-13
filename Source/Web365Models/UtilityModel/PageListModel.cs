using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web365Models.UtilityModel
{
    public class PageListModel<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
    }
}
