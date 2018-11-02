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
	public partial class MemberEncounterNotes : ContentPage
	{

        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;

        public MemberEncounterNotes ()
		{
			InitializeComponent ();
		}

        public MemberEncounterNotes(Controls.MemberPlacard TheMember)
        {
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);

            GetEncounterNotes();
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

        async void GetEncounterNotes()
        {
            string URL = Settings.BASEURL;//"";
            string u = SelectedMember.TheData.SSN;

            ActWorking.IsVisible = true;
            ActWorking.IsRunning = true;

            URL += "/Login/EncounterProgressNotes";

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("LOGIN", Settings.USERNAME);
            c.DefaultRequestHeaders.Add("IDNUM", u);

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<MemberProgressNotes>>(response);

            int evenodd = 0;

            foreach (MemberProgressNotes s in theresult)
            {
                evenodd += 1;

                Controls.MemberEncounterNote m = new Controls.MemberEncounterNote(s);

                m.SetBackground(evenodd);

                TapGestureRecognizer trec = new TapGestureRecognizer();
                trec.NumberOfTapsRequired = 1;
                trec.Tapped += Trec_Tapped;

                m.GestureRecognizers.Add(trec);

                //TapGestureRecognizer trec = new TapGestureRecognizer();
                //trec.NumberOfTapsRequired = 1;
                //trec.Tapped += Trec_Tapped;

                //m.GestureRecognizers.Add(trec);

                lstNotes.Children.Add(m);

            }

            ActWorking.IsVisible = false;
            ActWorking.IsRunning = false;

            btnAddNewContact.IsVisible = true;

        }

        private void btnAddNewContact_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberCreateEncounterNote(SelectedMember);
        }

        private void Trec_Tapped(object sender, EventArgs e)
        {

            DisplayAlert("Notation:", ((Controls.MemberEncounterNote)sender).TheNote.NOTATION, "OK");

            //Application.Current.MainPage = new MemberFunctionsPage((CFCSMobile.Controls.MemberPlacard)sender);

            //DisplayAlert("Tapped", ((Controls.MemberPlacard)sender).FirstName + " " + ((Controls.MemberPlacard)sender).LastName + " Tapped On" +
            //    ((Controls.MemberPlacard)sender).TheData.MMID, "OK");
        }

    }
}