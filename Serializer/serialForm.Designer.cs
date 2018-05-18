namespace Serializer
{
    partial class serialForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.mnsFile = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmbinaryFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveActivation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveModule = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMacAddress = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.colEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colLicenseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiration = new ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn();
            this.colSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarTextBoxDataGridViewColumn1 = new ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPenDrive = new System.Windows.Forms.Button();
            this.cbbPenDrive = new System.Windows.Forms.ComboBox();
            this.mnsFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "License Name";
            // 
            // txtLicense
            // 
            this.txtLicense.Location = new System.Drawing.Point(14, 48);
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.Size = new System.Drawing.Size(192, 21);
            this.txtLicense.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(581, 258);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(87, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mac Address";
            // 
            // txtMac
            // 
            this.txtMac.Location = new System.Drawing.Point(439, 48);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(192, 21);
            this.txtMac.TabIndex = 1;
            // 
            // mnsFile
            // 
            this.mnsFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
            this.mnsFile.Location = new System.Drawing.Point(0, 0);
            this.mnsFile.Name = "mnsFile";
            this.mnsFile.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnsFile.Size = new System.Drawing.Size(695, 24);
            this.mnsFile.TabIndex = 15;
            this.mnsFile.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoad,
            this.tsmiSave});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "File";
            // 
            // tsmiLoad
            // 
            this.tsmiLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadModule,
            this.tsmbinaryFile});
            this.tsmiLoad.Name = "tsmiLoad";
            this.tsmiLoad.Size = new System.Drawing.Size(129, 22);
            this.tsmiLoad.Text = "Load from";
            // 
            // tsmiLoadModule
            // 
            this.tsmiLoadModule.Name = "tsmiLoadModule";
            this.tsmiLoadModule.Size = new System.Drawing.Size(128, 22);
            this.tsmiLoadModule.Text = "Module";
            this.tsmiLoadModule.Click += new System.EventHandler(this.tsmiLoadModule_Click);
            // 
            // tsmbinaryFile
            // 
            this.tsmbinaryFile.Name = "tsmbinaryFile";
            this.tsmbinaryFile.Size = new System.Drawing.Size(128, 22);
            this.tsmbinaryFile.Text = "Activation";
            this.tsmbinaryFile.Click += new System.EventHandler(this.tsmbinaryFile_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSaveActivation,
            this.tsmSaveModule});
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(129, 22);
            this.tsmiSave.Text = "Save to";
            // 
            // tsmSaveActivation
            // 
            this.tsmSaveActivation.Name = "tsmSaveActivation";
            this.tsmSaveActivation.Size = new System.Drawing.Size(128, 22);
            this.tsmSaveActivation.Text = "Activation";
            this.tsmSaveActivation.Click += new System.EventHandler(this.tsmActivationSave_Click);
            // 
            // tsmSaveModule
            // 
            this.tsmSaveModule.Name = "tsmSaveModule";
            this.tsmSaveModule.Size = new System.Drawing.Size(128, 22);
            this.tsmSaveModule.Text = "Module";
            this.tsmSaveModule.Click += new System.EventHandler(this.tsmSaveModule_Click);
            // 
            // btnMacAddress
            // 
            this.btnMacAddress.Location = new System.Drawing.Point(637, 48);
            this.btnMacAddress.Name = "btnMacAddress";
            this.btnMacAddress.Size = new System.Drawing.Size(31, 23);
            this.btnMacAddress.TabIndex = 16;
            this.btnMacAddress.Text = "...";
            this.btnMacAddress.UseVisualStyleBackColor = true;
            this.btnMacAddress.Click += new System.EventHandler(this.btnMacAddress_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Pendrive";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEnable,
            this.colLicenseType,
            this.colApplication,
            this.colModuleName,
            this.colExpiration,
            this.colSerial});
            this.DataGridView1.Location = new System.Drawing.Point(14, 88);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(654, 150);
            this.DataGridView1.TabIndex = 12;
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.extendedDataGridView1_CellDoubleClick);
            // 
            // colEnable
            // 
            this.colEnable.HeaderText = "Enable";
            this.colEnable.Name = "colEnable";
            // 
            // colLicenseType
            // 
            this.colLicenseType.HeaderText = "License";
            this.colLicenseType.Name = "colLicenseType";
            this.colLicenseType.ReadOnly = true;
            this.colLicenseType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLicenseType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colApplication
            // 
            this.colApplication.HeaderText = "Application";
            this.colApplication.Name = "colApplication";
            this.colApplication.ReadOnly = true;
            // 
            // colModuleName
            // 
            this.colModuleName.HeaderText = "Module";
            this.colModuleName.Name = "colModuleName";
            this.colModuleName.ReadOnly = true;
            // 
            // colExpiration
            // 
            this.colExpiration.HeaderText = "ExpirationDate";
            this.colExpiration.Name = "colExpiration";
            this.colExpiration.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colExpiration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSerial
            // 
            this.colSerial.HeaderText = "Serial";
            this.colSerial.Name = "colSerial";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "License";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Application";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Module";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // calendarTextBoxDataGridViewColumn1
            // 
            this.calendarTextBoxDataGridViewColumn1.HeaderText = "ExpirationDate";
            this.calendarTextBoxDataGridViewColumn1.Name = "calendarTextBoxDataGridViewColumn1";
            this.calendarTextBoxDataGridViewColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarTextBoxDataGridViewColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Serial";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // btnPenDrive
            // 
            this.btnPenDrive.Location = new System.Drawing.Point(376, 46);
            this.btnPenDrive.Name = "btnPenDrive";
            this.btnPenDrive.Size = new System.Drawing.Size(31, 23);
            this.btnPenDrive.TabIndex = 19;
            this.btnPenDrive.Text = "...";
            this.btnPenDrive.UseVisualStyleBackColor = true;
            this.btnPenDrive.Click += new System.EventHandler(this.btnPenDrive_Click);
            // 
            // cbbPenDrive
            // 
            this.cbbPenDrive.FormattingEnabled = true;
            this.cbbPenDrive.Location = new System.Drawing.Point(213, 48);
            this.cbbPenDrive.Name = "cbbPenDrive";
            this.cbbPenDrive.Size = new System.Drawing.Size(157, 21);
            this.cbbPenDrive.TabIndex = 20;
            // 
            // serialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 330);
            this.Controls.Add(this.cbbPenDrive);
            this.Controls.Add(this.btnPenDrive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMacAddress);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.txtMac);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtLicense);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mnsFile);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mnsFile;
            this.Name = "serialForm";
            this.Text = "Form1";
            this.mnsFile.ResumeLayout(false);
            this.mnsFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.MenuStrip mnsFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadModule;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveActivation;
        private System.Windows.Forms.Button btnMacAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLicenseType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApplication;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModuleName;
        private ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn colExpiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSerial;
        private System.Windows.Forms.ToolStripMenuItem tsmbinaryFile;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ERPFramework.DataGridViewControls.CalendarTextBoxDataGridViewColumn calendarTextBoxDataGridViewColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button btnPenDrive;
        private System.Windows.Forms.ComboBox cbbPenDrive;
    }
}

