namespace Serializer
{
    partial class registerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registerForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.txtLicense = new MetroFramework.Controls.MetroTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.txtMac = new MetroFramework.Controls.MetroTextBox();
            this.DataGridView1 = new MetroFramework.Controls.MetroGrid();
            this.colEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colLicenseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiration = new ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn();
            this.colSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPenDrive = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.btnFindPen = new MetroFramework.Controls.MetroButton();
            this.metroToolbar1 = new MetroFramework.Extender.MetroToolbar();
            this.mtbLoad = new MetroFramework.Extender.MetroToolbarPushButton();
            this.mtbCreate = new MetroFramework.Extender.MetroToolbarPushButton();
            this.metroToolbarSeparator1 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnOk = new MetroFramework.Extender.MetroToolbarPushButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.metroToolbar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label1.Name = "label1";
            this.label1.UseStyleColors = true;
            // 
            // txtLicense
            // 
            // 
            // 
            // 
            this.txtLicense.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txtLicense.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txtLicense.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txtLicense.CustomButton.Name = "";
            this.txtLicense.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txtLicense.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtLicense.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txtLicense.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtLicense.CustomButton.UseSelectable = true;
            this.txtLicense.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.txtLicense.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtLicense.Lines = new string[0];
            resources.ApplyResources(this.txtLicense, "txtLicense");
            this.txtLicense.MaxLength = 32767;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.PasswordChar = '\0';
            this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLicense.SelectedText = "";
            this.txtLicense.SelectionLength = 0;
            this.txtLicense.SelectionStart = 0;
            this.txtLicense.ShortcutsEnabled = true;
            this.txtLicense.UseSelectable = true;
            this.txtLicense.UseStyleColors = true;
            this.txtLicense.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtLicense.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Serializer.Properties.Resources.Emerald;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label3.Name = "label3";
            this.label3.UseStyleColors = true;
            // 
            // txtMac
            // 
            // 
            // 
            // 
            this.txtMac.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.txtMac.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.txtMac.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.txtMac.CustomButton.Name = "";
            this.txtMac.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.txtMac.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMac.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.txtMac.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMac.CustomButton.UseSelectable = true;
            this.txtMac.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.txtMac.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtMac.Lines = new string[0];
            resources.ApplyResources(this.txtMac, "txtMac");
            this.txtMac.MaxLength = 32767;
            this.txtMac.Name = "txtMac";
            this.txtMac.PasswordChar = '\0';
            this.txtMac.ReadOnly = true;
            this.txtMac.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMac.SelectedText = "";
            this.txtMac.SelectionLength = 0;
            this.txtMac.SelectionStart = 0;
            this.txtMac.ShortcutsEnabled = true;
            this.txtMac.UseSelectable = true;
            this.txtMac.UseStyleColors = true;
            this.txtMac.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMac.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(226)))), ((int)(((byte)(255)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEnable,
            this.colLicenseType,
            this.colApplication,
            this.colModuleName,
            this.colExpiration,
            this.colSerial});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.EnableHeadersVisualStyles = false;
            resources.ApplyResources(this.DataGridView1, "DataGridView1");
            this.DataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.DataGridView1.RowHeadersVisible = false;
            this.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.DataGridView1.UseStyleColors = true;
            // 
            // colEnable
            // 
            this.colEnable.FillWeight = 60F;
            this.colEnable.Frozen = true;
            resources.ApplyResources(this.colEnable, "colEnable");
            this.colEnable.Name = "colEnable";
            // 
            // colLicenseType
            // 
            resources.ApplyResources(this.colLicenseType, "colLicenseType");
            this.colLicenseType.Name = "colLicenseType";
            this.colLicenseType.ReadOnly = true;
            this.colLicenseType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLicenseType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colApplication
            // 
            resources.ApplyResources(this.colApplication, "colApplication");
            this.colApplication.Name = "colApplication";
            this.colApplication.ReadOnly = true;
            // 
            // colModuleName
            // 
            resources.ApplyResources(this.colModuleName, "colModuleName");
            this.colModuleName.Name = "colModuleName";
            this.colModuleName.ReadOnly = true;
            // 
            // colExpiration
            // 
            resources.ApplyResources(this.colExpiration, "colExpiration");
            this.colExpiration.Name = "colExpiration";
            this.colExpiration.ReadOnly = true;
            this.colExpiration.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colExpiration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSerial
            // 
            resources.ApplyResources(this.colSerial, "colSerial");
            this.colSerial.Name = "colSerial";
            // 
            // txtPenDrive
            // 
            // 
            // 
            // 
            this.txtPenDrive.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.txtPenDrive.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode2")));
            this.txtPenDrive.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location2")));
            this.txtPenDrive.CustomButton.Name = "";
            this.txtPenDrive.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size2")));
            this.txtPenDrive.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPenDrive.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex2")));
            this.txtPenDrive.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPenDrive.CustomButton.UseSelectable = true;
            this.txtPenDrive.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible2")));
            this.txtPenDrive.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPenDrive.Lines = new string[0];
            resources.ApplyResources(this.txtPenDrive, "txtPenDrive");
            this.txtPenDrive.MaxLength = 32767;
            this.txtPenDrive.Name = "txtPenDrive";
            this.txtPenDrive.PasswordChar = '\0';
            this.txtPenDrive.ReadOnly = true;
            this.txtPenDrive.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPenDrive.SelectedText = "";
            this.txtPenDrive.SelectionLength = 0;
            this.txtPenDrive.SelectionStart = 0;
            this.txtPenDrive.ShortcutsEnabled = true;
            this.txtPenDrive.UseSelectable = true;
            this.txtPenDrive.UseStyleColors = true;
            this.txtPenDrive.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPenDrive.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label2.Name = "label2";
            this.label2.UseStyleColors = true;
            // 
            // btnFindPen
            // 
            resources.ApplyResources(this.btnFindPen, "btnFindPen");
            this.btnFindPen.Name = "btnFindPen";
            this.btnFindPen.UseSelectable = true;
            this.btnFindPen.UseStyleColors = true;
            this.btnFindPen.Click += new System.EventHandler(this.btnFindPen_Click);
            // 
            // metroToolbar1
            // 
            resources.ApplyResources(this.metroToolbar1, "metroToolbar1");
            this.metroToolbar1.Controls.Add(this.mtbLoad);
            this.metroToolbar1.Controls.Add(this.mtbCreate);
            this.metroToolbar1.Controls.Add(this.metroToolbarSeparator1);
            this.metroToolbar1.Controls.Add(this.btnOk);
            this.metroToolbar1.Name = "metroToolbar1";
            this.metroToolbar1.UseSelectable = true;
            this.metroToolbar1.UseStyleColors = true;
            this.metroToolbar1.ItemClicked += new System.EventHandler<MetroFramework.Extender.MetroToolbarButtonType>(this.metroToolbar1_ItemClicked);
            // 
            // mtbLoad
            // 
            this.mtbLoad.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Search;
            resources.ApplyResources(this.mtbLoad, "mtbLoad");
            this.mtbLoad.Image = global::Serializer.Properties.Resources.Open32;
            this.mtbLoad.ImageSize = 32;
            this.mtbLoad.IsVisible = true;
            this.mtbLoad.Name = "mtbLoad";
            this.mtbLoad.NoFocusImage = global::Serializer.Properties.Resources.Open32g;
            this.mtbLoad.UseSelectable = true;
            this.mtbLoad.UseStyleColors = true;
            // 
            // mtbCreate
            // 
            this.mtbCreate.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Save;
            resources.ApplyResources(this.mtbCreate, "mtbCreate");
            this.mtbCreate.Image = global::Serializer.Properties.Resources.CreateArchive32;
            this.mtbCreate.ImageSize = 32;
            this.mtbCreate.IsVisible = true;
            this.mtbCreate.Name = "mtbCreate";
            this.mtbCreate.NoFocusImage = global::Serializer.Properties.Resources.CreateArchive32g;
            this.mtbCreate.UseSelectable = true;
            this.mtbCreate.UseStyleColors = true;
            // 
            // metroToolbarSeparator1
            // 
            resources.ApplyResources(this.metroToolbarSeparator1, "metroToolbarSeparator1");
            this.metroToolbarSeparator1.ImageSize = 32;
            this.metroToolbarSeparator1.Name = "metroToolbarSeparator1";
            this.metroToolbarSeparator1.UseSelectable = true;
            // 
            // btnOk
            // 
            this.btnOk.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Exit;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Image = global::Serializer.Properties.Resources.Checked32;
            this.btnOk.ImageSize = 32;
            this.btnOk.IsVisible = true;
            this.btnOk.Name = "btnOk";
            this.btnOk.NoFocusImage = global::Serializer.Properties.Resources.Checked32g;
            this.btnOk.UseSelectable = true;
            // 
            // metroLabel1
            // 
            resources.ApplyResources(this.metroLabel1, "metroLabel1");
            this.metroLabel1.Name = "metroLabel1";
            // 
            // openDialog
            // 
            this.openDialog.DefaultExt = "*.config";
            this.openDialog.FileName = "ApplicationModules.config";
            resources.ApplyResources(this.openDialog, "openDialog");
            // 
            // registerForm
            // 
            this.ApplyImageInvert = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.metroToolbar1);
            this.Controls.Add(this.btnFindPen);
            this.Controls.Add(this.txtPenDrive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.txtMac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtLicense);
            this.Controls.Add(this.label1);
            this.Name = "registerForm";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.metroToolbar1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroTextBox txtLicense;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroTextBox txtMac;
        private MetroFramework.Controls.MetroGrid DataGridView1;
        private MetroFramework.Controls.MetroTextBox txtPenDrive;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroButton btnFindPen;
        private MetroFramework.Extender.MetroToolbar metroToolbar1;
        private MetroFramework.Extender.MetroToolbarPushButton mtbLoad;
        private MetroFramework.Extender.MetroToolbarPushButton mtbCreate;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Extender.MetroToolbarSeparator metroToolbarSeparator1;
        private MetroFramework.Extender.MetroToolbarPushButton btnOk;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicenseType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModuleName;
        private ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn colExpiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerial;
    }
}