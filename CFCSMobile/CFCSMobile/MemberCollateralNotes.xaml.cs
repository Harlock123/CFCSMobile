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
	public partial class MemberCollateralNotes : ContentPage
	{

        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

        public MemberCollateralNotes ()
		{
			InitializeComponent ();
		}

        public MemberCollateralNotes(Controls.MemberPlacard TheMember)
        {
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);

            GetCollateralNotes();
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

        async void GetCollateralNotes()
        {
            string URL = Settings.BASEURL;//"";
            string u = SelectedMember.TheData.SSN;


            URL += "/Login/AllExceptEncounterProgressNotes/" + u;

            HttpClient c = new HttpClient();

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<MemberProgressNotes>>(response);

            int evenodd = 0;

            foreach (MemberProgressNotes s in theresult)
            {
                evenodd += 1;

                Controls.MemberProgressNote m = new Controls.MemberProgressNote(s);

                m.SetBackground(evenodd);

                TapGestureRecognizer trec = new TapGestureRecognizer();
                trec.NumberOfTapsRequired = 1;
                trec.Tapped += Trec_Tapped;

                m.GestureRecognizers.Add(trec);

                lstNotes.Children.Add(m);

            }

            btnAddNewContact.IsVisible = true;

        }

        private void Trec_Tapped(object sender, EventArgs e)
        {

            DisplayAlert("Notation:", ((Controls.MemberProgressNote)sender).TheNote.NOTATION, "OK");

        }

        private void btnAddNewContact_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberCreateCollateralNote(SelectedMember);
        }
    }
}