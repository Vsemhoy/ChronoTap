
using ChronoTap.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Elements.Text
{
    internal class LegendLabel : VerticalStackLayout
    {
        public Label textLabel = new Label();

        public LegendLabel(string text)
        {
            HorizontalStackLayout topPadding = new HorizontalStackLayout();
            topPadding.HeightRequest = BaseTheme.THEME.modalLegendTopMargin;

            HorizontalStackLayout bottomPadding = new HorizontalStackLayout();
            bottomPadding.HeightRequest = BaseTheme.THEME.modalLegendTopMargin / 8;

            this.textLabel.Padding = 4;
            this.textLabel.Text = text;
            this.textLabel.FontSize = BaseTheme.THEME.modalLegendTextSize;

            this.Children.Add(topPadding);
            this.Children.Add(this.textLabel);
            this.Children.Add(bottomPadding);
        }

    }
}
