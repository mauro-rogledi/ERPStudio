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
            //ActivationManager.Load();
            DataGridView1.Rows.Clear();

            foreach (var module in ActivationManager.activationDataMemory.Modules)
            {
               var row = DataGridView1.Rows.Add();
                DataGridView1.Rows[row].Cells[nameof(colEnable)].Value = module.Value.Enabled;
                DataGridView1.Rows[row].Cells[nameof(colLicenseType)].Value = module.Value.SerialType.ToString();
                DataGridView1.Rows[row].Cells[nameof(colModuleName)].Value = module.Value.Name;
                DataGridView1.Rows[row].Cells[nameof(colCode)].Value = module.Value.Code;
                DataGridView1.Rows[row].Cells[nameof(colSerial)].Value = module.Value.SerialNo;
                if (module.Value.SerialType.HasFlag(SerialType.EXPIRATION_DATE))
                    DataGridView1.Rows[row].Cells[nameof(colExpiration)].Value = module.Value.Expiration;

#if DEBUG
                DataGridView1.Rows[row].Cells[nameof(colSerial)].Value =
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
            for (int t = 0; t < DataGridView1.RowCount; t++)
            {
                Boolean.TryParse(DataGridView1.Rows[t].Cells[nameof(colEnable)].Value.ToString(), out bool enabled);
                var serial = DataGridView1.Rows[t].Cells[nameof(colSerial)].Value.ToString();

                if (enabled && serial.IsEmpty())
                {
                    MessageBox.Show(Properties.Resources.Msg_MissingSerialNumber, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var expiration = DateTime.Today;
                Enum.TryParse<SerialType>(DataGridView1.Rows[t].Cells[nameof(colLicenseType)].Value.ToString(), out SerialType sType);
                var code = (string)DataGridView1.Rows[t].Cells[nameof(colCode)].Value;
                var name = (string)DataGridView1.Rows[t].Cells[nameof(colModuleName)].Value;
                if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                    expiration = (DateTime)DateTime.Parse(DataGridView1.Rows[t].Cells[nameof(colExpiration)].Value.ToString());

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
            //ActivationManager.Clear();
            //ActivationManager.activationDataSave.License = txtLicense.Text;
            //ActivationManager.activationDataSave.PenDrive = txtPenDrive.Text;

            //for (int t = 0; t < DataGridView1.Rows.Count; t++)
            //{
            //    var expiration = DateTime.Today;
            //    var enable = (bool)DataGridView1.Rows[t].Cells[nameof(colEnable)].Value;
            //    var sType = (SerialType)DataGridView1.Rows[t].Cells[nameof(colLicenseType)].Value;
            //    var application = (string)DataGridView1.Rows[t].Cells[nameof(colApplication)].Value;
            //    var module = (string)DataGridView1.Rows[t].Cells[nameof(colModuleName)].Value;
            //    if (sType.HasFlag(SerialType.EXPIRATION_DATE))
            //        expiration = DateTime.Parse(DataGridView1.Rows[t].Cells[nameof(colExpiration)].Value.ToString());
            //    var serial = DataGridView1.Rows[t].Cells[nameof(colSerial)].Value.ToString();
            //    ActivationManager.AddModule(enable, module, sType, expiration, serial);
            //}
            //ActivationManager.Save();
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