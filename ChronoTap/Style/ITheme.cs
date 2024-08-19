using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Style
{
    interface ITheme
    {
        public string Name { get; set; }

        public Shadow CardShadow { get; set; }

        public int CardCorner { get; set; }

        public int MiniCardHeightMinHeight { get; set; }

        public int formFramePadding { get; set; }
        public int modalLegendTextSize { get; set; }
        public int modalLegendTopMargin { get; set; }
        public int categoryItemMiniCardMinHeight { get; set; }
    }
}
