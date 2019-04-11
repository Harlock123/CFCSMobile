using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;
using Newtonsoft.Json;

namespace CFCSMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMESSAGE : Grid
    {

        public UserMessage TheMSG = null;

        public UserMESSAGE(UserMessage u)
        {
            InitializeComponent();

            MessageType.ValueText = u.MESSAGETYPE;
            CreateDate.ValueText = u.DATECREATED.ToShortDateString();
            NarrativeShort.Text = u.BODYSHORT;
            MessageSource.ValueText = u.SOURCE;

            TheMSG = u;

        }

        public void SetBackground(int evenodd)
        {

            if (evenodd % 2 == 0)
            {
                theGrid.BackgroundColor = Settings.EvenColor;
                FlagAsReadButton.BackgroundColor = Settings.EvenColor;
            }
            else
            {
                theGrid.BackgroundColor = Settings.OddColor;
                FlagAsReadButton.BackgroundColor = Settings.OddColor;
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            string URL = Settings.BASEURL;
            

            URL += "/Login/FlagMessageAsRead";

            string TheObjectTurnedIntoAString = TheMSG.msgID.ToString();

            HttpClient c = new HttpClient();

            var TheContent = new StringContent(TheObjectTurnedIntoAString, Encoding.UTF8, "application/json");

            var response = await c.PostAsync(URL, TheContent);

            if (response.IsSuccessStatusCode)
            {
                // Yeah we made it

                //await DisplayAlert("Status...", "The Encounter was written to the system", "OK");
                //Application.Current.MainPage = new MemberEncounterNotes(SelectedMember);
                Application.Current.MainPage = new UserMessages();


            }
            else
            {
                //await DisplayAlert("Status...", "There was a problem in saving The Encounter to the system", "OK");
                ////Application.Current.MainPage = new MemberCollateralNotes(SelectedMember);
                ///
                theGrid.BackgroundColor = Settings.ErrorColor;
                LBL.TitleText = "Message Detail: Some error occured flagging this message as read";
            }


            //Console.WriteLine("Setting MSG as read ID:" + TheMSG.msgID.ToString());
        }
    }
}