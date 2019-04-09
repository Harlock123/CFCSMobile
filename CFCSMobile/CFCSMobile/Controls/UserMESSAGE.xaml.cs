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
    public partial class UserMESSAGE : Grid
    {

        public UserMessage TheMSG = null;

        public UserMESSAGE(UserMessage u)
        {
            InitializeComponent();

            MessageType.ValueText = u.MESSAGETYPE;
            CreateDate.ValueText = u.DATECREATED.ToShortDateString();
            NarrativeShort.Text = u.BODYSHORT;
            MessageSource.ValueText = u.SOURCE;

            TheMSG = u;

        }

        public void SetBackground(int evenodd)
        {

            if (evenodd % 2 == 0)
                theGrid.BackgroundColor = Settings.EvenColor;
            else
                theGrid.BackgroundColor = Settings.OddColor;
        }
    }
}