using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            ContactDate.MaximumDate = DateTime.Now; // cannot pick a date in the future for contact

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

        private async void TheSaveButton_Clicked(object sender, EventArgs e)
        {
            // do something here

            if (!ValiddatedUI())
            {
                await DisplayAlert("Big trouble in little china", 
                    "You must select a Note Type, a Contact Type, a Contact date, and finally enter a comment or narrative about this contact note...", "Understood");
            }
            else
            {

                string URL = Settings.BASEURL;//"";
                string u = SelectedMember.TheData.SSN;

                URL += "/Login/SaveNote";

                MemberProgressNotes mpn = new MemberProgressNotes();
                mpn.SSN = u;
                mpn.AUTHOR = Settings.USERNAME;
                mpn.CONTACTDATE = ContactDate.Date;
                mpn.NOTATION = ContactNarrative.Text;

                CodedDescriptor c1 = (CodedDescriptor)NoteTypePicker.SelectedItem;
                CodedDescriptor c2 = (CodedDescriptor)ContactTypePicker.SelectedItem;

                //mpn.CONTACTTYPEDESCRIPTION = c2.code;
                mpn.NOTECONTACTDESC = c2.code;
                mpn.NOTETYPEDESC = c1.code;
                mpn.mpnID = -1; // Adding a new note

                string TheObjectTurnedIntoAString = JsonConvert.SerializeObject(mpn);

                HttpClient c = new HttpClient();

                var TheContent = new StringContent(TheObjectTurnedIntoAString, Encoding.UTF8, "application/json");


                var response = await c.PostAsync(URL, TheContent);

                if (response.IsSuccessStatusCode)
                {
                    // Yeah we made it

                    await DisplayAlert("Status...", "The Contact was written to the system", "OK");
                    Application.Current.MainPage = new MemberCollateralNotes(SelectedMember);

                }
                else
                {
                    await DisplayAlert("Status...", "There was a problem in saving The Contact to the system", "OK");
                    //Application.Current.MainPage = new MemberCollateralNotes(SelectedMember);
                }

                //var theresult = JsonConvert.DeserializeObject<Boolean>(response);
            }

        }

        private Boolean ValiddatedUI()
        {
            Boolean result = true; // assume success

            if (NoteTypePicker.SelectedItem == null)
                result = false;

            if (ContactTypePicker.SelectedItem == null)
                result = false;

            if (ContactNarrative.Text is null)
                result = false;
            else
                if (ContactNarrative.Text.Trim() + "" == "")
                    result = false;
            
            return result;

        }
    }
}