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

            if (Settings.LOGGEDIN)
            {
                lblWelcome.Text = "Welcome " + Settings.FIRSTNAME + " " + Settings.LASTNAME;

                GetMOTD();
                GetMyCaseLoad();
            }
            else
            {
                Application.Current.MainPage = new Login();
            }

        }

        async void GetMyCaseLoad()
        {
            string URL = Settings.BASEURL;//"";
            string u = Settings.USERNAME;

           
            URL += "/Login/GetCaseLoad/" + u;

            HttpClient c = new HttpClient();

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<MemberDetailsShort>>(response);

            int evenodd = 0;

            foreach (MemberDetailsShort s in theresult)
            {
                evenodd += 1;
                Controls.MemberPlacard m = new Controls.MemberPlacard(s.FirstName,s.LastName,s);

                m.SetBackground(evenodd);

                TapGestureRecognizer trec = new TapGestureRecognizer();
                trec.NumberOfTapsRequired = 1;
                trec.Tapped += Trec_Tapped;

                m.GestureRecognizers.Add(trec);

                lstMyCaseLoad.Children.Add(m);

            }

            //lvMYCASELOAD.ItemsSource = theresult;

        }

        private void Trec_Tapped(object sender, EventArgs e)
        {

            Application.Current.MainPage = new MemberFunctionsPage((CFCSMobile.Controls.MemberPlacard)sender);

            //DisplayAlert("Tapped", ((Controls.MemberPlacard)sender).FirstName + " " + ((Controls.MemberPlacard)sender).LastName + " Tapped On" +
            //    ((Controls.MemberPlacard)sender).TheData.MMID, "OK");
        }

        async void GetMOTD()
        {

            string URL = Settings.BASEURL; // "";
            string u = Settings.USERNAME;


            URL += "/Login/MOTD/" + u;


            HttpClient c = new HttpClient();

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<string>(response);

            if (theresult != "")
            {
                btnMOTD.IsVisible = true;

                Settings.MOTD = theresult;
            }
            else
            {
                btnMOTD.IsVisible = false;

                Settings.MOTD = "";
            }

            //txtMOTD.Text = theresult;

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        void Handle_MOTDClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Message of the day", Settings.MOTD, "OK");
        }
    }
}
