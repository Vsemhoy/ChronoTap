using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Com.StoryPage
{
    internal class StoryPage : ContentPage
    {
        public StackLayout baseStack = new StackLayout();

        public Button MainButton = new Button();

        public StoryPage()
        {
            this.Title = "Super af sdfasdf";
            this.MainButton.Text = "Hello woodf!";

            this.baseStack.Children.Add(this.MainButton);

            this.Content = this.baseStack;
        }
    }
}

