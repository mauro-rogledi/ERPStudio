using System.Collections.Generic;
using System.Windows.Forms;

using ERPFramework;
using ERPFramework.Data;
using MetroFramework.Forms;

namespace ERPManager
{
    partial class AboutForm : MetroForm
    {
        public AboutForm()
        {
            InitializeComponent();
            lblAppname.Text = ModuleManager.ApplicationName;

            lblLicensed.Text = SerialManager.SerialData.License;
            List<string> module = new List<string> {"ApplicationFramework"};

            listBox1.Items.Add(GetDllVersion(new NameSpace("", "ApplicationFramework", "", "")));
            module.Add("ApplicationManager");
            listBox1.Items.Add(GetDllVersion(new NameSpace("", "ApplicationManager", "", "")));
            foreach (ApplicationMenuModule amm in ModuleManager.ModuleList)
            {
                if (!module.Contains(amm.Namespace.Library))
                {
                    listBox1.Items.Add(GetDllVersion(new NameSpace("", amm.Namespace.Library, "", "")));
                    module.Add(amm.Namespace.Library);
                }
                listBox1.Items.Add(GetModuleVersion(amm));
            }
        }

        private string GetDllVersion(NameSpace amm)
        {
            amm.Application = "ModuleData.RegisterModule";
            RegisterModule registerTable = (RegisterModule)DllManager.CreateIstance(amm, null);
            if (registerTable != null)
            {
                return string.Concat(amm.Library, " ", registerTable.DllVersion.ToString());
            }
            return "";
        }

        private string GetModuleVersion(ApplicationMenuModule amm)
        {
            string module = amm.Namespace.Module;
            amm.Namespace.Application = "ModuleData.RegisterModule";
            RegisterModule registerTable = (RegisterModule)DllManager.CreateIstance(amm.Namespace, null);
            if (registerTable != null)
            {
                return string.Concat(amm.Namespace.Module, " ", registerTable.CurrentVersion().ToString());
            }

            return "";
        }
    }
}