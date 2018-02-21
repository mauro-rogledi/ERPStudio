using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ApplicationLibrary.DBManager;
using ApplicationLibrary.ExtendedControls;
using ApplicationLibrary.SqlConnector;
using ApplicationManager.ModuleData;
using ApplicationLibrary;
using ApplicationLibrary.Forms;

namespace ApplicationManager
{
    /// <summary>
    /// Summary description for usersForm.
    /// </summary>
    public partial class usersFormOld : DocumentForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public usersFormOld()
            : base("users")
        {
            InitializeComponent();
            InitializeAuxComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeAuxComponent()
        {
            PrivilegeTypes = new EnumsManager<UserType>(cbbPrivilege);
        }

        protected override void OnAttachData()
        {
            dbManager = new dbUserManagement("userForm", new RadarUsers(), this);
            dbManager.AddMaster(this, AM_Users.Name, true);
            dbManager.OnBeforeRowUpdating += new SqlABRowUpdatingEventHandler(dbManager_OnBeforeRowUpdating);
        }

        protected override void OnBindData()
        {
            BindControl(txtUserName, AM_Users.Username);
            BindControl(txtName, AM_Users.Surname);
            BindControl(txtPass, AM_Users.Password);
            BindControl(cbbPrivilege, AM_Users.UserType, "SelectedValue");
            BindControl(ckbForceChange, AM_Users.ChangePassword, "DBChecked");
            BindControl(ckbBlocked, AM_Users.Blocked, "DBChecked");

            BindControl(dteExpire);
            BindControl(rdbNever);
            BindControl(rdbDate);
            BindControl(btnReset);
        }

        protected override void OnPrepareAuxData()
        {
            base.OnPrepareAuxData();

            if (!IsNew)
            {
                dteExpire.Value = dbManager.GetColumn<DateTime>(AM_Users.ExpireDate, 0);
                rdbNever.Checked = !dbManager.GetColumn<bool>(AM_Users.Expired, 0);
                rdbDate.Checked = dbManager.GetColumn<bool>(AM_Users.Expired, 0);
                txtPass.Text = dbManager.GetColumn<string>(AM_Users.Password, 0).ToString();
            }
        }

        protected override bool OnBeforeAddNew()
        {
            txtPass.Text = string.Empty;
            return base.OnBeforeAddNew();
        }

        protected override bool OnBeforeSave()
        {
            if (this.IsNew)
            {
                txtPass.Text = Cryption.Encrypt(txtPass.Text);
                int x = txtPass.Text.Length;
            }

            //dbManager.SetColumn(UserManagement.ExpireDate, dbManager.Pos, dteExpire.Value);
            return base.OnBeforeSave();
        }

        protected override void OnDisableControlsForEdit()
        {
            ExpireChanged();
            base.OnDisableControlsForEdit();
        }

        protected override void OnDisableControlsForNew()
        {
            ckbForceChange.Checked = true;

            //cbbPrivilege.SelectedValue = (int)UserType.User;
            cbbPrivilege.Text = UserType.User.ToString();
            ExpireChanged();
            base.OnDisableControlsForNew();
        }

        private void ExpireChanged()
        {
            dteExpire.Enabled = rdbDate.Checked;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usersForm));
            this.lblUsername = new MetroFramework.Controls.MetroLabel();
            this.txtUsername = new MetroFramework.Controls.MetroTextBox();
            this.lblName = new MetroFramework.Controls.MetroLabel();
            this.txtName = new MetroFramework.Controls.MetroTextBox();
            this.lblPrivilege = new MetroFramework.Controls.MetroLabel();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            resources.ApplyResources(this.lblUsername, "lblUsername");
            this.lblUsername.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblUsername.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblUsername.Name = "lblUsername";
            // 
            // txtUsername
            // 
            // 
            // 
            // 
            this.txtUsername.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txtUsername.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txtUsername.CustomButton.Name = "";
            this.txtUsername.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txtUsername.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUsername.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txtUsername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUsername.CustomButton.UseSelectable = true;
            this.txtUsername.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.txtUsername.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUsername.Lines = new string[0];
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.MaxLength = 32767;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = '\0';
            this.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUsername.SelectedText = "";
            this.txtUsername.SelectionLength = 0;
            this.txtUsername.SelectionStart = 0;
            this.txtUsername.ShortcutsEnabled = true;
            this.txtUsername.UseSelectable = true;
            this.txtUsername.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUsername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtUsername.Click += new System.EventHandler(this.txtUsername_Click);
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblName.Name = "lblName";
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.txtName.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txtName.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.txtName.CustomButton.Name = "";
            this.txtName.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.txtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtName.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.txtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtName.CustomButton.UseSelectable = true;
            this.txtName.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.txtName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtName.Lines = new string[0];
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.MaxLength = 32767;
            this.txtName.Name = "txtName";
            this.txtName.PasswordChar = '\0';
            this.txtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtName.SelectedText = "";
            this.txtName.SelectionLength = 0;
            this.txtName.SelectionStart = 0;
            this.txtName.ShortcutsEnabled = true;
            this.txtName.UseSelectable = true;
            this.txtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtName.Click += new System.EventHandler(this.txtUsername_Click);
            // 
            // lblPrivilege
            // 
            resources.ApplyResources(this.lblPrivilege, "lblPrivilege");
            this.lblPrivilege.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblPrivilege.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPrivilege.Name = "lblPrivilege";
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.metroComboBox1, "metroComboBox1");
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.UseSelectable = true;
            // 
            // usersForm
            // 
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPrivilege);
            this.Controls.Add(this.lblUsername);
            this.Name = "usersForm";
            this.Controls.SetChildIndex(this.lblUsername, 0);
            this.Controls.SetChildIndex(this.lblPrivilege, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.txtUsername, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.metroComboBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void rdbNever_CheckedChanged(object sender, System.EventArgs e)
        {
            ExpireChanged();
        }

        private void dbManager_OnBeforeRowUpdating(object sender, SqlABRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Delete) return;

            e.Row[AM_Users.Password.Name] = txtPass.Text;
            e.Row[AM_Users.ExpireDate.Name] = dteExpire.Value;
            e.Row[AM_Users.Blocked.Name] = ckbBlocked.DBChecked;
            e.Row[AM_Users.Blocked.Name] = ckbBlocked.DBChecked;
            e.Row[AM_Users.Expired.Name] = rdbDate.DBChecked;
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(
               Properties.Resources.Msg_Password,
               Properties.Resources.Msg_ResetPass,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtPass.Text = Cryption.Encrypt("999999"); ;
                MessageBox.Show(
                       Properties.Resources.Msg_Pass999999,
                       Properties.Resources.Msg_PassChange);
                ckbForceChange.Checked = true;
            }
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Classe derivata per gestioni particolari
    /// </summary>
    internal class dbUserManagement : DBManager
    {
        public dbUserManagement(string name, RadarForm radar, DocumentForm document)
            : base(name, radar, document)
        {
        }

        protected override string CreateMasterQuery()
        {
            //			myTable		 = UserManagement.Name;
            //			myForeignKey = UserManagement.UserName.Name;
            this.Table = new AM_Users();

            return string.Format("SELECT * from {0} WHERE {1} = @p1", AM_Users.Name, AM_Users.Username);
        }

        protected override List<SqlABParameter> CreateMasterParam()
        {
            List<SqlABParameter> PList = new List<SqlABParameter>();

            SqlABParameter sParam = new SqlABParameter("@p1", AM_Users.Username);
            sParam.Value = "";
            PList.Add(sParam);
            return PList;
        }

        protected override void SetParameters(IRadarParameters key, DBCollection collection)
        {
            collection.Parameter[0].Value = key.Params[0];
        }
    }

    #region UserPrivilege

    public class UserPrivilege
    {
        private int myArchive;
        private string mydisplay;

        public UserPrivilege(UserType archive, string display)
        {
            this.myArchive = (int)archive;
            this.mydisplay = display;
        }

        public int Archive { get { return myArchive; } }

        public string Display { get { return mydisplay; } }

        public override string ToString()
        {
            return this.myArchive.ToString();
        }
    }

    #endregion
}