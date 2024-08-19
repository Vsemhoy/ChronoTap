using ChronoTap.Pages.Elements.Text;
using ChronoTap.Storage;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Style;
using ChronoTap.Pages.Elements.Modals;
using ChronoTap.Storage.Database.Models;
using ChronoTap.Storage;
using ChronoTap.Style;
using Microsoft.Maui.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Elements.Modals
{
    internal class TypeEditorModal : ContentPage
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

        public Button button_iconPicker = new Button();

        private Entry titleInput = new Entry();
        private Editor descriptionInput = new Editor();

        private Picker picker_category = new Picker();
        private Picker picker_nextTrack = new Picker();
        private Picker picker_ringtone = new Picker();
        private Picker picker_sensitivity = new Picker();

        private CheckBox chb_getLocationStart = new CheckBox();
        private CheckBox chb_getLocationEnd = new CheckBox();
        private CheckBox chb_getLocationTrack = new CheckBox();
        private CheckBox chb_beepOnDurationLimit = new CheckBox();
        private CheckBox chb_isArchieved = new CheckBox();
        private CheckBox chb_runNextTrackOnStop = new CheckBox();
        private CheckBox chb_runNextTrackOnLimit = new CheckBox();
        private CheckBox chb_stopOnDurationLimit = new CheckBox();

        private Entry durationPicker = new Entry();

        public string Prefix_currentCategory = "■ ";
        public string Prefix_otherCategory = "□ ";

        public string titleText = string.Empty;
        public string descriptionText = string.Empty;

        public bool val_getLocationStart = false;
        public bool val_getLocationEnd = false;
        public bool val_getLocationTrack = false;
        public bool val_beepOnDurationLimit = false;
        public bool val_isArchieved = false;
        public bool val_runNextTrackOnStop = false;
        public bool val_runNextTrackOnLimit = false;
        public bool val_stopOnDurationLimit = false;


        public string val_select_category = string.Empty;
        public int val_select_sensitivity = 0;
        public int val_duration_limit = 0;
        public string? val_next_track = null;

        public TypeEditorModal()
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


            var category = new LegendLabel("Category:");
            this.mainStack.Children.Add(category);
            this.mainStack.Children.Add(this.picker_category);

            this.button_iconPicker.Text = "Choose the icon";
            this.button_iconPicker.Clicked += Button_iconPicker_Clicked;
            this.mainStack.Children.Add(this.button_iconPicker);

            var durlimit = new LegendLabel("Duration limit:");
            this.mainStack.Children.Add(durlimit);
            this.mainStack.Children.Add(this.durationPicker);
            this.durationPicker.Keyboard = Keyboard.Numeric;
            this.durationPicker.Text = "0";
            this.durationPicker.MaxLength = 5;

            HorizontalStackLayout stack1 = new HorizontalStackLayout();
            stack1.HorizontalOptions = LayoutOptions.End;
            var beeplimint = new LegendLabel("Beep on duration limit:");
            beeplimint.HorizontalOptions = LayoutOptions.Start;
            //this.mainStack.Children.Add(beeplimint);
            this.chb_beepOnDurationLimit.HorizontalOptions = LayoutOptions.End;
            stack1.Children.Add(beeplimint);
            stack1.Children.Add(this.chb_beepOnDurationLimit);
            this.mainStack.Children.Add(stack1);
            this.chb_beepOnDurationLimit.IsEnabled = false;


            HorizontalStackLayout stack2 = new HorizontalStackLayout();
            stack2.HorizontalOptions = LayoutOptions.End;
            var stopdurl = new LegendLabel("Stop on duration limit:");
            stack2.Children.Add(stopdurl);
            stack2.Children.Add(this.chb_stopOnDurationLimit);
            this.chb_stopOnDurationLimit.HorizontalOptions = LayoutOptions.End;
            this.mainStack.Children.Add(stack2);
            this.chb_stopOnDurationLimit.IsEnabled = false;


            HorizontalStackLayout stack3 = new HorizontalStackLayout();
            stack3.HorizontalOptions = LayoutOptions.End;
            var getlstart = new LegendLabel("Get location on start:");
            stack3.Children.Add(getlstart);
            stack3.Children.Add(this.chb_getLocationStart);
            this.chb_getLocationStart.HorizontalOptions = LayoutOptions.End;
            this.mainStack.Children.Add(stack3);


            HorizontalStackLayout stack4 = new HorizontalStackLayout();
            stack4.HorizontalOptions = LayoutOptions.End;
            var getlsend = new LegendLabel("Get location on end:");
            stack4.Children.Add(getlsend);
            stack4.Children.Add(this.chb_getLocationEnd);
            this.chb_getLocationEnd.HorizontalOptions = LayoutOptions.End;
            this.mainStack.Children.Add(stack4);


            HorizontalStackLayout stack5 = new HorizontalStackLayout();
            stack5.HorizontalOptions = LayoutOptions.End;
            var trackloc = new LegendLabel("Track location:");
            stack5.Children.Add(trackloc);
            stack5.Children.Add(this.chb_getLocationTrack);
            this.chb_getLocationTrack.HorizontalOptions = LayoutOptions.End;
            this.mainStack.Children.Add(stack5);
            this.chb_getLocationTrack.CheckedChanged += Chb_getLocationTrack_BindingContextChanged;
            this.chb_getLocationTrack.IsEnabled = false;

            var locsens = new LegendLabel("Location sensitivity:");
            this.mainStack.Children.Add(locsens);
            this.mainStack.Children.Add(this.picker_sensitivity);

            this.picker_sensitivity.Items.Add("Lowest");
            this.picker_sensitivity.Items.Add("Low");
            this.picker_sensitivity.Items.Add("Medium");
            this.picker_sensitivity.Items.Add("High");
            this.picker_sensitivity.SelectedIndex = 0;


            HorizontalStackLayout stack7 = new HorizontalStackLayout();
            stack7.HorizontalOptions = LayoutOptions.End;
            var runnextdurl = new LegendLabel("Run next on duration limit:");
            stack7.Children.Add(runnextdurl);
            stack7.Children.Add(this.chb_runNextTrackOnLimit);
            this.chb_getLocationEnd.HorizontalOptions = LayoutOptions.End;
            this.mainStack.Children.Add(stack7);
            this.chb_runNextTrackOnLimit.IsEnabled = false;


            HorizontalStackLayout stack8 = new HorizontalStackLayout();
            stack8.HorizontalOptions = LayoutOptions.End;
            var runnextstop = new LegendLabel("Run next when I stop it:");
            stack8.Children.Add(runnextstop);
            stack8.Children.Add(this.chb_runNextTrackOnStop);
            this.chb_getLocationEnd.HorizontalOptions = LayoutOptions.End;
            this.mainStack.Children.Add(stack8);
            this.chb_runNextTrackOnStop.IsEnabled = false;


            //HorizontalStackLayout stack10 = new HorizontalStackLayout();
            //stack10.HorizontalOptions = LayoutOptions.End;
            //var nextrack = new LegendLabel("Next track:");
            //stack10.Children.Add(nextrack);
            //stack10.Children.Add(this.picker_nextTrack);
            //this.chb_getLocationEnd.HorizontalOptions = LayoutOptions.End;
            //this.mainStack.Children.Add(stack10);


            var nextrack = new LegendLabel("Next track:");
            this.mainStack.Children.Add(nextrack);
            this.mainStack.Children.Add(this.picker_nextTrack);
            this.picker_nextTrack.Items.Add(" - ");
            this.picker_nextTrack.SelectedIndex = 0;


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

        private void Chb_getLocationTrack_BindingContextChanged(object? sender, EventArgs e)
        {
            this.picker_sensitivity.IsEnabled = this.chb_getLocationTrack.IsChecked;
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
                this.Title = "Create new event type";
            }
            else
            {
                buttonGrid_2.Children.Add(this.button_save);
                Grid.SetColumn(this.button_save, 1);
                this.Title = "Edit event type";

            }


            this.picker_nextTrack.Items.Clear();
            this.picker_nextTrack.Items.Add("-");
            for (int i = 0; i < LocalStorage.Types.Count; i++)
            {
                this.picker_nextTrack.Items.Add(LocalStorage.Types[i].Title);
            }



            this.picker_category.Items.Clear();
            for (int i = 0; i < LocalStorage.Categories.Count; i++)
            {
                if (LocalStorage.OpenedCategory.Id == LocalStorage.Categories[i].Id)
                {
                    this.picker_category.Items.Add(this.Prefix_currentCategory + LocalStorage.Categories[i].Title);

                    this.picker_category.SelectedIndex = i;
                }
                else
                {
                    this.picker_category.Items.Add(this.Prefix_otherCategory + LocalStorage.Categories[i].Title);

                }
            }

        }


        internal async Task SetData(EventType editedEvent)
        {
            this.id = editedEvent.Id;
            LocalStorage.SelectedIcon = editedEvent.Icon;
            this.titleInput.Text = editedEvent.Title != null ? editedEvent.Title : "";
            this.descriptionInput.Text = editedEvent.Description != null ? editedEvent.Description : "";

            this.titleText = editedEvent.Title != null ? editedEvent.Title : "";
            this.descriptionText = editedEvent.Description != null ? editedEvent.Description : "";

            this.picker_sensitivity.IsEnabled = editedEvent.GetLocationTrack;
            this.chb_getLocationTrack.IsChecked = editedEvent.GetLocationTrack;
            this.chb_getLocationEnd.IsChecked = editedEvent.GetLocationEnd;
            this.chb_getLocationStart.IsChecked = editedEvent.GetLocationStart;
            this.chb_beepOnDurationLimit.IsChecked = editedEvent.BeepOnDurationLimit;
            this.chb_stopOnDurationLimit.IsChecked = editedEvent.StopOnDurationLimit;
            this.chb_runNextTrackOnLimit.IsChecked = editedEvent.RunNextTrackOnDurationLimit;
            this.chb_runNextTrackOnStop.IsChecked = editedEvent.RunNextTrackOnStopByUser;

            this.durationPicker.Text = editedEvent.DurationLimit.ToString();
            this.Harvest();

            this.SetButtonBlock();
        }


        private async void Button_iconPicker_Clicked(object? sender, EventArgs e)
        {
            LocalStorage.SelectedIcon = LocalStorage.EditedType.Icon;
            IconPickerModal impm = new IconPickerModal();
            await Navigation.PushAsync(impm);
        }


        internal async void Harvest()
        {
            this.titleText = this.titleInput.Text.Trim();
            this.descriptionText = this.descriptionInput.Text.Trim();

            this.val_getLocationStart = this.chb_getLocationStart.IsChecked;
            this.val_getLocationEnd = this.chb_getLocationEnd.IsChecked;
            this.val_getLocationTrack = this.chb_getLocationTrack.IsChecked;
            this.val_beepOnDurationLimit = this.chb_beepOnDurationLimit.IsChecked;
            //this.val_isArchieved = 
            this.val_runNextTrackOnStop = this.chb_runNextTrackOnStop.IsChecked;
            this.val_runNextTrackOnLimit = this.chb_runNextTrackOnLimit.IsChecked;
            this.val_duration_limit = int.Parse(this.durationPicker.Text.ToString());
            this.val_stopOnDurationLimit = this.chb_stopOnDurationLimit.IsChecked;

            try
            {
                for (int i = 0; i < LocalStorage.Categories.Count; i++)
                {
                    string checkString = this.picker_category.Items[this.picker_category.SelectedIndex].Substring(this.Prefix_currentCategory.Length);
                    if (this.picker_category.Items.Count > 0 &&
                        LocalStorage.Categories[i].Title.Trim() == checkString.Trim()
                        )
                    {
                        this.val_select_category = LocalStorage.Categories[i].Id;
                        break;
                    }
                }
                this.val_select_sensitivity = this.picker_sensitivity.SelectedIndex;
                this.val_next_track = null;
                for (int i = 0; i < LocalStorage.Types.Count; i++)
                {
                    if (this.picker_nextTrack.Items.Count > 0 && LocalStorage.Types[i].Title == this.picker_nextTrack.Items[this.picker_nextTrack.SelectedIndex])
                    {
                        this.val_next_track = LocalStorage.Types[i].Id;
                        break;
                    }
                }

            }
            catch (Exception exx)
            {
                var ex = exx.Message;
            }
        }


        internal bool CheckValid()
        {
            this.Harvest();
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
    }
}
