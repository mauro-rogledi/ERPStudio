namespace ERPFramework.Preferences
{
    partial class PreferencePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencePanel));
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.metroPropertyGrid1 = new MetroFramework.Extender.MetroPropertyGrid();
            this.flyToolbar = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.btnNew = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnEdit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSave = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnList = new MetroFramework.Extender.MetroToolbarDropDownButton();
            this.btnUndo = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnDelete = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnExit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnComputer = new MetroFramework.Extender.MetroLinkChecked();
            this.btnUser = new MetroFramework.Extender.MetroLinkChecked();
            this.btnApplication = new MetroFramework.Extender.MetroLinkChecked();
            this.flyToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroPropertyGrid1
            // 
            resources.ApplyResources(this.metroPropertyGrid1, "metroPropertyGrid1");
            this.metroPropertyGrid1.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.metroPropertyGrid1.Name = "metroPropertyGrid1";
            this.metroPropertyGrid1.SelectedObject = null;
            this.metroPropertyGrid1.TabStop = false;
            this.metroPropertyGrid1.UseSelectable = true;
            this.metroPropertyGrid1.UseStyleColors = true;
            // 
            // flyToolbar
            // 
            this.flyToolbar.Controls.Add(this.btnNew);
            this.flyToolbar.Controls.Add(this.btnEdit);
            this.flyToolbar.Controls.Add(this.btnSave);
            this.flyToolbar.Controls.Add(this.btnList);
            this.flyToolbar.Controls.Add(this.btnUndo);
            this.flyToolbar.Controls.Add(this.btnDelete);
            this.flyToolbar.Controls.Add(this.btnExit);
            this.flyToolbar.Controls.Add(this.metroLabel1);
            this.flyToolbar.Controls.Add(this.btnComputer);
            this.flyToolbar.Controls.Add(this.btnUser);
            this.flyToolbar.Controls.Add(this.btnApplication);
            resources.ApplyResources(this.flyToolbar, "flyToolbar");
            this.flyToolbar.Name = "flyToolbar";
            this.flyToolbar.UseStyleColors = true;
            // 
            // btnNew
            // 
            //this.btnNew.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.New;
            this.btnNew.Image = global::ERPFramework.Properties.Resources.New32;
            this.btnNew.ImageSize = 32;
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.NoFocusImage = global::ERPFramework.Properties.Resources.New32g;
            this.metroToolTip1.SetToolTip(this.btnNew, resources.GetString("btnNew.ToolTip"));
            this.btnNew.UseSelectable = true;
            this.btnNew.UseStyleColors = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            //this.btnEdit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Edit;
            this.btnEdit.Image = global::ERPFramework.Properties.Resources.Edit32;
            this.btnEdit.ImageSize = 32;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NoFocusImage = global::ERPFramework.Properties.Resources.Edit32g;
            this.metroToolTip1.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseSelectable = true;
            this.btnEdit.UseStyleColors = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            //this.btnSave.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Save;
            this.btnSave.Image = global::ERPFramework.Properties.Resources.Save32;
            this.btnSave.ImageSize = 32;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.NoFocusImage = global::ERPFramework.Properties.Resources.Save32g;
            this.metroToolTip1.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseSelectable = true;
            this.btnSave.UseStyleColors = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnList
            // 
            //this.btnList.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.New;
            this.btnList.Image = global::ERPFramework.Properties.Resources.List32;
            resources.ApplyResources(this.btnList, "btnList");
            this.btnList.ImageSize = 32;
            this.btnList.Name = "btnList";
            this.btnList.NoFocusImage = global::ERPFramework.Properties.Resources.List32g;
            this.metroToolTip1.SetToolTip(this.btnList, resources.GetString("btnList.ToolTip"));
            this.btnList.UseSelectable = true;
            this.btnList.UseStyleColors = true;
            this.btnList.DropDownItemClicked += new System.EventHandler(this.btnList_DropDownItemClicked);
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnUndo
            // 
            //this.btnUndo.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Undo;
            this.btnUndo.Image = global::ERPFramework.Properties.Resources.Undo32;
            this.btnUndo.ImageSize = 32;
            resources.ApplyResources(this.btnUndo, "btnUndo");
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.NoFocusImage = global::ERPFramework.Properties.Resources.Undo32g;
            this.metroToolTip1.SetToolTip(this.btnUndo, resources.GetString("btnUndo.ToolTip"));
            this.btnUndo.UseSelectable = true;
            this.btnUndo.UseStyleColors = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnDelete
            // 
            //this.btnDelete.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Delete;
            this.btnDelete.Image = global::ERPFramework.Properties.Resources.Delete32;
            this.btnDelete.ImageSize = 32;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NoFocusImage = global::ERPFramework.Properties.Resources.Delete32g;
            this.metroToolTip1.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseSelectable = true;
            this.btnDelete.UseStyleColors = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            //this.btnExit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Delete;
            this.btnExit.Image = global::ERPFramework.Properties.Resources.Exit32;
            this.btnExit.ImageSize = 32;
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::ERPFramework.Properties.Resources.Exit32g;
            this.metroToolTip1.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            this.btnExit.UseStyleColors = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            resources.ApplyResources(this.metroLabel1, "metroLabel1");
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.UseStyleColors = true;
            // 
            // btnComputer
            // 
            this.btnComputer.CheckedImage = global::ERPFramework.Properties.Resources.Laptop32ck;
            this.btnComputer.CheckOnClick = false;
            this.btnComputer.Image = global::ERPFramework.Properties.Resources.Laptop32;
            this.btnComputer.ImageSize = 32;
            resources.ApplyResources(this.btnComputer, "btnComputer");
            this.btnComputer.Name = "btnComputer";
            this.btnComputer.NoFocusCheckedImage = global::ERPFramework.Properties.Resources.Laptop32ckg;
            this.btnComputer.NoFocusImage = global::ERPFramework.Properties.Resources.Laptop32g;
            this.metroToolTip1.SetToolTip(this.btnComputer, resources.GetString("btnComputer.ToolTip"));
            this.btnComputer.UseSelectable = true;
            this.btnComputer.UseStyleColors = true;
            // 
            // btnUser
            // 
            this.btnUser.CheckedImage = global::ERPFramework.Properties.Resources.User32ck;
            this.btnUser.CheckOnClick = false;
            this.btnUser.Image = global::ERPFramework.Properties.Resources.User32;
            this.btnUser.ImageSize = 32;
            resources.ApplyResources(this.btnUser, "btnUser");
            this.btnUser.Name = "btnUser";
            this.btnUser.NoFocusCheckedImage = global::ERPFramework.Properties.Resources.User32ckg;
            this.btnUser.NoFocusImage = global::ERPFramework.Properties.Resources.User32g;
            this.metroToolTip1.SetToolTip(this.btnUser, resources.GetString("btnUser.ToolTip"));
            this.btnUser.UseSelectable = true;
            this.btnUser.UseStyleColors = true;
            // 
            // btnApplication
            // 
            this.btnApplication.CheckedImage = global::ERPFramework.Properties.Resources.Form32ck;
            this.btnApplication.CheckOnClick = false;
            this.btnApplication.Image = global::ERPFramework.Properties.Resources.Form32;
            this.btnApplication.ImageSize = 32;
            resources.ApplyResources(this.btnApplication, "btnApplication");
            this.btnApplication.Name = "btnApplication";
            this.btnApplication.NoFocusCheckedImage = global::ERPFramework.Properties.Resources.Form32ckg;
            this.btnApplication.NoFocusImage = global::ERPFramework.Properties.Resources.Form32g;
            this.metroToolTip1.SetToolTip(this.btnApplication, resources.GetString("btnApplication.ToolTip"));
            this.btnApplication.UseSelectable = true;
            this.btnApplication.UseStyleColors = true;
            // 
            // PreferencePanel
            // 
            this.Controls.Add(this.metroPropertyGrid1);
            this.Controls.Add(this.flyToolbar);
            this.Name = "PreferencePanel";
            resources.ApplyResources(this, "$this");
            this.UseStyleColors = true;
            this.flyToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Extender.MetroFlowLayoutPanel flyToolbar;
        private MetroFramework.Extender.MetroToolbarPushButton btnNew;
        private MetroFramework.Extender.MetroToolbarPushButton btnEdit;
        private MetroFramework.Extender.MetroToolbarPushButton btnSave;
        private MetroFramework.Extender.MetroToolbarDropDownButton btnList;
        private MetroFramework.Extender.MetroToolbarPushButton btnUndo;
        private MetroFramework.Extender.MetroToolbarPushButton btnDelete;
        private MetroFramework.Extender.MetroToolbarPushButton btnExit;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Extender.MetroLinkChecked btnComputer;
        private MetroFramework.Extender.MetroLinkChecked btnUser;
        private MetroFramework.Extender.MetroLinkChecked btnApplication;
        private MetroFramework.Extender.MetroPropertyGrid metroPropertyGrid1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
