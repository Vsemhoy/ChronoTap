using ChronoTap.Core;
using ChronoTap.Pages.Elements.Cards;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Storage;

using Microcharts;
using SkiaSharp;
using Microcharts.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace ChronoTap.Pages.Com.ChartPage
{
    internal class ChartPage : ContentPage
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

        public List<EventCategory> categoryList = new List<EventCategory>();
        public List<EventType> typeList = new List<EventType>();
        public List<Clocker> clockList = new List<Clocker>();


        public int totalDuration = 0;
        public List<Clocker> categoryDuration = new List<Clocker>();
        public List<Clocker> typeDuration = new List<Clocker>();


        public ChartPage()
        {
            this.BackgroundColor = Color.FromHex("#F0F0F3");
            //this.startDate = DateTime.UtcNow; // Текущая дата и время
            //DateTime firstMinuteTomorrow = startDate.Date.AddDays(1); // 00:00:00 завтрашнего дня
            //this.endDate = DateTime.UtcNow; // Текущая дата и время
            //DateTime lastMinuteYesterday = endDate.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59); // 23:59:59 вчерашнего дня
            this.startDate = this.startDate.ToLocalTime();
            this.endDate = this.endDate.ToLocalTime();

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

            //this.SetFilters();

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
            this.startPicker.Date = this.startDate.ToLocalTime();
            middleDateStack.Children.Add(this.startPicker);
            Grid.SetColumn(this.startPicker, 0);
            middleDateStack.Children.Add(labelTo);
            Grid.SetColumn(labelTo, 1);
            this.endPicker.Date = this.endDate.ToLocalTime();
            middleDateStack.Children.Add(this.endPicker);
            Grid.SetColumn(this.endPicker, 2);
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

            this.Title = this.startDate.DayOfWeek.ToString() + "'" + this.startDate.Day.ToString();

            this.unlock_This();
        }

        private async void EndPicker_DateSelected(object? sender, DateChangedEventArgs e)
        {

            DateTime _selectedDate = e.NewDate;

            if (DateTime.Compare(_selectedDate, this.startDate) > 0)
            {
                // future don't bigger than past date

            }
            else
            {
                // future don't bigger than past date
                this.endDate = _selectedDate;
                this.startDate = _selectedDate;
                this.startPicker.Date = _selectedDate.ToLocalTime();
            }
            this.endDate = _selectedDate;
            if (!this.blockLoad)
            {
                await this.LoadDefaults();

            }
            this.unlock_This();
        }

        private async void StartPicker_DateSelected(object? sender, DateChangedEventArgs e)
        {

            DateTime _selectedDate = e.NewDate;
            if (DateTime.Compare(this.endDate, _selectedDate) > 0)
            {
                // future don't bigger than past date

            }
            else
            {
                this.startDate = _selectedDate;
                this.endDate = _selectedDate;
                this.endPicker.Date = _selectedDate.ToLocalTime();
                // future don't bigger than past date
            }
            this.startDate = _selectedDate;

            if (!this.blockLoad)
            {

                await this.LoadDefaults();
            }
            this.unlock_This();
        }

        private async void Button_next_Clicked(object? sender, EventArgs e)
        {
            this.startDate = this.startDate.Date.AddDays(1);
            this.endDate = this.endDate.Date.AddDays(1);
            this.startPicker.Date = this.startDate.ToLocalTime();
            this.endPicker.Date = this.endDate.ToLocalTime();
            if (!this.blockLoad)
            {
                await this.LoadDefaults();
            }
            this.unlock_This();
        }

        private async void Button_prev_Clicked(object? sender, EventArgs e)
        {
            this.startDate = this.startDate.Date.AddDays(-1);
            this.endDate = this.endDate.Date.AddDays(-1);
            this.startPicker.Date = this.startDate.ToLocalTime();
            this.endPicker.Date = this.endDate.ToLocalTime();
            if (!this.blockLoad)
            {
                await this.LoadDefaults();
            }
            this.unlock_This();
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
            this.lock_This();
            this.Boot();

            string lastDate = "";
            this.itemStack.Children.Clear();
            this.categoryList.Clear();
            this.typeList.Clear();
            this.clockList.Clear();

            var tomorrow = this.GetTomorrow(this.endDate);
            var yesterday = this.GetYesterday(this.startDate);
            LocalStorage.ChronoEventCollection = await ChronoEvent.GetActiveItemsAsync(tomorrow, yesterday);
            await Task.Delay(100);
            var items = LocalStorage.ChronoEventCollection;

            if (items.Count > 0)
            {
                StackLayout divider = new StackLayout();
                divider.BackgroundColor = Color.FromArgb("#44444444");
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
                    dateLabel.TextColor = Colors.Black;
                    dateLabel.Text = start.ToLocalTime().DayOfWeek.ToString() + "  " + start.ToLocalTime().Day.ToString() + " " + GlobalManager.Localizer.monthName(start.ToLocalTime().Month) + " " + start.ToLocalTime().Year.ToString();
                    dateLabel.FontSize = 14;
                    dateLabel.Padding = new Thickness(6);
                    dateLabel.HorizontalTextAlignment = TextAlignment.End;
                    dateDividerStack.Children.Add(dateLabel);
                    this.itemStack.Children.Add(dateDividerStack);

                    StackLayout divider3 = new StackLayout();
                    divider3.BackgroundColor = Color.FromArgb("#77777777");
                    divider3.HeightRequest = 1;
                    this.itemStack.Add(divider3);
                }
                lastDate = startDate;
                //DateTime fin = item.EndAt;

                //string startTime = start.Date.ToLocalTime().ToString();


                //string startTime = start.ToLocalTime().ToShortTimeString();

                //string endTime = fin.ToLocalTime().ToShortTimeString();
                Clocker clocker = new Clocker(item.Duration, item.CategoryId, item.TypeId);
                clocker.start = item.StartAt;
                clocker.end = item.EndAt;
                this.clockList.Add(clocker);

               
                //StackLayout divider = new StackLayout();
                //divider.BackgroundColor = Color.FromArgb("#77777777");
                //divider.HeightRequest = 1;
                //this.itemStack.Add(divider);
            }

            this.unlock_This();


            this.CountStatistics();
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





        public async void CountStatistics()
        {
            this.totalDuration = 0;
            this.categoryDuration = new List<Clocker>();
            this.typeDuration = new List<Clocker>();
            for (int i = 0; i < this.categoryList.Count; i++)
            {
                var catClock = new Clocker(0, categoryList[i].Id, string.Empty);
                this.categoryDuration.Add(catClock);
            }
            for (int i = 0; i < this.typeList.Count; i++)
            {
                var catClock = new Clocker(0, typeList[i].CategoryId, typeList[i].Id);
                this.typeDuration.Add(catClock);
            }

            for (int i = 0; i < this.clockList.Count; i++)
            {
                var item = this.clockList[i];
                this.totalDuration += item.duration;
                for (int j = 0; j < this.categoryDuration.Count; j++)
                {
                    if (this.categoryDuration[j].category == item.category)
                    {
                        this.categoryDuration[j].duration += item.duration;
                        break;
                    }
                }


                for (int j = 0; j < this.typeDuration.Count; j++)
                {
                    if (this.typeDuration[j].type == item.type)
                    {
                        this.typeDuration[j].duration += item.duration;
                        break;
                    }
                }
            }

            var a = this.totalDuration;
            this.DrawChartPie();
        }



        public async void DrawChartPie()
        {
            ChartEntry[] entries = new[]
            {
                new ChartEntry(212)
                {
                    Label = "Windows",
                    ValueLabel = "112",
                    Color = SKColor.Parse("#2c3e50")
                },
                new ChartEntry(248)
                {
                    Label = "Android",
                    ValueLabel = "648",
                    Color = SKColor.Parse("#77d065")
                },
                new ChartEntry(128)
                {
                    Label = "IOS",
                    ValueLabel = "482",
                    Color = SKColor.Parse("#b455b6")
                },
                new ChartEntry(514)
                {
                    Label = ".NET MAUI",
                    ValueLabel = "214",
                    Color = SKColor.Parse("#3498db")
                },
            };


            var chart = new DonutChart
            {
                Entries = entries
            };

            ChartView chavy = new ChartView();
            chavy.Chart = chart;
            LinearItemsLayout laty = new LinearItemsLayout(ItemsLayoutOrientation.Vertical);
            laty.BindingContext = chavy;
            Frame framy = new Frame();
            framy.Content = laty;
            this.itemStack.Children.Add(framy);


        }



        private void lock_This()
        {
            this.blockLoad = true;
        }

        private async void unlock_This()
        {
            await Task.Delay(100);
            this.blockLoad = false;
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

    internal class Clocker
    {

        public int duration { get; set; }
        public string category { get; set; }
        public string type { get; set; }

        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public Clocker(int duration, string category, string type)
        {
            this.duration = duration;
            this.category = category;
            this.type = type;
        }
    }


}

