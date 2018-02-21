using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ERPFramework.Controls;

namespace ERPFramework.Forms
{
    partial class DocumentBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentBase));
            this.aopAddons = new MetroFramework.Extender.MetroAddOnPanel();
            this.SuspendLayout();
            // 
            // aopAddons
            // 
            resources.ApplyResources(this.aopAddons, "aopAddons");
            this.aopAddons.HorizontalScrollbarBarColor = true;
            this.aopAddons.HorizontalScrollbarHighlightOnWheel = false;
            this.aopAddons.HorizontalScrollbarSize = 10;
            this.aopAddons.Name = "aopAddons";
            this.aopAddons.UseCustomBackColor = true;
            this.aopAddons.UseStyleColors = true;
            this.aopAddons.VerticalScrollbarBarColor = true;
            this.aopAddons.VerticalScrollbarHighlightOnWheel = false;
            this.aopAddons.VerticalScrollbarSize = 10;
            this.aopAddons.ButtonClick += new System.EventHandler(this.aopAddons_ButtonClick);
            // 
            // DocumentBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.aopAddons);
            resources.ApplyResources(this, "$this");
            this.Name = "DocumentBase";
            this.UseStyleColors = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.formDB_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.formDB_KeyUp);
            this.ResumeLayout(false);

        }
        #endregion
        private MetroFramework.Extender.MetroAddOnPanel aopAddons;
    }

}
