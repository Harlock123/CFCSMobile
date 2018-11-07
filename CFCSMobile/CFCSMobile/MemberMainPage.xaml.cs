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
	public partial class MemberMainPage : ContentPage
	{
        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

        public MemberMainPage ()
		{
			InitializeComponent ();

            if (Settings.LOGGEDIN)
            {
                lblWelcome.Text = "Welcome " + Settings.FIRSTNAME + " " + Settings.LASTNAME;

                Controls.MemberPlacard m = new Controls.MemberPlacard(Settings.MemberLoggedIn.FirstName, Settings.MemberLoggedIn.LastName, Settings.MemberLoggedIn);

                SelectedMember = m;

                //GetMOTD();
                //GetLookups();
                //GetMyCaseLoad();
            }
            else
            {
                Application.Current.MainPage = new Login();
            }
        }

        private void Handle_MOTDClicked(object sender, EventArgs e)
        {

        }

        private void Handle_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        private void btnTeam_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberTeam(SelectedMember);
        }

        private void btnAssessments_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Under Construction", "Presently Under Construction", "OK");
        }

        private void btnPlans_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Under Construction", "Presently Under Construction", "OK");
        }

        private void btnMessages_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Under Construction", "Presently Under Construction", "OK");
        }
    }
}
