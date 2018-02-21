using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    [Designer(typeof(MySnapLinesDesigner))]
    public partial class TextFileFolderBrowse : MetroFramework.Controls.MetroTextBox
    {
        public enum BrowseDialog { Folder, Open, Save } ;

        [DefaultValue("All|*.*")]
        public string Filter { get; set; }

        [DefaultValue(BrowseDialog.Folder)]
        public BrowseDialog BrowseMode { get; set; }

        public string HeaderText { get; set; }

        public bool ShowNewFolderButton { get; set; }

        public TextFileFolderBrowse()
        {
            ShowNewFolderButton = false;
            CustomButton.Image = Properties.Resources.Link24;
            CustomButton.Location = new System.Drawing.Point(139, 1);
            ButtonClick += TextFileFolderBrowse_ButtonClick;
        }

        private void TextFileFolderBrowse_ButtonClick(object sender, EventArgs e)
        {
            switch (BrowseMode)
            {
                case BrowseDialog.Folder:
                    FolderMode();
                    break;

                case BrowseDialog.Open:
                    OpenMode();
                    break;

                case BrowseDialog.Save:
                    SaveMode();
                    break;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            switch (BrowseMode)
            {
                case BrowseDialog.Folder:
                    FolderMode();
                    break;

                case BrowseDialog.Open:
                    OpenMode();
                    break;

                case BrowseDialog.Save:
                    SaveMode();
                    break;
            }
        }

        private void FolderMode()
        {
            FolderBrowserDialog fbrDialog = new FolderBrowserDialog();
            fbrDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            fbrDialog.Description = HeaderText;
            fbrDialog.SelectedPath = Text;
            fbrDialog.ShowNewFolderButton = ShowNewFolderButton;

            if (fbrDialog.ShowDialog() == DialogResult.OK)
                Text = fbrDialog.SelectedPath;
        }

        private void OpenMode()
        {
            OpenFileDialog fbrDialog = new OpenFileDialog();
            fbrDialog.AutoUpgradeEnabled = true;
            fbrDialog.CheckFileExists = true;
            fbrDialog.Multiselect = false;
            fbrDialog.Filter = Filter;

            if (fbrDialog.ShowDialog() == DialogResult.OK)
                Text = fbrDialog.FileName;
        }

        private void SaveMode()
        {
            SaveFileDialog fbrDialog = new SaveFileDialog();
            fbrDialog.AutoUpgradeEnabled = true;
            fbrDialog.CheckFileExists = false;
            fbrDialog.Filter = Filter;

            if (fbrDialog.ShowDialog() == DialogResult.OK)
                Text = fbrDialog.FileName;
        }
    }
}