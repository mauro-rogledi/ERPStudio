using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    /// <summary>
    /// Summary description for PopUpComboBox.
    /// </summary>
    public class PopUpComboBox : MetroFramework.Forms.MetroForm
    {
        private System.Windows.Forms.ListView listView1;
        private string select = string.Empty;
        private SortedList<string, string> SL;

        public delegate void AfterRowSelectEventHandler(object sender, string SelectedValue);

        public event AfterRowSelectEventHandler AfterRowSelectEvent;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public PopUpComboBox(SortedList<string, string> _sl, string _sel)
        {
            InitializeComponent();
            SL = _sl;
            select = _sel;

            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Code", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Description", -2, HorizontalAlignment.Left);

            for (int i = 0; i < SL.Count; i++)
            {
                ListViewItem item = new ListViewItem(SL.Keys[i]);
                item.SubItems.Add(SL.Values[i].ToString());
                listView1.Items.Add(item);
                if (SL.Keys[i] == _sel)
                    item.Selected = true;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            int height = this.Height;

            for (int t = 0; t < height; t += 4)
            {
                this.Height = t;
                Application.DoEvents();
            }
        }

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
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();

            //
            // listView1
            //
            this.listView1.AllowColumnReorder = true;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(280, 88);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 0;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.Click += new EventHandler(listView1_Click);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            this.listView1.MouseMove += new MouseEventHandler(listView1_MouseMove);

            //
            // PopUpComboBox
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(280, 88);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopUpComboBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PopUpComboBox";
            this.Load += new System.EventHandler(this.PopUpComboBox_Load);
            this.Deactivate += new System.EventHandler(this.PopUpComboBox_Deactivate);
            this.ResumeLayout(false);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (AfterRowSelectEvent != null && listView1.SelectedItems.Count > 0)
                AfterRowSelectEvent(this, listView1.SelectedItems[0].Text);

            this.Close();
        }

        #endregion

        private void listView1_DoubleClick(object sender, System.EventArgs e)
        {
            if (AfterRowSelectEvent != null && listView1.SelectedItems.Count > 0)
                AfterRowSelectEvent(this, listView1.SelectedItems[0].Text);

            this.Close();
        }

        private void PopUpComboBox_Deactivate(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void PopUpComboBox_Load(object sender, EventArgs e)
        {
            listView1.Focus();
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            int idx = listView1.Items.IndexOf(listView1.GetItemAt(e.X, e.Y));

            //int idx = listView1.IndexFromPoint(e.X, e.Y);
            if (idx >= 0)
                listView1.Items[idx].Selected = true;
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.Enter)
            {
                if (AfterRowSelectEvent != null && listView1.SelectedItems.Count > 0)
                    AfterRowSelectEvent(this, listView1.SelectedItems[0].Text);

                this.Close();
            }
        }
    }
}