using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ERPFramework.Preferences
{
    partial class PreferenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferenceForm));
            this.btnExit = new MetroFramework.Controls.MetroLink();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.preferenceContainer1 = new ERPFramework.Preferences.PreferenceContainer();
            ((System.ComponentModel.ISupportInitialize)(this.preferenceContainer1)).BeginInit();
            this.preferenceContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Image = global::ERPFramework.Properties.Resources.Shutdown32;
            this.btnExit.ImageSize = 32;
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::ERPFramework.Properties.Resources.Shutdown32g;
            this.metroToolTip1.SetToolTip(this.btnExit, resources.GetString("btnExit.ToolTip"));
            this.btnExit.UseSelectable = true;
            this.btnExit.UseStyleColors = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // preferenceContainer1
            // 
            resources.ApplyResources(this.preferenceContainer1, "preferenceContainer1");
            this.preferenceContainer1.Name = "preferenceContainer1";
            // 
            // preferenceContainer1.Panel1
            // 
            this.preferenceContainer1.Panel1.BackColor = System.Drawing.Color.White;
            // 
            // PreferenceForm
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.preferenceContainer1);
            this.Controls.Add(this.btnExit);
            this.Name = "PreferenceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.preferenceContainer1)).EndInit();
            this.preferenceContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private MetroFramework.Controls.MetroLink btnExit;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private PreferenceContainer preferenceContainer1;
    }

}
