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

            MemberFN.Text = fn;
            MemberLN.Text = ln;

            TheData = s;

        }

        public string FirstName = "";
        public string LastName = "";

        public CFCSMobile.MemberDetailsShort TheData = new CFCSMobile.MemberDetailsShort();

	}
}