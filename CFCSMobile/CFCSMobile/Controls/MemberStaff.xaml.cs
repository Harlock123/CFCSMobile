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

            TheObserver = ob;
            lblStaff.Text = ob.OBSERVER + " " + ob.CREDENTIALONE.Trim() + " " + ob.CREDENTIALTWO.Trim();
            lblSupervisor.Text = ob.OBSTYPE;
            lblPhone.Text = ob.PHONE;
            lblEmail.Text = ob.EMAIL;
            lblEntryDate.Text = ob.CREATEDATE.ToShortDateString();
            lblStartDate.Text = ob.strSDATE;
            lblEndDate.Text = ob.strEDATE;           
        }
	}
}