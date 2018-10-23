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
	public partial class MemberReferral : Grid
	{
        MemberReferralSource thereferral = new MemberReferralSource();

		public MemberReferral ()
		{
			InitializeComponent();
		}

        public MemberReferral(MemberReferralSource src)
        {
            InitializeComponent();

            //Should be done in the XAML now
            //theGrid.BackgroundColor = Settings.ReferralBackgroundColor;

            lblAddress.ValueText = src.RSADDRESS1.Trim() + " " + src.RSADDRESS2.Trim() + " " + 
                              src.RSADDRESS3.Trim() + " " + src.RSCITY.Trim() + " " + 
                              src.RSSTATE.Trim() + " " + src.RSZIP.Trim();

            lblAgency.Text = src.AGENCYDESC.Trim();

            lblCellPhone.ValueText = src.RSHOMEPHONE.Trim();
            lblEmail.ValueText = src.RSEMAIL.Trim();
            lblFullName.Text = src.RSFIRSTNAME.Trim() + " " + src.RSLASTNAME.Trim();
            lblRelationship.Text = src.RSROLEDESCRIPTION.Trim();
            lblWorkPhone.ValueText = src.RSWORKPHONE.Trim();

            thereferral = src;
        }
    }
}