using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            MemberLN.Text = "DOB: " + s.DOB;

            MemberPHONE.Text = s.Phone1;

            MemberAddress.Text = "Member Address Goes Here";

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
                theGrid.BackgroundColor = Color.LightGreen;
            else
                theGrid.BackgroundColor = Color.LightYellow;
        }

        public string FirstName = "";
        public string LastName = "";

        public CFCSMobile.MemberDetailsShort TheData = new CFCSMobile.MemberDetailsShort();

	}
}