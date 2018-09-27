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

            MemberFN.Text = fn + " " + ln; ;
            MemberLN.Text = s.DOB;

            if (s.Gender == "M")
            {
                imgGender.Source = ImageSource.FromResource("Male.png");
              
            }

            if (s.Gender == "F")
            {
                imgGender.Source = ImageSource.FromResource("FeMale.png");

            }


            TheData = s;

        }

        public string FirstName = "";
        public string LastName = "";

        public CFCSMobile.MemberDetailsShort TheData = new CFCSMobile.MemberDetailsShort();

	}
}