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
	public partial class MemberAuths : ContentPage
	{
        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

        public MemberAuths ()
		{
			InitializeComponent ();
		}

        public MemberAuths(Controls.MemberPlacard TheMember)
        {
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);

            GetAuths();
        }
        
        private void btnMOTD_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberFunctionsPage(SelectedMember);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        async void GetAuths()
        {
            string URL = Settings.BASEURL;//"";
            string u = SelectedMember.TheData.SSN;

            ActWorking.IsVisible = true;
            ActWorking.IsRunning = true;

            URL += "/Login/GetAuths";

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("LOGIN", Settings.USERNAME);
            c.DefaultRequestHeaders.Add("IDNUM", u);


            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<AuthorizedService>>(response);

            int evenodd = 0;

            foreach (AuthorizedService s in theresult)
            {
                evenodd += 1;

                Controls.MemberAuth m = new Controls.MemberAuth(s);

                m.SetBackground(evenodd);

                //TapGestureRecognizer trec = new TapGestureRecognizer();
                //trec.NumberOfTapsRequired = 1;
                //trec.Tapped += Trec_Tapped;

                //m.GestureRecognizers.Add(trec);

                lstAuths.Children.Add(m);

            }

            ActWorking.IsVisible = false;
            ActWorking.IsRunning = false;

            //lvMYCASELOAD.ItemsSource = theresult;

        }


    }
}