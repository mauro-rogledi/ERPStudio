using ERPFramework;
using ERPFramework.Data;
using ERPFramework.ModulesHelper;
using MetroFramework.Forms;

namespace ERPManager
{
    partial class InfoForm : MetroForm
    {
        public InfoForm()
        {
            InitializeComponent();
            lblAppname.Text = ActivationManager.ApplicationName;
            lblLicensed.Text = ActivationManager.activationDataSave.License;

            Add(GetDllVersion(new NameSpace("", nameof(ERPFramework), "", "")));
            Add(GetDllVersion(new NameSpace("", nameof(ERPManager), "", "")));

            foreach (ApplicationMenuModule amm in ModuleManager.ModuleList)
                Add(GetDllVersion(amm.Namespace));

            lsbVersions.Columns[0].Width = -1;
            lsbVersions.Columns[1].Width = -1;
        }

        private static System.Tuple<string, string> GetDllVersion(NameSpace amm)
        {
            amm.Application = "ModuleData.RegisterModule";
            var registerTable = (RegisterModule)DllManager.CreateIstance(amm, null);
            if (registerTable != null)
                return new System.Tuple<string, string>(amm.Library, registerTable.DllVersion.ToString());
            return null;
        }

        private void Add(System.Tuple<string, string> module)
        {
            if (module != null && !lsbVersions.Items.ContainsKey(module.Item1))
            {
                var item = lsbVersions.Items.Add(module.Item1);
                item.SubItems.Add(module.Item2);
                item.Name = module.Item1;
            }
        }
    }
}