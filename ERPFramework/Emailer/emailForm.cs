using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ERPFramework.Emailer
{
    public partial class emailForm : MetroFramework.Forms.MetroForm
    {
        public string Address
        {
            get { return txtAddress.Text; }
            set { txtAddress.Text = value; }
        }

        public string Subject
        {
            get { return txtSubject.Text; }
            set { txtSubject.Text = value; }
        }

        public string Body
        {
            get { return rtfBody.Text; }
            set { rtfBody.Text = value; }
        }

        public string Attachment
        {
            get { return txtAttachment.Text; }
            set
            {
                txtAttachment.Text = value;
                pbxAttachment.Enabled = !string.IsNullOrEmpty(value);
            }
        }

        public delegate void LoadAddressEventHandler(emailAddress ea);

        public event LoadAddressEventHandler LoadAddress;

        public emailForm()
        {
            InitializeComponent();
        }

        private void btnLoadAddress_Click(object sender, EventArgs e)
        {
            if (LoadAddress != null)
            {
                emailAddress ea = new emailAddress();
                LoadAddress(ea);
                if (ea.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Address = ea.Address;

                ea.Close();
            }
        }

        private void pbxAttachment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Attachment))
                return;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Attachment;
            Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
        }

        private void tsbSend_Click(object sender, EventArgs e)
        {
            EmailSender.ReadDataClient();
            EmailSender.ToAddress = Address;
            if (!EmailSender.SendMail(Subject, Body, Attachment))
                MessageBox.Show(EmailSender.Error, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}