using ChronoTap.Core;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Style;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Elements.Cards
{
    internal class CategoryMiniCard : Frame
    {
        public Frame inFrame = new Frame();


        public VerticalStackLayout body = new VerticalStackLayout();

        public Image image = new Image();

        public Label textLabel = new Label();

        public TapGestureRecognizer onClick = new TapGestureRecognizer();

        public bool isActive = false;

        public string id { get; set; }

        public CategoryMiniCard(EventCategory itemObject)
        {
            this.id = itemObject.Id;
            this.MinimumHeightRequest = BaseTheme.THEME.categoryItemMiniCardMinHeight;
            this.Padding = new Thickness(0);
            this.CornerRadius = BaseTheme.THEME.CardCorner;
            this.BackgroundColor = Color.FromHex(itemObject.BgColor);
            //this.BorderColor = Color.FromArgb("EEAAAAAA");
            this.inFrame.Padding = new Thickness(1);
            this.inFrame.CornerRadius = BaseTheme.THEME.CardCorner - 1;
            this.inFrame.BackgroundColor = Color.FromArgb("#22FFFFFF");
            this.inFrame.BorderColor = Colors.Transparent;
            //this.inFrame.Padding = 2;

            this.Shadow = BaseTheme.THEME.CardShadow;

            if (itemObject.Icon != string.Empty && itemObject.Icon != null)
            {
                this.image.Source = "ico/" + itemObject.Icon;
                this.image.HeightRequest = BaseTheme.THEME.MiniCardHeightMinHeight;
                this.image.Opacity = 1;
                this.image.VerticalOptions = LayoutOptions.Center;
                //CommunityToolkit.Maui.Behaviors.IconTintColorBehavior tintColor = new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior();
                //tintColor.TintColor = Color.FromHex(itemObject.TextColor);
                //this.image.Behaviors.Add(tintColor);
            }



            this.body.VerticalOptions = LayoutOptions.Center;


            this.textLabel.Text = itemObject.Title;
            this.textLabel.Padding = 2;
            this.textLabel.TextColor = Color.FromHex(itemObject.TextColor);
            this.textLabel.HorizontalTextAlignment = TextAlignment.Center;

            if (itemObject.Icon != string.Empty && itemObject.Icon != null)
            {
                this.body.Children.Add(this.image);

            }
            this.body.Children.Add(this.textLabel);
            this.inFrame.Content = this.body;
            this.Content = this.inFrame;

            this.onClick.NumberOfTapsRequired = 1;
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
            var shadow = new Shadow();
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);
            shadow.Brush = brush;


            await Task.Delay(40);

            shadow.Opacity = 0.99f;
            shadow.Offset = new Point(0, 5);
            shadow.Radius = 10;

            this.image.Opacity = 0.95;
            this.inFrame.Opacity = 0.85;
            this.Shadow = shadow;



            await Task.Delay(40);

            shadow.Opacity = 0.9f;
            shadow.Offset = new Point(0, 8);
            shadow.Radius = 12;

            this.image.Opacity = 0.9;
            this.inFrame.Opacity = 0.9;
            this.Shadow = shadow;



            await Task.Delay(40);

            shadow.Opacity = 0.8f;
            shadow.Offset = new Point(0.5, 7);
            shadow.Radius = 13;

            this.image.Opacity = 0.8;
            this.inFrame.Opacity = 0.95;
            this.Shadow = shadow;



            await Task.Delay(40);

            shadow.Opacity = 0.6f;
            shadow.Offset = new Point(1, 8);
            shadow.Radius = 15;

            this.image.Opacity = 1;
            this.inFrame.Opacity = 1;

            this.Shadow = shadow;

            //await Navigation.PushAsync(ModalManager.categoryEditorModal);
            //await Navigation.PushAsync(ModalManager.categoryEditorModal);
            //this.isActive = !this.isActive;
            //this.SetActive(this.isActive);
        }


        public async void SetActive(bool state)
        {
            this.isActive = state;
            if (this.isActive)
            {
                this.Padding = 1;
                this.inFrame.BorderColor = Color.FromArgb("EEAAAAAA");
                this.textLabel.FontAttributes = FontAttributes.Bold;
            }
            else
            {
                this.Padding = 0;
                this.inFrame.BorderColor = Colors.Transparent;
                this.textLabel.FontAttributes = FontAttributes.None;
            }
        }


    }
}