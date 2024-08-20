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
    internal class ChronoStory_miniCard : VerticalStackLayout
    {
        private VerticalStackLayout shell = new VerticalStackLayout();

        private Grid topGrid = new Grid();
        private Grid bottomGrid = new Grid();

        public TapGestureRecognizer tapHandler = new TapGestureRecognizer();

        public string id { get; set; }
        ChronoEvent sourceObject { get; set; }
        EventType sourceEventType { get; set; }

        public ChronoStory_miniCard(ChronoEvent item)
        {
            this.id = item.Id;
            this.sourceObject = item;
            var cat = LocalStorage.AllCategories.Where(i => i.Id == item.CategoryId).Single();
            var typ = LocalStorage.AllTypes.Where(i => i.Id == item.TypeId).Single();
            this.sourceEventType = typ;

            var bgColor = Microsoft.Maui.Graphics.Color.FromHex(cat.BgColor);
            var textColor = Microsoft.Maui.Graphics.Color.FromHex(cat.TextColor);

            this.topGrid.AddColumnDefinition(new ColumnDefinition { Width = 44 });
            this.topGrid.AddColumnDefinition(new ColumnDefinition { Width = GridLength.Star });
            this.topGrid.AddColumnDefinition(new ColumnDefinition { Width = GridLength.Auto });
            this.topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });


            DateTime fin = item.EndAt;
            DateTime start = item.StartAt;

            //string startTime = start.Date.ToLocalTime().ToString();
            string startDate = start.ToLocalTime().ToShortDateString();

            string startTime = start.ToLocalTime().ToShortTimeString();

            string endTime = fin.ToLocalTime().ToShortTimeString();


            TimeSpan difference = fin - start;

            var D = difference.Days;
            var H = difference.Hours;
            var M = difference.Minutes;
            var S = difference.Seconds;

            string totalstring = "";
            if (D != 0 && D.ToString() != "")
            {
                totalstring += D.ToString() + " Days and " + H.ToString() + " Hours";
            }
            else
            {
                totalstring += H.ToString("D2") + ":" + M.ToString("D2") + ":" + S.ToString("D2");
            }



            //shell.Padding = new Thickness(6);
            shell.BackgroundColor = bgColor;

            if (item.Result != null && item.Result != string.Empty)
            {
                VerticalStackLayout resultStack = new VerticalStackLayout();
                Label resultLabel = new Label();
                resultLabel.Text = item.Result;
                resultLabel.TextColor = Color.FromHex(cat.TextColor);
                resultLabel.FontSize = 14;
                resultLabel.Padding = new Thickness(4);
                resultStack.Children.Add(resultLabel);
                shell.Children.Add(resultStack);
            }

            if (item.Description != null && item.Description != string.Empty)
            {
                VerticalStackLayout descrStack = new VerticalStackLayout();
                Label descrLabel = new Label();
                descrLabel.Text = item.Description;
                descrLabel.TextColor = Color.FromHex(cat.TextColor);
                descrLabel.FontSize = 12;
                descrLabel.Padding = new Thickness(4);
                descrStack.Children.Add(descrLabel);
                shell.Children.Add(descrStack);
            }


            StackLayout iconContainer = new StackLayout();
            Image image = new Image();
            image.Source = "src/" + typ.Icon;
            image.MaximumHeightRequest = 30;
            CommunityToolkit.Maui.Behaviors.IconTintColorBehavior tintColor = new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior();
            tintColor.TintColor = Color.FromHex(cat.TextColor);
            image.Behaviors.Add(tintColor);

            iconContainer.Children.Add(image);
            iconContainer.Padding = new Thickness(3);
            iconContainer.VerticalOptions = LayoutOptions.Center;
            this.topGrid.Children.Add(iconContainer);
            Grid.SetColumn(iconContainer, 0);

            VerticalStackLayout typeLabelStack = new VerticalStackLayout();
            Label typeLabel = new Label();
            typeLabel.TextColor = textColor;
            typeLabel.Text = typ.Title;
            typeLabel.FontSize = 13;
            typeLabel.Margin = new Thickness(3);
            typeLabelStack.Children.Add(typeLabel);

            HorizontalStackLayout timeStack = new HorizontalStackLayout();
            Label startLabel = new Label();
            startLabel.FontSize = 10;
            startLabel.TextColor = textColor;
            startLabel.Text = startTime.ToString();
            startLabel.Padding = new Thickness(3);
            startLabel.Opacity = 0.6;

            Label endLabel = new Label();
            endLabel.TextColor = textColor;
            endLabel.Text = endTime.ToString();
            endLabel.Padding = new Thickness(3);
            endLabel.Opacity = 0.6;
            endLabel.FontSize = 10;

            timeStack.Children.Add(startLabel);
            timeStack.Children.Add(endLabel);

            typeLabelStack.Children.Add(timeStack);

            this.topGrid.Children.Add(typeLabelStack);
            Grid.SetColumn(typeLabelStack, 1);

            VerticalStackLayout durationStack = new VerticalStackLayout();
            Label duration = new Label();
            duration.TextColor = textColor;
            duration.Text = totalstring + " ";
            duration.FontSize = 16;
            duration.Margin = new Thickness(3);
            durationStack.Children.Add(duration);

            Label date = new Label();
            date.TextColor = textColor;
            date.Text = startDate;
            date.FontSize = 12;
            //date.Margin = new Thickness(6);
            date.Opacity = 0.5;
            durationStack.Children.Add(date);


            this.topGrid.Children.Add(durationStack);
            Grid.SetColumn(durationStack, 2);

            this.shell.Children.Add(this.topGrid);

            this.tapHandler.NumberOfTapsRequired = 2;
            this.tapHandler.Tapped += OpenEventEditor;
            this.shell.GestureRecognizers.Add(this.tapHandler);
            this.Children.Add(this.shell);
        }

        private async void OpenEventEditor(object? sender, TappedEventArgs e)
        {
            if (this.id != null)
            {
                LocalStorage.EditedChrono = this.sourceObject;
                ModalManager.eventEditorModal.SetData(this.sourceEventType, this.sourceObject);
                await Navigation.PushAsync(ModalManager.eventEditorModal);
            }
        }

        public VerticalStackLayout Get()
        {
            return this.shell;
        }
    }
}
