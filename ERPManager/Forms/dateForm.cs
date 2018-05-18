using System;

using ERPFramework;

namespace ERPManager.Forms
{
    public partial class dateForm : ERPFramework.Forms.AskForm
    {
        public dateForm()
        {
            InitializeComponent();
            txtCalendar.Today = GlobalInfo.CurrentDate;
        }

        public override bool OnOk()
        {
            GlobalInfo.CurrentDate = DateTime.Parse(txtCalendar.Text);
            return base.OnOk();
        }

        public override bool OnCancel()
        {
            return base.OnCancel();
        }
    }
}