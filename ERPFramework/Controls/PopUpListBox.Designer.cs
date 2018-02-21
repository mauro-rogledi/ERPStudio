using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPFramework.Controls
{
    partial class PopUpListBox<T>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.listbox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listbox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbox1.Location = new System.Drawing.Point(0, 0);
            this.listbox1.Name = "listView1";
            this.listbox1.Size = new System.Drawing.Size(280, 88);
            this.listbox1.TabIndex = 0;
            this.listbox1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listbox1.Click += new System.EventHandler(this.listView1_Click);
            this.listbox1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listbox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            this.listbox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
            // 
            // PopUpComboBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(280, 88);
            this.Controls.Add(this.listbox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopUpComboBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PopUpComboBox";
            this.Deactivate += new System.EventHandler(this.PopUpComboBox_Deactivate);
            this.Load += new System.EventHandler(this.PopUpComboBox_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ListBox listbox1;

    }
}
