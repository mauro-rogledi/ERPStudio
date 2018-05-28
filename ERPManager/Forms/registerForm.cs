using ERPFramework;
using ERPFramework.ModulesHelper;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ERPManager
{
    public partial class registerForm : ERPFramework.Forms.AskForm
    {
        public bool LoadFromBinary = false;
        public bool LoadData { get; set; } = true;

        public registerForm(string applicationName)
        {
            InitializeComponent();
            Text = applicationName;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtMac.Text = ActivationManager.ReadMacAddress();
            txtLicense.Text = ActivationManager.activationDataMemory.License;
            txtPenDrive.Text = ActivationManager.activationDataMemory.PenDrive;
            PrepareGrid();
        }

        private void PrepareGrid()
        {
            dgwModules.Rows.Clear();

            foreach (var module in ActivationManager.Modules)
            {
               var row = dgwModules.Rows.Add();
                dgwModules.Rows[row].Cells[nameof(colEnable)].Value = module.Value.Enabled;
                dgwModules.Rows[row].Cells[nameof(colLicenseType)].Value = module.Value.SerialType.ToString();
                dgwModules.Rows[row].Cells[nameof(colModuleName)].Value = module.Value.Name;
                dgwModules.Rows[row].Cells[nameof(colCode)].Value = module.Value.Code;
                dgwModules.Rows[row].Cells[nameof(colSerial)].Value = module.Value.SerialNo;
                if (module.Value.SerialType.HasFlag(SerialType.EXPIRATION_DATE))
                    dgwModules.Rows[row].Cells[nameof(colExpiration)].Value = module.Value.Expiration;

#if DEBUG
                dgwModules.Rows[row].Cells[nameof(colSerial)].Value =
                ActivationManager.CreateSerial(txtLicense.Text, txtMac.Text, module.Value.Code, module.Value.SerialType, module.Value.Expiration, txtPenDrive.Text);
#endif
            }
        }


        public override bool OnOk()
        {
            if (!CheckData())
                return false;

            ActivationManager.Save();
            return true;
        }


        private bool CheckData()
        {
            for (int t = 0; t < dgwModules.RowCount; t++)
            {
                Boolean.TryParse(dgwModules.Rows[t].Cells[nameof(colEnable)].Value.ToString(), out bool enabled);
                var serial = dgwModules.Rows[t].Cells[nameof(colSerial)].Value.ToString();

                if (enabled && serial.IsEmpty())
                {
                    MessageBox.Show(Properties.Resources.Msg_MissingSerialNumber, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var expiration = DateTime.Today;
                Enum.TryParse<SerialType>(dgwModules.Rows[t].Cells[nameof(colLicenseType)].Value.ToString(), out SerialType sType);
                var code = (string)dgwModules.Rows[t].Cells[nameof(colCode)].Value;
                var name = (string)dgwModules.Rows[t].Cells[nameof(colModuleName)].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = (DateTime)DateTime.Parse(dgwModules.Rows[t].Cells[nameof(colExpiration)].Value.ToString());

                if (serial != ActivationManager.CreateSerial(txtLicense.Text, txtMac.Text, code, sType, expiration, txtPenDrive.Text))
                {
                    var mess = string.Format(Properties.Resources.Msg_IncorrectSerialNumber, name);
                    MessageBox.Show(mess, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void SaveSerial()
        {
            ActivationManager.Clear();
            ActivationManager.License = txtLicense.Text;
            ActivationManager.PenDrive = txtPenDrive.Text;

            for (int t = 0; t < dgwModules.Rows.Count; t++)
            {
                var expiration = DateTime.Today;
                //dgwModules.GetValue<string>()

                var name = (string)dgwModules.Rows[t].Cells[nameof(colModuleName)].Value;
                var enable = (bool)dgwModules.Rows[t].Cells[nameof(colEnable)].Value;
                var sType = (SerialType)dgwModules.Rows[t].Cells[nameof(colLicenseType)].Value;
                var module = (string)dgwModules.Rows[t].Cells[nameof(colModuleName)].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = DateTime.Parse(dgwModules.Rows[t].Cells[nameof(colExpiration)].Value.ToString());
                var serial = dgwModules.Rows[t].Cells[nameof(colSerial)].Value.ToString();


                ActivationManager.Module(name).Expiration = expiration;
                ActivationManager.Module(name).SerialNo = serial;
            }
            ActivationManager.Save();
        }

        private void btnFindPen_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPenDrive.Text = USBSerialNumber.GetNameFromDriveLetter(fbd.SelectedPath);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}