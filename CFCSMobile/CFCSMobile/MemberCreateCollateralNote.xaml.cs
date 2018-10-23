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
	public partial class MemberCreateCollateralNote : ContentPage
	{
        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

        public MemberCreateCollateralNote ()
		{
			InitializeComponent ();
		}

        public MemberCreateCollateralNote (Controls.MemberPlacard TheMember)
        {
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);

            NoteTypePicker.ItemsSource = Settings.Lookups.LOOKUPPROGRESSNOTETYPES;
            ContactTypePicker.ItemsSource = Settings.Lookups.LOOKUPNOTECONTACTTYPE;

            //GetCollateralNotes();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberCollateralNotes(SelectedMember);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        private void TheSaveButton_Clicked(object sender, EventArgs e)
        {
            // do something here


        }
    }
}