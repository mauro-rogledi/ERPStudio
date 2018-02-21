namespace ERPManager
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.lsbVersions = new MetroFramework.Controls.MetroListView();
            this.colModule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblAppname = new MetroFramework.Controls.MetroLabel();
            this.lblLicensed = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::ERPManager.Properties.Resources.logo;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
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
            this.label2.Name = "label2";
            this.label2.UseStyleColors = true;
            // 
            // lsbVersions
            // 
            resources.ApplyResources(this.lsbVersions, "lsbVersions");
            this.lsbVersions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colModule,
            this.colVersion});
            this.lsbVersions.FullRowSelect = true;
            this.lsbVersions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsbVersions.HideSelection = false;
            this.lsbVersions.MultiSelect = false;
            this.lsbVersions.Name = "lsbVersions";
            this.lsbVersions.OwnerDraw = true;
            this.lsbVersions.ShowGroups = false;
            this.lsbVersions.UseCompatibleStateImageBehavior = false;
            this.lsbVersions.UseSelectable = true;
            this.lsbVersions.UseStyleColors = true;
            this.lsbVersions.View = System.Windows.Forms.View.Details;
            // 
            // colModule
            // 
            resources.ApplyResources(this.colModule, "colModule");
            // 
            // colVersion
            // 
            resources.ApplyResources(this.colVersion, "colVersion");
            // 
            // lblAppname
            // 
            resources.ApplyResources(this.lblAppname, "lblAppname");
            this.lblAppname.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblAppname.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblAppname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(94)))), ((int)(((byte)(77)))));
            this.lblAppname.Name = "lblAppname";
            this.lblAppname.UseCustomForeColor = true;
            // 
            // lblLicensed
            // 
            resources.ApplyResources(this.lblLicensed, "lblLicensed");
            this.lblLicensed.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblLicensed.Name = "lblLicensed";
            this.lblLicensed.UseStyleColors = true;
            // 
            // InfoForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblLicensed);
            this.Controls.Add(this.lblAppname);
            this.Controls.Add(this.lsbVersions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroListView lsbVersions;
        private MetroFramework.Controls.MetroLabel lblAppname;
        private MetroFramework.Controls.MetroLabel lblLicensed;
        private System.Windows.Forms.ColumnHeader colModule;
        private System.Windows.Forms.ColumnHeader colVersion;
    }
}
