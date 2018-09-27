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
                        
            //if (Application.Current.Properties.ContainsKey("BASEURL"))
            //{
            //    URL = Application.Current.Properties["BASEURL"] as string;
            //}
            //else
            //{
            //    Application.Current.Properties.Add("BASEURL", "http://192.168.12.48/CFCSMobileWebServices/api");

            //    URL = Application.Current.Properties["BASEURL"] as string;
            //    //URL = "http://192.168.12.48/CFCSMobileWebServices/api";  // just in case
            //}

            URL += "/Login/DoLogin/" + u + "/" + p;


            try
            {
                ActWorking.IsVisible = true;
                ActWorking.IsRunning = true;

                HttpClient c = new HttpClient();

                var response = await c.GetStringAsync(URL);

                var theresult = JsonConvert.DeserializeObject<PersonLoggedIn>(response);

                ActWorking.IsVisible = false;
                ActWorking.IsRunning = false;

                if (theresult.Success)
                {

                    if (Application.Current.Properties.ContainsKey("USERNAME"))
                    {
                        Application.Current.Properties["USERNAME"] = theresult.UserName;
                    }
                    else
                    {
                        Application.Current.Properties.Add("USERNAME", theresult.UserName);
                    }

                    if (Application.Current.Properties.ContainsKey("FIRSTNAME"))
                    {
                        Application.Current.Properties["FIRSTNAME"] = theresult.FirstName;
                    }
                    else
                    {
                        Application.Current.Properties.Add("FIRSTNAME", theresult.FirstName);
                    }

                    if (Application.Current.Properties.ContainsKey("LASTNAME"))
                    {
                        Application.Current.Properties["LASTNAME"] = theresult.LastName;
                    }
                    else
                    {
                        Application.Current.Properties.Add("LASTNAME", theresult.LastName);
                    }

                    if (Application.Current.Properties.ContainsKey("LOGGEDIN"))
                    {
                        Application.Current.Properties["LOGGEDIN"] = theresult.Success;
                    }
                    else
                    {
                        Application.Current.Properties.Add("LOGGEDIN", theresult.Success);
                    }

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

                ActWorking.IsVisible = false;
                ActWorking.IsRunning = false;
            }

        }
    }
}