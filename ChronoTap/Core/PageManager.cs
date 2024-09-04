using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoTap.Pages.Root;
using ChronoTap.Pages.Com.MainPage;
using ChronoTap.Pages.Com.StoryPage;
using ChronoTap.Pages.Com.ChartPage;


namespace ChronoTap.Core
{
    internal class PageManager
    {

        public static TypeBrowserInCategoryPage TypeBrowserInCategoryPage = new TypeBrowserInCategoryPage();

        public static MainPage MainPage = new MainPage();



        public static StoryPage StoryPage = new StoryPage();

        //public static ChartPage ChartPage = new ChartPage();

        // All other pages can be initialized before call it within RootShell
        public static Shell RootShell = new RootShell();



    }
}
