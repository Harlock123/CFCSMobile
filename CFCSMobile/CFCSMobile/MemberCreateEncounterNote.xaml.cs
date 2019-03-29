﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFCSMobile.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;
using Newtonsoft.Json;


namespace CFCSMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MemberCreateEncounterNote : ContentPage
	{
        private CFCSMobile.Controls.MemberPlacard SelectedMember = null;
        private AuthServiceForDisplay TheSelectedAuth = null;

        public MemberCreateEncounterNote ()
		{
			InitializeComponent ();
		}

        public MemberCreateEncounterNote(MemberPlacard TheMember)
        {
            InitializeComponent();

            SelectedMember = TheMember;

            lstMemberStuff.Children.Add(SelectedMember);

            GetAuths();
        }


        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MemberEncounterNotes(SelectedMember);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();
            Application.Current.MainPage = new Login();
        }

        async void GetService(string SVCID)
        {
            string URL = Settings.BASEURL;//"";
            string u = SVCID;

            //ActWorking.IsVisible = true;
            //ActWorking.IsRunning = true;

            URL += "/Login/GetSvcForAuth";

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("LOGIN", Settings.USERNAME);
            c.DefaultRequestHeaders.Add("IDNUM", u);

            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<SvcForAuth>(response);

            List<SvcForAuth> TheList = new List<SvcForAuth>();

            TheList.Add(theresult);

            AvailableServices.ItemsSource = TheList;
            AvailableServices.SelectedIndex = 0; // pick the first and should be the only one
        }

        async void GetAuths()
        {
            string URL = Settings.BASEURL;//"";
            string u = SelectedMember.TheData.SSN;

            //ActWorking.IsVisible = true;
            //ActWorking.IsRunning = true;

            URL += "/Login/GetAuthsForNow";

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("LOGIN", Settings.USERNAME);
            c.DefaultRequestHeaders.Add("IDNUM", u);


            var response = await c.GetStringAsync(URL);

            var theresult = JsonConvert.DeserializeObject<List<AuthorizedService>>(response);

            var TheList = new List<AuthServiceForDisplay>();

            foreach (AuthorizedService s in theresult)
            {

                AuthServiceForDisplay sd = new AuthServiceForDisplay(s);

                TheList.Add(sd);

            }

            AvailableAuths.ItemsSource = TheList;

            //ActWorking.IsVisible = false;
            //ActWorking.IsRunning = false;

            //lvMYCASELOAD.ItemsSource = theresult;

        }

        private bool ValidateUI()
        {
            bool result = true;

            if (AvailableAuths.SelectedItem == null)
                result = false;

            if (AvailableServices.SelectedItem == null)
                result = false;

            if (AvailableDSPs.SelectedItem == null)
                result = false;

            if (txtMinutes.Text + "" == "")
                result = false;
            else
            {
                int mins = 0;
                if (!int.TryParse(txtMinutes.Text, out mins))
                    result = false;
            }

            if (txtNotation.Text + "" == "")
            {
                result = false;
            }


            return result;
        }


        private async void TheSaveButton_Clicked(object sender, EventArgs e)
        {
            // do something here



            if (!ValidateUI())
            {
                await DisplayAlert("Big trouble in little china",
                    "You must select an Authorization, Service, Encounter Date, Number of Minutes spent, and a bit of Narrative for the encounter", "Understood");
            }
            //else
            //{

            //    string URL = Settings.BASEURL;//"";
            //    string u = SelectedMember.TheData.SSN;

            //    URL += "/Login/SaveNote";

            //    MemberProgressNotes mpn = new MemberProgressNotes();
            //    mpn.SSN = u;
            //    mpn.AUTHOR = Settings.USERNAME;
            //    mpn.CONTACTDATE = ContactDate.Date;
            //    mpn.NOTATION = ContactNarrative.Text;

            //    CodedDescriptor c1 = (CodedDescriptor)NoteTypePicker.SelectedItem;
            //    CodedDescriptor c2 = (CodedDescriptor)ContactTypePicker.SelectedItem;

            //    //mpn.CONTACTTYPEDESCRIPTION = c2.code;
            //    mpn.NOTECONTACTDESC = c2.code;
            //    mpn.NOTETYPEDESC = c1.code;
            //    mpn.mpnID = -1; // Adding a new note

            //    string TheObjectTurnedIntoAString = JsonConvert.SerializeObject(mpn);

            //    HttpClient c = new HttpClient();

            //    var TheContent = new StringContent(TheObjectTurnedIntoAString, Encoding.UTF8, "application/json");


            //    var response = await c.PostAsync(URL, TheContent);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        // Yeah we made it

            //        await DisplayAlert("Status...", "The Contact was written to the system", "OK");
            //        Application.Current.MainPage = new MemberCollateralNotes(SelectedMember);

            //    }
            //    else
            //    {
            //        await DisplayAlert("Status...", "There was a problem in saving The Contact to the system", "OK");
            //        //Application.Current.MainPage = new MemberCollateralNotes(SelectedMember);
            //    }

            //    //var theresult = JsonConvert.DeserializeObject<Boolean>(response);
            //}

        }

        private void AvailableAuths_SelectedIndexChanged(object sender, EventArgs e)
        {
            TheSelectedAuth = (AuthServiceForDisplay)AvailableAuths.SelectedItem;

            GetService(TheSelectedAuth.TheAuth.COSTCENTER);

            dtpSD.Date = DateTime.Now;
            dtpSD.MinimumDate = DateTime.Now;
            dtpSD.MaximumDate = TheSelectedAuth.TheAuth.ENDDATE;

            AvailableDSPs.Items.Add(Settings.USERNAME);
            AvailableDSPs.SelectedIndex = 0;


            //dtpED.MinimumDate = DateTime.Now;
            //dtpED.MaximumDate = TheSelectedAuth.TheAuth.ENDDATE;

            //lblUnits.Text = "Units (" + TheSelectedAuth.TheAuth.REMAININGUNITS.ToString() + " max)";

        }
    }
}