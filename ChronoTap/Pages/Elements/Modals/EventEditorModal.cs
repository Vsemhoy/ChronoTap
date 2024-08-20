using ChronoTap.Core;
using ChronoTap.Pages.Elements.Text;
using ChronoTap.Storage;
using ChronoTap.Storage.Database.Models;
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



        private Entry resultInput = new Entry();
        private Editor descriptionInput = new Editor();

        public string? resultText = string.Empty;
        public string? descriptionText = string.Empty;

        public ChronoEvent sourceData { get; set; }

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



            this.resultInput.BackgroundColor = Colors.White;
            this.descriptionInput.BackgroundColor = Colors.White;
            this.descriptionInput.AutoSize = EditorAutoSizeOption.TextChanges;
            var title = new LegendLabel("Result:");
            this.mainStack.Children.Add(title);
            this.mainStack.Children.Add(this.resultInput);
            this.resultInput.MaxLength = 220;
            var description = new LegendLabel("Description:");
            this.mainStack.Children.Add(description);
            this.mainStack.Children.Add(this.descriptionInput);
            this.descriptionInput.MaxLength = 2200;


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
            this.button_save.Clicked += Button_saveEvent_Clicked;
        }


        private async void Button_saveEvent_Clicked(object? sender, EventArgs e)
        {
            if (ModalManager.eventEditorModal.CheckValid())
            {
                LocalStorage.EditedChrono = this.sourceData;
                await ModalManager.EventEditorModal_ActionSave();
                
            } else
            {
                ModalManager.eventEditorModal.Alert("Error", "Check input text");
            }
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
                //this.Title = "Create new category";
            }
            else
            {
                buttonGrid_2.Children.Add(this.button_save);
                Grid.SetColumn(this.button_save, 1);
                //this.Title = "Edit category";

            }
        }


        public void SetData(EventType evType, ChronoEvent sourceEvent)
        {
            if (sourceEvent.Id == null || sourceEvent.Id == string.Empty)
            {
                this.Title = "New " + evType.Title;
                this.resultText = string.Empty;
                this.descriptionText = string.Empty;
                this.id = null;
                
            } else
            {
                this.id = sourceEvent.Id;
                this.Title = evType.Title;
                this.resultText = sourceEvent.Result;
                this.descriptionText = sourceEvent.Description;

            }
            this.resultInput.Text = this.resultText == null ? string.Empty : this.resultText;
            this.descriptionInput.Text = this.descriptionText == null ? string.Empty : this.descriptionText;
            this.sourceData = sourceEvent;
            this.SetButtonBlock();
        }




        internal async void Harvest()
        {
            this.resultText = this.resultInput.Text.Trim();
            this.descriptionText = this.descriptionInput.Text.Trim();

            //this.val_getLocationStart = this.chb_getLocationStart.IsChecked;
            //this.val_getLocationEnd = this.chb_getLocationEnd.IsChecked;
            //this.val_getLocationTrack = this.chb_getLocationTrack.IsChecked;
            //this.val_beepOnDurationLimit = this.chb_beepOnDurationLimit.IsChecked;
            ////this.val_isArchieved = 
            //this.val_runNextTrackOnStop = this.chb_runNextTrackOnStop.IsChecked;
            //this.val_runNextTrackOnLimit = this.chb_runNextTrackOnLimit.IsChecked;
            //this.val_duration_limit = int.Parse(this.durationPicker.Text.ToString());
            //this.val_stopOnDurationLimit = this.chb_stopOnDurationLimit.IsChecked;

            //try
            //{
            //    for (int i = 0; i < LocalStorage.Categories.Count; i++)
            //    {
            //        string checkString = this.picker_category.Items[this.picker_category.SelectedIndex].Substring(this.Prefix_currentCategory.Length);
            //        if (this.picker_category.Items.Count > 0 &&
            //            LocalStorage.Categories[i].Title.Trim() == checkString.Trim()
            //            )
            //        {
            //            this.val_select_category = LocalStorage.Categories[i].Id;
            //            break;
            //        }
            //    }
            //    this.val_select_sensitivity = this.picker_sensitivity.SelectedIndex;
            //    this.val_next_track = null;
            //    for (int i = 0; i < LocalStorage.Types.Count; i++)
            //    {
            //        if (this.picker_nextTrack.Items.Count > 0 && LocalStorage.Types[i].Title == this.picker_nextTrack.Items[this.picker_nextTrack.SelectedIndex])
            //        {
            //            this.val_next_track = LocalStorage.Types[i].Id;
            //            break;
            //        }
            //    }

            //}
            //catch (Exception exx)
            //{
            //    var ex = exx.Message;
            //}
        }


        internal bool CheckValid()
        {
            this.Harvest();
            //if (this.titleText.Length < 2)
            //{
            //    this.Alert("Ooops!", "The name of your card is too short!", "OK");
            //    return false;
            //}
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
    }
}
