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
	public partial class MemberFunctionsPage : ContentPage
	{
        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

		public MemberFunctionsPage ()
		{
			InitializeComponent ();
		}

        public MemberFunctionsPage(Controls.MemberPlacard TheMember)
        {
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);
        }

        private void btnMOTD_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        void Handle_Encounter_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Tapped", "Handle Encounter", "OK");
        }


        void Handle_Collateral_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Tapped", "Handle Collateral", "OK");
        }

        void Handle_Auth_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MemberAuths(SelectedMember);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        private void btnTeam_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberTeam(SelectedMember);
        }
    }
}