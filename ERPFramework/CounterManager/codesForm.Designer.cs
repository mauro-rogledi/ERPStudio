namespace ERPFramework.CounterManager
{
    partial class codesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(codesForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbbCode = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.txtDescription = new MetroFramework.Controls.MetroTextBox();
            this.dgwSegments = new ERPFramework.Controls.ExtendedDataGridView();
            this.sgmSegmentNo = new ERPFramework.DataGridViewControls.NumericTextBoxDataGridViewColumn();
            this.sgmCodeType = new ERPFramework.DataGridViewControls.MetroDataGridViewComboBoxColumn();
            this.sgmLength = new ERPFramework.DataGridViewControls.NumericTextBoxDataGridViewColumn();
            this.sgmHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSegments)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbCode
            // 
            resources.ApplyResources(this.cbbCode, "cbbCode");
            this.cbbCode.FormattingEnabled = true;
            this.cbbCode.Name = "cbbCode";
            this.cbbCode.UseSelectable = true;
            this.cbbCode.SelectedIndexChanged += new System.EventHandler(this.cbbCode_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label1.Name = "label1";
            this.label1.UseStyleColors = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.label2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label2.Name = "label2";
            this.label2.UseStyleColors = true;
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            // 
            // 
            // 
            this.txtDescription.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription");
            this.txtDescription.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName");
            this.txtDescription.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor")));
            this.txtDescription.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize")));
            this.txtDescription.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode")));
            this.txtDescription.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage")));
            this.txtDescription.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout")));
            this.txtDescription.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock")));
            this.txtDescription.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle")));
            this.txtDescription.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font")));
            this.txtDescription.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txtDescription.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign")));
            this.txtDescription.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex")));
            this.txtDescription.CustomButton.ImageKey = resources.GetString("resource.ImageKey");
            this.txtDescription.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txtDescription.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txtDescription.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize")));
            this.txtDescription.CustomButton.Name = "";
            this.txtDescription.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft")));
            this.txtDescription.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txtDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDescription.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txtDescription.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign")));
            this.txtDescription.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation")));
            this.txtDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDescription.CustomButton.UseSelectable = true;
            this.txtDescription.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.txtDescription.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtDescription.Lines = new string[0];
            this.txtDescription.MaxLength = 32767;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PasswordChar = '\0';
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescription.SelectedText = "";
            this.txtDescription.SelectionLength = 0;
            this.txtDescription.SelectionStart = 0;
            this.txtDescription.ShortcutsEnabled = true;
            this.txtDescription.UseSelectable = true;
            this.txtDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // dgwSegments
            // 
            resources.ApplyResources(this.dgwSegments, "dgwSegments");
            this.dgwSegments.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.dgwSegments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgwSegments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgwSegments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgwSegments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgwSegments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwSegments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgwSegments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwSegments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sgmSegmentNo,
            this.sgmCodeType,
            this.sgmLength,
            this.sgmHeader});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgwSegments.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgwSegments.DocumentForm = null;
            this.dgwSegments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwSegments.EnableHeadersVisualStyles = false;
            this.dgwSegments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgwSegments.Name = "dgwSegments";
            this.dgwSegments.NoMessage = false;
            this.dgwSegments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgwSegments.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgwSegments.RowHeadersVisible = false;
            this.dgwSegments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwSegments.RowIndex = null;
            this.dgwSegments.RowTemplate.Height = 24;
            this.dgwSegments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwSegments.ShowEditingIcon = false;
            // 
            // sgmSegmentNo
            // 
            this.sgmSegmentNo.AllowGroupSeparator = false;
            this.sgmSegmentNo.AllowNegative = false;
            resources.ApplyResources(this.sgmSegmentNo, "sgmSegmentNo");
            this.sgmSegmentNo.MaxDecimalPlaces = 0;
            this.sgmSegmentNo.MaxWholeDigits = 2;
            this.sgmSegmentNo.Name = "sgmSegmentNo";
            this.sgmSegmentNo.RangeMax = 5D;
            this.sgmSegmentNo.RangeMin = 1D;
            this.sgmSegmentNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sgmCodeType
            // 
            this.sgmCodeType.DataSource = null;
            this.sgmCodeType.DisplayMember = "";
            resources.ApplyResources(this.sgmCodeType, "sgmCodeType");
            this.sgmCodeType.Name = "sgmCodeType";
            this.sgmCodeType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sgmCodeType.ValueMember = "";
            // 
            // sgmLength
            // 
            this.sgmLength.AllowGroupSeparator = false;
            this.sgmLength.AllowNegative = false;
            resources.ApplyResources(this.sgmLength, "sgmLength");
            this.sgmLength.MaxDecimalPlaces = 0;
            this.sgmLength.MaxWholeDigits = 2;
            this.sgmLength.Name = "sgmLength";
            this.sgmLength.RangeMax = 0D;
            this.sgmLength.RangeMin = 99D;
            this.sgmLength.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sgmHeader
            // 
            resources.ApplyResources(this.sgmHeader, "sgmHeader");
            this.sgmHeader.Name = "sgmHeader";
            // 
            // codesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dgwSegments);
            this.Controls.Add(this.cbbCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Name = "codesForm";
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbbCode, 0);
            this.Controls.SetChildIndex(this.dgwSegments, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgwSegments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private MetroFramework.Controls.MetroComboBox cbbCode;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroTextBox txtDescription;
        private ERPFramework.Controls.ExtendedDataGridView dgwSegments;
        private DataGridViewControls.NumericTextBoxDataGridViewColumn sgmSegmentNo;
        private DataGridViewControls.MetroDataGridViewComboBoxColumn sgmCodeType;
        private DataGridViewControls.NumericTextBoxDataGridViewColumn sgmLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn sgmHeader;
    }
}