using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace CFCSMobile.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MemberPlacard : Grid
	{
		public MemberPlacard ()
		{
			InitializeComponent ();
		}

        public MemberPlacard (string fn, string ln, MemberDetailsShort s)
        {
            InitializeComponent();

            FirstName = fn;
            LastName = ln;

            MemberFN.Text = fn + " " + ln;
            MemberDOB.ValueText = s.DOB;

            MemberPHONE.ValueText = s.Phone1;

            MemberAddress m = s.memberAddress;

            MemberAddress.ValueText = m.Address1 + " " + m.Address2.Trim() + " " + m.City.Trim() + " " + m.State + "  " + m.ZipCode; 

            if (s.Gender == "M")
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.UWP:
                        imgGender.Source = ImageSource.FromFile("Assets/Male.png");
                        break;
                    default:
                        imgGender.Source = ImageSource.FromFile("Male.png");
                        break;
                }

                //imgGender.Source = ImageSource.FromFile("Male.png");

            }
            else
            {

                if (s.Gender == "F")
                {

                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            imgGender.Source = ImageSource.FromFile("Assets/FeMale.png");
                            break;
                        default:
                            imgGender.Source = ImageSource.FromFile("FeMale.png");
                            break;
                    }

                    //imgGender.Source = ImageSource.FromFile("FeMale.png");

                }
                else
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            imgGender.Source = ImageSource.FromFile("Assets/Unknown.png");
                            break;
                        default:
                            imgGender.Source = ImageSource.FromFile("Unknown.png");
                            break;
                    }

                    //imgGender.Source = ImageSource.FromFile("Unknown.png");
                }
            }


            TheData = s;

        }

        public void SetBackground(int evenodd)
        {

            if (evenodd % 2 == 0)
                theGrid.BackgroundColor = Settings.EvenColor;
            else
                theGrid.BackgroundColor = Settings.OddColor;
        }

        public string FirstName = "";
        public string LastName = "";

        public CFCSMobile.MemberDetailsShort TheData = new CFCSMobile.MemberDetailsShort();

	}
}