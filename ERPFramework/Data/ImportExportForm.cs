using System;
using System.ComponentModel;

namespace ERPFramework.Data
{
    public partial class ImportExportForm : MetroFramework.Forms.MetroForm
    {
        public ImportExportForm()
        {
            InitializeComponent();
            cbbProvider.SelectedIndex = 1;
            ImportExportData.RowToElaborate += ImportExportData_RowToElaborate;
            ImportExportData.RowElaborated += ImportExportData_RowElaborated;
        }


        private void wizard1_AfterSwitchPages(object sender, Controls.Wizard.AfterSwitchPagesEventArgs e)
        {
            switch (e.NewIndex)
            {
                case 0:
                    wizard1.NextText = "Next";
                    wizard1.CancelEnabled = true;
                    break;
                case 1:
                case 2:
                    wizard1.NextText = "Elaborate";
                    wizard1.CancelEnabled = true;
                    break;
                case 3:
                    wizard1.CancelEnabled = false;
                    wizard1.NextText = "Finish";
                    System.Windows.Forms.Application.DoEvents();
                    Elaborate();
                    break;
            }
        }

        private void ImportExportData_RowToElaborate(object sender, string tablename)
        {
            var lvi = lsvResult.Items.Add(tablename);
            lvi.EnsureVisible();
            System.Windows.Forms.Application.DoEvents();
        }

        private void ImportExportData_RowElaborated(object sender, Tuple<int, bool> row)
        {
            var index = lsvResult.Items.Count - 1;
            lsvResult.Items[index].SubItems.Add(row.Item1.ToString());
            System.Windows.Forms.Application.DoEvents();
        }

        private void Elaborate()
        {
            if (cbbProvider.SelectedIndex == 1)
                ImportExportData.ExportData(txtExport.Text);
            else
                ImportExportData.ImportData(txtImport.Text, ckbClean.Checked);

            wizard1.CancelEnabled = true;
        }

        private void wizard1_BeforeSwitchPages(object sender, Controls.Wizard.BeforeSwitchPagesEventArgs e)
        {
            if (e.OldIndex < e.NewIndex)
            {
                switch (e.OldIndex)
                {
                    case 0:
                        if (cbbProvider.SelectedIndex == 0)
                            e.NewIndex = 2;
                        break;
                    case 1:
                        if (FolderNotOk(e.OldIndex))
                            e.NewIndex = e.OldIndex;
                        else
                            e.NewIndex = 3;
                        break;
                    case 2:
                        if (FolderNotOk(e.OldIndex))
                            e.NewIndex = e.OldIndex;
                        break;
                }
            }
            else
                if (e.OldIndex == 2)
                e.NewIndex = 0;
        }

        private bool FolderNotOk(int page)
        {
            var folder = string.Empty;
            switch (page)
            {
                case 1:
                    folder = txtExport.Text;
                    break;
                case 2:
                    folder = txtImport.Text;
                    break;
            }

            if (folder.IsEmpty())
                return true;

            return !System.IO.Directory.Exists(folder);
        }

        private void wizard1_Finish(object sender, CancelEventArgs e)
        {
        }

        private void txtExport_ButtonClick(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtExport.Text = folderBrowserDialog.SelectedPath;
        }

        private void txtImport_ButtonClick(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtImport.Text = folderBrowserDialog.SelectedPath;
        }
    }
}
