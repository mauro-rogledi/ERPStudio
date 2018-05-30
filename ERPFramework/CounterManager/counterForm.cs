using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.Forms;
using ERPFramework.Libraries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ERPFramework.CounterManager
{
    public partial class counterForm : DocumentForm
    {
        private ComboBoxManager cbbManager;
        private EnumsManager<PrefixSuffixType> PrefixManager;
        private EnumsManager<PrefixSuffixType> SuffixManager;
        private EnumsManager<FiscalKey> FiscalKeyManager;

        public counterForm()
            : base("counter")
        {
            Title = "";
            InitializeComponent();
            InitializeAuxComponent();
        }

        #region Virtual Method

        protected override void OnInitializeData()
        {
            dbManager = new dbmanagerCounter("counterForm", this);
            dbManager.AttachRadar<RadarCounter>();

            dbManager.AddMaster<EF_Counter>();
            dbManager.AddSlave<EF_CounterValue>();
            dbManager.AddRelation(EF_CounterValue.Name, EF_Counter.Type, EF_CounterValue.Type);

            dgwValues.AutoGenerateColumns = false;
            dgwValues.DataSource = dbManager.SlaveBinding(EF_CounterValue.Name);

            PrefixManager =  new EnumsManager<PrefixSuffixType>(cbbPreInit,"");
            FiscalKeyManager = new EnumsManager<FiscalKey>(cbbFiscalKey, "");
            SuffixManager = new EnumsManager<PrefixSuffixType>(cbbSufInit, "");
        }

        protected override void OnBindData()
        {
            BindControl(cbbCode, EF_Counter.Type);
            BindControl(txtDescription, EF_Counter.Description);
            BindControl(ckbPreEnable, EF_Counter.HasPrefix);
            BindControl(ckbPreReadonly, EF_Counter.PrefixRO);
            BindControl(txtPreValue, EF_Counter.PrefixValue);
            BindControl(txtPreSep, EF_Counter.PrefixSep);
            BindControl(cbbPreInit, EF_Counter.PrefixType);

            BindControl(ckbSufEnable, EF_Counter.HasSuffix);
            BindControl(ckbSufReadonly, EF_Counter.SuffixRO);
            BindControl(txtSufValue, EF_Counter.SuffixValue);
            BindControl(txtSufSep, EF_Counter.SuffixSep);
            BindControl(cbbSufInit, EF_Counter.SuffixType);

            BindControl(ntbLength, EF_Counter.CodeLen);
            BindControl(cbbFiscalKey, EF_Counter.CodeKey);

            BindControl(dgwValues);

            colCode.DataPropertyName = EF_CounterValue.Code.Name;
            colValue.DataPropertyName = EF_CounterValue.NumericValue.Name;

            dgwValues.AddReadOnlyColumn(EF_CounterValue.Code, true);
        }

        protected override void OnDisableControlsForEdit()
        {
            cbbCode.Enabled = false;
            DisableControl();
            base.OnDisableControlsForEdit();
        }

        protected override void OnDisableControlsForNew()
        {
            DisableControl();
        }

        protected override void OnPrepareAuxData()
        {
        }

        protected override void FocusOnNew()
        {
            cbbCode.Focus();
        }

        protected override bool OnBeforeSave()
        {
            return base.OnBeforeSave();
        }

        #endregion

        private void InitializeAuxComponent()
        {
            cbbManager = new ComboBoxManager();
            cbbManager.CreateList<int>();
            foreach (KeyValuePair<int, string> counter in GlobalInfo.CounterTypes)
                cbbManager.AddValue(counter.Key, counter.Value);

            cbbManager.AttachTo(cbbCode);
        }

        private void DisableControl()
        {
            bool hasPrefix = ckbPreEnable.Checked;
            bool isPrefixRO = ckbPreReadonly.Checked;
            PrefixSuffixType PrefixType = (PrefixSuffixType)cbbPreInit.SelectedIndex;
            cbbPreInit.Enabled = hasPrefix;
            ckbPreReadonly.Enabled = hasPrefix;
            txtPreSep.Enabled = hasPrefix;
            txtPreValue.Enabled = hasPrefix && PrefixType == PrefixSuffixType.Custom;

            bool hasSuffix = ckbSufEnable.Checked;
            bool isSuffixRO = ckbSufReadonly.Checked;
            PrefixSuffixType SuffixType = (PrefixSuffixType)cbbSufInit.SelectedIndex;
            cbbSufInit.Enabled = hasSuffix;
            ckbSufReadonly.Enabled = hasSuffix;
            txtSufSep.Enabled = hasSuffix;
            txtSufValue.Enabled = hasSuffix && SuffixType == PrefixSuffixType.Custom;
        }

        private void ckbPreEnable_CheckStateChanged(object sender, EventArgs e)
        {
            DisableControl();
        }

        private void cbbPreInit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableControl();
        }

        private void ckbPreReadonly_CheckStateChanged(object sender, EventArgs e)
        {
            DisableControl();
        }

        private void txtPreMask_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPreValue_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPreSep_TextChanged(object sender, EventArgs e)
        {
        }

        private void ckbSufEnable_CheckStateChanged(object sender, EventArgs e)
        {
            DisableControl();
        }

        private void cbbSufInit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableControl();
        }

        private void ckbSufReadonly_CheckStateChanged(object sender, EventArgs e)
        {
            DisableControl();
        }
    }

    #region dbmanagerCounter

    internal class dbmanagerCounter : ERPFramework.Data.DBManager
    {
        public dbmanagerCounter(string name, DocumentForm document)
            : base(name, document)
        { }

        protected override void dAdapter_MasterRowUpdating(object sender, RowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row[EF_Counter.Year.Name] = GlobalInfo.CurrentDate.Year;
            }
            base.dAdapter_RowUpdating(sender, e);
        }

        protected override string CreateMasterQuery(ref List<SqlProxyParameter> dParam)
        {
            QueryBuilder qb = new QueryBuilder();

            return qb.SelectAllFrom<EF_Counter>().
                Where(EF_Counter.Year).IsEqualTo(dParam[0]).
                And(EF_Counter.Type).IsEqualTo(dParam[1]).
                Query;
        }

        protected override List<SqlProxyParameter> CreateMasterParam()
        {
            List<SqlProxyParameter> PList = new List<SqlProxyParameter>();

            SqlProxyParameter nParam = new SqlProxyParameter("@p1", EF_Counter.Year);
            nParam.Value = EF_Counter.Year.DefaultValue;
            PList.Add(nParam);

            SqlProxyParameter sParam = new SqlProxyParameter("@p2", EF_Counter.Type);
            sParam.Value = EF_Counter.Type.DefaultValue;
            PList.Add(sParam);
            return PList;
        }

        protected override void SetParameters(IRadarParameters key, DBCollection collection)
        {
            if (collection.Name == EF_Counter.Name)
            {
                collection.Parameter[0].Value = key[0];
                collection.Parameter[1].Value = key[1];
            }
            else
            {
                collection.Parameter[0].Value = key[1];
            }
        }

        protected override string CreateSlaveQuery(string name, List<SqlProxyParameter> dParam)
        {
            if (name == EF_CounterValue.Name)
            {
                QueryBuilder qb = new QueryBuilder().
                       SelectAllFrom<EF_CounterValue>().
                        Where(EF_CounterValue.Type).IsEqualTo(dParam[0]).
                        OrderBy(EF_CounterValue.Code);

                return qb.Query;
            }

            return "";
        }

        protected override List<SqlProxyParameter> CreateSlaveParam(string name)
        {
            if (name == EF_CounterValue.Name)
            {
                List<SqlProxyParameter> PList = new List<SqlProxyParameter>();

                SqlProxyParameter sParam = new SqlProxyParameter("@p2", EF_CounterValue.Code);
                sParam.Value = EF_CounterValue.Code.DefaultValue;
                PList.Add(sParam);
                return PList;
            }
            return null;
        }
    }

    #endregion
}