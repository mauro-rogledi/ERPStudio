using System.Windows.Forms;

namespace ERPFramework.Forms
{
    partial class DocumentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentForm));
            this.metroToolTip = new MetroFramework.Components.MetroToolTip();
            this.btnAddOn = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnExit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnPref = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnPrint = new MetroFramework.Extender.MetroToolbarDropDownButton();
            this.btnPreview = new MetroFramework.Extender.MetroToolbarDropDownButton();
            this.btnFilter = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSearch = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnUndo = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnDelete = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSave = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnEdit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnNew = new MetroFramework.Extender.MetroToolbarPushButton();
            this.aopAddons = new MetroFramework.Extender.MetroAddOnPanel();
            this.metroToolbar = new ERPFramework.Controls.ExMetroToolbar();
            this.btnSep4 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnSep3 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnSep2 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnSep1 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.metroToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroToolTip
            // 
            this.metroToolTip.Style = MetroFramework.MetroColorStyle.Default;
            this.metroToolTip.StyleManager = null;
            this.metroToolTip.Theme = MetroFramework.MetroThemeStyle.Default;
            // 
            // btnAddOn
            // 
            resources.ApplyResources(this.btnAddOn, "btnAddOn");
            this.btnAddOn.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.AddOn;
            this.btnAddOn.Image = global::ERPFramework.Properties.Resources.More32;
            this.btnAddOn.ImageSize = 32;
            this.btnAddOn.IsVisible = true;
            this.btnAddOn.Name = "btnAddOn";
            this.btnAddOn.NoFocusImage = global::ERPFramework.Properties.Resources.More32g;
            this.metroToolTip.SetToolTip(this.btnAddOn, resources.GetString("btnAddOn.ToolTip"));
            this.btnAddOn.UseSelectable = true;
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
            this.metroToolTip.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            this.btnExit.UseStyleColors = true;
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
            this.metroToolTip.SetToolTip(this.btnPref, resources.GetString("btnPref.ToolTip"));
            this.btnPref.UseSelectable = true;
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
            this.metroToolTip.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseSelectable = true;
            // 
            // btnPreview
            // 
            resources.ApplyResources(this.btnPreview, "btnPreview");
            this.btnPreview.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Preview;
            this.btnPreview.Image = global::ERPFramework.Properties.Resources.Preview32;
            this.btnPreview.ImageSize = 32;
            this.btnPreview.IsVisible = true;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.NoFocusImage = global::ERPFramework.Properties.Resources.Preview32g;
            this.metroToolTip.SetToolTip(this.btnPreview, resources.GetString("btnPreview.ToolTip"));
            this.btnPreview.UseSelectable = true;
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Filter;
            this.btnFilter.Image = global::ERPFramework.Properties.Resources.Filter32;
            this.btnFilter.ImageSize = 32;
            this.btnFilter.IsVisible = true;
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.NoFocusImage = global::ERPFramework.Properties.Resources.Filter32g;
            this.metroToolTip.SetToolTip(this.btnFilter, resources.GetString("btnFilter.ToolTip"));
            this.btnFilter.UseSelectable = true;
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Search;
            this.btnSearch.Image = global::ERPFramework.Properties.Resources.Search32;
            this.btnSearch.ImageSize = 32;
            this.btnSearch.IsVisible = true;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NoFocusImage = global::ERPFramework.Properties.Resources.Search32g;
            this.metroToolTip.SetToolTip(this.btnSearch, resources.GetString("btnSearch.ToolTip"));
            this.btnSearch.UseSelectable = true;
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
            this.metroToolTip.SetToolTip(this.btnUndo, resources.GetString("btnUndo.ToolTip"));
            this.btnUndo.UseSelectable = true;
            this.btnUndo.UseStyleColors = true;
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Delete;
            this.btnDelete.Image = global::ERPFramework.Properties.Resources.Delete32;
            this.btnDelete.ImageSize = 32;
            this.btnDelete.IsVisible = true;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NoFocusImage = global::ERPFramework.Properties.Resources.Delete32g;
            this.metroToolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseSelectable = true;
            this.btnDelete.UseStyleColors = true;
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
            this.metroToolTip.SetToolTip(this.btnSave, resources.GetString("btnSave.ToolTip"));
            this.btnSave.UseSelectable = true;
            this.btnSave.UseStyleColors = true;
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
            this.metroToolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseSelectable = true;
            this.btnEdit.UseStyleColors = true;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Image = global::ERPFramework.Properties.Resources.New32;
            this.btnNew.ImageSize = 32;
            this.btnNew.IsVisible = true;
            this.btnNew.Name = "btnNew";
            this.btnNew.NoFocusImage = global::ERPFramework.Properties.Resources.New32g;
            this.metroToolTip.SetToolTip(this.btnNew, resources.GetString("btnNew.ToolTip"));
            this.btnNew.UseSelectable = true;
            this.btnNew.UseStyleColors = true;
            // 
            // aopAddons
            // 
            resources.ApplyResources(this.aopAddons, "aopAddons");
            this.aopAddons.HorizontalScrollbarBarColor = true;
            this.aopAddons.HorizontalScrollbarHighlightOnWheel = false;
            this.aopAddons.HorizontalScrollbarSize = 10;
            this.aopAddons.Name = "aopAddons";
            this.metroToolTip.SetToolTip(this.aopAddons, resources.GetString("aopAddons.ToolTip"));
            this.aopAddons.UseCustomBackColor = true;
            this.aopAddons.UseStyleColors = true;
            this.aopAddons.VerticalScrollbarBarColor = true;
            this.aopAddons.VerticalScrollbarHighlightOnWheel = false;
            this.aopAddons.VerticalScrollbarSize = 10;
            this.aopAddons.ButtonClick += new System.EventHandler(this.aopAddons_ButtonClick);
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
            this.metroToolbar.Controls.Add(this.btnAddOn);
            this.metroToolbar.Controls.Add(this.btnExit);
            this.metroToolbar.Controls.Add(this.btnSep4);
            this.metroToolbar.Controls.Add(this.btnPref);
            this.metroToolbar.Controls.Add(this.btnSep3);
            this.metroToolbar.Controls.Add(this.btnPrint);
            this.metroToolbar.Controls.Add(this.btnPreview);
            this.metroToolbar.Controls.Add(this.btnSep2);
            this.metroToolbar.Controls.Add(this.btnFilter);
            this.metroToolbar.Controls.Add(this.btnSearch);
            this.metroToolbar.Controls.Add(this.btnSep1);
            this.metroToolbar.Controls.Add(this.btnUndo);
            this.metroToolbar.Controls.Add(this.btnDelete);
            this.metroToolbar.Controls.Add(this.btnSave);
            this.metroToolbar.Controls.Add(this.btnEdit);
            this.metroToolbar.Controls.Add(this.btnNew);
            this.metroToolbar.Name = "metroToolbar";
            this.metroToolTip.SetToolTip(this.metroToolbar, resources.GetString("metroToolbar.ToolTip"));
            this.metroToolbar.UseSelectable = true;
            this.metroToolbar.ItemClicked += new System.EventHandler<MetroFramework.Extender.MetroToolbarButtonType>(this.metroToolbar_ItemClicked);
            this.metroToolbar.ToolStripMenuClick += new System.EventHandler<string>(this.metroToolbar_ToolStripMenuClick);
            // 
            // btnSep4
            // 
            resources.ApplyResources(this.btnSep4, "btnSep4");
            this.btnSep4.ImageSize = 8;
            this.btnSep4.Name = "btnSep4";
            this.metroToolTip.SetToolTip(this.btnSep4, resources.GetString("btnSep4.ToolTip"));
            this.btnSep4.UseSelectable = true;
            // 
            // btnSep3
            // 
            resources.ApplyResources(this.btnSep3, "btnSep3");
            this.btnSep3.ImageSize = 8;
            this.btnSep3.Name = "btnSep3";
            this.metroToolTip.SetToolTip(this.btnSep3, resources.GetString("btnSep3.ToolTip"));
            this.btnSep3.UseSelectable = true;
            // 
            // btnSep2
            // 
            resources.ApplyResources(this.btnSep2, "btnSep2");
            this.btnSep2.ImageSize = 8;
            this.btnSep2.Name = "btnSep2";
            this.metroToolTip.SetToolTip(this.btnSep2, resources.GetString("btnSep2.ToolTip"));
            this.btnSep2.UseSelectable = true;
            // 
            // btnSep1
            // 
            resources.ApplyResources(this.btnSep1, "btnSep1");
            this.btnSep1.ImageSize = 8;
            this.btnSep1.Name = "btnSep1";
            this.metroToolTip.SetToolTip(this.btnSep1, resources.GetString("btnSep1.ToolTip"));
            this.btnSep1.UseSelectable = true;
            this.btnSep1.UseStyleColors = true;
            // 
            // DocumentForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CausesValidation = false;
            this.Controls.Add(this.aopAddons);
            this.Controls.Add(this.metroToolbar);
            this.Name = "DocumentForm";
            this.metroToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.UseStyleColors = true;
            this.metroToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private ERPFramework.Controls.ExMetroToolbar metroToolbar;
        private MetroFramework.Extender.MetroAddOnPanel aopAddons;
        private MetroFramework.Extender.MetroToolbarPushButton btnNew;
        private MetroFramework.Extender.MetroToolbarPushButton btnSave;
        private MetroFramework.Extender.MetroToolbarPushButton btnEdit;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep1;
        private MetroFramework.Extender.MetroToolbarPushButton btnUndo;
        private MetroFramework.Extender.MetroToolbarPushButton btnDelete;
        private MetroFramework.Extender.MetroToolbarPushButton btnFilter;
        private MetroFramework.Extender.MetroToolbarPushButton btnSearch;
        private MetroFramework.Extender.MetroToolbarDropDownButton btnPreview;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep2;
        private MetroFramework.Extender.MetroToolbarDropDownButton btnPrint;
        private MetroFramework.Extender.MetroToolbarPushButton btnAddOn;
        private MetroFramework.Extender.MetroToolbarPushButton btnExit;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep4;
        private MetroFramework.Extender.MetroToolbarPushButton btnPref;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep3;
        protected MetroFramework.Components.MetroToolTip metroToolTip;
    }

}
