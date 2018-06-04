using ERPFramework;
using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.ModulesHelper;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ERPManager
{
    public partial class registerForm : ERPFramework.Forms.AskForm
    {
        private DataTable serialTable;

        public registerForm(string applicationName)
        {
            InitializeComponent();
            Text = applicationName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        protected override void OnBindData()
        {
            serialTable = new EF_Serial().CreateTable();
            dgwModules.AutoGenerateColumns = false;
            dgwModules.DataSource = serialTable;


            BindColumn(colEnable, EF_Serial.Enable);
            BindColumn(colModuleName, EF_Serial.ModuleName);
            BindColumn(colSerial, EF_Serial.Serial);
            BindColumn(colLicenseType, EF_Serial.SerialType);
            BindColumn(colExpiration, EF_Serial.Expiration);
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
            ActivationManager.Modules.ToList().ForEach(module =>
           {
               var row = serialTable.NewRow();
               row.SetValue<bool>(EF_Serial.Enable, module.Value.Enabled);
               row.SetValue<string>(EF_Serial.Code, module.Value.Code);
               row.SetValue<string>(EF_Serial.ModuleName, module.Value.Name);
               row.SetValue<SerialType>(EF_Serial.SerialType, module.Value.SerialType);
               row.SetValue<DateTime>(EF_Serial.Expiration, module.Value.Expiration);
#if DEBUG
               module.Value.SerialNo = ActivationManager.CreateSerial(txtLicense.Text, txtMac.Text, module.Value.Code, module.Value.SerialType, module.Value.Expiration, txtPenDrive.Text);
#endif
               row.SetValue<string>(EF_Serial.Serial, module.Value.SerialNo);

               serialTable.Rows.Add(row);
           });
        }

        public override bool OnOk()
        {
            return CheckData()
                ? SaveSerial()
                : false;
        }

        private bool CheckData()
        {
            var isOK = true;

            serialTable.Rows.Cast<DataRow>().ToList().ForEach(row =>
            {
                if (!isOK)
                    return;

                if (row.GetValue<bool>(EF_Serial.Enable) && row.GetValue<string>(EF_Serial.Serial).IsEmpty())
                {
                    isOK = false;
                    row.SetValue<ActivationState>(EF_Serial.Active, ActivationState.NotActivate);
                    MessageBox.Show(Properties.Resources.Msg_MissingSerialNumber, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var code = row.GetValue<string>(EF_Serial.Code);
                var serialType = row.GetValue<SerialType>(EF_Serial.SerialType);
                var expirationDate = row.GetValue<DateTime>(EF_Serial.Expiration);

                var serial = ActivationManager.CreateSerial(txtLicense.Text, txtMac.Text, code, serialType, expirationDate, txtPenDrive.Text);
                if (serial != row.GetValue<string>(EF_Serial.Serial))
                {
                    isOK = false;
                    row.SetValue<ActivationState>(EF_Serial.Active, ActivationState.NotActivate);
                    var mess = string.Format(Properties.Resources.Msg_IncorrectSerialNumber, row.GetValue<string>(EF_Serial.ModuleName));
                    MessageBox.Show(mess, Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                var activationState = (row.GetValue<SerialType>(EF_Serial.SerialType) & SerialType.TRIAL) == SerialType.TRIAL
                                        ? ActivationState.Activate | ActivationState.Trial
                                        : ActivationState.Activate;

                row.SetValue<ActivationState>(EF_Serial.Active, activationState);
            });

            return isOK;
        }

        private bool SaveSerial()
        {
            ActivationManager.Clear();
            ActivationManager.License = txtLicense.Text;
            ActivationManager.PenDrive = txtPenDrive.Text;

            serialTable.Rows.Cast<DataRow>().ToList().ForEach(row =>
            {
                var module = ActivationManager.Module(row.GetValue<string>(EF_Serial.Code));
                module.SerialNo = row.GetValue<string>(EF_Serial.Serial);
                module.State = row.GetValue<ActivationState>(EF_Serial.Active);
                module.Enabled = row.GetValue<bool>(EF_Serial.Enable);
            });

            return ActivationManager.Save();
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
    }
}