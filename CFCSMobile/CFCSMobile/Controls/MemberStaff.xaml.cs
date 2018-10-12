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
	public partial class MemberStaff : Grid
	{
        MemberObservers TheObserver = new MemberObservers();

		public MemberStaff ()
		{
			InitializeComponent ();
		}

        public MemberStaff(MemberObservers ob)
        {
            InitializeComponent();

            theGrid.BackgroundColor = Settings.StaffBackgroundColor;

            TheObserver = ob;
            lblStaff.Text = ob.OBSERVER + " " + ob.CREDENTIALONE.Trim() + " " + ob.CREDENTIALTWO.Trim();
            lblSupervisor.Text = ob.OBSTYPE;
            lblPhone.Text = ob.PHONE;

            lblEmail.TitleFontSize = 10;
            lblEmail.ValueFontSize = 12;
            lblEmail.ValueText = ob.EMAIL;
            lblEmail.TitleText = "Email:";

            //lblEmail.Text = ob.EMAIL;

            lblEntryDate.TitleFontSize = 10;
            lblEntryDate.ValueText = ob.CREATEDATE.ToShortDateString();
            lblEntryDate.TitleText = "Entry:";
            lblEntryDate.ValueFontSize = 12;

            lblStartDate.TitleFontSize = 10;
            lblStartDate.ValueText = ob.strSDATE;
            lblStartDate.TitleText = "Start:";
            lblStartDate.ValueFontSize = 12;

            lblEndDate.TitleFontSize = 10;
            lblEndDate.ValueText = ob.strEDATE;
            lblEndDate.TitleText = "End:";
            lblEndDate.ValueFontSize = 12;
            //lblEntryDate = ob.CREATEDATE.ToShortDateString();

            //lblStartDate.Text = ob.strSDATE;
            //lblEndDate.Text = ob.strEDATE;           
        }
	}
}