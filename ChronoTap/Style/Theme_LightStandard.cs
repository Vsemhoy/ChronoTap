using ChronoTap.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Style
{

    class Theme_LightStandard : ITheme
    {
        public string Name { get; set; }
        public Shadow CardShadow { get; set; }
        public int CardCorner { get; set; }

        public int MiniCardHeightMinHeight { get; set; }
        public int formFramePadding { get; set; }
        public int modalLegendTextSize { get; set; }
        public int modalLegendTopMargin { get; set; }
        public int categoryItemMiniCardMinHeight { get; set; }

        public Theme_LightStandard()
        {
            this.Name = "Light Standard";
            this.CardShadow = new Shadow
            {
                Brush = new SolidColorBrush(Colors.Black),
                Opacity = 0.5f,
                Offset = new Point(1, 8),
                Radius = 15
            };

            this.CardCorner = 4;

            this.MiniCardHeightMinHeight = 30;

            this.formFramePadding = 14;
            this.modalLegendTextSize = 12;
            this.modalLegendTopMargin = 8;
            this.categoryItemMiniCardMinHeight = 82;
        }


    }
}
