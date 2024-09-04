using ChronoTap.Storage.Database.Models;
using ChronoTap.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoTap.Core;

namespace ChronoTap.Pages.Elements.Cards
{
    internal class TypeStackCard_in_Category : HorizontalStackLayout
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Url2 { get; set; }

        public bool IsActive { get; set; }


        private StackLayout swiperBody = new StackLayout();
        private Grid gridRow = new Grid();
        public TapGestureRecognizer tapHandler = new TapGestureRecognizer();

        public TapGestureRecognizer triggerTapHandler = new TapGestureRecognizer();

        public double fullWidth = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density) * (DeviceDisplay.Current.MainDisplayInfo.Density * 200);

        public SwipeView swiper = new SwipeView();
        public SwipeItem swipeLeftItem = new SwipeItem();
        public SwipeItem swipeRightItem = new SwipeItem();

        public Image image = new Image();


        EventType sourceObject { get; set; }

        Label title = new Label();
        Label descr = new Label();



        public TypeStackCard_in_Category(EventType typeCard)
        {
            this.IsActive = false;
            if (LocalStorage.ActiveChrono != null)
            {
                if (LocalStorage.ActiveChrono.TypeId == typeCard.Id)
                {
                    this.IsActive = true;
                }
            }
            this.sourceObject = typeCard;
            this.id = typeCard.Id;

            this.WidthRequest = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);
            gridRow.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            gridRow.ColumnDefinitions.Add(new ColumnDefinition { Width = 46 }); // Icon
            gridRow.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star }); // Name
            //gridRow.ColumnDefinitions.Add(new ColumnDefinition {Width = 46}); // Activator

            StackLayout iconContainer = new StackLayout();

            this.image.Source = "src/" + typeCard.Icon;
            this.image.HeightRequest = 40;
            this.image.Margin = 2;
            iconContainer.Children.Add(this.image);
            iconContainer.Padding = new Thickness(3);
            iconContainer.VerticalOptions = LayoutOptions.Center;

            VerticalStackLayout textContainer = new VerticalStackLayout();
            title.Text = typeCard.Title;
            title.FontSize = 14;
            //title.TextColor = Colors.White;
            textContainer.Children.Add(title);
            textContainer.Padding = new Thickness(3);


            descr.Text = typeCard.Description;
            descr.FontSize = 9;
            descr.TextColor = Colors.Black;
            descr.Opacity = 0.65;
            textContainer.Children.Add(descr);




            this.gridRow.BackgroundColor = Colors.WhiteSmoke;

            gridRow.Children.Add(iconContainer);
            Grid.SetRow(iconContainer, 0);
            Grid.SetColumn(iconContainer, 0);
            gridRow.Children.Add(textContainer);
            Grid.SetRow(textContainer, 0);
            Grid.SetColumn(textContainer, 1);


            this.gridRow.WidthRequest = (DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density);


            Button btn = new Button { Text = "Super red ", BackgroundColor = Colors.Red };


            //body.Padding = new Thickness(3);
            this.BackgroundColor = Colors.WhiteSmoke;

            //body.GestureRecognizers

            tapHandler.NumberOfTapsRequired = 2;
            //tapHandler.Tapped += this.TapEventStyle;
            tapHandler.Tapped += RunTaskEvent;
            swiperBody.GestureRecognizers.Add(tapHandler);





            this.swipeLeftItem.Text = "Edit type";
            this.swipeLeftItem.BackgroundColor = Colors.Gold;


            this.swipeRightItem.Text = "Run Task";
            this.swipeRightItem.BackgroundColor = Colors.LightSeaGreen;

            this.swiper.LeftItems.Add(swipeLeftItem);
            swiperBody.Children.Add(gridRow);
            this.swiper.Content = swiperBody;
            this.swiper.RightItems.Add(swipeRightItem);
            swipeLeftItem.Clicked += SwipeLeftItem_OpenTypeEditor_Clicked;
            swipeRightItem.Clicked += SwipeRightItem_RunTask_Clicked;


            this.Children.Add(swiper);
            this.SetTriggerState(400);


        }

        private async void RunTaskEvent(object? sender, TappedEventArgs e)
        {
            if (this.IsActive == true)
            {
                ModalManager.eventEditorModal.SetData(LocalStorage.ActiveType, LocalStorage.ActiveChrono);

                await Navigation.PushAsync(ModalManager.eventEditorModal);
                return;

            }
            await PageManager.TypeBrowserInCategoryPage.DeactivateAllCards(this.id);
            await ChronoEvent.StopAllTasks();
            this.IsActive = true;
            LocalStorage.ActiveCategory = LocalStorage.OpenedCategory;
            LocalStorage.ActiveType = this.sourceObject;
            LocalStorage.ActiveChrono = await ChronoEvent.StartTask();
            this.SetTriggerState(100);
            //LocalStorage.ActiveType = LocalStorage.
            PageManager.MainPage.categoryStack.SetActiveCard();
            PageManager.MainPage.SetActiveCard();
        }

        private async void SwipeRightItem_RunTask_Clicked(object? sender, EventArgs e)
        {
            await ChronoEvent.StopAllTasks();
            if (this.IsActive == true)
            {
                await PageManager.TypeBrowserInCategoryPage.DeactivateAllCards(string.Empty);
                this.IsActive = false;
                LocalStorage.ActiveType = null;
                LocalStorage.ActiveCategory = null;
            }
            else
            {
                await PageManager.TypeBrowserInCategoryPage.DeactivateAllCards(this.id);
                LocalStorage.ActiveType = this.sourceObject;
                LocalStorage.ActiveCategory = LocalStorage.OpenedCategory;
                LocalStorage.ActiveChrono = await ChronoEvent.StartTask();

                this.IsActive = true;
            }
            this.SetTriggerState(300);
            PageManager.MainPage.categoryStack.SetActiveCard();
            PageManager.MainPage.SetActiveCard();
        }


        private void SwipeLeftItem_OpenTypeEditor_Clicked(object? sender, EventArgs e)
        {
            LocalStorage.EditedType = this.sourceObject;
        }






        public async void SetTriggerState(int withDelay = 500)
        {
            try
            {

                await Task.Delay(withDelay);

                if (this.IsActive == true)
                {

                    this.swipeRightItem.BackgroundColor = Colors.Crimson;
                    this.swipeRightItem.Text = "Stop";
                    //this.swiperBody.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex(LocalStorage.OpenedCategory.BgColor);
                    this.gridRow.BackgroundColor = Microsoft.Maui.Graphics.Color.FromHex(LocalStorage.OpenedCategory.BgColor);
                    this.title.TextColor = Microsoft.Maui.Graphics.Color.FromHex(LocalStorage.OpenedCategory.TextColor);
                    this.descr.TextColor = Microsoft.Maui.Graphics.Color.FromHex(LocalStorage.OpenedCategory.TextColor);

                    this.image.Behaviors.Clear();
                    CommunityToolkit.Maui.Behaviors.IconTintColorBehavior tintColor = new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior();
                    tintColor.TintColor = Microsoft.Maui.Graphics.Color.FromHex(LocalStorage.OpenedCategory.TextColor);
                    this.image.Behaviors.Add(tintColor);

                }
                else
                {
                    this.image.Behaviors.Clear();
                    this.gridRow.BackgroundColor = Colors.WhiteSmoke;
                    this.swipeRightItem.BackgroundColor = Colors.LightSeaGreen;
                    this.swipeRightItem.Text = "Run";
                    this.title.TextColor = Colors.Black;
                    this.descr.TextColor = Colors.Black;
                    CommunityToolkit.Maui.Behaviors.IconTintColorBehavior tintColor = new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior();
                    tintColor.TintColor = Colors.Black;
                    this.image.Behaviors.Add(tintColor);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
