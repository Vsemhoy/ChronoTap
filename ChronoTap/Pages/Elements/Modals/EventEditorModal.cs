using ChronoTap.Pages.Elements.Text;
using ChronoTap.Style;
using ChronoTap.Style;
using Microsoft.Maui.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Elements.Modals
{
    internal class EventEditorModal : ContentPage
    {

        public string id { get; set; }

        private Grid mainGrid = new Grid();
        private ScrollView scrollView = new ScrollView();
        private VerticalStackLayout mainStack = new VerticalStackLayout();

        private Grid buttonGrid_1 = new Grid();
        private Grid buttonGrid_2 = new Grid();

        private VerticalStackLayout buttonStack = new VerticalStackLayout();


        public Button button_close = new Button();
        public Button button_create = new Button();
        public Button button_save = new Button();
        public Button button_delete = new Button();



        private Entry titleInput = new Entry();
        private Editor descriptionInput = new Editor();


        public EventEditorModal()
        {
            this.mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            this.mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
            this.mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Auto) });

            this.buttonGrid_1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.buttonGrid_1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });

            this.buttonGrid_2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.buttonGrid_2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });

            this.button_delete.BackgroundColor = Colors.OrangeRed;
            this.button_delete.TextColor = Colors.White;

            this.mainStack.Padding = BaseTheme.THEME.formFramePadding;



            this.titleInput.BackgroundColor = Colors.White;
            this.descriptionInput.BackgroundColor = Colors.White;
            this.descriptionInput.AutoSize = EditorAutoSizeOption.TextChanges;
            var title = new LegendLabel("Name:");
            this.mainStack.Children.Add(title);
            this.mainStack.Children.Add(this.titleInput);
            var description = new LegendLabel("Description:");
            this.mainStack.Children.Add(description);
            this.mainStack.Children.Add(this.descriptionInput);


            this.button_close.Text = "Close";
            this.button_create.Text = "Create";
            this.button_save.Text = "Save";
            this.button_delete.Text = "Delete";
            this.button_close.Margin = new Thickness(6);
            this.button_create.Margin = new Thickness(6);
            this.button_save.Margin = new Thickness(6);
            this.button_delete.Margin = new Thickness(6);



            //this.buttonStack.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.buttonStack.Children.Add(this.buttonGrid_1);
            this.buttonStack.Children.Add(this.buttonGrid_2);
            this.scrollView.Content = this.mainStack;
            this.mainGrid.Children.Add(this.scrollView);
            Grid.SetRow(this.mainStack, 0);
            this.mainGrid.Children.Add(this.buttonStack);
            Grid.SetRow(this.buttonStack, 1);
            this.Content = this.mainGrid;

            this.SetButtonBlock();
        }


        public void SetButtonBlock()
        {
            this.buttonGrid_1.Children.Clear();
            this.buttonGrid_2.Children.Clear();

            this.buttonGrid_2.Children.Add(this.button_close);
            Grid.SetColumn(this.button_close, 0);

            if (this.id == "" || this.id == null)
            {
                buttonGrid_2.Children.Add(this.button_create);
                Grid.SetColumn(this.button_create, 1);
                this.Title = "Create new category";
            }
            else
            {
                buttonGrid_2.Children.Add(this.button_save);
                Grid.SetColumn(this.button_save, 1);
                this.Title = "Edit category";

            }
        }
    }
}
