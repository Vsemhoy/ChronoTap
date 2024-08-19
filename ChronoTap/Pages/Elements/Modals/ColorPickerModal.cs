using ChronoTap.Style;
using ChronoTap.Style;
using Microsoft.Maui.Graphics;
using System;

namespace ChronoTap.Pages.Elements.Modals
{
    class ColorPickerModal : ContentPage
    {
        VerticalStackLayout body = new VerticalStackLayout();




        private Frame demoCard = new Frame();
        private Frame textFrame = new Frame();
        private Frame bgFrame = new Frame();

        public Label demoText = new Label();

        public Slider txt_Red_Slider = new Slider();
        public Slider txt_Green_Slider = new Slider();
        public Slider txt_Blue_Slider = new Slider();
        public Slider bg_Red_Slider = new Slider();
        public Slider bg_Green_Slider = new Slider();
        public Slider bg_Blue_Slider = new Slider();


        private int tR = 0;
        private int tG = 0;
        private int tB = 0;
        private int tA = 255;
        private int bR = 255;
        private int bG = 255;
        private int bB = 255;
        private int bA = 255;

        public Color TextColor = null;
        public Color BgColor = null;

        public Button setButton = new Button();
        public Button button_close = new Button();
        private Grid buttonGrid_1 = new Grid();

        public ColorPickerModal(string bgColor, string txColor) // ref Microsoft.Maui.Graphics.Color bgColor, ref Microsoft.Maui.Graphics.Color textColor
        {
            Grid grid = new Grid();
            this.buttonGrid_1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.buttonGrid_1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });

            grid.AddRowDefinition(new RowDefinition { Height = GridLength.Star });
            grid.AddRowDefinition(new RowDefinition { Height = GridLength.Auto });

            this.TextColor = Color.FromHex(txColor);
            this.BgColor = Color.FromHex(bgColor);

            this.body.BackgroundColor = Colors.White;
            this.body.Padding = 12;

            int cornerRadius = 9;

            this.demoCard.Shadow = BaseTheme.THEME.CardShadow;
            this.textFrame.Shadow = BaseTheme.THEME.CardShadow;


            this.demoCard.Margin = new Thickness(24);
            this.demoCard.BackgroundColor = this.BgColor;
            this.demoCard.BorderColor = Colors.LightSlateGray;
            this.demoCard.CornerRadius = cornerRadius;
            this.demoCard.Padding = 12;

            this.demoText.Text = "Demo Text";
            this.demoCard.Content = this.demoText;
            this.demoText.TextColor = this.TextColor;

            this.textFrame.Margin = new Thickness(22);
            this.textFrame.BackgroundColor = Colors.White;
            this.textFrame.BorderColor = Colors.LightSlateGray;
            this.textFrame.CornerRadius = cornerRadius;
            this.textFrame.Padding = 12;

            VerticalStackLayout txstack = new VerticalStackLayout();

            Label txLabel = new Label();
            txLabel.Text = "Text color";
            txstack.Children.Add(txLabel);

            int[] colorTValues = this.HexToRgb(txColor);
            int[] colorBValues = this.HexToRgb(bgColor);

            this.txt_Red_Slider.ThumbColor = Colors.Red;
            this.txt_Red_Slider.Margin = new Thickness(9);
            this.txt_Red_Slider.Minimum = 0;
            this.txt_Red_Slider.Maximum = 256;
            this.txt_Red_Slider.Value = colorTValues[0];
            this.txt_Red_Slider.ValueChanged += Slider_HandlerChanged;
            txstack.Children.Add(this.txt_Red_Slider);
            this.txt_Green_Slider.Margin = new Thickness(9);
            this.txt_Green_Slider.ThumbColor = Colors.Green;
            this.txt_Green_Slider.Minimum = 0;
            this.txt_Green_Slider.Maximum = 256;
            this.txt_Green_Slider.Value = colorTValues[1];
            this.txt_Green_Slider.ValueChanged += Slider_HandlerChanged;
            txstack.Children.Add(this.txt_Green_Slider);
            this.txt_Blue_Slider.Margin = new Thickness(9);
            this.txt_Blue_Slider.ThumbColor = Colors.Blue;
            this.txt_Blue_Slider.Minimum = 0;
            this.txt_Blue_Slider.Maximum = 256;
            this.txt_Blue_Slider.Value = colorTValues[2];
            this.txt_Blue_Slider.ValueChanged += Slider_HandlerChanged;
            txstack.Children.Add(this.txt_Blue_Slider);
            this.textFrame.Content = txstack;

            this.bgFrame.Margin = new Thickness(22);
            this.bgFrame.BackgroundColor = Colors.White;
            this.bgFrame.BorderColor = Colors.LightSlateGray;
            this.bgFrame.CornerRadius = cornerRadius;
            this.bgFrame.Padding = 12;

            VerticalStackLayout bgStack = new VerticalStackLayout();

            Label bxLabel = new Label();
            bxLabel.Text = "Background color";
            bgStack.Children.Add(bxLabel);

            this.bg_Red_Slider.ThumbColor = Colors.Red;
            this.bg_Red_Slider.Margin = new Thickness(9);
            this.bg_Red_Slider.Minimum = 0;
            this.bg_Red_Slider.Maximum = 256;
            this.bg_Red_Slider.Value = colorBValues[0];
            this.bg_Red_Slider.ValueChanged += Slider_HandlerChanged;
            bgStack.Children.Add(this.bg_Red_Slider);
            this.bg_Green_Slider.ThumbColor = Colors.Green;
            this.bg_Green_Slider.Margin = new Thickness(9);
            this.bg_Green_Slider.Minimum = 0;
            this.bg_Green_Slider.Maximum = 256;
            this.bg_Green_Slider.Value = colorBValues[1];
            this.bg_Green_Slider.ValueChanged += Slider_HandlerChanged;
            bgStack.Children.Add(this.bg_Green_Slider);
            this.bg_Blue_Slider.ThumbColor = Colors.Blue;
            this.bg_Blue_Slider.Margin = new Thickness(9);
            this.bg_Blue_Slider.Minimum = 0;
            this.bg_Blue_Slider.Maximum = 256;
            this.bg_Blue_Slider.Value = colorBValues[2];
            this.bg_Blue_Slider.ValueChanged += Slider_HandlerChanged;
            bgStack.Children.Add(this.bg_Blue_Slider);
            this.bgFrame.Content = bgStack;

            this.body.Children.Add(this.demoCard);
            this.body.Children.Add(this.textFrame);
            this.body.Children.Add(this.bgFrame);

            this.setButton.Text = "Set colors";
            this.setButton.Margin = BaseTheme.THEME.formFramePadding;
            this.button_close.Text = "Close";
            this.button_close.Margin = BaseTheme.THEME.formFramePadding;

            this.buttonGrid_1.Padding = BaseTheme.THEME.formFramePadding;
            this.buttonGrid_1.Children.Add(this.button_close);
            Grid.SetColumn(this.button_close, 0);
            this.buttonGrid_1.Children.Add(this.setButton);
            Grid.SetColumn(this.setButton, 1);


            //Content = this.body;
            grid.Children.Add(this.body);
            Grid.SetRow(this.body, 0);
            grid.Children.Add(this.buttonGrid_1);
            Grid.SetRow(this.buttonGrid_1, 1);
            this.Content = grid;

            this.setButton.Clicked += Button_close_Clicked;
            this.button_close.Clicked += Button_close_Clicked;
        }

        private async void Button_close_Clicked(object? sender, EventArgs e)
        {
            await Task.Delay(100);
            await Navigation.PopModalAsync();
        }



        private void Slider_HandlerChanged(object? sender, EventArgs e)
        {
            this.tR = (int)this.txt_Red_Slider.Value;
            this.tG = (int)this.txt_Green_Slider.Value;
            this.tB = (int)this.txt_Blue_Slider.Value;

            this.bR = (int)this.bg_Red_Slider.Value;
            this.bG = (int)this.bg_Green_Slider.Value;
            this.bB = (int)this.bg_Blue_Slider.Value;

            Color tclr = Color.FromRgba(tR, tG, tB, tA);
            Color bclr = Color.FromRgba(bR, bG, bB, bA);

            this.BgColor = bclr;
            this.TextColor = tclr;

            this.demoCard.BackgroundColor = bclr;
            this.demoText.TextColor = tclr;
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            // Handle any logic when the page appears
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Handle any cleanup when the page disappears
            //DisposeResources();
            //Navigation.PopModalAsync();
        }

        private void DisposeResources()
        {
            // Dispose or cleanup any resources


        }


        private int[] HexToRgb(string hex)
        {
            int[] result = [0, 0, 0];

            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }

            if (hex.Length != 6)
            {
                throw new ArgumentException("Invalid hex color format");
            }
            result[0] = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            result[1] = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            result[2] = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return result;
        }

    }
}
