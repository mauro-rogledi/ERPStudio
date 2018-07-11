using ERPFramework.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPManager.Forms
{
    public partial class ProvaForm : ERPFramework.Forms.DataEntryForm
    {
        public ProvaForm()
        {
            InitializeComponent();
        }

        protected override void OnInitializeData()
        {
            DataModelManager
                .MasterTable<EF_Users>(CreateMasterQuery, DeclareMasterParam, SetMasterParam);
        }

        private static string CreateMasterQuery(SqlProxyParameterCollection param)
        {
            return "";
        }

        private static void DeclareMasterParam(SqlProxyParameterCollection param)
        {
            throw new NotImplementedException();
        }

        private static void SetMasterParam(SqlProxyParameterCollection arg1, DataRow arg2)
        {
            throw new NotImplementedException();
        }

        public override void OnBindData()
        {
            base.OnBindData();
        }
    }
}
