using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;


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

            GetMyMessages();
        }

        async void GetMyMessages()
        {
            string URL = Settings.BASEURL;//"";
            string u = Settings.USERNAME;


            URL += "/Login/GetMessagesFor";

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("LOGIN", u);

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<UserMessage>>(response);

            int evenodd = 0;

            foreach (UserMessage s in theresult)
            {
                evenodd += 1;
                Controls.UserMESSAGE m = new Controls.UserMESSAGE(s);

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

            DisplayAlert("Narrative", ((Controls.UserMESSAGE)sender).TheMSG.BODY, "OK");

            //Application.Current.MainPage = new MemberFunctionsPage((CFCSMobile.Controls.MemberPlacard)sender);

            //DisplayAlert("Tapped", ((Controls.MemberPlacard)sender).FirstName + " " + ((Controls.MemberPlacard)sender).LastName + " Tapped On" +
            //    ((Controls.MemberPlacard)sender).TheData.MMID, "OK");
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