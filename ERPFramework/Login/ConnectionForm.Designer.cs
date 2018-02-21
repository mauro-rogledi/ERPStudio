namespace ERPFramework.Login
{
    partial class ConnectionForm
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.wizard1 = new ERPFramework.Controls.Wizard();
            this.wzpProvider = new ERPFramework.Controls.WizardPage();
            this.cbbProvider = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.wpzSqLite = new ERPFramework.Controls.WizardPage();
            this.label13 = new MetroFramework.Controls.MetroLabel();
            this.txtnewLIT = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.txtexistLIT = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.label12 = new MetroFramework.Controls.MetroLabel();
            this.txtPassLIT = new MetroFramework.Controls.MetroTextBox();
            this.rdbnewLIT = new MetroFramework.Controls.MetroRadioButton();
            this.rdbexistLIT = new MetroFramework.Controls.MetroRadioButton();
            this.wzpSqlCompact = new ERPFramework.Controls.WizardPage();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.txtnewCMP = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.txtexistCMP = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.label9 = new MetroFramework.Controls.MetroLabel();
            this.txtpassCMP = new MetroFramework.Controls.MetroTextBox();
            this.rdbNewCMP = new MetroFramework.Controls.MetroRadioButton();
            this.rdbExistCMP = new MetroFramework.Controls.MetroRadioButton();
            this.wzpSqlChoose = new ERPFramework.Controls.WizardPage();
            this.cbbExistSQL = new MetroFramework.Controls.MetroComboBox();
            this.txtNewSQL = new MetroFramework.Controls.MetroTextBox();
            this.rdbNewSQL = new MetroFramework.Controls.MetroRadioButton();
            this.rdbExistSQL = new MetroFramework.Controls.MetroRadioButton();
            this.label8 = new MetroFramework.Controls.MetroLabel();
            this.wzpSqlServer = new ERPFramework.Controls.WizardPage();
            this.label7 = new MetroFramework.Controls.MetroLabel();
            this.txtPass = new MetroFramework.Controls.MetroTextBox();
            this.txtUser = new MetroFramework.Controls.MetroTextBox();
            this.cbbAuthentication = new MetroFramework.Controls.MetroComboBox();
            this.cbbServer = new MetroFramework.Controls.MetroComboBox();
            this.label6 = new MetroFramework.Controls.MetroLabel();
            this.label5 = new MetroFramework.Controls.MetroLabel();
            this.label4 = new MetroFramework.Controls.MetroLabel();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.wizardPage1 = new ERPFramework.Controls.WizardPage();
            this.label10 = new MetroFramework.Controls.MetroLabel();
            this.textFileFolderBrowse1 = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.textFileFolderBrowse2 = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.label11 = new MetroFramework.Controls.MetroLabel();
            this.textBox1 = new MetroFramework.Controls.MetroTextBox();
            this.radioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.radioButton2 = new MetroFramework.Controls.MetroRadioButton();
            this.wizard1.SuspendLayout();
            this.wzpProvider.SuspendLayout();
            this.wpzSqLite.SuspendLayout();
            this.wzpSqlCompact.SuspendLayout();
            this.wzpSqlChoose.SuspendLayout();
            this.wzpSqlServer.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            resources.ApplyResources(this.wizard1, "wizard1");
            this.wizard1.Controls.Add(this.wpzSqLite);
            this.wizard1.Controls.Add(this.wzpSqlCompact);
            this.wizard1.Controls.Add(this.wzpSqlChoose);
            this.wizard1.Controls.Add(this.wzpSqlServer);
            this.wizard1.Controls.Add(this.wzpProvider);
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.Name = "wizard1";
            this.wizard1.Pages.AddRange(new ERPFramework.Controls.WizardPage[] {
            this.wzpProvider,
            this.wzpSqlServer,
            this.wzpSqlChoose,
            this.wzpSqlCompact,
            this.wpzSqLite});
            this.wizard1.BeforeSwitchPages += new ERPFramework.Controls.Wizard.BeforeSwitchPagesEventHandler(this.wizard1_BeforeSwitchPages);
            this.wizard1.AfterSwitchPages += new ERPFramework.Controls.Wizard.AfterSwitchPagesEventHandler(this.wizard1_AfterSwitchPages);
            this.wizard1.Finish += new System.ComponentModel.CancelEventHandler(this.wizard1_Finish);
            // 
            // wzpProvider
            // 
            resources.ApplyResources(this.wzpProvider, "wzpProvider");
            this.wzpProvider.Controls.Add(this.cbbProvider);
            this.wzpProvider.Controls.Add(this.label1);
            this.wzpProvider.Name = "wzpProvider";
            // 
            // cbbProvider
            // 
            resources.ApplyResources(this.cbbProvider, "cbbProvider");
            this.cbbProvider.FormattingEnabled = true;
            this.cbbProvider.Name = "cbbProvider";
            this.cbbProvider.UseSelectable = true;
            this.cbbProvider.UseStyleColors = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label1.Name = "label1";
            this.label1.UseStyleColors = true;
            // 
            // wpzSqLite
            // 
            resources.ApplyResources(this.wpzSqLite, "wpzSqLite");
            this.wpzSqLite.Controls.Add(this.label13);
            this.wpzSqLite.Controls.Add(this.txtnewLIT);
            this.wpzSqLite.Controls.Add(this.txtexistLIT);
            this.wpzSqLite.Controls.Add(this.label12);
            this.wpzSqLite.Controls.Add(this.txtPassLIT);
            this.wpzSqLite.Controls.Add(this.rdbnewLIT);
            this.wpzSqLite.Controls.Add(this.rdbexistLIT);
            this.wpzSqLite.Name = "wpzSqLite";
            this.wpzSqLite.Style = ERPFramework.Controls.WizardPageStyle.OK;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Name = "label13";
            this.label13.UseStyleColors = true;
            // 
            // txtnewLIT
            // 
            resources.ApplyResources(this.txtnewLIT, "txtnewLIT");
            this.txtnewLIT.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Save;
            // 
            // 
            // 
            this.txtnewLIT.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription");
            this.txtnewLIT.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName");
            this.txtnewLIT.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor")));
            this.txtnewLIT.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize")));
            this.txtnewLIT.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode")));
            this.txtnewLIT.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage")));
            this.txtnewLIT.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout")));
            this.txtnewLIT.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock")));
            this.txtnewLIT.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle")));
            this.txtnewLIT.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font")));
            this.txtnewLIT.CustomButton.Image = global::ERPFramework.Properties.Resources.Search16;
            this.txtnewLIT.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign")));
            this.txtnewLIT.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex")));
            this.txtnewLIT.CustomButton.ImageKey = resources.GetString("resource.ImageKey");
            this.txtnewLIT.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txtnewLIT.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txtnewLIT.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize")));
            this.txtnewLIT.CustomButton.Name = "";
            this.txtnewLIT.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft")));
            this.txtnewLIT.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txtnewLIT.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtnewLIT.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txtnewLIT.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign")));
            this.txtnewLIT.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation")));
            this.txtnewLIT.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtnewLIT.CustomButton.UseSelectable = true;
            this.txtnewLIT.Filter = "SQLite files |*.db|All files |*.* ";
            this.txtnewLIT.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtnewLIT.HeaderText = null;
            this.txtnewLIT.Lines = new string[0];
            this.txtnewLIT.MaxLength = 32767;
            this.txtnewLIT.Name = "txtnewLIT";
            this.txtnewLIT.PasswordChar = '\0';
            this.txtnewLIT.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtnewLIT.SelectedText = "";
            this.txtnewLIT.SelectionLength = 0;
            this.txtnewLIT.SelectionStart = 0;
            this.txtnewLIT.ShortcutsEnabled = true;
            this.txtnewLIT.ShowButton = true;
            this.txtnewLIT.ShowNewFolderButton = false;
            this.txtnewLIT.UseSelectable = true;
            this.txtnewLIT.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtnewLIT.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtexistLIT
            // 
            resources.ApplyResources(this.txtexistLIT, "txtexistLIT");
            this.txtexistLIT.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Open;
            // 
            // 
            // 
            this.txtexistLIT.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription1");
            this.txtexistLIT.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName1");
            this.txtexistLIT.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor1")));
            this.txtexistLIT.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize1")));
            this.txtexistLIT.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode1")));
            this.txtexistLIT.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage1")));
            this.txtexistLIT.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout1")));
            this.txtexistLIT.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock1")));
            this.txtexistLIT.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle1")));
            this.txtexistLIT.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font1")));
            this.txtexistLIT.CustomButton.Image = global::ERPFramework.Properties.Resources.Search16;
            this.txtexistLIT.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign1")));
            this.txtexistLIT.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex1")));
            this.txtexistLIT.CustomButton.ImageKey = resources.GetString("resource.ImageKey1");
            this.txtexistLIT.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.txtexistLIT.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.txtexistLIT.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize1")));
            this.txtexistLIT.CustomButton.Name = "";
            this.txtexistLIT.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft1")));
            this.txtexistLIT.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.txtexistLIT.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtexistLIT.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.txtexistLIT.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign1")));
            this.txtexistLIT.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation1")));
            this.txtexistLIT.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtexistLIT.CustomButton.UseSelectable = true;
            this.txtexistLIT.Filter = "SQLite files |*.db|All files |*.* ";
            this.txtexistLIT.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtexistLIT.HeaderText = null;
            this.txtexistLIT.Lines = new string[0];
            this.txtexistLIT.MaxLength = 32767;
            this.txtexistLIT.Name = "txtexistLIT";
            this.txtexistLIT.PasswordChar = '\0';
            this.txtexistLIT.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtexistLIT.SelectedText = "";
            this.txtexistLIT.SelectionLength = 0;
            this.txtexistLIT.SelectionStart = 0;
            this.txtexistLIT.ShortcutsEnabled = true;
            this.txtexistLIT.ShowButton = true;
            this.txtexistLIT.ShowNewFolderButton = false;
            this.txtexistLIT.UseSelectable = true;
            this.txtexistLIT.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtexistLIT.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label12.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label12.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label12.Name = "label12";
            this.label12.UseStyleColors = true;
            // 
            // txtPassLIT
            // 
            resources.ApplyResources(this.txtPassLIT, "txtPassLIT");
            // 
            // 
            // 
            this.txtPassLIT.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription2");
            this.txtPassLIT.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName2");
            this.txtPassLIT.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor2")));
            this.txtPassLIT.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize2")));
            this.txtPassLIT.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode2")));
            this.txtPassLIT.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage2")));
            this.txtPassLIT.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout2")));
            this.txtPassLIT.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock2")));
            this.txtPassLIT.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle2")));
            this.txtPassLIT.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font2")));
            this.txtPassLIT.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txtPassLIT.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign2")));
            this.txtPassLIT.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex2")));
            this.txtPassLIT.CustomButton.ImageKey = resources.GetString("resource.ImageKey2");
            this.txtPassLIT.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode2")));
            this.txtPassLIT.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location2")));
            this.txtPassLIT.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize2")));
            this.txtPassLIT.CustomButton.Name = "";
            this.txtPassLIT.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft2")));
            this.txtPassLIT.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size2")));
            this.txtPassLIT.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPassLIT.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex2")));
            this.txtPassLIT.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign2")));
            this.txtPassLIT.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation2")));
            this.txtPassLIT.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassLIT.CustomButton.UseSelectable = true;
            this.txtPassLIT.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.txtPassLIT.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPassLIT.Lines = new string[0];
            this.txtPassLIT.MaxLength = 32767;
            this.txtPassLIT.Name = "txtPassLIT";
            this.txtPassLIT.PasswordChar = '●';
            this.txtPassLIT.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassLIT.SelectedText = "";
            this.txtPassLIT.SelectionLength = 0;
            this.txtPassLIT.SelectionStart = 0;
            this.txtPassLIT.ShortcutsEnabled = true;
            this.txtPassLIT.UseSelectable = true;
            this.txtPassLIT.UseStyleColors = true;
            this.txtPassLIT.UseSystemPasswordChar = true;
            this.txtPassLIT.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassLIT.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // rdbnewLIT
            // 
            resources.ApplyResources(this.rdbnewLIT, "rdbnewLIT");
            this.rdbnewLIT.Checked = true;
            this.rdbnewLIT.Name = "rdbnewLIT";
            this.rdbnewLIT.TabStop = true;
            this.rdbnewLIT.UseSelectable = true;
            this.rdbnewLIT.UseStyleColors = true;
            // 
            // rdbexistLIT
            // 
            resources.ApplyResources(this.rdbexistLIT, "rdbexistLIT");
            this.rdbexistLIT.Name = "rdbexistLIT";
            this.rdbexistLIT.UseSelectable = true;
            this.rdbexistLIT.UseStyleColors = true;
            this.rdbexistLIT.CheckedChanged += new System.EventHandler(this.rdbExistLIT_CheckedChanged);
            // 
            // wzpSqlCompact
            // 
            resources.ApplyResources(this.wzpSqlCompact, "wzpSqlCompact");
            this.wzpSqlCompact.Controls.Add(this.label2);
            this.wzpSqlCompact.Controls.Add(this.txtnewCMP);
            this.wzpSqlCompact.Controls.Add(this.txtexistCMP);
            this.wzpSqlCompact.Controls.Add(this.label9);
            this.wzpSqlCompact.Controls.Add(this.txtpassCMP);
            this.wzpSqlCompact.Controls.Add(this.rdbNewCMP);
            this.wzpSqlCompact.Controls.Add(this.rdbExistCMP);
            this.wzpSqlCompact.Name = "wzpSqlCompact";
            this.wzpSqlCompact.Style = ERPFramework.Controls.WizardPageStyle.OK;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            this.label2.UseStyleColors = true;
            // 
            // txtnewCMP
            // 
            resources.ApplyResources(this.txtnewCMP, "txtnewCMP");
            this.txtnewCMP.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Save;
            // 
            // 
            // 
            this.txtnewCMP.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription3");
            this.txtnewCMP.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName3");
            this.txtnewCMP.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor3")));
            this.txtnewCMP.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize3")));
            this.txtnewCMP.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode3")));
            this.txtnewCMP.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage3")));
            this.txtnewCMP.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout3")));
            this.txtnewCMP.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock3")));
            this.txtnewCMP.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle3")));
            this.txtnewCMP.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font3")));
            this.txtnewCMP.CustomButton.Image = global::ERPFramework.Properties.Resources.Search16;
            this.txtnewCMP.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign3")));
            this.txtnewCMP.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex3")));
            this.txtnewCMP.CustomButton.ImageKey = resources.GetString("resource.ImageKey3");
            this.txtnewCMP.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode3")));
            this.txtnewCMP.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location3")));
            this.txtnewCMP.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize3")));
            this.txtnewCMP.CustomButton.Name = "";
            this.txtnewCMP.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft3")));
            this.txtnewCMP.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size3")));
            this.txtnewCMP.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtnewCMP.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex3")));
            this.txtnewCMP.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign3")));
            this.txtnewCMP.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation3")));
            this.txtnewCMP.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtnewCMP.CustomButton.UseSelectable = true;
            this.txtnewCMP.Filter = "SQL Compact files |*.sdf|All files |*.* ";
            this.txtnewCMP.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtnewCMP.HeaderText = null;
            this.txtnewCMP.Lines = new string[0];
            this.txtnewCMP.MaxLength = 32767;
            this.txtnewCMP.Name = "txtnewCMP";
            this.txtnewCMP.PasswordChar = '\0';
            this.txtnewCMP.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtnewCMP.SelectedText = "";
            this.txtnewCMP.SelectionLength = 0;
            this.txtnewCMP.SelectionStart = 0;
            this.txtnewCMP.ShortcutsEnabled = true;
            this.txtnewCMP.ShowButton = true;
            this.txtnewCMP.ShowNewFolderButton = false;
            this.txtnewCMP.UseSelectable = true;
            this.txtnewCMP.UseStyleColors = true;
            this.txtnewCMP.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtnewCMP.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtexistCMP
            // 
            resources.ApplyResources(this.txtexistCMP, "txtexistCMP");
            this.txtexistCMP.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Open;
            // 
            // 
            // 
            this.txtexistCMP.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription4");
            this.txtexistCMP.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName4");
            this.txtexistCMP.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor4")));
            this.txtexistCMP.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize4")));
            this.txtexistCMP.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode4")));
            this.txtexistCMP.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage4")));
            this.txtexistCMP.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout4")));
            this.txtexistCMP.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock4")));
            this.txtexistCMP.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle4")));
            this.txtexistCMP.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font4")));
            this.txtexistCMP.CustomButton.Image = global::ERPFramework.Properties.Resources.Search16;
            this.txtexistCMP.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign4")));
            this.txtexistCMP.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex4")));
            this.txtexistCMP.CustomButton.ImageKey = resources.GetString("resource.ImageKey4");
            this.txtexistCMP.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode4")));
            this.txtexistCMP.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location4")));
            this.txtexistCMP.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize4")));
            this.txtexistCMP.CustomButton.Name = "";
            this.txtexistCMP.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft4")));
            this.txtexistCMP.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size4")));
            this.txtexistCMP.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtexistCMP.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex4")));
            this.txtexistCMP.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign4")));
            this.txtexistCMP.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation4")));
            this.txtexistCMP.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtexistCMP.CustomButton.UseSelectable = true;
            this.txtexistCMP.Filter = "SQL Compact files |*.sdf|All files |*.* ";
            this.txtexistCMP.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtexistCMP.HeaderText = null;
            this.txtexistCMP.Lines = new string[0];
            this.txtexistCMP.MaxLength = 32767;
            this.txtexistCMP.Name = "txtexistCMP";
            this.txtexistCMP.PasswordChar = '\0';
            this.txtexistCMP.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtexistCMP.SelectedText = "";
            this.txtexistCMP.SelectionLength = 0;
            this.txtexistCMP.SelectionStart = 0;
            this.txtexistCMP.ShortcutsEnabled = true;
            this.txtexistCMP.ShowButton = true;
            this.txtexistCMP.ShowNewFolderButton = false;
            this.txtexistCMP.UseSelectable = true;
            this.txtexistCMP.UseStyleColors = true;
            this.txtexistCMP.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtexistCMP.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label9.Name = "label9";
            this.label9.UseStyleColors = true;
            // 
            // txtpassCMP
            // 
            resources.ApplyResources(this.txtpassCMP, "txtpassCMP");
            // 
            // 
            // 
            this.txtpassCMP.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription5");
            this.txtpassCMP.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName5");
            this.txtpassCMP.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor5")));
            this.txtpassCMP.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize5")));
            this.txtpassCMP.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode5")));
            this.txtpassCMP.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage5")));
            this.txtpassCMP.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout5")));
            this.txtpassCMP.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock5")));
            this.txtpassCMP.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle5")));
            this.txtpassCMP.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font5")));
            this.txtpassCMP.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.txtpassCMP.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign5")));
            this.txtpassCMP.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex5")));
            this.txtpassCMP.CustomButton.ImageKey = resources.GetString("resource.ImageKey5");
            this.txtpassCMP.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode5")));
            this.txtpassCMP.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location5")));
            this.txtpassCMP.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize5")));
            this.txtpassCMP.CustomButton.Name = "";
            this.txtpassCMP.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft5")));
            this.txtpassCMP.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size5")));
            this.txtpassCMP.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtpassCMP.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex5")));
            this.txtpassCMP.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign5")));
            this.txtpassCMP.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation5")));
            this.txtpassCMP.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtpassCMP.CustomButton.UseSelectable = true;
            this.txtpassCMP.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.txtpassCMP.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtpassCMP.Lines = new string[0];
            this.txtpassCMP.MaxLength = 32767;
            this.txtpassCMP.Name = "txtpassCMP";
            this.txtpassCMP.PasswordChar = '●';
            this.txtpassCMP.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtpassCMP.SelectedText = "";
            this.txtpassCMP.SelectionLength = 0;
            this.txtpassCMP.SelectionStart = 0;
            this.txtpassCMP.ShortcutsEnabled = true;
            this.txtpassCMP.UseSelectable = true;
            this.txtpassCMP.UseStyleColors = true;
            this.txtpassCMP.UseSystemPasswordChar = true;
            this.txtpassCMP.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtpassCMP.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // rdbNewCMP
            // 
            resources.ApplyResources(this.rdbNewCMP, "rdbNewCMP");
            this.rdbNewCMP.Checked = true;
            this.rdbNewCMP.Name = "rdbNewCMP";
            this.rdbNewCMP.TabStop = true;
            this.rdbNewCMP.UseSelectable = true;
            this.rdbNewCMP.UseStyleColors = true;
            // 
            // rdbExistCMP
            // 
            resources.ApplyResources(this.rdbExistCMP, "rdbExistCMP");
            this.rdbExistCMP.Name = "rdbExistCMP";
            this.rdbExistCMP.UseSelectable = true;
            this.rdbExistCMP.UseStyleColors = true;
            this.rdbExistCMP.CheckedChanged += new System.EventHandler(this.rdbExistCMP_CheckedChanged);
            // 
            // wzpSqlChoose
            // 
            resources.ApplyResources(this.wzpSqlChoose, "wzpSqlChoose");
            this.wzpSqlChoose.Controls.Add(this.cbbExistSQL);
            this.wzpSqlChoose.Controls.Add(this.txtNewSQL);
            this.wzpSqlChoose.Controls.Add(this.rdbNewSQL);
            this.wzpSqlChoose.Controls.Add(this.rdbExistSQL);
            this.wzpSqlChoose.Controls.Add(this.label8);
            this.wzpSqlChoose.Name = "wzpSqlChoose";
            this.wzpSqlChoose.Style = ERPFramework.Controls.WizardPageStyle.OK;
            // 
            // cbbExistSQL
            // 
            resources.ApplyResources(this.cbbExistSQL, "cbbExistSQL");
            this.cbbExistSQL.FormattingEnabled = true;
            this.cbbExistSQL.Name = "cbbExistSQL";
            this.cbbExistSQL.UseSelectable = true;
            this.cbbExistSQL.UseStyleColors = true;
            // 
            // txtNewSQL
            // 
            resources.ApplyResources(this.txtNewSQL, "txtNewSQL");
            // 
            // 
            // 
            this.txtNewSQL.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription6");
            this.txtNewSQL.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName6");
            this.txtNewSQL.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor6")));
            this.txtNewSQL.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize6")));
            this.txtNewSQL.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode6")));
            this.txtNewSQL.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage6")));
            this.txtNewSQL.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout6")));
            this.txtNewSQL.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock6")));
            this.txtNewSQL.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle6")));
            this.txtNewSQL.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font6")));
            this.txtNewSQL.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.txtNewSQL.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign6")));
            this.txtNewSQL.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex6")));
            this.txtNewSQL.CustomButton.ImageKey = resources.GetString("resource.ImageKey6");
            this.txtNewSQL.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode6")));
            this.txtNewSQL.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location6")));
            this.txtNewSQL.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize6")));
            this.txtNewSQL.CustomButton.Name = "";
            this.txtNewSQL.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft6")));
            this.txtNewSQL.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size6")));
            this.txtNewSQL.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNewSQL.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex6")));
            this.txtNewSQL.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign6")));
            this.txtNewSQL.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation6")));
            this.txtNewSQL.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNewSQL.CustomButton.UseSelectable = true;
            this.txtNewSQL.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible2")));
            this.txtNewSQL.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtNewSQL.Lines = new string[0];
            this.txtNewSQL.MaxLength = 32767;
            this.txtNewSQL.Name = "txtNewSQL";
            this.txtNewSQL.PasswordChar = '\0';
            this.txtNewSQL.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNewSQL.SelectedText = "";
            this.txtNewSQL.SelectionLength = 0;
            this.txtNewSQL.SelectionStart = 0;
            this.txtNewSQL.ShortcutsEnabled = true;
            this.txtNewSQL.UseSelectable = true;
            this.txtNewSQL.UseStyleColors = true;
            this.txtNewSQL.WaterMark = "Nome database";
            this.txtNewSQL.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNewSQL.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // rdbNewSQL
            // 
            resources.ApplyResources(this.rdbNewSQL, "rdbNewSQL");
            this.rdbNewSQL.Checked = true;
            this.rdbNewSQL.Name = "rdbNewSQL";
            this.rdbNewSQL.TabStop = true;
            this.rdbNewSQL.UseSelectable = true;
            this.rdbNewSQL.UseStyleColors = true;
            // 
            // rdbExistSQL
            // 
            resources.ApplyResources(this.rdbExistSQL, "rdbExistSQL");
            this.rdbExistSQL.Name = "rdbExistSQL";
            this.rdbExistSQL.UseSelectable = true;
            this.rdbExistSQL.UseStyleColors = true;
            this.rdbExistSQL.CheckedChanged += new System.EventHandler(this.rdbExistSQL_CheckedChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Name = "label8";
            this.label8.UseStyleColors = true;
            // 
            // wzpSqlServer
            // 
            resources.ApplyResources(this.wzpSqlServer, "wzpSqlServer");
            this.wzpSqlServer.Controls.Add(this.label7);
            this.wzpSqlServer.Controls.Add(this.txtPass);
            this.wzpSqlServer.Controls.Add(this.txtUser);
            this.wzpSqlServer.Controls.Add(this.cbbAuthentication);
            this.wzpSqlServer.Controls.Add(this.cbbServer);
            this.wzpSqlServer.Controls.Add(this.label6);
            this.wzpSqlServer.Controls.Add(this.label5);
            this.wzpSqlServer.Controls.Add(this.label4);
            this.wzpSqlServer.Controls.Add(this.label3);
            this.wzpSqlServer.Name = "wzpSqlServer";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label7.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label7.Name = "label7";
            this.label7.UseStyleColors = true;
            // 
            // txtPass
            // 
            resources.ApplyResources(this.txtPass, "txtPass");
            // 
            // 
            // 
            this.txtPass.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription7");
            this.txtPass.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName7");
            this.txtPass.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor7")));
            this.txtPass.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize7")));
            this.txtPass.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode7")));
            this.txtPass.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage7")));
            this.txtPass.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout7")));
            this.txtPass.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock7")));
            this.txtPass.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle7")));
            this.txtPass.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font7")));
            this.txtPass.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            this.txtPass.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign7")));
            this.txtPass.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex7")));
            this.txtPass.CustomButton.ImageKey = resources.GetString("resource.ImageKey7");
            this.txtPass.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode7")));
            this.txtPass.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location7")));
            this.txtPass.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize7")));
            this.txtPass.CustomButton.Name = "";
            this.txtPass.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft7")));
            this.txtPass.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size7")));
            this.txtPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPass.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex7")));
            this.txtPass.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign7")));
            this.txtPass.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation7")));
            this.txtPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPass.CustomButton.UseSelectable = true;
            this.txtPass.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible3")));
            this.txtPass.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPass.Lines = new string[0];
            this.txtPass.MaxLength = 32767;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '●';
            this.txtPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPass.SelectedText = "";
            this.txtPass.SelectionLength = 0;
            this.txtPass.SelectionStart = 0;
            this.txtPass.ShortcutsEnabled = true;
            this.txtPass.UseSelectable = true;
            this.txtPass.UseStyleColors = true;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            // 
            // 
            // 
            this.txtUser.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription8");
            this.txtUser.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName8");
            this.txtUser.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor8")));
            this.txtUser.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize8")));
            this.txtUser.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode8")));
            this.txtUser.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage8")));
            this.txtUser.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout8")));
            this.txtUser.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock8")));
            this.txtUser.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle8")));
            this.txtUser.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font8")));
            this.txtUser.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            this.txtUser.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign8")));
            this.txtUser.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex8")));
            this.txtUser.CustomButton.ImageKey = resources.GetString("resource.ImageKey8");
            this.txtUser.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode8")));
            this.txtUser.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location8")));
            this.txtUser.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize8")));
            this.txtUser.CustomButton.Name = "";
            this.txtUser.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft8")));
            this.txtUser.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size8")));
            this.txtUser.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUser.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex8")));
            this.txtUser.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign8")));
            this.txtUser.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation8")));
            this.txtUser.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUser.CustomButton.UseSelectable = true;
            this.txtUser.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible4")));
            this.txtUser.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtUser.Lines = new string[0];
            this.txtUser.MaxLength = 32767;
            this.txtUser.Name = "txtUser";
            this.txtUser.PasswordChar = '\0';
            this.txtUser.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUser.SelectedText = "";
            this.txtUser.SelectionLength = 0;
            this.txtUser.SelectionStart = 0;
            this.txtUser.ShortcutsEnabled = true;
            this.txtUser.UseSelectable = true;
            this.txtUser.UseStyleColors = true;
            this.txtUser.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUser.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // cbbAuthentication
            // 
            resources.ApplyResources(this.cbbAuthentication, "cbbAuthentication");
            this.cbbAuthentication.FormattingEnabled = true;
            this.cbbAuthentication.Items.AddRange(new object[] {
            resources.GetString("cbbAuthentication.Items"),
            resources.GetString("cbbAuthentication.Items1")});
            this.cbbAuthentication.Name = "cbbAuthentication";
            this.cbbAuthentication.UseSelectable = true;
            this.cbbAuthentication.UseStyleColors = true;
            this.cbbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cbbAuthentication_SelectedIndexChanged);
            // 
            // cbbServer
            // 
            resources.ApplyResources(this.cbbServer, "cbbServer");
            this.cbbServer.FormattingEnabled = true;
            this.cbbServer.Name = "cbbServer";
            this.cbbServer.UseSelectable = true;
            this.cbbServer.UseStyleColors = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label6.Name = "label6";
            this.label6.UseStyleColors = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label5.Name = "label5";
            this.label5.UseStyleColors = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label4.Name = "label4";
            this.label4.UseStyleColors = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label3.Name = "label3";
            this.label3.UseStyleColors = true;
            // 
            // wizardPage1
            // 
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.Controls.Add(this.label10);
            this.wizardPage1.Controls.Add(this.textFileFolderBrowse1);
            this.wizardPage1.Controls.Add(this.textFileFolderBrowse2);
            this.wizardPage1.Controls.Add(this.label11);
            this.wizardPage1.Controls.Add(this.textBox1);
            this.wizardPage1.Controls.Add(this.radioButton1);
            this.wizardPage1.Controls.Add(this.radioButton2);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Style = ERPFramework.Controls.WizardPageStyle.OK;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label10.Name = "label10";
            // 
            // textFileFolderBrowse1
            // 
            resources.ApplyResources(this.textFileFolderBrowse1, "textFileFolderBrowse1");
            this.textFileFolderBrowse1.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Save;
            // 
            // 
            // 
            this.textFileFolderBrowse1.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription9");
            this.textFileFolderBrowse1.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName9");
            this.textFileFolderBrowse1.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor9")));
            this.textFileFolderBrowse1.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize9")));
            this.textFileFolderBrowse1.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode9")));
            this.textFileFolderBrowse1.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage9")));
            this.textFileFolderBrowse1.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout9")));
            this.textFileFolderBrowse1.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock9")));
            this.textFileFolderBrowse1.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle9")));
            this.textFileFolderBrowse1.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font9")));
            this.textFileFolderBrowse1.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            this.textFileFolderBrowse1.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign9")));
            this.textFileFolderBrowse1.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex9")));
            this.textFileFolderBrowse1.CustomButton.ImageKey = resources.GetString("resource.ImageKey9");
            this.textFileFolderBrowse1.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode9")));
            this.textFileFolderBrowse1.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location9")));
            this.textFileFolderBrowse1.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize9")));
            this.textFileFolderBrowse1.CustomButton.Name = "";
            this.textFileFolderBrowse1.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft9")));
            this.textFileFolderBrowse1.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size9")));
            this.textFileFolderBrowse1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textFileFolderBrowse1.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex9")));
            this.textFileFolderBrowse1.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign9")));
            this.textFileFolderBrowse1.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation9")));
            this.textFileFolderBrowse1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textFileFolderBrowse1.CustomButton.UseSelectable = true;
            this.textFileFolderBrowse1.Filter = "SQL Compact files |*.sdf|All files |*.* ";
            this.textFileFolderBrowse1.HeaderText = null;
            this.textFileFolderBrowse1.Lines = new string[0];
            this.textFileFolderBrowse1.MaxLength = 32767;
            this.textFileFolderBrowse1.Name = "textFileFolderBrowse1";
            this.textFileFolderBrowse1.PasswordChar = '\0';
            this.textFileFolderBrowse1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textFileFolderBrowse1.SelectedText = "";
            this.textFileFolderBrowse1.SelectionLength = 0;
            this.textFileFolderBrowse1.SelectionStart = 0;
            this.textFileFolderBrowse1.ShortcutsEnabled = true;
            this.textFileFolderBrowse1.ShowButton = true;
            this.textFileFolderBrowse1.ShowNewFolderButton = false;
            this.textFileFolderBrowse1.UseSelectable = true;
            this.textFileFolderBrowse1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textFileFolderBrowse1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // textFileFolderBrowse2
            // 
            resources.ApplyResources(this.textFileFolderBrowse2, "textFileFolderBrowse2");
            this.textFileFolderBrowse2.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Open;
            // 
            // 
            // 
            this.textFileFolderBrowse2.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription10");
            this.textFileFolderBrowse2.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName10");
            this.textFileFolderBrowse2.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor10")));
            this.textFileFolderBrowse2.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize10")));
            this.textFileFolderBrowse2.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode10")));
            this.textFileFolderBrowse2.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage10")));
            this.textFileFolderBrowse2.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout10")));
            this.textFileFolderBrowse2.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock10")));
            this.textFileFolderBrowse2.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle10")));
            this.textFileFolderBrowse2.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font10")));
            this.textFileFolderBrowse2.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image6")));
            this.textFileFolderBrowse2.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign10")));
            this.textFileFolderBrowse2.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex10")));
            this.textFileFolderBrowse2.CustomButton.ImageKey = resources.GetString("resource.ImageKey10");
            this.textFileFolderBrowse2.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode10")));
            this.textFileFolderBrowse2.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location10")));
            this.textFileFolderBrowse2.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize10")));
            this.textFileFolderBrowse2.CustomButton.Name = "";
            this.textFileFolderBrowse2.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft10")));
            this.textFileFolderBrowse2.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size10")));
            this.textFileFolderBrowse2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textFileFolderBrowse2.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex10")));
            this.textFileFolderBrowse2.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign10")));
            this.textFileFolderBrowse2.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation10")));
            this.textFileFolderBrowse2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textFileFolderBrowse2.CustomButton.UseSelectable = true;
            this.textFileFolderBrowse2.Filter = "SQL Compact files |*.sdf|All files |*.* ";
            this.textFileFolderBrowse2.HeaderText = null;
            this.textFileFolderBrowse2.Lines = new string[0];
            this.textFileFolderBrowse2.MaxLength = 32767;
            this.textFileFolderBrowse2.Name = "textFileFolderBrowse2";
            this.textFileFolderBrowse2.PasswordChar = '\0';
            this.textFileFolderBrowse2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textFileFolderBrowse2.SelectedText = "";
            this.textFileFolderBrowse2.SelectionLength = 0;
            this.textFileFolderBrowse2.SelectionStart = 0;
            this.textFileFolderBrowse2.ShortcutsEnabled = true;
            this.textFileFolderBrowse2.ShowButton = true;
            this.textFileFolderBrowse2.ShowNewFolderButton = false;
            this.textFileFolderBrowse2.UseSelectable = true;
            this.textFileFolderBrowse2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textFileFolderBrowse2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            // 
            // 
            // 
            this.textBox1.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription11");
            this.textBox1.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName11");
            this.textBox1.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor11")));
            this.textBox1.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize11")));
            this.textBox1.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode11")));
            this.textBox1.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage11")));
            this.textBox1.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout11")));
            this.textBox1.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock11")));
            this.textBox1.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle11")));
            this.textBox1.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font11")));
            this.textBox1.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image7")));
            this.textBox1.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign11")));
            this.textBox1.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex11")));
            this.textBox1.CustomButton.ImageKey = resources.GetString("resource.ImageKey11");
            this.textBox1.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode11")));
            this.textBox1.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location11")));
            this.textBox1.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize11")));
            this.textBox1.CustomButton.Name = "";
            this.textBox1.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft11")));
            this.textBox1.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size11")));
            this.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox1.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex11")));
            this.textBox1.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign11")));
            this.textBox1.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation11")));
            this.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBox1.CustomButton.UseSelectable = true;
            this.textBox1.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible5")));
            this.textBox1.Lines = new string[0];
            this.textBox1.MaxLength = 32767;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.ShortcutsEnabled = true;
            this.textBox1.UseSelectable = true;
            this.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseSelectable = true;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseSelectable = true;
            // 
            // ConnectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizard1);
            this.Name = "ConnectionForm";
            this.wizard1.ResumeLayout(false);
            this.wzpProvider.ResumeLayout(false);
            this.wzpProvider.PerformLayout();
            this.wpzSqLite.ResumeLayout(false);
            this.wpzSqLite.PerformLayout();
            this.wzpSqlCompact.ResumeLayout(false);
            this.wzpSqlCompact.PerformLayout();
            this.wzpSqlChoose.ResumeLayout(false);
            this.wzpSqlChoose.PerformLayout();
            this.wzpSqlServer.ResumeLayout(false);
            this.wzpSqlServer.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ERPFramework.Controls.Wizard wizard1;
        private ERPFramework.Controls.WizardPage wzpProvider;
        private ERPFramework.Controls.WizardPage wzpSqlCompact;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private ERPFramework.Controls.WizardPage wzpSqlServer;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroComboBox cbbProvider;
        private MetroFramework.Controls.MetroRadioButton rdbNewCMP;
        private MetroFramework.Controls.MetroRadioButton rdbExistCMP;
        private MetroFramework.Controls.MetroLabel label6;
        private MetroFramework.Controls.MetroLabel label5;
        private MetroFramework.Controls.MetroLabel label4;
        private MetroFramework.Controls.MetroLabel label7;
        private MetroFramework.Controls.MetroTextBox txtPass;
        private MetroFramework.Controls.MetroTextBox txtUser;
        private MetroFramework.Controls.MetroComboBox cbbAuthentication;
        private MetroFramework.Controls.MetroComboBox cbbServer;
        private ERPFramework.Controls.WizardPage wzpSqlChoose;
        private MetroFramework.Controls.MetroLabel label8;
        private MetroFramework.Controls.MetroTextBox txtNewSQL;
        private MetroFramework.Controls.MetroRadioButton rdbNewSQL;
        private MetroFramework.Controls.MetroRadioButton rdbExistSQL;
        private MetroFramework.Controls.MetroComboBox cbbExistSQL;
        private MetroFramework.Controls.MetroLabel label9;
        private MetroFramework.Controls.MetroTextBox txtpassCMP;
        private MetroFramework.Extender.MetroTextFileFolderBrowse txtnewCMP;
        private MetroFramework.Extender.MetroTextFileFolderBrowse txtexistCMP;
        private Controls.WizardPage wizardPage1;
        private MetroFramework.Controls.MetroLabel label10;
        private MetroFramework.Extender.MetroTextFileFolderBrowse textFileFolderBrowse1;
        private MetroFramework.Extender.MetroTextFileFolderBrowse textFileFolderBrowse2;
        private MetroFramework.Controls.MetroLabel label11;
        private MetroFramework.Controls.MetroTextBox textBox1;
        private MetroFramework.Controls.MetroRadioButton radioButton1;
        private MetroFramework.Controls.MetroRadioButton radioButton2;
        private Controls.WizardPage wpzSqLite;
        private MetroFramework.Controls.MetroLabel label13;
        private MetroFramework.Extender.MetroTextFileFolderBrowse txtnewLIT;
        private MetroFramework.Extender.MetroTextFileFolderBrowse txtexistLIT;
        private MetroFramework.Controls.MetroLabel label12;
        private MetroFramework.Controls.MetroTextBox txtPassLIT;
        private MetroFramework.Controls.MetroRadioButton rdbnewLIT;
        private MetroFramework.Controls.MetroRadioButton rdbexistLIT;

    }
}