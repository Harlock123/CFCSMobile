using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CFCSMobile
{
    public partial class App : Application
    {
        public Boolean isLoggedIn = false;

        public App()
        {
            InitializeComponent();

            // http://30.68.44.146:53557/api

            if (Application.Current.Properties.ContainsKey("BASEURL"))
            {
                Application.Current.Properties["BASEURL"] = "http://30.68.44.146:53557/api";
            }
            else
            {
                Application.Current.Properties.Add("BASEURL", "http://30.68.44.146:53557/api");
            }

            if (!isLoggedIn)
            {
                MainPage = new Login();
            }
            else
            {
                MainPage = new MainPage();
            }

            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

            isLoggedIn = false;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
