using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Xamarin.Forms;

namespace CFCSMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey("LOGGEDIN"))
            {
                lblWelcome.Text = "Welcome " + (string)Application.Current.Properties["FIRSTNAME"] + " " + (string)Application.Current.Properties["LASTNAME"];
            }
            else
            {
                Application.Current.MainPage = new Login();
            }

        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }
    }
}
