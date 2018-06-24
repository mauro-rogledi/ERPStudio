namespace ERPManager.Forms
{
    partial class usersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
                password.Dispose();

            base.Dispose(disposing);
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
            this.txtUserName = new MetroFramework.Controls.MetroTextBox();
            this.lblName = new MetroFramework.Controls.MetroLabel();
            this.txtName = new MetroFramework.Controls.MetroTextBox();
            this.lblPrivilege = new MetroFramework.Controls.MetroLabel();
            this.cbbPrivilege = new MetroFramework.Controls.MetroComboBox();
            this.rdbNever = new MetroFramework.Controls.MetroRadioButton();
            this.rdbDate = new MetroFramework.Controls.MetroRadioButton();
            this.dtbExpire = new ERPFramework.Controls.DateTextBox();
            this.mtgForceChange = new MetroFramework.Controls.MetroToggle();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mtgBlocked = new MetroFramework.Controls.MetroToggle();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.mcpUser = new MetroFramework.Extender.MetroCollapsiblePanel();
            this.flyPanel = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.mcpStatus = new MetroFramework.Extender.MetroCollapsiblePanel();
            this.mcpPassword = new MetroFramework.Extender.MetroCollapsiblePanel();
            this.mcpUser.SuspendLayout();
            this.flyPanel.SuspendLayout();
            this.mcpStatus.SuspendLayout();
            this.mcpPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            resources.ApplyResources(this.lblUsername, "lblUsername");
            this.lblUsername.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblUsername.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.UseStyleColors = true;
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            // 
            // 
            // 
            this.txtUserName.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription");
            this.txtUserName.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName");
            this.txtUserName.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor")));
            this.txtUserName.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize")));
            this.txtUserName.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode")));
            this.txtUserName.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage")));
            this.txtUserName.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout")));
            this.txtUserName.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock")));
            this.txtUserName.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle")));
            this.txtUserName.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font")));
            this.txtUserName.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txtUserName.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign")));
            this.txtUserName.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex")));
            this.txtUserName.CustomButton.ImageKey = resources.GetString("resource.ImageKey");
            this.txtUserName.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txtUserName.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txtUserName.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize")));
            this.txtUserName.CustomButton.Name = "";
            this.txtUserName.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft")));
            this.txtUserName.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txtUserName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserName.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txtUserName.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign")));
            this.txtUserName.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation")));
            this.txtUserName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserName.CustomButton.UseSelectable = true;
            this.txtUserName.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.txtUserName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUserName.Lines = new string[0];
            this.txtUserName.MaxLength = 32767;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.ShortcutsEnabled = true;
            this.txtUserName.UseSelectable = true;
            this.txtUserName.UseStyleColors = true;
            this.txtUserName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblName.Name = "lblName";
            this.lblName.UseStyleColors = true;
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            // 
            // 
            // 
            this.txtName.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription1");
            this.txtName.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName1");
            this.txtName.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor1")));
            this.txtName.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize1")));
            this.txtName.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode1")));
            this.txtName.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage1")));
            this.txtName.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout1")));
            this.txtName.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock1")));
            this.txtName.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle1")));
            this.txtName.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font1")));
            this.txtName.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.txtName.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign1")));
            this.txtName.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex1")));
            this.txtName.CustomButton.ImageKey = resources.GetString("resource.ImageKey1");
            this.txtName.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.txtName.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.txtName.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize1")));
            this.txtName.CustomButton.Name = "";
            this.txtName.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft1")));
            this.txtName.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.txtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtName.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.txtName.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign1")));
            this.txtName.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation1")));
            this.txtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtName.CustomButton.UseSelectable = true;
            this.txtName.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.txtName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtName.Lines = new string[0];
            this.txtName.MaxLength = 32767;
            this.txtName.Name = "txtName";
            this.txtName.PasswordChar = '\0';
            this.txtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtName.SelectedText = "";
            this.txtName.SelectionLength = 0;
            this.txtName.SelectionStart = 0;
            this.txtName.ShortcutsEnabled = true;
            this.txtName.UseSelectable = true;
            this.txtName.UseStyleColors = true;
            this.txtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lblPrivilege
            // 
            resources.ApplyResources(this.lblPrivilege, "lblPrivilege");
            this.lblPrivilege.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblPrivilege.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPrivilege.Name = "lblPrivilege";
            this.lblPrivilege.UseStyleColors = true;
            // 
            // cbbPrivilege
            // 
            resources.ApplyResources(this.cbbPrivilege, "cbbPrivilege");
            this.cbbPrivilege.FormattingEnabled = true;
            this.cbbPrivilege.Name = "cbbPrivilege";
            this.cbbPrivilege.UseSelectable = true;
            this.cbbPrivilege.UseStyleColors = true;
            // 
            // rdbNever
            // 
            resources.ApplyResources(this.rdbNever, "rdbNever");
            this.rdbNever.Checked = true;
            this.rdbNever.Name = "rdbNever";
            this.rdbNever.TabStop = true;
            this.rdbNever.UseSelectable = true;
            this.rdbNever.UseStyleColors = true;
            // 
            // rdbDate
            // 
            resources.ApplyResources(this.rdbDate, "rdbDate");
            this.rdbDate.Name = "rdbDate";
            this.rdbDate.UseSelectable = true;
            this.rdbDate.UseStyleColors = true;
            // 
            // dtbExpire
            // 
            resources.ApplyResources(this.dtbExpire, "dtbExpire");
            // 
            // 
            // 
            this.dtbExpire.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription2");
            this.dtbExpire.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName2");
            this.dtbExpire.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor2")));
            this.dtbExpire.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize2")));
            this.dtbExpire.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode2")));
            this.dtbExpire.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage2")));
            this.dtbExpire.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout2")));
            this.dtbExpire.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock2")));
            this.dtbExpire.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle2")));
            this.dtbExpire.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font2")));
            this.dtbExpire.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.dtbExpire.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign2")));
            this.dtbExpire.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex2")));
            this.dtbExpire.CustomButton.ImageKey = resources.GetString("resource.ImageKey2");
            this.dtbExpire.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode2")));
            this.dtbExpire.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location2")));
            this.dtbExpire.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize2")));
            this.dtbExpire.CustomButton.Name = "";
            this.dtbExpire.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft2")));
            this.dtbExpire.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size2")));
            this.dtbExpire.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.dtbExpire.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex2")));
            this.dtbExpire.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign2")));
            this.dtbExpire.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation2")));
            this.dtbExpire.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtbExpire.CustomButton.UseSelectable = true;
            this.dtbExpire.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.dtbExpire.GroupSeparator = '/';
            this.dtbExpire.MaxLength = 32767;
            this.dtbExpire.Name = "dtbExpire";
            this.dtbExpire.PasswordChar = '\0';
            this.dtbExpire.RangeMin = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.dtbExpire.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dtbExpire.SelectedText = "";
            this.dtbExpire.SelectionLength = 0;
            this.dtbExpire.SelectionStart = 0;
            this.dtbExpire.ShortcutsEnabled = true;
            this.dtbExpire.ShowButton = true;
            this.dtbExpire.Today = new System.DateTime(1799, 12, 31, 0, 0, 0, 0);
            this.dtbExpire.UseSelectable = true;
            this.dtbExpire.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.dtbExpire.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mtgForceChange
            // 
            resources.ApplyResources(this.mtgForceChange, "mtgForceChange");
            this.mtgForceChange.DisplayStatus = false;
            this.mtgForceChange.Name = "mtgForceChange";
            this.mtgForceChange.UseSelectable = true;
            this.mtgForceChange.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            resources.ApplyResources(this.metroLabel2, "metroLabel2");
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.UseStyleColors = true;
            // 
            // mtgBlocked
            // 
            resources.ApplyResources(this.mtgBlocked, "mtgBlocked");
            this.mtgBlocked.DisplayStatus = false;
            this.mtgBlocked.Name = "mtgBlocked";
            this.mtgBlocked.UseSelectable = true;
            this.mtgBlocked.UseStyleColors = true;
            // 
            // metroLabel3
            // 
            resources.ApplyResources(this.metroLabel3, "metroLabel3");
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.UseStyleColors = true;
            // 
            // mcpUser
            // 
            resources.ApplyResources(this.mcpUser, "mcpUser");
            this.mcpUser.Controls.Add(this.lblUsername);
            this.mcpUser.Controls.Add(this.txtUserName);
            this.mcpUser.Controls.Add(this.lblName);
            this.mcpUser.Controls.Add(this.txtName);
            this.mcpUser.FontSize = MetroFramework.MetroLabelSize.Small;
            this.mcpUser.HorizontalScrollbarBarColor = true;
            this.mcpUser.HorizontalScrollbarHighlightOnWheel = false;
            this.mcpUser.HorizontalScrollbarSize = 10;
            this.mcpUser.Name = "mcpUser";
            this.mcpUser.ShowLabel = true;
            this.mcpUser.VerticalScrollbarBarColor = true;
            this.mcpUser.VerticalScrollbarHighlightOnWheel = false;
            this.mcpUser.VerticalScrollbarSize = 10;
            // 
            // flyPanel
            // 
            resources.ApplyResources(this.flyPanel, "flyPanel");
            this.flyPanel.Controls.Add(this.mcpUser);
            this.flyPanel.Controls.Add(this.mcpStatus);
            this.flyPanel.Controls.Add(this.mcpPassword);
            this.flyPanel.Name = "flyPanel";
            // 
            // mcpStatus
            // 
            resources.ApplyResources(this.mcpStatus, "mcpStatus");
            this.mcpStatus.Controls.Add(this.mtgForceChange);
            this.mcpStatus.Controls.Add(this.mtgBlocked);
            this.mcpStatus.Controls.Add(this.metroLabel3);
            this.mcpStatus.Controls.Add(this.metroLabel2);
            this.mcpStatus.Controls.Add(this.cbbPrivilege);
            this.mcpStatus.Controls.Add(this.lblPrivilege);
            this.mcpStatus.FontSize = MetroFramework.MetroLabelSize.Small;
            this.mcpStatus.HorizontalScrollbarBarColor = true;
            this.mcpStatus.HorizontalScrollbarHighlightOnWheel = false;
            this.mcpStatus.HorizontalScrollbarSize = 10;
            this.mcpStatus.Name = "mcpStatus";
            this.mcpStatus.ShowLabel = true;
            this.mcpStatus.VerticalScrollbarBarColor = true;
            this.mcpStatus.VerticalScrollbarHighlightOnWheel = false;
            this.mcpStatus.VerticalScrollbarSize = 10;
            // 
            // mcpPassword
            // 
            resources.ApplyResources(this.mcpPassword, "mcpPassword");
            this.mcpPassword.Controls.Add(this.dtbExpire);
            this.mcpPassword.Controls.Add(this.rdbDate);
            this.mcpPassword.Controls.Add(this.rdbNever);
            this.mcpPassword.FontSize = MetroFramework.MetroLabelSize.Small;
            this.mcpPassword.HorizontalScrollbarBarColor = true;
            this.mcpPassword.HorizontalScrollbarHighlightOnWheel = false;
            this.mcpPassword.HorizontalScrollbarSize = 10;
            this.mcpPassword.Name = "mcpPassword";
            this.mcpPassword.ShowLabel = true;
            this.mcpPassword.VerticalScrollbarBarColor = true;
            this.mcpPassword.VerticalScrollbarHighlightOnWheel = false;
            this.mcpPassword.VerticalScrollbarSize = 10;
            // 
            // usersForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.flyPanel);
            this.Name = "usersForm";
            this.Controls.SetChildIndex(this.flyPanel, 0);
            this.mcpUser.ResumeLayout(false);
            this.mcpUser.PerformLayout();
            this.flyPanel.ResumeLayout(false);
            this.mcpStatus.ResumeLayout(false);
            this.mcpStatus.PerformLayout();
            this.mcpPassword.ResumeLayout(false);
            this.mcpPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel lblUsername;
        private MetroFramework.Controls.MetroTextBox txtUserName;
        private MetroFramework.Controls.MetroLabel lblName;
        private MetroFramework.Controls.MetroLabel lblPrivilege;
        private MetroFramework.Controls.MetroComboBox cbbPrivilege;
        private MetroFramework.Controls.MetroTextBox txtName;
        private MetroFramework.Controls.MetroRadioButton rdbNever;
        private MetroFramework.Controls.MetroRadioButton rdbDate;
        private ERPFramework.Controls.DateTextBox dtbExpire;
        private MetroFramework.Controls.MetroToggle mtgForceChange;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroToggle mtgBlocked;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Extender.MetroCollapsiblePanel mcpUser;
        private MetroFramework.Extender.MetroFlowLayoutPanel flyPanel;
        private MetroFramework.Extender.MetroCollapsiblePanel mcpStatus;
        private MetroFramework.Extender.MetroCollapsiblePanel mcpPassword;
    }
}