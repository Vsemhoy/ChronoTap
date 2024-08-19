using ChronoTap.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Style
{
    static class BaseTheme
    {
        public static ITheme THEME;

        public static void SetTheme(string name = "")
        {
            THEME = new Theme_LightStandard();
        }
    }
}
