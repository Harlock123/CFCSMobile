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

            // http://192.168.1.250/CFCSMobileWebServices/api // Desktop at home
            // http://192.168.12.48/CFCSMobileWebServices/api // MSI Big Boy
            // http://30.68.44.146:53557/api  // MAC

            if (!Settings.LOGGEDIN)
            {
                MainPage = new Login();
            }
            else
            {
                MainPage = new MainPage();
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

            Settings.LOGGEDIN = false;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
