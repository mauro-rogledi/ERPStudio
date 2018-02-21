using System;
using System.Windows.Forms;

namespace ERPFramework.RicherTextBox
{
    public partial class ReplaceForm : FindForm
    {
        public new RichTextBox RtbInstance
        {
            set
            {
                base.RtbInstance = value;
            }
            get
            {
                return base.RtbInstance;
            }
        }

        public new string InitialText
        {
            set
            {
                base.InitialText = value;
            }
        }

        public ReplaceForm()
        {
            InitializeComponent();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (RtbInstance.SelectionLength > 0)
            {
                int start = RtbInstance.SelectionStart;
                int len = RtbInstance.SelectionLength;
                RtbInstance.Text = RtbInstance.Text.Remove(start, len);
                RtbInstance.Text = RtbInstance.Text.Insert(start, txtReplace.Text);
                RtbInstance.Focus();
            }
        }
    }
}