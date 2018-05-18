using System;
using System.ComponentModel;
using System.Windows.Forms;
using ERPFramework.ModulesHelper;

namespace Serializer
{
    public partial class licenseForm : Form
    {
        public SerialType serialType { get; set; }

        public licenseForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            foreach (SerialType i in Enum.GetValues(typeof(SerialType)))
            {
                listBox1.Items.Add(i.ToString());
                if (serialType.HasFlag(i))
                    listBox1.SelectedItems.Add(listBox1.Items[listBox1.Items.Count - 1]);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                serialType = 0;
                foreach (string i in listBox1.SelectedItems)
                {
                    serialType |= (SerialType)Enum.Parse(typeof(SerialType), i);
                }
            }
        }
    }
}