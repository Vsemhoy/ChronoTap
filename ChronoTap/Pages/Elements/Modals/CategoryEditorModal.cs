using ChronoTap.Core;
using ChronoTap.Pages.Elements.Text;
using ChronoTap.Storage;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Style;
using Microsoft.Maui.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Elements.Modals
{
    internal class CategoryEditorModal : ContentPage
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

        public Button button_colorPicker = new Button();


        public ColorPickerModal colorPickerModal;
        public Button button_iconPicker = new Button();

        public Color bgColor = Colors.CadetBlue;
        public Color txColor = Colors.White;
        public string titleText = string.Empty;
        public string descriptionText = string.Empty;

        public ToolbarItem tbButton_removeCategory = new ToolbarItem();

        public CategoryEditorModal()
        {
            this.mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            this.mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
            this.mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Auto) });

            this.buttonGrid_1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.buttonGrid_1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_1.Padding = BaseTheme.THEME.formFramePadding / 2;

            this.buttonGrid_2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.buttonGrid_2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            this.buttonGrid_2.Padding = BaseTheme.THEME.formFramePadding / 2;

            this.button_delete.BackgroundColor = Colors.OrangeRed;
            this.button_delete.TextColor = Colors.White;

            this.mainStack.Padding = BaseTheme.THEME.formFramePadding;



            this.titleInput.BackgroundColor = Colors.White;
            this.titleInput.MaxLength = 64;
            this.descriptionInput.MaxLength = 256;
            this.descriptionInput.BackgroundColor = Colors.White;
            this.descriptionInput.AutoSize = EditorAutoSizeOption.TextChanges;
            var title = new LegendLabel("Name:");
            this.mainStack.Children.Add(title);
            this.mainStack.Children.Add(this.titleInput);
            var description = new LegendLabel("Description:");
            this.mainStack.Children.Add(description);
            this.mainStack.Children.Add(this.descriptionInput);


            this.descriptionInput.TextChanged += DescriptionInput_TextChanged;
            this.titleInput.TextChanged += TitleInput_TextChanged;


            this.button_colorPicker.Text = "Chose colors...";
            this.button_colorPicker.Padding = BaseTheme.THEME.formFramePadding;
            this.button_colorPicker.Margin = BaseTheme.THEME.formFramePadding * 2;
            this.button_colorPicker.Clicked += Button_colorPicker_Clicked;
            this.mainStack.Children.Add(this.button_colorPicker);
            this.button_colorPicker.TextColor = this.txColor;
            this.button_colorPicker.BackgroundColor = this.bgColor;
            this.button_colorPicker.Shadow = BaseTheme.THEME.CardShadow;

            this.button_iconPicker.Text = "Choose the icon";
            this.button_iconPicker.Clicked += Button_iconPicker_Clicked;
            this.mainStack.Children.Add(this.button_iconPicker);

            this.button_close.Text = "Close";
            this.button_create.Text = "Create";
            this.button_save.Text = "Save";
            this.button_delete.Text = "Delete";
            this.button_close.Margin = new Thickness(6);
            this.button_create.Margin = new Thickness(6);
            this.button_save.Margin = new Thickness(6);
            this.button_delete.Margin = new Thickness(6);

            this.button_close.Clicked += Button_close_Clicked;
            this.button_delete.Clicked += Button_close_Clicked;



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

        private async void Button_iconPicker_Clicked(object? sender, EventArgs e)
        {
            if (LocalStorage.EditedCategory != null)
            {
                LocalStorage.SelectedIcon = LocalStorage.EditedCategory.Icon;
            } else
            {
                LocalStorage.SelectedIcon = string.Empty;
            }
            IconPickerModal impm = new IconPickerModal();
            await Navigation.PushAsync(impm);
        }

        private void TitleInput_TextChanged(object? sender, TextChangedEventArgs e)
        {
            this.titleText = this.titleInput.Text.Trim();
        }

        private void DescriptionInput_TextChanged(object? sender, TextChangedEventArgs e)
        {
            this.descriptionText = this.descriptionInput.Text.Trim();
        }

        private async void Button_close_Clicked(object? sender, EventArgs e)
        {
            await Task.Delay(100);
            await Navigation.PopAsync();
        }


        /// <summary>
        /// Open modal window with color picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void Button_colorPicker_Clicked(object? sender, EventArgs e)
        {
            this.colorPickerModal = new ColorPickerModal(this.bgColor.ToHex(), this.txColor.ToHex());
            this.colorPickerModal.setButton.Clicked += SetColors_in_colorPicker;
            await Navigation.PushModalAsync(this.colorPickerModal);
        }

        private async void SetColors_in_colorPicker(object? sender, EventArgs e)
        {
            this.SetPickerColor(this.colorPickerModal.BgColor, this.colorPickerModal.TextColor);
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

                this.ToolbarItems.Clear();
            }
            else
            {
                buttonGrid_2.Children.Add(this.button_save);
                Grid.SetColumn(this.button_save, 1);
                this.Title = "Edit category";

                //tbButton_removeCategory.IconImageSource = "trash.svg";
                tbButton_removeCategory.Text = "Remove Category";
                tbButton_removeCategory.Order = ToolbarItemOrder.Secondary;
                this.ToolbarItems.Add(tbButton_removeCategory);
            }
        }


        public void SetPickerColor(Color bgColor, Color txColor)
        {
            this.button_colorPicker.TextColor = txColor;
            this.button_colorPicker.BackgroundColor = bgColor;
            this.bgColor = bgColor;
            this.txColor = txColor;
        }


        public bool CheckValid()
        {
            if (this.titleText.Length < 2)
            {
                this.Alert("Ooops!", "The name of your card is too short!", "OK");
                return false;
            }
            return true;
        }

        public void Alert(string title, string message, string button = "OK")
        {
            DisplayAlert(title, message, button);
        }


        public async void Hide()
        {
            await Navigation.PopAsync();
        }

        internal async Task setData(EventCategory openedCategory)
        {
            this.id = openedCategory.Id;
            LocalStorage.SelectedIcon = openedCategory.Icon;
            this.titleInput.Text = openedCategory.Title != null ? openedCategory.Title : "";
            this.descriptionInput.Text = openedCategory.Description != null ? openedCategory.Description : "";

            this.titleText = openedCategory.Title != null ? openedCategory.Title : "";
            this.descriptionText = openedCategory.Description != null ? openedCategory.Description : "";

            this.bgColor = Color.FromHex(openedCategory.BgColor);
            this.txColor = Color.FromHex(openedCategory.TextColor);

            this.button_colorPicker.TextColor = this.txColor;
            this.button_colorPicker.BackgroundColor = this.bgColor;

            this.SetButtonBlock();
        }
    }
}
