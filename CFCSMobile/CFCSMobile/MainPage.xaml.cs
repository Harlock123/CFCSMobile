using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Xamarin.Forms;

namespace CFCSMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            byte[] hash;

            SHA256Managed alg = new SHA256Managed();

            hash = alg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(txtEntry.Text));

            lblOUT.Text = Convert.ToBase64String(hash);



        }
    }
}
