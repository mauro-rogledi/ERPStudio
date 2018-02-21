using System;
using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public partial class PopUpCalendar : MetroFramework.Forms.MetroForm
    {
        private DateTime SelectedDate;

        public delegate void AfterDateSelectedSelectEventHandler(object sender, DateTime SelectedDate);

        public event AfterDateSelectedSelectEventHandler AfterDateSelectEvent;

        public PopUpCalendar(DateTime selectedDate)
        {
            SelectedDate = selectedDate;
            InitializeComponent();
        }

        private void PopUpCalendar_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopUpCalendar_Load(object sender, EventArgs e)
        {
            monthCalendar1.SelectionStart = SelectedDate;
            monthCalendar1.MaxSelectionCount = 1;
            monthCalendar1.Focus();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (AfterDateSelectEvent != null)
                AfterDateSelectEvent(this, monthCalendar1.SelectionStart);
        }
    }
}