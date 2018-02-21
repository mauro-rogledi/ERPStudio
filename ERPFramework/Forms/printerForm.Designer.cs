namespace ERPFramework.Forms
{
    partial class PrinterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterForm));
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.btnPrint = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnExit = new MetroFramework.Extender.MetroToolbarPushButton();
            this.nudToPage = new MetroFramework.Extender.MetroToolbarNumericUpDown();
            this.nudFromPage = new MetroFramework.Extender.MetroToolbarNumericUpDown();
            this.cbbPages = new MetroFramework.Extender.MetroToolbarComboBox();
            this.btnLf = new MetroFramework.Extender.MetroToolbarButton();
            this.btnRg = new MetroFramework.Extender.MetroToolbarButton();
            this.btnUp = new MetroFramework.Extender.MetroToolbarButton();
            this.btnDn = new MetroFramework.Extender.MetroToolbarButton();
            this.btnZoomIn = new MetroFramework.Extender.MetroToolbarDropDownButton();
            this.btnExport = new MetroFramework.Extender.MetroToolbarPushButton();
            this.btnCollate = new MetroFramework.Extender.MetroToolbarLinkChecked();
            this.nudCopies = new MetroFramework.Extender.MetroToolbarNumericUpDown();
            this.cbbPrinter = new MetroFramework.Extender.MetroToolbarComboBox();
            this.metroToolbar1 = new MetroFramework.Extender.MetroToolbar();
            this.btnSep4 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.lblTo = new MetroFramework.Extender.MetroToolbarLabel();
            this.lblFrom = new MetroFramework.Extender.MetroToolbarLabel();
            this.lblPages = new MetroFramework.Extender.MetroToolbarLabel();
            this.btnSep3 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnLfRg = new MetroFramework.Extender.MetroToolbarContainer();
            this.btnUpDn = new MetroFramework.Extender.MetroToolbarContainer();
            this.btnSep2 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.btnSep1 = new MetroFramework.Extender.MetroToolbarSeparator();
            this.lblCopies = new MetroFramework.Extender.MetroToolbarLabel();
            this.lblOn = new MetroFramework.Extender.MetroToolbarLabel();
            this.metroToolbar1.SuspendLayout();
            this.btnLfRg.SuspendLayout();
            this.btnUpDn.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            resources.ApplyResources(this.crystalReportViewer1, "crystalReportViewer1");
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayStatusBar = false;
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.metroToolTip1.SetToolTip(this.crystalReportViewer1, resources.GetString("crystalReportViewer1.ToolTip"));
            this.crystalReportViewer1.DoubleClickPage += new CrystalDecisions.Windows.Forms.PageMouseEventHandler(this.crystalReportViewer1_DoubleClickPage);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.Image = global::ERPFramework.Properties.Resources.Print32;
            this.btnPrint.ImageSize = 32;
            this.btnPrint.IsVisible = true;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoFocusImage = global::ERPFramework.Properties.Resources.Print32g;
            this.metroToolTip1.SetToolTip(this.btnPrint, resources.GetString("btnPrint.ToolTip"));
            this.btnPrint.UseSelectable = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Image = global::ERPFramework.Properties.Resources.Exit32;
            this.btnExit.ImageSize = 32;
            this.btnExit.IsVisible = true;
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::ERPFramework.Properties.Resources.Exit32g;
            this.metroToolTip1.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // nudToPage
            // 
            resources.ApplyResources(this.nudToPage, "nudToPage");
            this.nudToPage.BackColor = System.Drawing.Color.White;
            this.nudToPage.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.nudToPage.Lines = new string[] {
        "0"};
            this.nudToPage.MaxValue = 99;
            this.nudToPage.MinValue = 0;
            this.nudToPage.Name = "nudToPage";
            this.nudToPage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nudToPage.SelectedText = "";
            this.nudToPage.SelectionLength = 0;
            this.nudToPage.SelectionStart = 0;
            this.nudToPage.ShortcutsEnabled = true;
            this.nudToPage.TabStop = false;
            this.metroToolTip1.SetToolTip(this.nudToPage, resources.GetString("nudToPage.ToolTip"));
            this.nudToPage.UseSelectable = true;
            this.nudToPage.Value = 0;
            // 
            // nudFromPage
            // 
            resources.ApplyResources(this.nudFromPage, "nudFromPage");
            this.nudFromPage.BackColor = System.Drawing.Color.White;
            this.nudFromPage.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.nudFromPage.Lines = new string[] {
        "0"};
            this.nudFromPage.MaxValue = 99;
            this.nudFromPage.MinValue = 0;
            this.nudFromPage.Name = "nudFromPage";
            this.nudFromPage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nudFromPage.SelectedText = "";
            this.nudFromPage.SelectionLength = 0;
            this.nudFromPage.SelectionStart = 0;
            this.nudFromPage.ShortcutsEnabled = true;
            this.nudFromPage.TabStop = false;
            this.metroToolTip1.SetToolTip(this.nudFromPage, resources.GetString("nudFromPage.ToolTip"));
            this.nudFromPage.UseSelectable = true;
            this.nudFromPage.Value = 0;
            // 
            // cbbPages
            // 
            resources.ApplyResources(this.cbbPages, "cbbPages");
            this.cbbPages.BackColor = System.Drawing.Color.White;
            this.cbbPages.Name = "cbbPages";
            this.cbbPages.SelectedText = "";
            this.metroToolTip1.SetToolTip(this.cbbPages, resources.GetString("cbbPages.ToolTip"));
            this.cbbPages.UseSelectable = true;
            this.cbbPages.UseStyleColors = true;
            this.cbbPages.SelectedIndexChanged += new System.EventHandler(this.cbbPages_SelectedIndexChanged);
            // 
            // btnLf
            // 
            resources.ApplyResources(this.btnLf, "btnLf");
            this.btnLf.Image = global::ERPFramework.Properties.Resources.SortLeft32;
            this.btnLf.ImageSize = 32;
            this.btnLf.IsVisible = true;
            this.btnLf.Name = "btnLf";
            this.btnLf.NoFocusImage = global::ERPFramework.Properties.Resources.SortLeft32g;
            this.metroToolTip1.SetToolTip(this.btnLf, resources.GetString("btnLf.ToolTip"));
            this.btnLf.UseSelectable = true;
            this.btnLf.UseStyleColors = true;
            this.btnLf.Click += new System.EventHandler(this.btnPageFirst_Click);
            // 
            // btnRg
            // 
            resources.ApplyResources(this.btnRg, "btnRg");
            this.btnRg.Image = global::ERPFramework.Properties.Resources.SortRight32;
            this.btnRg.ImageSize = 32;
            this.btnRg.IsVisible = true;
            this.btnRg.Name = "btnRg";
            this.btnRg.NoFocusImage = global::ERPFramework.Properties.Resources.SortRight32g;
            this.metroToolTip1.SetToolTip(this.btnRg, resources.GetString("btnRg.ToolTip"));
            this.btnRg.UseSelectable = true;
            this.btnRg.Click += new System.EventHandler(this.btnPageLast_Click);
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Image = global::ERPFramework.Properties.Resources.SortUp32;
            this.btnUp.ImageSize = 32;
            this.btnUp.IsVisible = true;
            this.btnUp.Name = "btnUp";
            this.btnUp.NoFocusImage = global::ERPFramework.Properties.Resources.SortUp32g;
            this.metroToolTip1.SetToolTip(this.btnUp, resources.GetString("btnUp.ToolTip"));
            this.btnUp.UseSelectable = true;
            this.btnUp.UseStyleColors = true;
            this.btnUp.Click += new System.EventHandler(this.btnPageUp_Click);
            // 
            // btnDn
            // 
            resources.ApplyResources(this.btnDn, "btnDn");
            this.btnDn.Image = global::ERPFramework.Properties.Resources.SortDown32;
            this.btnDn.ImageSize = 32;
            this.btnDn.IsVisible = true;
            this.btnDn.Name = "btnDn";
            this.btnDn.NoFocusImage = global::ERPFramework.Properties.Resources.SortDown32g;
            this.metroToolTip1.SetToolTip(this.btnDn, resources.GetString("btnDn.ToolTip"));
            this.btnDn.UseSelectable = true;
            this.btnDn.Click += new System.EventHandler(this.btnPageDn_Click);
            // 
            // btnZoomIn
            // 
            resources.ApplyResources(this.btnZoomIn, "btnZoomIn");
            this.btnZoomIn.Image = global::ERPFramework.Properties.Resources.ZoomIn32;
            this.btnZoomIn.ImageSize = 32;
            this.btnZoomIn.IsVisible = true;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.NoFocusImage = global::ERPFramework.Properties.Resources.ZoomIn32g;
            this.metroToolTip1.SetToolTip(this.btnZoomIn, resources.GetString("btnZoomIn.ToolTip"));
            this.btnZoomIn.UseSelectable = true;
            this.btnZoomIn.DropDownItemClicked += new System.EventHandler(this.btnZoomIn_DropDownItemClicked);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Image = global::ERPFramework.Properties.Resources.Download32;
            this.btnExport.ImageSize = 32;
            this.btnExport.IsVisible = true;
            this.btnExport.Name = "btnExport";
            this.btnExport.NoFocusImage = global::ERPFramework.Properties.Resources.Download32g;
            this.metroToolTip1.SetToolTip(this.btnExport, resources.GetString("btnExport.ToolTip"));
            this.btnExport.UseSelectable = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCollate
            // 
            resources.ApplyResources(this.btnCollate, "btnCollate");
            this.btnCollate.CheckedImage = global::ERPFramework.Properties.Resources.CollateK32;
            this.btnCollate.CheckOnClick = true;
            this.btnCollate.Image = global::ERPFramework.Properties.Resources.Collate32;
            this.btnCollate.ImageSize = 32;
            this.btnCollate.IsVisible = true;
            this.btnCollate.Name = "btnCollate";
            this.btnCollate.NoFocusCheckedImage = global::ERPFramework.Properties.Resources.CollateK32g;
            this.btnCollate.NoFocusImage = global::ERPFramework.Properties.Resources.Collate32g;
            this.metroToolTip1.SetToolTip(this.btnCollate, resources.GetString("btnCollate.ToolTip"));
            this.btnCollate.UseSelectable = true;
            // 
            // nudCopies
            // 
            resources.ApplyResources(this.nudCopies, "nudCopies");
            this.nudCopies.BackColor = System.Drawing.Color.White;
            this.nudCopies.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.nudCopies.Lines = new string[] {
        "0"};
            this.nudCopies.MaxValue = 99;
            this.nudCopies.MinValue = 0;
            this.nudCopies.Name = "nudCopies";
            this.nudCopies.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nudCopies.SelectedText = "";
            this.nudCopies.SelectionLength = 0;
            this.nudCopies.SelectionStart = 0;
            this.nudCopies.ShortcutsEnabled = true;
            this.nudCopies.TabStop = false;
            this.metroToolTip1.SetToolTip(this.nudCopies, resources.GetString("nudCopies.ToolTip"));
            this.nudCopies.UseSelectable = true;
            this.nudCopies.UseStyleColors = true;
            this.nudCopies.Value = 0;
            // 
            // cbbPrinter
            // 
            resources.ApplyResources(this.cbbPrinter, "cbbPrinter");
            this.cbbPrinter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbbPrinter.Name = "cbbPrinter";
            this.cbbPrinter.SelectedText = "";
            this.metroToolTip1.SetToolTip(this.cbbPrinter, resources.GetString("cbbPrinter.ToolTip"));
            this.cbbPrinter.UseSelectable = true;
            // 
            // metroToolbar1
            // 
            resources.ApplyResources(this.metroToolbar1, "metroToolbar1");
            this.metroToolbar1.Controls.Add(this.btnExit);
            this.metroToolbar1.Controls.Add(this.btnSep4);
            this.metroToolbar1.Controls.Add(this.nudToPage);
            this.metroToolbar1.Controls.Add(this.lblTo);
            this.metroToolbar1.Controls.Add(this.nudFromPage);
            this.metroToolbar1.Controls.Add(this.lblFrom);
            this.metroToolbar1.Controls.Add(this.cbbPages);
            this.metroToolbar1.Controls.Add(this.lblPages);
            this.metroToolbar1.Controls.Add(this.btnSep3);
            this.metroToolbar1.Controls.Add(this.btnLfRg);
            this.metroToolbar1.Controls.Add(this.btnUpDn);
            this.metroToolbar1.Controls.Add(this.btnZoomIn);
            this.metroToolbar1.Controls.Add(this.btnSep2);
            this.metroToolbar1.Controls.Add(this.btnExport);
            this.metroToolbar1.Controls.Add(this.btnSep1);
            this.metroToolbar1.Controls.Add(this.btnCollate);
            this.metroToolbar1.Controls.Add(this.nudCopies);
            this.metroToolbar1.Controls.Add(this.lblCopies);
            this.metroToolbar1.Controls.Add(this.cbbPrinter);
            this.metroToolbar1.Controls.Add(this.lblOn);
            this.metroToolbar1.Controls.Add(this.btnPrint);
            this.metroToolbar1.Name = "metroToolbar1";
            this.metroToolTip1.SetToolTip(this.metroToolbar1, resources.GetString("metroToolbar1.ToolTip"));
            this.metroToolbar1.UseSelectable = true;
            this.metroToolbar1.UseStyleColors = true;
            // 
            // btnSep4
            // 
            resources.ApplyResources(this.btnSep4, "btnSep4");
            this.btnSep4.ImageSize = 32;
            this.btnSep4.Name = "btnSep4";
            this.metroToolTip1.SetToolTip(this.btnSep4, resources.GetString("btnSep4.ToolTip"));
            this.btnSep4.UseSelectable = true;
            // 
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblTo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblTo.Name = "lblTo";
            this.metroToolTip1.SetToolTip(this.lblTo, resources.GetString("lblTo.ToolTip"));
            this.lblTo.UseStyleColors = true;
            // 
            // lblFrom
            // 
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblFrom.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblFrom.Name = "lblFrom";
            this.metroToolTip1.SetToolTip(this.lblFrom, resources.GetString("lblFrom.ToolTip"));
            this.lblFrom.UseStyleColors = true;
            // 
            // lblPages
            // 
            resources.ApplyResources(this.lblPages, "lblPages");
            this.lblPages.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblPages.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblPages.Name = "lblPages";
            this.metroToolTip1.SetToolTip(this.lblPages, resources.GetString("lblPages.ToolTip"));
            this.lblPages.UseStyleColors = true;
            // 
            // btnSep3
            // 
            resources.ApplyResources(this.btnSep3, "btnSep3");
            this.btnSep3.ImageSize = 32;
            this.btnSep3.Name = "btnSep3";
            this.metroToolTip1.SetToolTip(this.btnSep3, resources.GetString("btnSep3.ToolTip"));
            this.btnSep3.UseSelectable = true;
            // 
            // btnLfRg
            // 
            resources.ApplyResources(this.btnLfRg, "btnLfRg");
            this.btnLfRg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLfRg.Controls.Add(this.btnLf);
            this.btnLfRg.Controls.Add(this.btnRg);
            this.btnLfRg.Name = "btnLfRg";
            this.metroToolTip1.SetToolTip(this.btnLfRg, resources.GetString("btnLfRg.ToolTip"));
            this.btnLfRg.UseSelectable = true;
            this.btnLfRg.UseStyleColors = true;
            // 
            // btnUpDn
            // 
            resources.ApplyResources(this.btnUpDn, "btnUpDn");
            this.btnUpDn.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUpDn.Controls.Add(this.btnUp);
            this.btnUpDn.Controls.Add(this.btnDn);
            this.btnUpDn.Name = "btnUpDn";
            this.metroToolTip1.SetToolTip(this.btnUpDn, resources.GetString("btnUpDn.ToolTip"));
            this.btnUpDn.UseSelectable = true;
            this.btnUpDn.UseStyleColors = true;
            // 
            // btnSep2
            // 
            resources.ApplyResources(this.btnSep2, "btnSep2");
            this.btnSep2.ImageSize = 32;
            this.btnSep2.Name = "btnSep2";
            this.metroToolTip1.SetToolTip(this.btnSep2, resources.GetString("btnSep2.ToolTip"));
            this.btnSep2.UseSelectable = true;
            // 
            // btnSep1
            // 
            resources.ApplyResources(this.btnSep1, "btnSep1");
            this.btnSep1.ImageSize = 32;
            this.btnSep1.Name = "btnSep1";
            this.metroToolTip1.SetToolTip(this.btnSep1, resources.GetString("btnSep1.ToolTip"));
            this.btnSep1.UseSelectable = true;
            // 
            // lblCopies
            // 
            resources.ApplyResources(this.lblCopies, "lblCopies");
            this.lblCopies.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblCopies.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblCopies.Name = "lblCopies";
            this.metroToolTip1.SetToolTip(this.lblCopies, resources.GetString("lblCopies.ToolTip"));
            this.lblCopies.UseStyleColors = true;
            // 
            // lblOn
            // 
            resources.ApplyResources(this.lblOn, "lblOn");
            this.lblOn.FontSize = MetroFramework.MetroLabelSize.Small;
            this.lblOn.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblOn.Name = "lblOn";
            this.metroToolTip1.SetToolTip(this.lblOn, resources.GetString("lblOn.ToolTip"));
            this.lblOn.UseStyleColors = true;
            // 
            // PrinterForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.metroToolbar1);
            this.Name = "PrinterForm";
            this.metroToolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.UseStyleColors = true;
            this.metroToolbar1.ResumeLayout(false);
            this.btnLfRg.ResumeLayout(false);
            this.btnUpDn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Extender.MetroToolbar metroToolbar1;
        private MetroFramework.Extender.MetroToolbarPushButton btnPrint;
        private MetroFramework.Extender.MetroToolbarComboBox cbbPrinter;
        private MetroFramework.Extender.MetroToolbarNumericUpDown nudCopies;
        private MetroFramework.Extender.MetroToolbarLinkChecked btnCollate;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep1;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep3;
        private MetroFramework.Extender.MetroToolbarDropDownButton btnZoomIn;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep2;
        private MetroFramework.Extender.MetroToolbarPushButton btnExport;
        private MetroFramework.Extender.MetroToolbarLabel lblPages;
        private MetroFramework.Extender.MetroToolbarLabel lblCopies;
        private MetroFramework.Extender.MetroToolbarLabel lblOn;
        private MetroFramework.Extender.MetroToolbarContainer btnUpDn;
        private MetroFramework.Extender.MetroToolbarContainer btnLfRg;
        private MetroFramework.Extender.MetroToolbarButton btnUp;
        private MetroFramework.Extender.MetroToolbarButton btnDn;
        private MetroFramework.Extender.MetroToolbarButton btnLf;
        private MetroFramework.Extender.MetroToolbarButton btnRg;
        private MetroFramework.Extender.MetroToolbarComboBox cbbPages;
        private MetroFramework.Extender.MetroToolbarLabel lblFrom;
        private MetroFramework.Extender.MetroToolbarNumericUpDown nudToPage;
        private MetroFramework.Extender.MetroToolbarLabel lblTo;
        private MetroFramework.Extender.MetroToolbarNumericUpDown nudFromPage;
        private MetroFramework.Extender.MetroToolbarPushButton btnExit;
        private MetroFramework.Extender.MetroToolbarSeparator btnSep4;
    }
}