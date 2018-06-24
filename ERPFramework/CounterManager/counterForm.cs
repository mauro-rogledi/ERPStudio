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
            dbManager = new dbmanagerCounter(this)
                .MasterTable<EF_Counter>()
                .SlaveTable<EF_CounterValue>()
                .Relation(EF_CounterValue.Name, EF_Counter.Type, EF_CounterValue.Type)
                .Radar<RadarCounter>();

            dgwValues.AutoGenerateColumns = false;
            dgwValues.DataSource = dbManager.SlaveBinding(EF_CounterValue.Name);

            PrefixManager = new EnumsManager<PrefixSuffixType>(cbbPreInit, "");
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
        public dbmanagerCounter(DocumentForm document)
            : base(document)
        { }

        protected override void dAdapter_MasterRowUpdating(object sender, RowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row.SetValue<int>(EF_Counter.Year, GlobalInfo.CurrentDate.Year);
            }
            base.dAdapter_RowUpdating(sender, e);
        }

        protected override string CreateMasterQuery(SqlProxyParameterCollection parameters)
        {
            return new QueryBuilder()
                .SelectAllFrom<EF_Counter>()
                .Where(EF_Counter.Year).IsEqualTo(parameters["@p1"])
                .And(EF_Counter.Type).IsEqualTo(parameters["@p2"])
                .Query;
        }

        protected override void CreateMasterParam(SqlProxyParameterCollection parameters)
        {
            parameters.Add(
                new SqlProxyParameter("@p1", EF_Counter.Year));

            parameters.Add(
                new SqlProxyParameter("@p2", EF_Counter.Type));
        }

        protected override string CreateSlaveQuery<T>(SqlProxyParameterCollection parameters)
        {
            if (typeof(T) == typeof(EF_CounterValue))
            {
                var qb = new QueryBuilder().
                       SelectAllFrom<EF_CounterValue>().
                        Where(EF_CounterValue.Type).IsEqualTo(parameters["@p2"]).
                        OrderBy(EF_CounterValue.Code);

                return qb.Query;
            }

            return "";
        }

        protected override void CreateSlaveParam<T>(SqlProxyParameterCollection parameters)
        {
            if (typeof(T) == typeof(EF_CounterValue))
            {
                parameters.Add(
                    new SqlProxyParameter("@p2", EF_CounterValue.Type));
            }
        }

        protected override void SetParameters(IRadarParameters key, DataAdapterProperties dataadapterproperties)
        {
            if (dataadapterproperties.Name == EF_Counter.Name)
            {
                dataadapterproperties.Parameters["@p1"].Value = key[EF_Counter.Year];
                dataadapterproperties.Parameters["@p2"].Value = key[EF_Counter.Type];
            }
            else
            {
                dataadapterproperties.Parameters["@p2"].Value = key[EF_CounterValue.Code];
            }
        }
    }

    #endregion
}