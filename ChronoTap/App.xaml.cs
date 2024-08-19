using ChronoTap.Core;
using ChronoTap.Pages.Root;
using ChronoTap.Storage;
using ChronoTap.Storage.Database;
using ChronoTap.Style;

namespace ChronoTap
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DatabaseService.INITIALZE_TABLES();

            LocalStorage.Boot();
            BaseTheme.SetTheme();
            //MainPage = new MainPage2();
            //MainPage = new NavigationPage( PageManager.MainPage);
            MainPage = new RootShell();

            //PageManager.MainPage.categoryStack.SetActiveCard();
            //PageManager.MainPage.SetActiveCard();
        }
    }
}
