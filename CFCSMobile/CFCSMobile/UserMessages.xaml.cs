using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CFCSMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMessages : ContentPage
    {
        public UserMessages()
        {
            InitializeComponent();

            if (Settings.LOGGEDIN)
            {
                lblWelcome.Text = Settings.FIRSTNAME + " " + Settings.LASTNAME + "'s Messages";

            }
            else
            {
                Application.Current.MainPage = new Login();
            }
        }

        private void Handle_MOTDClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Handle_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }
    }
}