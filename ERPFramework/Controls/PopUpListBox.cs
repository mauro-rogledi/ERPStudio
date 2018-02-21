using ERPFramework.Libraries;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    /// <summary>
    /// Summary description for PopUpComboBox.
    /// </summary>
    public partial class PopUpListBox<T> : System.Windows.Forms.Form
    {
        private string select = string.Empty;
        private List<GenericList<T>> SL;

        public delegate void AfterRowSelectEventHandler(object sender, string SelectedValue);

        public event AfterRowSelectEventHandler AfterRowSelectEvent;

        public PopUpListBox(List<GenericList<T>> _sl, string _sel, float maxlen)
        {
            InitializeComponent();
            SL = _sl;
            select = _sel;

            this.Width = (int)maxlen;

            listbox1.DataSource = _sl;
            listbox1.DisplayMember = "Display";
            listbox1.ValueMember = "Archive";
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

        private void listView1_DoubleClick(object sender, System.EventArgs e)
        {
            if (AfterRowSelectEvent != null && listbox1.SelectedItems.Count > 0)
                AfterRowSelectEvent(this, listbox1.SelectedItem.ToString());

            this.Close();
        }

        private void PopUpComboBox_Deactivate(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void PopUpComboBox_Load(object sender, EventArgs e)
        {
            listbox1.Focus();
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.Enter)
            {
                if (AfterRowSelectEvent != null && listbox1.SelectedItems.Count > 0)
                    AfterRowSelectEvent(this, listbox1.SelectedItem.ToString());

                this.Close();
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            int idx = listbox1.IndexFromPoint(e.X, e.Y);
            if (idx >= 0)
                listbox1.SelectedIndex = idx;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (AfterRowSelectEvent != null && listbox1.SelectedItems.Count > 0)
                AfterRowSelectEvent(this, listbox1.SelectedItem.ToString());

            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}