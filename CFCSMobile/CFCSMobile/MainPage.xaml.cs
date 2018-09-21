using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

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

                GetMOTD();
            }
            else
            {
                Application.Current.MainPage = new Login();
            }

        }

        async void GetMOTD()
        {

            string URL = "";
            string u = "";

            if (Application.Current.Properties.ContainsKey("BASEURL"))
            {
                URL = Application.Current.Properties["BASEURL"] as string;
                u = Application.Current.Properties["USERNAME"] as string;
            }
            else
            {
                URL = "http://30.68.44.146:53557/api";  // just in case
            }

            URL += "/Login/MOTD/" + u;


            HttpClient c = new HttpClient();

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<string>(response);

            txtMOTD.Text = theresult;

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }
    }
}
