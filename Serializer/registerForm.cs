using ERPFramework;
using ERPFramework.ModulesHelper;
using MetroFramework.Extender;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Serializer
{
    public partial class registerForm : MetroFramework.Forms.MetroForm
    {
        public registerForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            txtMac.Text = SerialManager.ReadMacAddress();
            base.OnLoad(e);
        }

        private void LoadDataFromBinary()
        {
            SerialManager.Load();
            DataGridView1.Rows.Clear();

            foreach (SerialModule sd in SerialManager.SerialData.Modules)
            {
                var row = DataGridView1.Rows.Add();
                DataGridView1.Rows[row].Cells["colenable"].Value = sd.Enable == bool.TrueString;
                DataGridView1.Rows[row].Cells[nameof(colLicenseType)].Value = sd.SerialTypeString;
                DataGridView1.Rows[row].Cells[nameof(colApplication)].Value = sd.Application;
                DataGridView1.Rows[row].Cells[nameof(colModuleName)].Value = sd.Module;
                DataGridView1.Rows[row].Cells[nameof(colSerial)].Value = sd.SerialNo;
                if (sd.SerialType.HasFlag(SerialType.EXPIRATION_DATE))
                    DataGridView1.Rows[row].Cells[nameof(colExpiration)].Value = sd.Expiration;

                txtLicense.Text = SerialManager.SerialData.License;
                txtPenDrive.Text = SerialManager.SerialData.PenDrive;
            }
        }

        private void LoadDataFromMenu(string appConfname)
        {
            var applPath = Path.GetDirectoryName(appConfname);
            if (applPath != null)
            {
                //var appConfname = Path.Combine(applPath, "ApplicationModules.config");

                var xDoc = new XmlDocument();
                xDoc.Load(appConfname);

                var xModules = xDoc.SelectNodes("modules/module");

                //if (xDoc.SelectSingleNode("modules").Attributes["pendrive"] != null)
                //    txtPendrive.Text = xDoc.SelectSingleNode("modules").Attributes["pendrive"].Value;

                if (xModules != null)
                    foreach (XmlNode xModule in xModules)
                    {
                        if (xModule.Attributes != null)
                        {
                            var moduleCode = xModule.Attributes["code"].Value;
                            var nSpace = new NameSpace(xModule.Attributes["namespace"].Value);

                            var moduleDir = Path.Combine(applPath, nSpace.Folder);
                            var menuFile = Path.Combine(moduleDir, "Menu","menu.config");

                            if (!File.Exists(menuFile))
                            {
                                MessageBox.Show("Missing menu file");
                                return;
                            }

                            var xDocMenu = new XmlDocument();
                            xDocMenu.Load(menuFile);

                            var xMod = xDocMenu.SelectNodes("menu/module");

                            if (xMod != null)
                                foreach (XmlNode xSignleMod in xMod)
                                {
                                    if (xSignleMod.Attributes == null)
                                        continue;

                                    var row = DataGridView1.Rows.Add();
                                    DataGridView1.Rows[row].Cells["colenable"].Value = xSignleMod.Attributes["enable"].Value == bool.TrueString;
                                    var sType = (SerialType)Enum.Parse(typeof(SerialType), xSignleMod.Attributes["serialtype"].Value);
                                    DataGridView1.Rows[row].Cells[nameof(colLicenseType)].Value = sType;
                                    DataGridView1.Rows[row].Cells[nameof(colApplication)].Value = moduleCode;
                                    DataGridView1.Rows[row].Cells[nameof(colModuleName)].Value = xSignleMod.Attributes["code"].Value;
                                    if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                                        DataGridView1.Rows[row].Cells[nameof(colExpiration)].Value = xSignleMod.Attributes["expirationdate"].Value;
                                }
                        }
                    }
            }
        }

        private bool ModuleExist(string application, string module)
        {
            for (int t = 0; t < DataGridView1.RowCount; t++)
            {
                if (DataGridView1.Rows[t].Cells[nameof(colApplication)].Value.ToString() == application &&
                    DataGridView1.Rows[t].Cells[nameof(colModuleName)].Value.ToString() == module)
                    return true;
            }
            return false;
        }

        private void CreateSerial()
        {
            for (int t = 0; t < DataGridView1.Rows.Count; t++)
            {
                var expiration = DateTime.Today;
                var sType = (SerialType)DataGridView1.Rows[t].Cells[nameof(colLicenseType)].Value;
                var application = (string)DataGridView1.Rows[t].Cells[nameof(colApplication)].Value;
                var module = (string)DataGridView1.Rows[t].Cells[nameof(colModuleName)].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = (DateTime)DateTime.Parse(DataGridView1.Rows[t].Cells[nameof(colExpiration)].Value.ToString());
                DataGridView1.Rows[t].Cells[nameof(colSerial)].Value = SerialManager.CreateSerial(txtLicense.Text, txtMac.Text, application, module, sType, expiration, txtPenDrive.Text);
            }
        }

        private bool CheckData()
        {
            for (int t = 0; t < DataGridView1.RowCount; t++)
            {
                if (DataGridView1.Rows[t].Cells["colenable"].Value.ToString() == bool.TrueString &&
                    DataGridView1.Rows[t].Cells[nameof(colSerial)].Value == null)
                {
                    MessageBox.Show(Properties.Resources.Msg_MissingSerialNumber, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var expiration = DateTime.Today;
                var sType = (SerialType)DataGridView1.Rows[t].Cells[nameof(colLicenseType)].Value;
                var application = (string)DataGridView1.Rows[t].Cells[nameof(colApplication)].Value;
                var module = (string)DataGridView1.Rows[t].Cells[nameof(colModuleName)].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = (DateTime)DateTime.Parse(DataGridView1.Rows[t].Cells[nameof(colExpiration)].Value.ToString());

                var serial = (string)DataGridView1.Rows[t].Cells[nameof(colSerial)].Value;

                if (serial != SerialManager.CreateSerial(txtLicense.Text, txtMac.Text, application, module, sType, expiration, txtPenDrive.Text))
                {
                    var mess = string.Format(Properties.Resources.Msg_IncorrectSerialNumber, application, module);
                    MessageBox.Show(mess, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        //private void SaveSerial()
        //{
        //    SerialManager.Clear();
        //    SerialManager.SerialData.License = txtLicense.Text;
        //    SerialManager.SerialData.PenDrive = txtPenDrive.Text;

        //    for (int t = 0; t < DataGridView1.Rows.Count; t++)
        //    {
        //        var expiration = DateTime.Today;
        //        var enable = (bool)DataGridView1.Rows[t].Cells[nameof(colEnable)].Value;
        //        var sType = (SerialType)DataGridView1.Rows[t].Cells[nameof(colLicenseType)].Value;
        //        var application = (string)DataGridView1.Rows[t].Cells[nameof(colApplication)].Value;
        //        var module = (string)DataGridView1.Rows[t].Cells[nameof(colModuleName)].Value;
        //        if (sType.HasFlag(SerialType.EXPIRATION_DATE))
        //            expiration = DateTime.Parse(DataGridView1.Rows[t].Cells[nameof(colExpiration)].Value.ToString());
        //        var serial = DataGridView1.Rows[t].Cells[nameof(colSerial)].Value.ToString();
        //        SerialManager.AddModule(application, enable, module, sType, expiration, serial);
        //    }
        //    SerialManager.Save();
        //}

        private void btnFindPen_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPenDrive.Text = "";// USBSerialNumber.GetNameFromDriveLetter(fbd.SelectedPath);
                }
            }
        }

        private void metroToolbar1_ItemClicked(object sender, MetroFramework.Extender.MetroToolbarButtonType e)
        {
            var btn = sender as IMetroToolBarButton;

            switch (btn.ButtonType)
            {
                case MetroToolbarButtonType.Exit:
                    this.Close();
                    return;
                case MetroToolbarButtonType.Save:
                    CreateSerial();
                    break;
                case MetroToolbarButtonType.Search:
                    if (openDialog.ShowDialog() == DialogResult.OK)
                        LoadDataFromMenu(openDialog.FileName);

                    break;
            }
        }
    }
}