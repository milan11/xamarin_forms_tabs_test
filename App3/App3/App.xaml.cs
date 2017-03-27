using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3
{
    public partial class App : Application
    {
        private ContentPage additionalContentPage;
        private TabbedPage tp;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(CreateTabbedPage());

            CreateAdditionalContentPage();
        }

        private void CreateAdditionalContentPage()
        {
            additionalContentPage = new ContentPage();

            {
                StackLayout sl = new StackLayout();

                sl.Children.Add(new Label() { Text = "This is additional ContentPage" });

                Button b = new Button() { Text = "Continue" };
                b.Clicked += async (sender, e) => { await MainPage.Navigation.PopToRootAsync(); };

                sl.Children.Add(b);

                additionalContentPage.Content = sl;
            }
        }

        private Page CreateTabbedPage()
        {
            tp = new TabbedPage();

            {
                ContentPage page = new ContentPage();
                page.Title = "Page 1";

                StackLayout sl = new StackLayout();
                sl.Children.Add(new Label() { Text = "This is page 1" });

                {
                    Button button = new Button() { Text = "Go to page 2 - using SelectedItem" };
                    button.Clicked += (sender, e) =>
                    {
                        tp.SelectedItem = 1;
                    };
                    sl.Children.Add(button);
                }

                {
                    Button button = new Button() { Text = "Go to page 2 - using CurrentPage" };
                    button.Clicked += (sender, e) =>
                    {
                        tp.CurrentPage = tp.Children[1];
                    };
                    sl.Children.Add(button);
                }

                {
                    Button button = new Button() { Text = "Go to page 2 - through problematic code" };
                    button.Clicked += (sender, e) =>
                    {
                        ProblematicCode();
                    };
                    sl.Children.Add(button);
                }

                page.Content = sl;
                tp.Children.Add(page);
            }

            {
                ContentPage page = new ContentPage();
                page.Title = "Page 2";

                StackLayout sl = new StackLayout();
                sl.Children.Add(new Label() { Text = "This is page 2" });

                page.Content = sl;
                tp.Children.Add(page);
            }

            return tp;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void ProblematicCode()
        {
            tp.CurrentPage = tp.Children[1];
            await MainPage.Navigation.PushAsync(additionalContentPage);
        }
    }
}
