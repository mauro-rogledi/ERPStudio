using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using ERPFramework.Emailer;
using ERPFramework.Data;

namespace ERPFramework.Preferences
{
    public partial class MailPreferencePanel : MailPreferencePanelNoVis
    {
        //private Cryption Encription = new Cryption();

        public MailPreferencePanel(string appName)
            : base(appName)
        {
            InitializeComponent();
            ButtonText = Properties.Resources.T_Email;
            ButtonImage = Properties.Resources.Edit24g;
        }

        public MailPreferencePanel()
        {
            InitializeComponent();
            ButtonText = Properties.Resources.T_Email;
            ButtonImage = Properties.Resources.Edit24g;
        }

        protected override void OnBindData()
        {
            //BindControl(txtName, "Name");
            //BindControl(txtEmailAddress, "EmailAddress");
            //BindControl(txtHost, "Host");
            //BindControl(nudPort, "Port", "Value");
            //BindControl(txtUserName, "UserName");
            //BindControl(txtPassword, "Password");

            //BindControl(ckbSSL, "EnableSsl", "Checked");
            //BindControl(ckbException, "Exception", "Checked");

            BindControl(btnTest);
        }

        protected override void OnBeforeEdit()
        {
            txtPassword.Text = Cryption.Decrypt(txtPassword.Text);
        }

        protected override void OnPrepareAuxData()
        {
        }

        protected override bool OnBeforeSave()
        {
            txtPassword.Text = Cryption.Encrypt(txtPassword.Text);
            txtPassword.DataBindings["Text"].WriteValue();

            return base.OnBeforeSave();
        }

        protected override bool OnBeforeAddNew()
        {
            return true;
        }

        protected override void OnValidateControl()
        {
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            EmailSender.FromAddress = txtEmailAddress.Text;
            EmailSender.FromDisplay = txtName.Text;
            EmailSender.Host = txtHost.Text;
            EmailSender.Port = (int)nudPort.Value;
            EmailSender.UserName = txtUserName.Text;
            EmailSender.Password = txtPassword.Text;
            EmailSender.EnableSsl = ckbSSL.Checked;
            EmailSender.UseDefaultCredentials = false;

            EmailSender.ToAddress = txtEmailAddress.Text;

            if (EmailSender.SendMail("Test Email", "Test Email", string.Empty))
                MessageBox.Show("OK", "Send Email");
            else
                MessageBox.Show(EmailSender.Error, "Send Email");
        }
    }

    #region MailPreferencePanelNoVis

    public partial class MailPreferencePanelNoVis : ERPFramework.Preferences.GenericPreferencePanel<MailPref>
    {
        public MailPreferencePanelNoVis(string appName)
            : base(appName, null)
        {
        }

        public MailPreferencePanelNoVis()
        {
        }
    }

    #endregion

    [DataContract]
    public class MailPref
    {
        [DataMember]
        public bool UseDefaultCredentials { get; set; }

        [DataMember]
        public string Host { get; set; }

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool EnableSsl { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public bool Exception { get; set; }

        public MailPref()
        {
        }
    }
}