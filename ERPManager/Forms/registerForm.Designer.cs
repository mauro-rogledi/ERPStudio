namespace ERPManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registerForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.txtLicense = new MetroFramework.Controls.MetroTextBox();
            this.btnOk = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
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
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseSelectable = true;
            this.btnOk.UseStyleColors = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseSelectable = true;
            this.btnCancel.UseStyleColors = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ERPManager.Properties.Resources.logo;
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(226)))), ((int)(((byte)(255)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.DataGridView1, "DataGridView1");
            this.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEnable,
            this.colLicenseType,
            this.colApplication,
            this.colModuleName,
            this.colExpiration,
            this.colSerial});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.EnableHeadersVisualStyles = false;
            this.DataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView1.RowHeadersVisible = false;
            this.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.DataGridView1.UseStyleColors = true;
            // 
            // colEnable
            // 
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
            // registerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.btnFindPen);
            this.Controls.Add(this.txtPenDrive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.txtMac);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtLicense);
            this.Controls.Add(this.label1);
            this.Name = "registerForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroTextBox txtLicense;
        private MetroFramework.Controls.MetroButton btnOk;
        private MetroFramework.Controls.MetroButton btnCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroTextBox txtMac;
        private MetroFramework.Controls.MetroGrid DataGridView1;
        private MetroFramework.Controls.MetroTextBox txtPenDrive;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroButton btnFindPen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicenseType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModuleName;
        private ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn colExpiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerial;
    }
}