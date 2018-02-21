using ERPFramework;
using MetroFramework.Forms;
using System;

namespace ERPManager.Forms
{
    public partial class settingForm : MetroForm
    {
        public enum SettingButton { User, Register, LastUser, Info };
        public event EventHandler<SettingButton> ButtonClick;
        public event EventHandler<ApplicationMenuItem> Open;

        public settingForm()
        {
            InitializeComponent();
        }

        private void settingForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingForm_Leave(object sender, EventArgs e)
        {

        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (Open != null)
                Open(this, new ApplicationMenuItem("Users", new NameSpace("ERPManager.ERPManager.Forms.usersForm"), DocumentType.Document, UserType.Administrator));
            this.Close();
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            if (Open != null)
                Open(this, new ApplicationMenuItem("Users", new NameSpace("ERPFramework.ERPFramework.CounterManager.codesForm"), DocumentType.Document, UserType.Administrator));
        }

        private void btnCounter_Click(object sender, EventArgs e)
        {
            if (Open != null)
                Open(this, new ApplicationMenuItem("Users", new NameSpace("ERPFramework.ERPFramework.CounterManager.counterForm"), DocumentType.Document, UserType.Administrator));
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, SettingButton.Register);
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            if (Open != null)
                Open(this, new ApplicationMenuItem("Preferences", new NameSpace("ERPFramework.ERPFramework.Preferences.PreferenceForm"), DocumentType.Preferences, UserType.Administrator));

        }

        private void btnLastUser_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, SettingButton.LastUser);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, SettingButton.Info);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (Open != null)
                Open(this, new ApplicationMenuItem("ImportExport", new NameSpace("ERPFramework.ERPFramework.Data.ImportExportForm"), DocumentType.Preferences, UserType.Administrator));

        }
    }
}
