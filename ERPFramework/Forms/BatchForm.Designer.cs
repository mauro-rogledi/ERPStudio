namespace ERPFramework.Forms
{
    partial class BatchForm
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchForm));
            this.metroToolbar1 = new ERPFramework.Controls.ExMetroToolbar();
            this.btnExecute = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSep1 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnExit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSep2 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnPref = new MetroFramework.Extender.MetroToolbarPushButton();
            this.metroToolbar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroToolbar1
            // 
            this.metroToolbar1.CausesValidation = false;
            this.metroToolbar1.Controls.Add(this.btnExit);
            this.metroToolbar1.Controls.Add(this.btnSep1);
            this.metroToolbar1.Controls.Add(this.btnPref);
            this.metroToolbar1.Controls.Add(this.btnSep2);
            this.metroToolbar1.Controls.Add(this.btnExecute);
            resources.ApplyResources(this.metroToolbar1, "metroToolbar1");
            this.metroToolbar1.Name = "metroToolbar1";
            this.metroToolbar1.UseSelectable = true;
            this.metroToolbar1.ItemClicked += new System.EventHandler<MetroFramework.Extender.MetroToolbarButtonType>(this.metroToolbar1_ItemClicked);
            // 
            // btnExecute
            // 
            this.btnExecute.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Execute;
            resources.ApplyResources(this.btnExecute, "btnExecute");
            this.btnExecute.Image = global::ERPFramework.Properties.Resources.Play32;
            this.btnExecute.ImageSize = 32;
            this.btnExecute.IsVisible = true;
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.NoFocusImage = global::ERPFramework.Properties.Resources.Play32g;
            this.btnExecute.UseSelectable = true;
            // 
            // btnSep1
            // 
            resources.ApplyResources(this.btnSep1, "btnSep1");
            this.btnSep1.ImageSize = 32;
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.UseSelectable = true;
            // 
            // btnExit
            // 
            this.btnExit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Exit;
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Image = global::ERPFramework.Properties.Resources.Exit32;
            this.btnExit.ImageSize = 32;
            this.btnExit.IsVisible = true;
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::ERPFramework.Properties.Resources.Exit32g;
            this.btnExit.UseSelectable = true;
            // 
            // btnSep2
            // 
            resources.ApplyResources(this.btnSep2, "btnSep2");
            this.btnSep2.ImageSize = 32;
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.UseSelectable = true;
            // 
            // btnPref
            // 
            this.btnPref.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Preference;
            resources.ApplyResources(this.btnPref, "btnPref");
            this.btnPref.Image = global::ERPFramework.Properties.Resources.Setting32;
            this.btnPref.ImageSize = 32;
            this.btnPref.IsVisible = true;
            this.btnPref.Name = "btnPref";
            this.btnPref.NoFocusImage = global::ERPFramework.Properties.Resources.Setting32g;
            this.btnPref.UseSelectable = true;
            // 
            // BatchForm
            // 
            this.Controls.Add(this.metroToolbar1);
            resources.ApplyResources(this, "$this");
            this.Name = "BatchForm";
            this.UseStyleColors = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.formDB_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.formDB_KeyUp);
            this.metroToolbar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private ERPFramework.Controls.ExMetroToolbar metroToolbar1;
        private MetroFramework.Extender.MetroToolbarPushButton btnExit;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep1;
        private MetroFramework.Extender.MetroToolbarPushButton btnPref;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep2;
        private MetroFramework.Extender.MetroToolbarPushButton btnExecute;
    }

}
