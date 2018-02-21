namespace ERPFramework.Data
{
    partial class ImportExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExportForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.wizard1 = new ERPFramework.Controls.Wizard();
            this.wzpImport = new ERPFramework.Controls.WizardPage();
            this.ckbClean = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtImport = new MetroFramework.Controls.MetroTextBox();
            this.wzpExport = new ERPFramework.Controls.WizardPage();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtExport = new MetroFramework.Controls.MetroTextBox();
            this.wzpOperation = new ERPFramework.Controls.WizardPage();
            this.cbbProvider = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.wzpResult = new ERPFramework.Controls.WizardPage();
            this.wizardPage1 = new ERPFramework.Controls.WizardPage();
            this.label10 = new MetroFramework.Controls.MetroLabel();
            this.textFileFolderBrowse1 = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.textFileFolderBrowse2 = new MetroFramework.Extender.MetroTextFileFolderBrowse();
            this.label11 = new MetroFramework.Controls.MetroLabel();
            this.textBox1 = new MetroFramework.Controls.MetroTextBox();
            this.radioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.radioButton2 = new MetroFramework.Controls.MetroRadioButton();
            this.lsvResult = new MetroFramework.Controls.MetroListView();
            this.colTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wizard1.SuspendLayout();
            this.wzpImport.SuspendLayout();
            this.wzpExport.SuspendLayout();
            this.wzpOperation.SuspendLayout();
            this.wzpResult.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // wizard1
            // 
            this.wizard1.Controls.Add(this.wzpResult);
            this.wizard1.Controls.Add(this.wzpImport);
            this.wizard1.Controls.Add(this.wzpExport);
            this.wizard1.Controls.Add(this.wzpOperation);
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.wizard1.HeaderFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(20, 60);
            this.wizard1.Margin = new System.Windows.Forms.Padding(2);
            this.wizard1.Name = "wizard1";
            this.wizard1.Pages.AddRange(new ERPFramework.Controls.WizardPage[] {
            this.wzpOperation,
            this.wzpExport,
            this.wzpImport,
            this.wzpResult});
            this.wizard1.Size = new System.Drawing.Size(565, 265);
            this.wizard1.TabIndex = 1;
            this.wizard1.UseSelectable = true;
            this.wizard1.UseStyleColors = true;
            this.wizard1.BeforeSwitchPages += new ERPFramework.Controls.Wizard.BeforeSwitchPagesEventHandler(this.wizard1_BeforeSwitchPages);
            this.wizard1.AfterSwitchPages += new ERPFramework.Controls.Wizard.AfterSwitchPagesEventHandler(this.wizard1_AfterSwitchPages);
            this.wizard1.Finish += new System.ComponentModel.CancelEventHandler(this.wizard1_Finish);
            // 
            // wzpImport
            // 
            this.wzpImport.Controls.Add(this.ckbClean);
            this.wzpImport.Controls.Add(this.metroLabel2);
            this.wzpImport.Controls.Add(this.txtImport);
            this.wzpImport.Location = new System.Drawing.Point(0, 0);
            this.wzpImport.Name = "wzpImport";
            this.wzpImport.Size = new System.Drawing.Size(565, 217);
            this.wzpImport.TabIndex = 18;
            // 
            // ckbClean
            // 
            this.ckbClean.AutoSize = true;
            this.ckbClean.Location = new System.Drawing.Point(0, 97);
            this.ckbClean.Name = "ckbClean";
            this.ckbClean.Size = new System.Drawing.Size(152, 15);
            this.ckbClean.TabIndex = 4;
            this.ckbClean.Text = "Clear data before import";
            this.ckbClean.UseSelectable = true;
            this.ckbClean.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(0, 36);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(172, 15);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Select folder where import data";
            this.metroLabel2.UseStyleColors = true;
            // 
            // txtImport
            // 
            // 
            // 
            // 
            this.txtImport.CustomButton.Image = global::ERPFramework.Properties.Resources.Search16;
            this.txtImport.CustomButton.Location = new System.Drawing.Point(540, 1);
            this.txtImport.CustomButton.Name = "";
            this.txtImport.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtImport.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtImport.CustomButton.TabIndex = 1;
            this.txtImport.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtImport.CustomButton.UseSelectable = true;
            this.txtImport.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtImport.IconRight = true;
            this.txtImport.Lines = new string[0];
            this.txtImport.Location = new System.Drawing.Point(0, 59);
            this.txtImport.MaxLength = 32767;
            this.txtImport.Name = "txtImport";
            this.txtImport.PasswordChar = '\0';
            this.txtImport.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtImport.SelectedText = "";
            this.txtImport.SelectionLength = 0;
            this.txtImport.SelectionStart = 0;
            this.txtImport.ShortcutsEnabled = true;
            this.txtImport.ShowButton = true;
            this.txtImport.ShowClearButton = true;
            this.txtImport.Size = new System.Drawing.Size(562, 23);
            this.txtImport.TabIndex = 2;
            this.txtImport.UseSelectable = true;
            this.txtImport.UseStyleColors = true;
            this.txtImport.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtImport.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtImport.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.txtImport_ButtonClick);
            // 
            // wzpExport
            // 
            this.wzpExport.Controls.Add(this.metroLabel1);
            this.wzpExport.Controls.Add(this.txtExport);
            this.wzpExport.Location = new System.Drawing.Point(0, 0);
            this.wzpExport.Name = "wzpExport";
            this.wzpExport.Size = new System.Drawing.Size(565, 217);
            this.wzpExport.TabIndex = 17;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(0, 36);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(174, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Select folder to export data";
            this.metroLabel1.UseStyleColors = true;
            // 
            // txtExport
            // 
            // 
            // 
            // 
            this.txtExport.CustomButton.Image = global::ERPFramework.Properties.Resources.Search16;
            this.txtExport.CustomButton.Location = new System.Drawing.Point(540, 1);
            this.txtExport.CustomButton.Name = "";
            this.txtExport.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtExport.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtExport.CustomButton.TabIndex = 1;
            this.txtExport.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtExport.CustomButton.UseSelectable = true;
            this.txtExport.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtExport.IconRight = true;
            this.txtExport.Lines = new string[0];
            this.txtExport.Location = new System.Drawing.Point(0, 59);
            this.txtExport.MaxLength = 32767;
            this.txtExport.Name = "txtExport";
            this.txtExport.PasswordChar = '\0';
            this.txtExport.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtExport.SelectedText = "";
            this.txtExport.SelectionLength = 0;
            this.txtExport.SelectionStart = 0;
            this.txtExport.ShortcutsEnabled = true;
            this.txtExport.ShowButton = true;
            this.txtExport.ShowClearButton = true;
            this.txtExport.Size = new System.Drawing.Size(562, 23);
            this.txtExport.TabIndex = 0;
            this.txtExport.UseSelectable = true;
            this.txtExport.UseStyleColors = true;
            this.txtExport.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtExport.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtExport.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.txtExport_ButtonClick);
            // 
            // wzpOperation
            // 
            this.wzpOperation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.wzpOperation.Controls.Add(this.cbbProvider);
            this.wzpOperation.Controls.Add(this.label1);
            this.wzpOperation.Location = new System.Drawing.Point(0, 0);
            this.wzpOperation.Margin = new System.Windows.Forms.Padding(2);
            this.wzpOperation.Name = "wzpOperation";
            this.wzpOperation.Size = new System.Drawing.Size(565, 217);
            this.wzpOperation.TabIndex = 11;
            this.wzpOperation.Title = "Select Operation";
            // 
            // cbbProvider
            // 
            this.cbbProvider.FormattingEnabled = true;
            this.cbbProvider.ItemHeight = 23;
            this.cbbProvider.Items.AddRange(new object[] {
            "Import",
            "Export"});
            this.cbbProvider.Location = new System.Drawing.Point(108, 43);
            this.cbbProvider.Margin = new System.Windows.Forms.Padding(2);
            this.cbbProvider.Name = "cbbProvider";
            this.cbbProvider.Size = new System.Drawing.Size(175, 29);
            this.cbbProvider.TabIndex = 3;
            this.cbbProvider.UseSelectable = true;
            this.cbbProvider.UseStyleColors = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label1.Location = new System.Drawing.Point(10, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Operation";
            this.label1.UseStyleColors = true;
            // 
            // wzpResult
            // 
            this.wzpResult.Controls.Add(this.lsvResult);
            this.wzpResult.Location = new System.Drawing.Point(0, 0);
            this.wzpResult.Name = "wzpResult";
            this.wzpResult.Size = new System.Drawing.Size(565, 217);
            this.wzpResult.Style = ERPFramework.Controls.WizardPageStyle.OK;
            this.wzpResult.TabIndex = 19;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label10);
            this.wizardPage1.Controls.Add(this.textFileFolderBrowse1);
            this.wizardPage1.Controls.Add(this.textFileFolderBrowse2);
            this.wizardPage1.Controls.Add(this.label11);
            this.wizardPage1.Controls.Add(this.textBox1);
            this.wizardPage1.Controls.Add(this.radioButton1);
            this.wizardPage1.Controls.Add(this.radioButton2);
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(200, 100);
            this.wizardPage1.Style = ERPFramework.Controls.WizardPageStyle.OK;
            this.wizardPage1.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label10.Location = new System.Drawing.Point(10, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 0;
            // 
            // textFileFolderBrowse1
            // 
            this.textFileFolderBrowse1.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Save;
            // 
            // 
            // 
            this.textFileFolderBrowse1.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.textFileFolderBrowse1.CustomButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textFileFolderBrowse1.CustomButton.Location = new System.Drawing.Point(202, 2);
            this.textFileFolderBrowse1.CustomButton.Name = "";
            this.textFileFolderBrowse1.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.textFileFolderBrowse1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textFileFolderBrowse1.CustomButton.TabIndex = 1;
            this.textFileFolderBrowse1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textFileFolderBrowse1.CustomButton.UseSelectable = true;
            this.textFileFolderBrowse1.Filter = "SQL Compact files |*.sdf|All files |*.* ";
            this.textFileFolderBrowse1.HeaderText = null;
            this.textFileFolderBrowse1.Lines = new string[0];
            this.textFileFolderBrowse1.Location = new System.Drawing.Point(215, 82);
            this.textFileFolderBrowse1.MaxLength = 32767;
            this.textFileFolderBrowse1.Name = "textFileFolderBrowse1";
            this.textFileFolderBrowse1.PasswordChar = '\0';
            this.textFileFolderBrowse1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textFileFolderBrowse1.SelectedText = "";
            this.textFileFolderBrowse1.SelectionLength = 0;
            this.textFileFolderBrowse1.SelectionStart = 0;
            this.textFileFolderBrowse1.ShortcutsEnabled = true;
            this.textFileFolderBrowse1.ShowButton = true;
            this.textFileFolderBrowse1.Size = new System.Drawing.Size(222, 22);
            this.textFileFolderBrowse1.TabIndex = 10;
            this.textFileFolderBrowse1.UseSelectable = true;
            this.textFileFolderBrowse1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textFileFolderBrowse1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // textFileFolderBrowse2
            // 
            this.textFileFolderBrowse2.BrowseMode = MetroFramework.Extender.MetroTextFileFolderBrowse.BrowseDialog.Open;
            // 
            // 
            // 
            this.textFileFolderBrowse2.CustomButton.Image = null;
            this.textFileFolderBrowse2.CustomButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textFileFolderBrowse2.CustomButton.Location = new System.Drawing.Point(202, 2);
            this.textFileFolderBrowse2.CustomButton.Name = "";
            this.textFileFolderBrowse2.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.textFileFolderBrowse2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textFileFolderBrowse2.CustomButton.TabIndex = 1;
            this.textFileFolderBrowse2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textFileFolderBrowse2.CustomButton.UseSelectable = true;
            this.textFileFolderBrowse2.Filter = "SQL Compact files |*.sdf|All files |*.* ";
            this.textFileFolderBrowse2.HeaderText = null;
            this.textFileFolderBrowse2.Lines = new string[0];
            this.textFileFolderBrowse2.Location = new System.Drawing.Point(215, 50);
            this.textFileFolderBrowse2.MaxLength = 32767;
            this.textFileFolderBrowse2.Name = "textFileFolderBrowse2";
            this.textFileFolderBrowse2.PasswordChar = '\0';
            this.textFileFolderBrowse2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textFileFolderBrowse2.SelectedText = "";
            this.textFileFolderBrowse2.SelectionLength = 0;
            this.textFileFolderBrowse2.SelectionStart = 0;
            this.textFileFolderBrowse2.ShortcutsEnabled = true;
            this.textFileFolderBrowse2.ShowButton = true;
            this.textFileFolderBrowse2.Size = new System.Drawing.Size(222, 22);
            this.textFileFolderBrowse2.TabIndex = 9;
            this.textFileFolderBrowse2.UseSelectable = true;
            this.textFileFolderBrowse2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textFileFolderBrowse2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(21, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 8;
            // 
            // textBox1
            // 
            // 
            // 
            // 
            this.textBox1.CustomButton.Image = null;
            this.textBox1.CustomButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox1.CustomButton.Location = new System.Drawing.Point(80, 1);
            this.textBox1.CustomButton.Name = "";
            this.textBox1.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox1.CustomButton.TabIndex = 1;
            this.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBox1.CustomButton.UseSelectable = true;
            this.textBox1.CustomButton.Visible = false;
            this.textBox1.Lines = new string[0];
            this.textBox1.Location = new System.Drawing.Point(215, 112);
            this.textBox1.MaxLength = 32767;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.ShortcutsEnabled = true;
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 7;
            this.textBox1.UseSelectable = true;
            this.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 87);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(104, 24);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseSelectable = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(24, 58);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(104, 24);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.UseSelectable = true;
            // 
            // lsvResult
            // 
            this.lsvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTable,
            this.colRow});
            this.lsvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvResult.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lsvResult.FullRowSelect = true;
            this.lsvResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvResult.Location = new System.Drawing.Point(0, 0);
            this.lsvResult.MultiSelect = false;
            this.lsvResult.Name = "lsvResult";
            this.lsvResult.OwnerDraw = true;
            this.lsvResult.Size = new System.Drawing.Size(565, 217);
            this.lsvResult.TabIndex = 0;
            this.lsvResult.UseCompatibleStateImageBehavior = false;
            this.lsvResult.UseSelectable = true;
            this.lsvResult.UseStyleColors = true;
            this.lsvResult.View = System.Windows.Forms.View.Details;
            // 
            // colTable
            // 
            this.colTable.Text = "Tablename";
            this.colTable.Width = 241;
            // 
            // colRow
            // 
            this.colRow.Text = "Row Exported";
            this.colRow.Width = 173;
            // 
            // ImportExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 345);
            this.Controls.Add(this.wizard1);
            this.Name = "ImportExportForm";
            this.Text = "Import and Export Data";
            this.wizard1.ResumeLayout(false);
            this.wzpImport.ResumeLayout(false);
            this.wzpImport.PerformLayout();
            this.wzpExport.ResumeLayout(false);
            this.wzpExport.PerformLayout();
            this.wzpOperation.ResumeLayout(false);
            this.wzpOperation.PerformLayout();
            this.wzpResult.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Wizard wizard1;
        private Controls.WizardPage wzpOperation;
        private MetroFramework.Controls.MetroComboBox cbbProvider;
        private MetroFramework.Controls.MetroLabel label1;
        private Controls.WizardPage wzpExport;
        private Controls.WizardPage wizardPage1;
        private MetroFramework.Controls.MetroLabel label10;
        private MetroFramework.Extender.MetroTextFileFolderBrowse textFileFolderBrowse1;
        private MetroFramework.Extender.MetroTextFileFolderBrowse textFileFolderBrowse2;
        private MetroFramework.Controls.MetroLabel label11;
        private MetroFramework.Controls.MetroTextBox textBox1;
        private MetroFramework.Controls.MetroRadioButton radioButton1;
        private MetroFramework.Controls.MetroRadioButton radioButton2;
        private MetroFramework.Controls.MetroTextBox txtExport;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private Controls.WizardPage wzpImport;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtImport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private Controls.WizardPage wzpResult;
        private MetroFramework.Controls.MetroCheckBox ckbClean;
        private MetroFramework.Controls.MetroListView lsvResult;
        private System.Windows.Forms.ColumnHeader colTable;
        private System.Windows.Forms.ColumnHeader colRow;
    }
}