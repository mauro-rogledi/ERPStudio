namespace ERPFramework.Controls
{
    partial class MetroInputDateRange
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetroInputDateRange));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.cbbRangeType = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.dtbTo = new ERPFramework.Controls.DateTextBox();
            this.dtbFrom = new ERPFramework.Controls.DateTextBox();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            resources.ApplyResources(this.metroLabel1, "metroLabel1");
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.UseStyleColors = true;
            // 
            // cbbRangeType
            // 
            this.cbbRangeType.FormattingEnabled = true;
            resources.ApplyResources(this.cbbRangeType, "cbbRangeType");
            this.cbbRangeType.Name = "cbbRangeType";
            this.cbbRangeType.UseSelectable = true;
            this.cbbRangeType.SelectedIndexChanged += new System.EventHandler(this.cbbRangeType_SelectedIndexChanged);
            // 
            // metroLabel2
            // 
            resources.ApplyResources(this.metroLabel2, "metroLabel2");
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.UseStyleColors = true;
            // 
            // metroLabel3
            // 
            resources.ApplyResources(this.metroLabel3, "metroLabel3");
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Small;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.UseStyleColors = true;
            // 
            // dtbTo
            // 
            // 
            // 
            // 
            this.dtbTo.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.dtbTo.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.dtbTo.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.dtbTo.CustomButton.Name = "";
            this.dtbTo.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.dtbTo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.dtbTo.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.dtbTo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtbTo.CustomButton.UseSelectable = true;
            this.dtbTo.GroupSeparator = '/';
            resources.ApplyResources(this.dtbTo, "dtbTo");
            this.dtbTo.MaxLength = 32767;
            this.dtbTo.Name = "dtbTo";
            this.dtbTo.PasswordChar = '\0';
            this.dtbTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dtbTo.SelectedText = "";
            this.dtbTo.SelectionLength = 0;
            this.dtbTo.SelectionStart = 0;
            this.dtbTo.ShortcutsEnabled = true;
            this.dtbTo.ShowButton = true;
            this.dtbTo.UseSelectable = true;
            this.dtbTo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.dtbTo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // dtbFrom
            // 
            // 
            // 
            // 
            this.dtbFrom.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.dtbFrom.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.dtbFrom.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.dtbFrom.CustomButton.Name = "";
            this.dtbFrom.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.dtbFrom.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.dtbFrom.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.dtbFrom.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtbFrom.CustomButton.UseSelectable = true;
            this.dtbFrom.GroupSeparator = '/';
            resources.ApplyResources(this.dtbFrom, "dtbFrom");
            this.dtbFrom.MaxLength = 32767;
            this.dtbFrom.Name = "dtbFrom";
            this.dtbFrom.PasswordChar = '\0';
            this.dtbFrom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dtbFrom.SelectedText = "";
            this.dtbFrom.SelectionLength = 0;
            this.dtbFrom.SelectionStart = 0;
            this.dtbFrom.ShortcutsEnabled = true;
            this.dtbFrom.ShowButton = true;
            this.dtbFrom.UseSelectable = true;
            this.dtbFrom.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.dtbFrom.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // MetroInputDateRange
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtbTo);
            this.Controls.Add(this.dtbFrom);
            this.Controls.Add(this.cbbRangeType);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "MetroInputDateRange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox cbbRangeType;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private ERPFramework.Controls.DateTextBox dtbFrom;
        private ERPFramework.Controls.DateTextBox dtbTo;
    }
}
