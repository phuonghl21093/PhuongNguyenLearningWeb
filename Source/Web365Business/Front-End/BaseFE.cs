using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Utility;
using Web365Cached;
using System.Web;

namespace Web365Business.Front_End
{
    public class BaseFE : CacheController
    {
        public BaseFE()
        {

        }

        public BaseFE(Web365Utility.StaticEnum.TypeCache typeCahe)
            : base(typeCahe)
        {

        } 
    }
}
