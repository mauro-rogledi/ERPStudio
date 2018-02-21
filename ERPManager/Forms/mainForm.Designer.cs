using System;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace ERPManager.Forms
{
    partial class frmMain
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            this.btnExit = new MetroFramework.Controls.MetroLink();
            this.btnSetting = new MetroFramework.Controls.MetroLink();
            this.btnCalendar = new MetroFramework.Controls.MetroLink();
            this.userControlHelper = new ERPFramework.ModulesHelper.OpenControlHelper();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.pnlInfo = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.pInfoImg = new System.Windows.Forms.PictureBox();
            this.pInfoTxt = new MetroFramework.Controls.MetroLabel();
            this.menuControl = new ERPManager.MenuManager.MenuControl();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userControlHelper)).BeginInit();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pInfoImg)).BeginInit();
            this.SuspendLayout();
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            this.metroStyleManager.Style = MetroFramework.MetroColorStyle.Pink;
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Image = global::ERPManager.Properties.Resources.Shutdown32;
            this.btnExit.ImageSize = 32;
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::ERPManager.Properties.Resources.Shutdown32g;
            this.metroToolTip1.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            this.btnExit.UseStyleColors = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSetting
            // 
            resources.ApplyResources(this.btnSetting, "btnSetting");
            this.btnSetting.Image = global::ERPManager.Properties.Resources.Setting32;
            this.btnSetting.ImageSize = 32;
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.NoFocusImage = global::ERPManager.Properties.Resources.Setting32g;
            this.metroToolTip1.SetToolTip(this.btnSetting, resources.GetString("btnSetting.ToolTip"));
            this.btnSetting.UseSelectable = true;
            this.btnSetting.UseStyleColors = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnCalendar
            // 
            resources.ApplyResources(this.btnCalendar, "btnCalendar");
            this.btnCalendar.Image = global::ERPManager.Properties.Resources.Calendar32;
            this.btnCalendar.ImageSize = 32;
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.NoFocusImage = global::ERPManager.Properties.Resources.Calendar32G;
            this.metroToolTip1.SetToolTip(this.btnCalendar, resources.GetString("btnCalendar.ToolTip"));
            this.btnCalendar.UseSelectable = true;
            this.btnCalendar.UseStyleColors = true;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // userControlHelper
            // 
            this.userControlHelper.Owner = this;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.pInfoTxt);
            this.pnlInfo.Controls.Add(this.pInfoImg);
            resources.ApplyResources(this.pnlInfo, "pnlInfo");
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.UseCustomBackColor = true;
            this.pnlInfo.UseStyleColors = true;
            // 
            // pInfoImg
            // 
            resources.ApplyResources(this.pInfoImg, "pInfoImg");
            this.pInfoImg.Name = "pInfoImg";
            this.pInfoImg.TabStop = false;
            // 
            // pInfoTxt
            // 
            resources.ApplyResources(this.pInfoTxt, "pInfoTxt");
            this.pInfoTxt.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.pInfoTxt.Name = "pInfoTxt";
            this.pInfoTxt.UseStyleColors = true;
            // 
            // menuControl
            // 
            resources.ApplyResources(this.menuControl, "menuControl");
            this.menuControl.Name = "menuControl";
            this.menuControl.UseSelectable = true;
            this.menuControl.UseStyleColors = true;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.menuControl);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.btnCalendar);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnExit);
            this.Name = "frmMain";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Pink;
            this.StyleManager = this.metroStyleManager;
            this.TransparencyKey = System.Drawing.Color.Empty;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userControlHelper)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pInfoImg)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private MetroFramework.Components.MetroStyleManager metroStyleManager;
        private MenuManager.MenuControl menuControl;
        private MetroFramework.Controls.MetroLink btnCalendar;
        private MetroFramework.Controls.MetroLink btnSetting;
        private MetroFramework.Controls.MetroLink btnExit;
        private ERPFramework.ModulesHelper.OpenControlHelper userControlHelper;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Extender.MetroFlowLayoutPanel pnlInfo;
        private PictureBox pInfoImg;
        private MetroFramework.Controls.MetroLabel pInfoTxt;
    }
}
