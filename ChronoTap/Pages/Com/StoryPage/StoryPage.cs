using ChronoTap.Storage.Database.Models;
using ChronoTap.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoTap.Pages.Elements.Cards;
using Xamarin.KotlinX.Coroutines.Future;

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
        //Button button_middle = new Button();
        Button button_prev = new Button();

        DatePicker startPicker = new DatePicker();
        DatePicker endPicker = new DatePicker();


        DateTime startDate = DateTime.UtcNow; // Past 
        DateTime endDate = DateTime.UtcNow;  // Future

        private bool blockLoad = false;

        public StoryPage()
        {
            this.BackgroundColor = Color.FromHex("#F0F0F3");
            //this.startDate = DateTime.UtcNow; // Текущая дата и время
            //DateTime firstMinuteTomorrow = startDate.Date.AddDays(1); // 00:00:00 завтрашнего дня
            //this.endDate = DateTime.UtcNow; // Текущая дата и время
            //DateTime lastMinuteYesterday = endDate.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59); // 23:59:59 вчерашнего дня


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
            //this.button_middle.Text = "middle button";
            Label labelTo = new Label();
            labelTo.Text = " < ";
            labelTo.FontSize = 14;
            labelTo.TextColor = Colors.DarkGray;
            labelTo.VerticalTextAlignment = TextAlignment.Center;
            labelTo.Padding = 6;

            Frame midFrame = new Frame();
            midFrame.CornerRadius = 6;
            midFrame.BackgroundColor = Colors.White;
            midFrame.Padding = 3;
            Grid middleDateStack = new Grid();
            middleDateStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            middleDateStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Auto) });
            middleDateStack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            middleDateStack.Children.Add(this.endPicker);
            Grid.SetColumn(this.endPicker, 0);
            middleDateStack.Children.Add(labelTo);
            Grid.SetColumn(labelTo, 1);
            middleDateStack.Children.Add(this.startPicker);
            Grid.SetColumn(this.startPicker, 2);
            midFrame.Content = middleDateStack;



            this.button_next.Text = "Next";
            bottomMiniGrid.Children.Add(this.button_prev);
            Grid.SetColumn(this.button_prev, 0);
            bottomMiniGrid.Children.Add(midFrame);
            Grid.SetColumn(midFrame, 1);
            bottomMiniGrid.Children.Add(this.button_next);
            Grid.SetColumn(this.button_next, 3);
            this.bottomStack.Children.Add(bottomMiniGrid);

            this.button_prev.Clicked += Button_prev_Clicked;
            this.button_next.Clicked += Button_next_Clicked;

            this.startPicker.DateSelected += StartPicker_DateSelected;
            this.endPicker.DateSelected += EndPicker_DateSelected;

            this.Title = this.startDate.ToShortDateString();\
            
            
        }

        private async void EndPicker_DateSelected(object? sender, DateChangedEventArgs e)
        {
            DateTime _selectedDate = e.NewDate;

            if (DateTime.Compare(_selectedDate, this.startDate) > 0)
            {
                // future don't bigger than past date

                this.startDate = _selectedDate;
                this.startPicker.Date = _selectedDate.ToLocalTime();
            } else
            {
                // future don't bigger than past date
            }
            this.endDate = _selectedDate;
            await  this.LoadDefaults();
        }

        private async void StartPicker_DateSelected(object? sender, DateChangedEventArgs e)
        {
            DateTime _selectedDate = e.NewDate;
            if (DateTime.Compare(this.endDate, _selectedDate) > 0)
            {
                // future don't bigger than past date

                this.endDate = _selectedDate;
                this.endPicker.Date = _selectedDate.ToLocalTime();
            }
            else
            {
                // future don't bigger than past date
            }

            this.startDate = _selectedDate;
            await  this.LoadDefaults();
        }

        private async void Button_next_Clicked(object? sender, EventArgs e)
        {
            this.startDate = this.startDate.Date.AddDays(1);
            this.endDate   = this.endDate.Date.AddDays(1);
            this.startPicker.Date = this.startDate;
            this.endPicker.Date = this.endDate;
            await this.LoadDefaults();
        }

        private async void Button_prev_Clicked(object? sender, EventArgs e)
        {
            this.startDate = this.startDate.Date.AddDays(-1);
            this.endDate   =   this.endDate.Date.AddDays(-1);
            this.startPicker.Date = this.startDate;
            this.endPicker.Date = this.endDate;
            await this.LoadDefaults();
        }

        public async void Boot()
        {
            LocalStorage.AllCategories = await EventCategory.GetAllItemsAsync();
            LocalStorage.AllTypes = await EventType.GetAllItemsAsync();
        }



        public DateTime GetTomorrow(DateTime date)
        {
            DateTime firstMinuteTomorrow = date.Date.AddDays(1).AddSeconds(-1);
            return firstMinuteTomorrow;
        }

        public DateTime GetYesterday(DateTime date)
        {
            DateTime lastMinuteYesterday = date.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59); // 23:59:59 вчерашнего дня
            return lastMinuteYesterday;
        }


        public async Task LoadDefaults()
        {
            this.Boot();
            this.Title = this.startDate.AddDays(1).ToLocalTime().ToShortDateString();
            string lastDate = "";
            this.itemStack.Children.Clear();
            LocalStorage.ChronoEventCollection = await ChronoEvent.GetActiveItemsAsync(this.GetTomorrow(this.endDate), this.GetYesterday(this.startDate) );
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

            DatePicker dpicker = new DatePicker();


            Button button1 = new Button();
            button1.Text = "AHFLKSDHFLJL F";
            button1.Margin = new Thickness(3);
            Button button2 = new Button();
            button2.Text = "Day";
            button2.Margin = new Thickness(3);
            Button button3 = new Button();
            button3.Text = "Week";
            button3.Margin = new Thickness(3);
            Button button4 = new Button();
            button4.Text = "Month F";
            button4.Margin = new Thickness(3);

            horStack.Children.Add(dpicker);
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

