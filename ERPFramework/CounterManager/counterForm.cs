using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.Forms;
using ERPFramework.Libraries;
using System;
using System.Collections.Generic;
using System.Data;

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

            dbManager.AddMaster<AM_Counter>();
            dbManager.AddSlave<AM_CounterValue>();
            dbManager.AddRelation(AM_CounterValue.Name, AM_Counter.Type, AM_CounterValue.Type);

            dgwValues.AutoGenerateColumns = false;
            dgwValues.DataSource = dbManager.SlaveBinding(AM_CounterValue.Name);

            PrefixManager =  new EnumsManager<PrefixSuffixType>(cbbPreInit,"");
            FiscalKeyManager = new EnumsManager<FiscalKey>(cbbFiscalKey, "");
            SuffixManager = new EnumsManager<PrefixSuffixType>(cbbSufInit, "");
        }

        protected override void OnBindData()
        {
            BindControl(cbbCode, AM_Counter.Type);
            BindControl(txtDescription, AM_Counter.Description);
            BindControl(ckbPreEnable, AM_Counter.HasPrefix);
            BindControl(ckbPreReadonly, AM_Counter.PrefixRO);
            BindControl(txtPreValue, AM_Counter.PrefixValue);
            BindControl(txtPreSep, AM_Counter.PrefixSep);
            BindControl(cbbPreInit, AM_Counter.PrefixType);

            BindControl(ckbSufEnable, AM_Counter.HasSuffix);
            BindControl(ckbSufReadonly, AM_Counter.SuffixRO);
            BindControl(txtSufValue, AM_Counter.SuffixValue);
            BindControl(txtSufSep, AM_Counter.SuffixSep);
            BindControl(cbbSufInit, AM_Counter.SuffixType);

            BindControl(ntbLength, AM_Counter.CodeLen);
            BindControl(cbbFiscalKey, AM_Counter.CodeKey);

            BindControl(dgwValues);

            colCode.DataPropertyName = AM_CounterValue.Code.Name;
            colValue.DataPropertyName = AM_CounterValue.NumericValue.Name;

            dgwValues.AddReadOnlyColumn(AM_CounterValue.Code, true);
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

        protected override void dAdapter_MasterRowUpdating(object sender, SqlABRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row[AM_Counter.Year.Name] = GlobalInfo.CurrentDate.Year;
            }
            base.dAdapter_RowUpdating(sender, e);
        }

        protected override string CreateMasterQuery(ref List<SqlABParameter> dParam)
        {
            QueryBuilder qb = new QueryBuilder();

            return qb.SelectAllFrom<AM_Counter>().
                Where(AM_Counter.Year).IsEqualTo(dParam[0]).
                And(AM_Counter.Type).IsEqualTo(dParam[1]).
                Query;
        }

        protected override List<SqlABParameter> CreateMasterParam()
        {
            List<SqlABParameter> PList = new List<SqlABParameter>();

            SqlABParameter nParam = new SqlABParameter("@p1", AM_Counter.Year);
            nParam.Value = AM_Counter.Year.DefaultValue;
            PList.Add(nParam);

            SqlABParameter sParam = new SqlABParameter("@p2", AM_Counter.Type);
            sParam.Value = AM_Counter.Type.DefaultValue;
            PList.Add(sParam);
            return PList;
        }

        protected override void SetParameters(IRadarParameters key, DBCollection collection)
        {
            if (collection.Name == AM_Counter.Name)
            {
                collection.Parameter[0].Value = key[0];
                collection.Parameter[1].Value = key[1];
            }
            else
            {
                collection.Parameter[0].Value = key[1];
            }
        }

        protected override string CreateSlaveQuery(string name, List<SqlABParameter> dParam)
        {
            if (name == AM_CounterValue.Name)
            {
                QueryBuilder qb = new QueryBuilder().
                       SelectAllFrom<AM_CounterValue>().
                        Where(AM_CounterValue.Type).IsEqualTo(dParam[0]).
                        OrderBy(AM_CounterValue.Code);

                return qb.Query;
            }

            return "";
        }

        protected override List<SqlABParameter> CreateSlaveParam(string name)
        {
            if (name == AM_CounterValue.Name)
            {
                List<SqlABParameter> PList = new List<SqlABParameter>();

                SqlABParameter sParam = new SqlABParameter("@p2", AM_CounterValue.Code);
                sParam.Value = AM_CounterValue.Code.DefaultValue;
                PList.Add(sParam);
                return PList;
            }
            return null;
        }
    }

    #endregion
}