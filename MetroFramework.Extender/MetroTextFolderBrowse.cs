using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public partial class MetroTextFileFolderBrowse : MetroFramework.Controls.MetroTextBox
    {
        public enum BrowseDialog { Folder, Open, Save } ;

        [DefaultValue("All|*.*")]
        [Description("c# files|*.cs|All files|*.*")]
        [Localizable(true)]
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public string Filter { get; set; }

        [DefaultValue(BrowseDialog.Folder)]
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public BrowseDialog BrowseMode { get; set; } = BrowseDialog.Folder;

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [Localizable(true)]
        public string HeaderText { get; set; }

        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool ShowNewFolderButton { get; set; } = false;

        public MetroTextFileFolderBrowse()
        {
            ShowNewFolderButton = false;
            CustomButton.Location = new System.Drawing.Point(139, 1);
            CustomButton.Image = Properties.Resources.Search16;
            ShowButton = true;
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