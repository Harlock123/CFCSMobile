using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;

namespace CFCSMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            byte[] hash;

            SHA256Managed alg = new SHA256Managed();

            hash = alg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(txtPassword.Text + ""));

            string pw = Convert.ToBase64String(hash);

            string uname = txtUserName.Text + "";

            DoLogin(uname, pw);


        }

        async void DoLogin(string u, string p)
        {

            string URL = CFCSMobile.Settings.BASEURL; //"";

            URL += "/Login/DoLogin";

            try
            {
                ActWorking.IsVisible = true;
                ActWorking.IsRunning = true;

                HttpClient c = new HttpClient();

                c.DefaultRequestHeaders.Add("LOGIN", u);
                c.DefaultRequestHeaders.Add("PW", p);
                
                var response = await c.GetStringAsync(URL);
                
                var theresult = JsonConvert.DeserializeObject<PersonLoggedIn>(response);

                ActWorking.IsVisible = false;
                ActWorking.IsRunning = false;

                if (theresult.Success)
                {

                    Settings.USERNAME = theresult.UserName;
                    Settings.FIRSTNAME = theresult.FirstName;
                    Settings.LASTNAME = theresult.LastName;
                    Settings.LOGGEDIN = true;

                    Application.Current.MainPage = new MainPage();

                }
                else
                {
                    txtPassword.Text = "";
                    txtUserName.Text = "";

                    ActWorking.IsVisible = false;
                    ActWorking.IsRunning = false;

                }
            }
            catch (Exception ex)
            {
                txtPassword.Text = "";
                txtUserName.Text = "";

                await DisplayAlert("Big trouble in little china", ex.Message, "OK");

                ActWorking.IsVisible = false;
                ActWorking.IsRunning = false;
            }

        }
    }
}