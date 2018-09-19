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
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
