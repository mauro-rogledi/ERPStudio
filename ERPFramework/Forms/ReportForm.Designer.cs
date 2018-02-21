namespace ERPFramework.Forms
{
    partial class ReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.tbcMain = new MetroFramework.Controls.MetroTabControl();
            this.tbpSelection = new MetroFramework.Controls.MetroTabPage();
            this.metroToolbar = new ERPFramework.Controls.ExMetroToolbar();
            this.btnExit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSep2 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnPref = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnSep1 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnPrint = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnPreview = new MetroFramework.Extender.MetroToolbarPushButton();
            this.tbpPrint = new MetroFramework.Controls.MetroTabPage();
            this.printerForm = new ERPFramework.Forms.PrinterForm();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.tbcMain.SuspendLayout();
            this.tbpSelection.SuspendLayout();
            this.metroToolbar.SuspendLayout();
            this.tbpPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpSelection);
            this.tbcMain.Controls.Add(this.tbpPrint);
            resources.ApplyResources(this.tbcMain, "tbcMain");
            this.tbcMain.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 1;
            this.tbcMain.UseSelectable = true;
            this.tbcMain.UseStyleColors = true;
            // 
            // tbpSelection
            // 
            this.tbpSelection.Controls.Add(this.metroToolbar);
            this.tbpSelection.HorizontalScrollbarBarColor = true;
            this.tbpSelection.HorizontalScrollbarHighlightOnWheel = false;
            this.tbpSelection.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.tbpSelection, "tbpSelection");
            this.tbpSelection.Name = "tbpSelection";
            this.tbpSelection.UseStyleColors = true;
            this.tbpSelection.VerticalScrollbarBarColor = true;
            this.tbpSelection.VerticalScrollbarHighlightOnWheel = false;
            this.tbpSelection.VerticalScrollbarSize = 10;
            // 
            // metroToolbar
            // 
            this.metroToolbar.CausesValidation = false;
            this.metroToolbar.Controls.Add(this.btnExit);
            this.metroToolbar.Controls.Add(this.btnSep2);
            this.metroToolbar.Controls.Add(this.btnPref);
            this.metroToolbar.Controls.Add(this.btnSep1);
            this.metroToolbar.Controls.Add(this.btnPrint);
            this.metroToolbar.Controls.Add(this.btnPreview);
            resources.ApplyResources(this.metroToolbar, "metroToolbar");
            this.metroToolbar.Name = "metroToolbar";
            this.metroToolbar.UseSelectable = true;
            this.metroToolbar.ItemClicked += new System.EventHandler<MetroFramework.Extender.MetroToolbarButtonType>(this.metroToolbar_ItemClicked);
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
            this.metroToolTip1.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            // 
            // btnSep2
            // 
            resources.ApplyResources(this.btnSep2, "btnSep2");
            this.btnSep2.ImageSize = 32;
            this.btnSep2.IsVisible = true;
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
            this.metroToolTip1.SetToolTip(this.btnPref, resources.GetString("btnPref.ToolTip"));
            this.btnPref.UseSelectable = true;
            // 
            // btnSep1
            // 
            resources.ApplyResources(this.btnSep1, "btnSep1");
            this.btnSep1.ImageSize = 32;
            this.btnSep1.IsVisible = true;
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.UseSelectable = true;
            // 
            // btnPrint
            // 
            this.btnPrint.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Print;
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Image = global::ERPFramework.Properties.Resources.Print32;
            this.btnPrint.ImageSize = 32;
            this.btnPrint.IsVisible = true;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoFocusImage = global::ERPFramework.Properties.Resources.Print32g;
            this.metroToolTip1.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseSelectable = true;
            // 
            // btnPreview
            // 
            this.btnPreview.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Preview;
            resources.ApplyResources(this.btnPreview, "btnPreview");
            this.btnPreview.Image = global::ERPFramework.Properties.Resources.Preview32;
            this.btnPreview.ImageSize = 32;
            this.btnPreview.IsVisible = true;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.NoFocusImage = global::ERPFramework.Properties.Resources.Preview32g;
            this.metroToolTip1.SetToolTip(this.btnPreview, resources.GetString("btnPreview.ToolTip"));
            this.btnPreview.UseSelectable = true;
            // 
            // tbpPrint
            // 
            this.tbpPrint.Controls.Add(this.printerForm);
            this.tbpPrint.HorizontalScrollbarBarColor = true;
            this.tbpPrint.HorizontalScrollbarHighlightOnWheel = false;
            this.tbpPrint.HorizontalScrollbarSize = 10;
            resources.ApplyResources(this.tbpPrint, "tbpPrint");
            this.tbpPrint.Name = "tbpPrint";
            this.tbpPrint.UseStyleColors = true;
            this.tbpPrint.UseVisualStyleBackColor = true;
            this.tbpPrint.VerticalScrollbarBarColor = true;
            this.tbpPrint.VerticalScrollbarHighlightOnWheel = false;
            this.tbpPrint.VerticalScrollbarSize = 10;
            // 
            // printerForm
            // 
            resources.ApplyResources(this.printerForm, "printerForm");
            this.printerForm.Name = "printerForm";
            this.printerForm.Title = null;
            this.printerForm.UseSelectable = true;
            this.printerForm.UseStyleColors = true;
            this.printerForm.Exit += new System.EventHandler(this.printerForm_Exit);
            this.printerForm.DoubleClickPage += new System.EventHandler<CrystalDecisions.Windows.Forms.PageMouseEventArgs>(this.crystalReportViewer1_DoubleClickPage);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ReportForm
            // 
            this.Controls.Add(this.tbcMain);
            resources.ApplyResources(this, "$this");
            this.Name = "ReportForm";
            this.UseStyleColors = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.formDB_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.formDB_KeyUp);
            this.tbcMain.ResumeLayout(false);
            this.tbpSelection.ResumeLayout(false);
            this.metroToolbar.ResumeLayout(false);
            this.tbpPrint.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        protected MetroFramework.Controls.MetroTabControl tbcMain;
        protected MetroFramework.Controls.MetroTabPage tbpSelection;
        protected MetroFramework.Controls.MetroTabPage tbpPrint;
        private PrinterForm printerForm;
        private ERPFramework.Controls.ExMetroToolbar metroToolbar;
        private MetroFramework.Extender.MetroToolbarPushButton btnExit;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep2;
        private MetroFramework.Extender.MetroToolbarPushButton btnPref;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep1;
        private MetroFramework.Extender.MetroToolbarPushButton btnPrint;
        private MetroFramework.Extender.MetroToolbarPushButton btnPreview;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }

}
