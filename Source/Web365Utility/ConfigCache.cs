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
    public static class ConfigCache
    {
        public static readonly int Cache10Minute = Convert.ToInt32(ConfigurationManager.AppSettings["Cache10Minute"]);
    }
}
