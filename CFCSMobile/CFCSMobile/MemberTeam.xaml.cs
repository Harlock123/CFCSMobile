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
	public partial class MemberTeam : ContentPage
	{
        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

        public MemberTeam ()
		{
			InitializeComponent ();
		}

        public MemberTeam(Controls.MemberPlacard TheMember)
        {
            
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);

            GetTeam();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberFunctionsPage(SelectedMember);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        async private void GetTeam()
        {
            // api/Login/MemberReferrals/12057
            // api/Login/MemberObservers/12057

            string URL = Settings.BASEURL;//"";
            string u = SelectedMember.TheData.SSN;

            ActWorking.IsVisible = true;
            ActWorking.IsRunning = true;

            URL += "/Login/MemberReferrals/" + u;

            HttpClient c = new HttpClient();

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<MemberReferralSource>>(response);

            foreach (MemberReferralSource s in theresult)
            {
                
                Controls.MemberReferral m = new Controls.MemberReferral(s);

                //TapGestureRecognizer trec = new TapGestureRecognizer();
                //trec.NumberOfTapsRequired = 1;
                //trec.Tapped += Trec_Tapped;

                //m.GestureRecognizers.Add(trec);

                lstTeamFolks.Children.Add(m);

            }

            URL = Settings.BASEURL;//"";
            URL += "/Login/MemberObservers/" + u;

            c = new HttpClient();

            var response2 = await c.GetStringAsync(URL);

            var theresult2 = JsonConvert.DeserializeObject<List<MemberObservers>>(response2);

            foreach (MemberObservers s in theresult2)
            {

                Controls.MemberStaff m = new Controls.MemberStaff(s);

                //TapGestureRecognizer trec = new TapGestureRecognizer();
                //trec.NumberOfTapsRequired = 1;
                //trec.Tapped += Trec_Tapped;

                //m.GestureRecognizers.Add(trec);

                lstTeamFolks.Children.Add(m);

            }

            ActWorking.IsVisible = false;
            ActWorking.IsRunning = false;

        }
    }
}