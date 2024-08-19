using ChronoTap.Core;
using ChronoTap.Pages.Elements.Cards;
using ChronoTap.Storage;
using ChronoTap.Style;
using ChronoTap.Core;
using ChronoTap.Storage;
using ChronoTap.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Com.MainPage
{
    internal class TypeBrowserInCategoryPage : ContentPage
    {
        public Grid baseGrid = new Grid();

        public VerticalStackLayout activeStack = new VerticalStackLayout();
        public VerticalStackLayout contentStack = new VerticalStackLayout();
        public ScrollView scrollStack = new ScrollView();

        public TypeAddMiniCard addTypeButtonCard = new TypeAddMiniCard();


        public ToolbarItem tbButton_editCategory = new ToolbarItem();

        private List<TypeStackCard_in_Category> currentCardStack = new List<TypeStackCard_in_Category>();

        public TypeBrowserInCategoryPage()
        {
            this.baseGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.baseGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            this.BackgroundColor = Color.FromHex("#F0F0F3");

            tbButton_editCategory.IconImageSource = "dotnet_bot.png";
            tbButton_editCategory.Text = "Edit Category";
            tbButton_editCategory.Order = ToolbarItemOrder.Secondary;
            this.ToolbarItems.Add(tbButton_editCategory);

            this.addTypeButtonCard.Margin = BaseTheme.THEME.formFramePadding;






            this.scrollStack.Content = this.contentStack;

            this.baseGrid.Children.Add(this.activeStack);
            Grid.SetRow(this.activeStack, 0);
            this.baseGrid.Children.Add(this.scrollStack);
            Grid.SetRow(this.scrollStack, 1);
            this.Content = this.baseGrid;



            this.addTypeButtonCard.onClick.Tapped += OnClick_AddType_Button;
            ModalManager.typeEditorModal.button_save.Clicked += Button_saveType_Clicked;
            ModalManager.typeEditorModal.button_create.Clicked += Button_createType_Clicked;
        }

        private async void Button_createType_Clicked(object? sender, EventArgs e)
        {

            if (ModalManager.typeEditorModal.CheckValid())
            {

                await ModalManager.TypeEditorModal_ActionCreate(sender, e);
                await this.SetTypeList();
            }
        }

        private async void Button_saveType_Clicked(object? sender, EventArgs e)
        {
            if (ModalManager.typeEditorModal.CheckValid())
            {
                await ModalManager.TypeEditorModal_ActionSave(sender, e);
                await this.SetTypeList();
            }
        }

        private async void OnClick_AddType_Button(object? sender, TappedEventArgs e)
        {
            LocalStorage.EditedType = new Storage.Database.Models.EventType();
            try
            {
                await ModalManager.typeEditorModal.SetData(LocalStorage.EditedType);
                await Navigation.PushAsync(ModalManager.typeEditorModal);

            }
            catch (Exception exx)
            {
                var ex = exx.Message;
            }
        }


        public async Task SetTypeList()
        {
            this.contentStack.Clear();
            this.currentCardStack.Clear();
            HorizontalStackLayout _separator = new HorizontalStackLayout
            {
                HeightRequest = 0.5,
                BackgroundColor = Colors.LightGray
            };
            this.contentStack.Children.Add(_separator);

            for (int i = 0; i < LocalStorage.Types.Count; i++)
            {
                if (LocalStorage.Types[i].CategoryId == LocalStorage.OpenedCategory.Id)
                {
                    TypeStackCard_in_Category estcard = new TypeStackCard_in_Category(LocalStorage.Types[i]);
                    if (LocalStorage.ActiveType != null && LocalStorage.ActiveType.Id == estcard.id)
                    {
                        estcard.IsActive = true;
                    }
                    this.currentCardStack.Add(estcard);
                    this.contentStack.Children.Add(estcard);
                    HorizontalStackLayout separator = new HorizontalStackLayout
                    {
                        HeightRequest = 0.5,
                        BackgroundColor = Colors.LightGray
                    };
                    this.contentStack.Children.Add(separator);
                    estcard.swipeLeftItem.Clicked += EditType_Button_clicked;
                }
            }
            this.contentStack.Children.Add(this.addTypeButtonCard);
        }



        public async Task DeactivateAllCards(string exceptId = "")
        {
            for (int i = 0; i < this.currentCardStack.Count; i++)
            {
                if (this.currentCardStack[i].id != exceptId)
                {
                    this.currentCardStack[i].IsActive = false;
                    this.currentCardStack[i].SetTriggerState(10);

                }
            }
        }



        private async void EditType_Button_clicked(object? sender, EventArgs e)
        {
            await Task.Delay(150);
            await ModalManager.typeEditorModal.SetData(LocalStorage.EditedType);
            await Navigation.PushAsync(ModalManager.typeEditorModal);
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            this.SetTypeList();
        }
    }
}
