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
    public partial class MemberEncounterNote : Grid
    {
        public MemberProgressNotes TheNote = new MemberProgressNotes();

        public MemberEncounterNote()
        {
            InitializeComponent();
        }

        public MemberEncounterNote(MemberProgressNotes n)
        {
            InitializeComponent();

            TheNote = n;

            CreateDate.ValueText = n.CREATEDATE.ToShortDateString();

            if (n.CONTACTDATE != Convert.ToDateTime(null))
                ContactDate.ValueText = n.CONTACTDATE.ToShortDateString();

            Author.ValueText = n.AUTHOR;
            ContactType.ValueText = n.NOTECONTACTDESC;

            NarrativeShort.Text = n.NOTATIONSHORT;
            //NarrativeShort.IsEnabled = false;

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