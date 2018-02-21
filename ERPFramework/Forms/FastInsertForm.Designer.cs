namespace ERPFramework.Forms
{
    partial class FastInsertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastInsertForm));
            this.metroToolbar = new ERPFramework.Controls.ExMetroToolbar();
            this.btnExit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSep3 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnPref = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSep2 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnPrint = new MetroFramework.Extender.MetroToolbarDropDownButton();
            this.btnSep1 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnUndo = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSave = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnEdit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnNew = new MetroFramework.Extender.MetroToolbarPushButton();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.metroToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroToolbar
            // 
            resources.ApplyResources(this.metroToolbar, "metroToolbar");
            this.metroToolbar.ButtonAddonVisible = true;
            this.metroToolbar.ButtonDeleteVisible = true;
            this.metroToolbar.ButtonPrefVisible = true;
            this.metroToolbar.ButtonPreviewVisible = true;
            this.metroToolbar.ButtonPrintVisible = true;
            this.metroToolbar.CausesValidation = false;
            this.metroToolbar.Controls.Add(this.btnExit);
            this.metroToolbar.Controls.Add(this.btnSep3);
            this.metroToolbar.Controls.Add(this.btnPref);
            this.metroToolbar.Controls.Add(this.btnSep2);
            this.metroToolbar.Controls.Add(this.btnPrint);
            this.metroToolbar.Controls.Add(this.btnSep1);
            this.metroToolbar.Controls.Add(this.btnUndo);
            this.metroToolbar.Controls.Add(this.btnSave);
            this.metroToolbar.Controls.Add(this.btnEdit);
            this.metroToolbar.Controls.Add(this.btnNew);
            this.metroToolbar.Name = "metroToolbar";
            this.metroToolTip1.SetToolTip(this.metroToolbar, resources.GetString("metroToolbar.ToolTip"));
            this.metroToolbar.UseSelectable = true;
            this.metroToolbar.ItemClicked += new System.EventHandler<MetroFramework.Extender.MetroToolbarButtonType>(this.metroToolbar_ItemClicked);
            this.metroToolbar.ToolStripMenuClick += new System.EventHandler<string>(this.metroToolbar_ToolStripMenuClick_1);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Exit;
            this.btnExit.Image = global::ERPFramework.Properties.Resources.Exit32;
            this.btnExit.ImageSize = 32;
            this.btnExit.IsVisible = true;
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::ERPFramework.Properties.Resources.Exit32g;
            this.metroToolTip1.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            // 
            // btnSep3
            // 
            resources.ApplyResources(this.btnSep3, "btnSep3");
            this.btnSep3.ImageSize = 32;
            this.btnSep3.Name = "btnSep3";
            this.metroToolTip1.SetToolTip(this.btnSep3, resources.GetString("btnSep3.ToolTip"));
            this.btnSep3.UseSelectable = true;
            // 
            // btnPref
            // 
            resources.ApplyResources(this.btnPref, "btnPref");
            this.btnPref.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Preference;
            this.btnPref.Image = global::ERPFramework.Properties.Resources.Setting32;
            this.btnPref.ImageSize = 32;
            this.btnPref.IsVisible = true;
            this.btnPref.Name = "btnPref";
            this.btnPref.NoFocusImage = global::ERPFramework.Properties.Resources.Setting32g;
            this.metroToolTip1.SetToolTip(this.btnPref, resources.GetString("btnPref.ToolTip"));
            this.btnPref.UseSelectable = true;
            // 
            // btnSep2
            // 
            resources.ApplyResources(this.btnSep2, "btnSep2");
            this.btnSep2.ImageSize = 32;
            this.btnSep2.Name = "btnSep2";
            this.metroToolTip1.SetToolTip(this.btnSep2, resources.GetString("btnSep2.ToolTip"));
            this.btnSep2.UseSelectable = true;
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Print;
            this.btnPrint.Image = global::ERPFramework.Properties.Resources.Print32;
            this.btnPrint.ImageSize = 32;
            this.btnPrint.IsVisible = true;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoFocusImage = global::ERPFramework.Properties.Resources.Print32g;
            this.metroToolTip1.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseSelectable = true;
            // 
            // btnSep1
            // 
            resources.ApplyResources(this.btnSep1, "btnSep1");
            this.btnSep1.ImageSize = 32;
            this.btnSep1.Name = "btnSep1";
            this.metroToolTip1.SetToolTip(this.btnSep1, resources.GetString("btnSep1.ToolTip"));
            this.btnSep1.UseSelectable = true;
            // 
            // btnUndo
            // 
            resources.ApplyResources(this.btnUndo, "btnUndo");
            this.btnUndo.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Undo;
            this.btnUndo.Image = global::ERPFramework.Properties.Resources.Undo32;
            this.btnUndo.ImageSize = 32;
            this.btnUndo.IsVisible = true;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.NoFocusImage = global::ERPFramework.Properties.Resources.Undo32g;
            this.metroToolTip1.SetToolTip(this.btnUndo, resources.GetString("btnUndo.ToolTip"));
            this.btnUndo.UseSelectable = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Save;
            this.btnSave.Image = global::ERPFramework.Properties.Resources.Save32;
            this.btnSave.ImageSize = 32;
            this.btnSave.IsVisible = true;
            this.btnSave.Name = "btnSave";
            this.btnSave.NoFocusImage = global::ERPFramework.Properties.Resources.Save32g;
            this.metroToolTip1.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseSelectable = true;
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Edit;
            this.btnEdit.Image = global::ERPFramework.Properties.Resources.Edit32;
            this.btnEdit.ImageSize = 32;
            this.btnEdit.IsVisible = true;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NoFocusImage = global::ERPFramework.Properties.Resources.Edit32g;
            this.metroToolTip1.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseSelectable = true;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Image = global::ERPFramework.Properties.Resources.New32;
            this.btnNew.ImageSize = 32;
            this.btnNew.IsVisible = true;
            this.btnNew.Name = "btnNew";
            this.btnNew.NoFocusImage = global::ERPFramework.Properties.Resources.New32g;
            this.metroToolTip1.SetToolTip(this.btnNew, resources.GetString("btnNew.ToolTip"));
            this.btnNew.UseSelectable = true;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // FastInsertForm
            // 
            resources.ApplyResources(this, "$this");
            this.ApplyImageInvert = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ControlBox = false;
            this.Controls.Add(this.metroToolbar);
            this.Name = "FastInsertForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.metroToolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.metroToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private ERPFramework.Controls.ExMetroToolbar metroToolbar;
        private MetroFramework.Extender.MetroToolbarPushButton btnExit;
        private MetroFramework.Extender.MetroToolbarPushButton btnNew;
        private MetroFramework.Extender.MetroToolbarPushButton btnEdit;
        private MetroFramework.Extender.MetroToolbarPushButton btnSave;
        private MetroFramework.Extender.MetroToolbarPushButton btnUndo;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep1;
        private MetroFramework.Extender.MetroToolbarDropDownButton btnPrint;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep2;
        private MetroFramework.Extender.MetroToolbarPushButton btnPref;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep3;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }

}
