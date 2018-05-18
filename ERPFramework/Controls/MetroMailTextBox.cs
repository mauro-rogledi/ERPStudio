using System;
using System.Windows.Forms;
using MetroFramework;

namespace ERPFramework.Controls
{
    public class MetroMailTextBox : MetroFramework.Controls.MetroTextBox
    {
        public MetroMailTextBox()
            : base()
        {
            TextChanged += MetroMailTextBox_TextChanged;
        }

        private void MetroMailTextBox_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
        }

        private bool isChanged;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CheckIsOK();
            base.OnValidating(e);
        }

        public bool CheckIsOK()
        {
            if (!isChanged || Text.IsEmpty())
                return true;

            int atPos = Text.IndexOf('@');
            if (atPos<0)
            {
                return MetroMessageBox.Show(FindForm(), Properties.Resources.Msg_MailIncorrect + Properties.Resources.Msg_Continue,
                                          Properties.Resources.Warning,
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
            int pointPos = Text.LastIndexOf('.', Text.Length-1);
            if (pointPos < 0 || pointPos<atPos)
            {
                return MetroMessageBox.Show(FindForm(), Properties.Resources.Msg_MailIncorrect + Properties.Resources.Msg_Continue,
                                          Properties.Resources.Warning,
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }


            isChanged = false;
            return true;
        }
    }
}
