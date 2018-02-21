using System;
using System.Windows.Forms;

namespace ERPFramework.Emailer
{
    public partial class emailAddress : MetroFramework.Forms.MetroForm
    {
        public string Address { get; private set; }

        public emailAddress()
        {
            InitializeComponent();
        }

        public void ClearData()
        {
            lsvAddress.Items.Clear();
        }

        public void AddAddress(string type, string name, string email)
        {
            ListViewItem lvi = new ListViewItem(type);
            lvi.SubItems.Add(name);
            lvi.SubItems.Add(email);

            lsvAddress.Items.Add(lvi);
        }

        public void SetColumnSize()
        {
            foreach (ColumnHeader col in lsvAddress.Columns)
                col.Width = -1;
        }

        private void lsvAddress_DoubleClick(object sender, EventArgs e)
        {
            Address = lsvAddress.SelectedItems[0].SubItems[2].Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Address = lsvAddress.SelectedItems[0].SubItems[2].Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Address = string.Empty;
        }
    }
}