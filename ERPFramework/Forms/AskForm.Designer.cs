namespace ERPFramework.Forms
{
    partial class AskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AskForm));
            this.mtlOk = new MetroFramework.Controls.MetroLink();
            this.mtlBack = new MetroFramework.Controls.MetroLink();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // mtlOk
            // 
            resources.ApplyResources(this.mtlOk, "mtlOk");
            this.mtlOk.Image = global::ERPFramework.Properties.Resources.Checked32;
            this.mtlOk.ImageSize = 32;
            this.mtlOk.Name = "mtlOk";
            this.mtlOk.NoFocusImage = global::ERPFramework.Properties.Resources.Checked32g;
            this.metroToolTip1.SetToolTip(this.mtlOk, resources.GetString("mtlOk.ToolTip"));
            this.mtlOk.UseSelectable = true;
            this.mtlOk.UseStyleColors = true;
            this.mtlOk.Click += new System.EventHandler(this.mtlOk_Click);
            // 
            // mtlBack
            // 
            resources.ApplyResources(this.mtlBack, "mtlBack");
            this.mtlBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mtlBack.Image = global::ERPFramework.Properties.Resources.CircledLeft32;
            this.mtlBack.ImageSize = 32;
            this.mtlBack.Name = "mtlBack";
            this.mtlBack.NoFocusImage = global::ERPFramework.Properties.Resources.CircledLeft32g;
            this.metroToolTip1.SetToolTip(this.mtlBack, resources.GetString("mtlBack.ToolTip"));
            this.mtlBack.UseSelectable = true;
            this.mtlBack.UseStyleColors = true;
            this.mtlBack.Click += new System.EventHandler(this.mtlBack_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // AskForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.mtlOk);
            this.Controls.Add(this.mtlBack);
            this.Name = "AskForm";
            this.ShowIcon = false;
            this.metroToolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLink mtlOk;
        private MetroFramework.Controls.MetroLink mtlBack;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}