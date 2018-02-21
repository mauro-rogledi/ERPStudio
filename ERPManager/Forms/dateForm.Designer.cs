namespace ERPManager.Forms
{
    partial class dateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dateForm));
            this.txtCalendar = new ERPFramework.Controls.DateTextBox();
            this.SuspendLayout();
            // 
            // txtCalendar
            // 
            resources.ApplyResources(this.txtCalendar, "txtCalendar");
            // 
            // 
            // 
            this.txtCalendar.CustomButton.AccessibleDescription = resources.GetString("resource.AccessibleDescription");
            this.txtCalendar.CustomButton.AccessibleName = resources.GetString("resource.AccessibleName");
            this.txtCalendar.CustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("resource.Anchor")));
            this.txtCalendar.CustomButton.AutoSize = ((bool)(resources.GetObject("resource.AutoSize")));
            this.txtCalendar.CustomButton.AutoSizeMode = ((System.Windows.Forms.AutoSizeMode)(resources.GetObject("resource.AutoSizeMode")));
            this.txtCalendar.CustomButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resource.BackgroundImage")));
            this.txtCalendar.CustomButton.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("resource.BackgroundImageLayout")));
            this.txtCalendar.CustomButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("resource.Dock")));
            this.txtCalendar.CustomButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("resource.FlatStyle")));
            this.txtCalendar.CustomButton.Font = ((System.Drawing.Font)(resources.GetObject("resource.Font")));
            this.txtCalendar.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.txtCalendar.CustomButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.ImageAlign")));
            this.txtCalendar.CustomButton.ImageIndex = ((int)(resources.GetObject("resource.ImageIndex")));
            this.txtCalendar.CustomButton.ImageKey = resources.GetString("resource.ImageKey");
            this.txtCalendar.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.txtCalendar.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.txtCalendar.CustomButton.MaximumSize = ((System.Drawing.Size)(resources.GetObject("resource.MaximumSize")));
            this.txtCalendar.CustomButton.Name = "";
            this.txtCalendar.CustomButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("resource.RightToLeft")));
            this.txtCalendar.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.txtCalendar.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCalendar.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.txtCalendar.CustomButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.TextAlign")));
            this.txtCalendar.CustomButton.TextImageRelation = ((System.Windows.Forms.TextImageRelation)(resources.GetObject("resource.TextImageRelation")));
            this.txtCalendar.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCalendar.CustomButton.UseSelectable = true;
            this.txtCalendar.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCalendar.GroupSeparator = '/';
            this.txtCalendar.MaxLength = 32767;
            this.txtCalendar.Name = "txtCalendar";
            this.txtCalendar.PasswordChar = '\0';
            this.txtCalendar.RangeMin = new System.DateTime(1799, 12, 31, 0, 0, 0, 0);
            this.txtCalendar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCalendar.SelectedText = "";
            this.txtCalendar.SelectionLength = 0;
            this.txtCalendar.SelectionStart = 0;
            this.txtCalendar.ShortcutsEnabled = true;
            this.txtCalendar.ShowButton = true;
            this.txtCalendar.UseSelectable = true;
            this.txtCalendar.UseStyleColors = true;
            this.txtCalendar.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCalendar.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // dateForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtCalendar);
            this.Name = "dateForm";
            this.Controls.SetChildIndex(this.txtCalendar, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private ERPFramework.Controls.DateTextBox txtCalendar;
    }
}