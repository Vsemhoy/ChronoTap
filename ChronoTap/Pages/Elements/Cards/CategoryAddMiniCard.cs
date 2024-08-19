
using ChronoTap.Style;
using ChronoTap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChronoTap.Style;




namespace ChronoTap.Pages.Elements.Cards
{
    internal class CategoryAddMiniCard : Frame
    {
        public Frame inFrame = new Frame();


        public VerticalStackLayout body = new VerticalStackLayout();

        public Image image = new Image();

        public TapGestureRecognizer onClick = new TapGestureRecognizer();

        public CategoryAddMiniCard()
        {
            this.MinimumHeightRequest = BaseTheme.THEME.categoryItemMiniCardMinHeight;
            this.Padding = new Thickness(0);
            this.CornerRadius = BaseTheme.THEME.CardCorner;
            this.BackgroundColor = Color.FromArgb("EEAAAAAA");
            this.BorderColor = Color.FromArgb("EEAAAAAA");
            this.inFrame.Padding = new Thickness(1);
            this.inFrame.CornerRadius = BaseTheme.THEME.CardCorner - 1;
            this.inFrame.BackgroundColor = Colors.White;
            this.inFrame.BorderColor = Color.FromArgb("FFEEEEEE");
            this.inFrame.Padding = 5;

            //VerticalStackLayout imageWrapperStack = new VerticalStackLayout();
            //imageWrapperStack.VerticalOptions = LayoutOptions.Center;
            this.image.Source = "plus_square.png";
            this.image.HeightRequest = BaseTheme.THEME.MiniCardHeightMinHeight;
            this.image.Opacity = 0.7;
            this.image.VerticalOptions = LayoutOptions.Center;

            this.body.VerticalOptions = LayoutOptions.Center;
            this.body.Children.Add(this.image);
            this.inFrame.Content = this.body;
            this.Content = this.inFrame;


            //this.body.GestureRecognizers.Add(this.onClick);
            this.inFrame.GestureRecognizers.Add(this.onClick);

            this.onClick.Tapped += OnClick_Tapped;
        }

        /// <summary>
        /// Open Category Editor as Create new Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnClick_Tapped(object? sender, TappedEventArgs e)
        {
            await Task.Delay(30);
            this.image.Opacity = 1;
            this.inFrame.Opacity = 0.85;
            this.Padding = 3;
            this.inFrame.Padding = 3;
            await Task.Delay(30);
            this.Padding = 2;
            this.image.Opacity = 0.9;
            this.inFrame.Opacity = 0.9;
            await Task.Delay(30);
            this.image.Opacity = 0.8;
            this.inFrame.Opacity = 0.95;
            this.Padding = 1;
            this.inFrame.Padding = 4;
            await Task.Delay(30);
            this.image.Opacity = 0.7;
            this.inFrame.Opacity = 1;
            this.Padding = 0;
            this.inFrame.Padding = 5;

            //await Navigation.PushAsync(ModalManager.categoryEditorModal);

        }

    }
}