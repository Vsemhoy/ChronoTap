using ChronoTap.Core;
using ChronoTap.Pages.Com.MainPage;
using ChronoTap.Pages.Com.MainPage.Stacks;

using ChronoTap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Root
{
    public partial class RootShell : Shell
    {

        ShellContent MainShellPage { get; set; }
        ShellContent StoryPage { get; set; }

        FlyoutItem MainFlyoutItem { get; set; }
        FlyoutItem StoryFlyoutItem { get; set; }





        public RootShell()
        {
            TabBar tabBar = new TabBar();
            this.FlyoutBehavior = FlyoutBehavior.Disabled;



            this.BackgroundColor = Colors.AliceBlue;

            this.MainShellPage = new ShellContent();
            this.MainShellPage.Content = PageManager.MainPage;
            this.MainShellPage.Title = "Browser";
            this.MainShellPage.Icon = "collection_play_fill.png";
            tabBar.Items.Add(this.MainShellPage);


            this.StoryPage = new ShellContent();
            this.StoryPage.Content = PageManager.StoryPage;
            this.StoryPage.Title = "Story";
            this.StoryPage.Icon = "clock_history.png";
            this.StoryPage.FlyoutItemIsVisible = false;
            tabBar.Items.Add(this.StoryPage);





            this.CurrentItem = this.MainShellPage;

            PageManager.MainPage.categoryStack.SetActiveCard();

            PageManager.MainPage.SetActiveCard();
        }
    }
}


