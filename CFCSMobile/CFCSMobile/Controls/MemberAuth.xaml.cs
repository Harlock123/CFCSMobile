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
	public partial class MemberAuth : Grid
	{
        AuthorizedService TheService = null;

		public MemberAuth ()
		{
			InitializeComponent ();
		}

        public MemberAuth(AuthorizedService s)
        {
            InitializeComponent();

            TheService = s;

            MemberService.Text = s.COSTCENTERDESC;
            MemberFunder.Text = s.FUNDER;

            string ed = s.ENDDATE.ToShortDateString();

            if (ed.Trim() == "")
                ed = "Open";

            MemberDateRange.Text = s.STARTDATE.ToShortDateString() + " - " + ed;

            MemberUnits.Text = "Units: " + s.UNITS.ToString();

            MemberRemainingUnits.Text = s.REMAININGUNITS.ToString() + " Remaining";

            MemberProvider.Text = s.PROVIDERNAME;


        }

        public void SetBackground(int evenodd)
        {

            if (evenodd % 2 == 0)
                theGrid.BackgroundColor = Color.FromHex("#FFB0EEB0");
            else
                theGrid.BackgroundColor = Color.LightYellow;
        }
    }
}