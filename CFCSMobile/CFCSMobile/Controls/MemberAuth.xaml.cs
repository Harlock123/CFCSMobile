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
            MemberDateRange.Text = s.STARTDATE.ToShortDateString() + " - " + s.ENDDATE.ToShortDateString();

        }

        public void SetBackground(int evenodd)
        {

            if (evenodd % 2 == 0)
                theGrid.BackgroundColor = Color.LightGreen;
            else
                theGrid.BackgroundColor = Color.LightYellow;
        }
    }
}