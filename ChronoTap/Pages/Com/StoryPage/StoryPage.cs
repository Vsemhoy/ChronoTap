using ChronoTap.Storage.Database.Models;
using ChronoTap.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoTap.Pages.Elements.Cards;

namespace ChronoTap.Pages.Com.StoryPage
{
    internal class StoryPage : ContentPage
    {
        Grid body = new Grid();

        ScrollView filterStack = new ScrollView();
        ScrollView scrollView = new ScrollView();
        VerticalStackLayout itemStack = new VerticalStackLayout();
        VerticalStackLayout bottomStack = new VerticalStackLayout();


        Button button_next = new Button();
        Button button_middle = new Button();
        Button button_prev = new Button();


        public StoryPage()
        {

            this.filterStack.Orientation = ScrollOrientation.Horizontal;

            this.body.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Auto) });
            this.body.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
            this.body.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Auto) });
            //this.body.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Auto) });

            this.scrollView.Content = this.itemStack;

            this.body.Children.Add(this.filterStack);
            Grid.SetRow(this.filterStack, 0);
            this.body.Children.Add(this.scrollView);
            Grid.SetRow(this.scrollView, 1);
            this.body.Children.Add(this.bottomStack);
            Grid.SetRow(this.bottomStack, 2);


            this.Content = this.body;

            this.SetFilters();

            Grid bottomMiniGrid = new Grid();
            bottomMiniGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Auto) });
            bottomMiniGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            bottomMiniGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Auto) });
            bottomMiniGrid.ColumnSpacing = 6;
            bottomMiniGrid.Margin = 3;
            this.button_prev.Text = "Prev";
            this.button_middle.Text = "middle button";
            this.button_next.Text = "Next";
            bottomMiniGrid.Children.Add(this.button_prev);
            Grid.SetColumn(this.button_prev, 0);
            bottomMiniGrid.Children.Add(this.button_middle);
            Grid.SetColumn(this.button_middle, 1);
            bottomMiniGrid.Children.Add(this.button_next);
            Grid.SetColumn(this.button_next, 2);
            this.bottomStack.Children.Add(bottomMiniGrid);
        }


        public async void Boot()
        {
            LocalStorage.AllCategories = await EventCategory.GetAllItemsAsync();
            LocalStorage.AllTypes = await EventType.GetAllItemsAsync();
        }



        public async Task LoadDefaults()
        {
            this.Boot();
            string lastDate = "";
            this.itemStack.Children.Clear();
            LocalStorage.ChronoEventCollection = await ChronoEvent.GetAllActiveItemsAsync();
            var items = LocalStorage.ChronoEventCollection;

            if (items.Count > 0)
            {
                StackLayout divider = new StackLayout();
                divider.BackgroundColor = Color.FromArgb("#77777777");
                divider.HeightRequest = 1;
                this.itemStack.Add(divider);
            }

            for (int i = 0; i < items.Count; i++)
            {

                var item = items[i];
                if (item.IsRunning == true)
                {
                    continue;
                }

                DateTime start = item.StartAt;
                string startDate = start.ToLocalTime().ToShortDateString();
                if (startDate != lastDate)
                {
                    HorizontalStackLayout dateDividerStack = new HorizontalStackLayout();
                    Label dateLabel = new Label();
                    dateLabel.TextColor = Colors.DarkGray;
                    dateLabel.Text = startDate;
                    dateLabel.FontSize = 14;
                    dateLabel.Padding = new Thickness(6);
                    dateLabel.HorizontalTextAlignment = TextAlignment.End;
                    dateDividerStack.Children.Add(dateLabel);
                    this.itemStack.Children.Add(dateDividerStack);
                }
                lastDate = startDate;
                //DateTime fin = item.EndAt;

                //string startTime = start.Date.ToLocalTime().ToString();


                //string startTime = start.ToLocalTime().ToShortTimeString();

                //string endTime = fin.ToLocalTime().ToShortTimeString();

                var cronoEventCard = new ChronoStory_miniCard(item);
                this.itemStack.Children.Add(cronoEventCard);


                StackLayout divider = new StackLayout();
                divider.BackgroundColor = Color.FromArgb("#77777777");
                divider.HeightRequest = 1;
                this.itemStack.Add(divider);
            }


        }



        private void SetFilters()
        {
            var horStack = new HorizontalStackLayout();
            

            Button button1 = new Button();
            button1.Text = "AHFLKSDHFLJL F";
            button1.Margin = new Thickness(3);
            Button button2 = new Button();
            button2.Text = "AHFLKSDHFLJL F";
            button2.Margin = new Thickness(3);
            Button button3 = new Button();
            button3.Text = "AHFLKSDHFLJL F";
            button3.Margin = new Thickness(3);
            Button button4 = new Button();
            button4.Text = "AHFLKSDHFLJL F";
            button4.Margin = new Thickness(3);

            horStack.Children.Add(button1);
            horStack.Children.Add(button2);
            horStack.Children.Add(button3);
            horStack.Children.Add(button4);

            //this.filterStack.Children.Clear();
            this.filterStack.Content = horStack;



        }



    protected override void OnAppearing()
    {

        base.OnAppearing();
        // Handle any logic when the page appears
        this.LoadDefaults();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Handle any cleanup when the page disappears
        //DisposeResources();
        Navigation.PopAsync();
    }

    private void DisposeResources()
    {
        // Dispose or cleanup any resources

    }
}
}

