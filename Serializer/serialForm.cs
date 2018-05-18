using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ERPFramework;
using ERPFramework.ModulesHelper;

namespace Serializer
{
    public partial class serialForm : Form
    {
        public serialForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            for (int t = 0; t < DataGridView1.Rows.Count; t++)
            {
                DateTime expiration = DateTime.Today;
                SerialType sType = (SerialType)DataGridView1.Rows[t].Cells["colLicenseType"].Value;
                string application = (string)DataGridView1.Rows[t].Cells["colApplication"].Value;
                string module = (string)DataGridView1.Rows[t].Cells["colModuleName"].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = (DateTime)DateTime.Parse(DataGridView1.Rows[t].Cells["colExpiration"].Value.ToString());
                DataGridView1.Rows[t].Cells["colSerial"].Value = SerialManager.CreateSerial(txtLicense.Text, txtMac.Text, application, module, sType, expiration, cbbPenDrive.Text);
            }
        }

        private void tsmiLoadModule_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckFileExists = true;
            fd.Filter = "Config|*.config|All|*.*";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fd.FileName);

            XmlNodeList xModules = xDoc.SelectNodes("modules/module");

            if (xDoc.SelectSingleNode("modules").Attributes["pendrive"] != null)
                cbbPenDrive.Text = xDoc.SelectSingleNode("modules").Attributes["pendrive"].Value;

            foreach (XmlNode xModule in xModules)
            {
                string moduleCode = xModule.Attributes["code"].Value;
                NameSpace nSpace = new NameSpace(xModule.Attributes["namespace"].Value);

                string moduleDir = Path.Combine(Path.GetDirectoryName(fd.FileName), nSpace.Folder);
                string menuFile = Path.Combine(moduleDir, "menu.config");

                if (!File.Exists(menuFile))
                {
                    MessageBox.Show("Missing menu file");
                    return;
                }

                XmlDocument xDocMenu = new XmlDocument();
                xDocMenu.Load(menuFile);

                XmlNodeList xMod = xDocMenu.SelectNodes("menu/module");

                foreach (XmlNode xSignleMod in xMod)
                {
                    int row = DataGridView1.Rows.Add();
                    DataGridView1.Rows[row].Cells["colenable"].Value = xSignleMod.Attributes["enable"].Value == bool.TrueString;
                    SerialType sType = (SerialType)Enum.Parse(typeof(SerialType), xSignleMod.Attributes["serialtype"].Value);
                    DataGridView1.Rows[row].Cells["colLicenseType"].Value = sType;
                    DataGridView1.Rows[row].Cells["colApplication"].Value = moduleCode;
                    DataGridView1.Rows[row].Cells["colModuleName"].Value = xSignleMod.Attributes["code"].Value;
                    if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                        DataGridView1.Rows[row].Cells["colExpiration"].Value = xSignleMod.Attributes["expirationdate"].Value;
                }
            }
        }

        private void btnMacAddress_Click(object sender, EventArgs e)
        {
            txtMac.Text = SerialManager.ReadMacAddress();
        }

        private void extendedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                licenseForm lf = new licenseForm();
                lf.serialType = (SerialType)DataGridView1.Rows[e.RowIndex].Cells["colLicenseType"].Value;
                if (lf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    DataGridView1.Rows[e.RowIndex].Cells["colLicenseType"].Value = lf.serialType;
            }
        }

        private void tsmbinaryFile_Click(object sender, EventArgs e)
        {
            SerialManager.Load();
            DataGridView1.Rows.Clear();

            //foreach (SerialModule sd in SerialManager.SerialData.Modules)
            //{
            //    int row = DataGridView1.Rows.Add();
            //    DataGridView1.Rows[row].Cells["colenable"].Value = sd.Enable == bool.TrueString;
            //    SerialType sType = (SerialType)Enum.Parse(typeof(SerialType), sd.SerialType);
            //    DataGridView1.Rows[row].Cells["colLicenseType"].Value = sType;
            //    DataGridView1.Rows[row].Cells["colApplication"].Value = sd.Application;
            //    DataGridView1.Rows[row].Cells["colModuleName"].Value = sd.Module;
            //    DataGridView1.Rows[row].Cells["colSerial"].Value = sd.SerialNo;
            //    if (sType.HasFlag(SerialType.EXPIRATION_DATE))
            //        DataGridView1.Rows[row].Cells["colExpiration"].Value = sd.Expiration;

            //    txtLicense.Text = SerialManager.SerialData.License;
            //    cbbPenDrive.Text = SerialManager.SerialData.PenDrive;
            //}
        }

        private void tsmActivationSave_Click(object sender, EventArgs e)
        {
            SerialManager.Clear();
            SerialManager.SerialData.License = txtLicense.Text;
            SerialManager.SerialData.PenDrive = cbbPenDrive.Text;

            for (int t = 0; t < DataGridView1.Rows.Count; t++)
            {
                DateTime expiration = DateTime.Today;
                bool enable = (bool)DataGridView1.Rows[t].Cells["colEnable"].Value;
                SerialType sType = (SerialType)DataGridView1.Rows[t].Cells["colLicenseType"].Value;
                string application = (string)DataGridView1.Rows[t].Cells["colApplication"].Value;
                string module = (string)DataGridView1.Rows[t].Cells["colModuleName"].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = DateTime.Parse(DataGridView1.Rows[t].Cells["colExpiration"].Value.ToString());
                string serial = DataGridView1.Rows[t].Cells["colSerial"].Value.ToString();
                SerialManager.AddModule(application, enable, module, sType, expiration, serial);
            }
            SerialManager.Save();
        }

        private void tsmSaveModule_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.CheckFileExists = true;
            fd.Filter = "Config|*.config|All|*.*";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fd.FileName);

            XmlNodeList xModules = xDoc.SelectNodes("modules/module");

            if (!string.IsNullOrEmpty(cbbPenDrive.Text))
            {
                if (xDoc.SelectSingleNode("modules").Attributes["pendrive"] == null)
                {
                    XmlAttribute xAttr = xDoc.CreateAttribute("pendrive");
                    xDoc.SelectSingleNode("modules").Attributes.Append(xAttr);
                }
                xDoc.SelectSingleNode("modules").Attributes["pendrive"].Value = cbbPenDrive.Text;
            }

            foreach (XmlNode xModule in xModules)
            {
                string moduleCode = xModule.Attributes["code"].Value;
                NameSpace nSpace = new NameSpace(xModule.Attributes["namespace"].Value);

                string moduleDir = Path.Combine(Path.GetDirectoryName(fd.FileName), nSpace.Folder);
                string menuFile = Path.Combine(moduleDir, "menu.config");

                if (!File.Exists(menuFile))
                {
                    MessageBox.Show("Missing menu file");
                    return;
                }

                XmlDocument xDocMenu = new XmlDocument();
                xDocMenu.Load(menuFile);

                XmlNodeList xMod = xDocMenu.SelectNodes("menu/module");

                foreach (XmlNode xSignleMod in xMod)
                {
                    if (xSignleMod.Attributes["enable"] == null)
                    {
                        XmlAttribute xAttr = xDocMenu.CreateAttribute("enable");
                        xSignleMod.Attributes.Append(xAttr);
                    }
                    xSignleMod.Attributes["enable"].Value = GetValue(moduleCode, xSignleMod.Attributes["code"].Value, "colenable");

                    if (xSignleMod.Attributes["serialtype"] == null)
                    {
                        XmlAttribute xAttr = xDocMenu.CreateAttribute("serialtype");
                        xSignleMod.Attributes.Append(xAttr);
                    }
                    xSignleMod.Attributes["serialtype"].Value = GetValue(moduleCode, xSignleMod.Attributes["code"].Value, "colLicenseType");

                    SerialType sType = (SerialType)Enum.Parse(typeof(SerialType), xSignleMod.Attributes["serialtype"].Value);

                    if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    {
                        if (xSignleMod.Attributes["expirationdate"] == null)
                        {
                            XmlAttribute xAttrDate = xDocMenu.CreateAttribute("expirationdate");
                            xSignleMod.Attributes.Append(xAttrDate);
                        }
                        xSignleMod.Attributes["expirationdate"].Value = GetValue(moduleCode, xSignleMod.Attributes["code"].Value, "colExpiration");
                    }
                }
                xDocMenu.Save(menuFile);
            }

            xDoc.Save(fd.FileName);
        }

        private string GetValue(string app, string mod, string col)
        {
            for (int t = 0; t < DataGridView1.RowCount; t++)
                if (DataGridView1.Rows[t].Cells["colApplication"].Value.ToString() == app &&
                    DataGridView1.Rows[t].Cells["colModuleName"].Value.ToString() == mod)
                    return DataGridView1.Rows[t].Cells[col].Value.ToString();

            return string.Empty;
        }

        private void btnPenDrive_Click(object sender, EventArgs e)
        {
            cbbPenDrive.Items.Clear();
            foreach (string drive in Directory.GetLogicalDrives())
            {
                DriveInfo dInfo = new DriveInfo(drive.Substring(0, 2));
                if (dInfo.IsReady)
                    MessageBox.Show(string.Format("Drive {0} - Label {1} - Type {2}", drive.Substring(0, 2), dInfo.VolumeLabel, dInfo.DriveType));

                if (dInfo.IsReady /*&& dInfo.DriveType == DriveType.Removable*/)
                    cbbPenDrive.Items.Add(dInfo.VolumeLabel);
            }
        }
    }
}