using ChronoTap.Core;
using ChronoTap.Core.Utils;
using ChronoTap.Pages.Com.MainPage.Stacks;
using ChronoTap.Pages.Elements.Cards;
using ChronoTap.Pages.Elements.Modals;
using ChronoTap.Storage;
using ChronoTap.Style;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Com.MainPage
{
    internal class MainPage : ContentPage
    {

        public StackLayout baseStack = new StackLayout();

        private Grid baseGrid = new Grid();

        public VerticalStackLayout ActiveStack = new VerticalStackLayout();

        public ScrollView ScrollStack = new ScrollView();

        public VerticalStackLayout ContentStack = new VerticalStackLayout();


        public CategoryStack categoryStack { get; set; }
        public RecentTypeStack recentTypeStack { get; set; }

        public ActiveActionMiniCard activeActionCard { get; set; }


        public Button MainButton = new Button();

        public MainPage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            this.Title = "V" + version.ToString();
            this.BackgroundColor = Color.FromHex("#F0F0F3");

            this.MainButton.Text = "Hello woof";

            ToolbarItem tbItem1 = new ToolbarItem();
            tbItem1.IconImageSource = "dotnet_bot.png";
            tbItem1.Text = "Create Category";
            tbItem1.Order = ToolbarItemOrder.Secondary;
            tbItem1.Clicked += TbItem1_Clicked;
            this.ToolbarItems.Add(tbItem1);


            this.categoryStack = new CategoryStack();

            this.recentTypeStack = new RecentTypeStack();


            Label label = new Label();
            label.Text = "OOOHO";



            this.baseGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            this.baseGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            //this.ScrollStack.BackgroundColor = Colors.Blue;



            this.ActiveStack.BackgroundColor = Colors.White;
            this.ActiveStack.Padding = 0;
            this.ActiveStack.Margin = 0;
            this.ActiveStack.Shadow = BaseTheme.THEME.CardShadow;


            this.ScrollStack.Content = this.ContentStack;
            this.ScrollStack.VerticalScrollBarVisibility = ScrollBarVisibility.Default;

            this.ContentStack.Children.Add(this.categoryStack);
            this.ContentStack.Children.Add(this.recentTypeStack);
            this.ContentStack.Children.Add(label);


            this.baseGrid.Children.Add(this.ActiveStack);
            Grid.SetRow(this.ActiveStack, 0);
            this.baseGrid.Children.Add(this.ScrollStack);
            Grid.SetRow(this.ScrollStack, 1);





            //this.baseStack.Children.Add(this.baseGrid);
            this.Content = this.baseGrid;

            this.SetActiveCard();
            //PageManager.TypeBrowserInCategoryPage.tbButton_editCategory.Clicked += Click_EditCategory_in_TypeManager;
        }

        private async void CallIconPicker_Clicked(object? sender, EventArgs e)
        {
            string location = await LocationService.GetCachedLocation();
            await DisplayAlert(location.Length.ToString(), location, "ok");
        }

        //private async void Click_EditCategory_in_TypeManager(object? sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(ModalManager.categoryEditorModal);
        //}

        private async void TbItem1_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(PageManager.TypeBrowserInCategoryPage);
        }


        public async void SetActiveCard()
        {
           
            if (LocalStorage.ActiveChrono == null)
            {
                await Task.Delay(1000);
            }
            this.ActiveStack.Children.Clear();
            this.activeActionCard = null;
          
     
            if (LocalStorage.ActiveChrono != null && LocalStorage.ActiveType != null)
            {
                this.activeActionCard = new ActiveActionMiniCard(LocalStorage.ActiveChrono);
                this.ActiveStack.Children.Add(this.activeActionCard);
                this.activeActionCard.SetDurationText_Process();
            }
            //this.ActiveStack.Children.Add(label2);
            //this.ActiveStack.BackgroundColor = Colors.NavajoWhite;
            //this.ActiveStack.Padding = 12;
            //this.ActiveStack.Shadow = BaseTheme.THEME.CardShadow;
        }

        public async void UnsetActiveCard()
        {
            this.ActiveStack.Children.Clear();
            this.activeActionCard = null;
        }



        protected override void OnAppearing()
        {

            base.OnAppearing();

            // Handle any logic when the page appears
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Handle any cleanup when the page disappears
            //DisposeResources();
            //Navigation.PopModalAsync();
        }

        private void DisposeResources()
        {
            // Dispose or cleanup any resources


        }
    }
}
