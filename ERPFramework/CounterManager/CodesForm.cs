using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.DataGridViewControls;
using ERPFramework.Forms;
using ERPFramework.Libraries;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace ERPFramework.CounterManager
{
    public partial class codesForm : DocumentForm
    {
        private ComboBoxManager cbbManager;
        private EnumsManager<InputType> eSegmentType;

        public ComboBox GetCode { get { return cbbCode; } }

        public MetroTextBox GetDescription { get { return txtDescription; } }

        public ExtendedDataGridView GetDataGrid { get { return dgwSegments; } }

        public ComboBoxManager GetCbbManager { get { return cbbManager; } }

        public codesForm()
            : base("codes")
        {
            InitializeComponent();
            InitializeAuxComponent();
        }

        #region Virtual Method

        protected override void OnInitializeData()
        {
            dbManager = new dbmanagerCodes(this)
                .MasterTable<EF_Codes>()
                .SlaveTable<EF_CodeSegment>()
                .Relation("CodeSegment", EF_Codes.CodeType, EF_CodeSegment.CodeType)
                .Radar<RadarCodes>();


            dgwSegments.AutoGenerateColumns = false;
            dgwSegments.DataSource = dbManager.SlaveBinding("CodeSegment");

            eSegmentType = new EnumsManager<InputType>(sgmCodeType, "");
            //sgmCodeType.Items.AddRange(1, 2, 3);


            //sgmSegmentNo.DataPropertyName = AM_CodeSegment.Segment.Name;
            // sgmLength.DataPropertyName = AM_CodeSegment.InputLen.Name;
            //sgmHeader.DataPropertyName = AM_CodeSegment.Description.Name;
        }

        protected override void OnBindData()
        {
            BindControl(cbbCode, EF_Codes.CodeType, "SelectedValue", NullValue.NotSet);
            BindControl(txtDescription, EF_Codes.Description);
            BindControl(dgwSegments);

            BindColumn(sgmSegmentNo, EF_CodeSegment.Segment);
            BindColumn(sgmCodeType, EF_CodeSegment.InputType);
            BindColumn(sgmLength, EF_CodeSegment.InputLen);
            BindColumn(sgmHeader, EF_CodeSegment.Description);
            sgmCodeType.DataPropertyName = "Archive";


            dgwSegments.RowIndex = EF_CodeSegment.Segment;
        }

        protected override void OnDisableControlsForEdit()
        {
            cbbCode.Enabled = false;
            txtDescription.Enabled = false;
            base.OnDisableControlsForEdit();
        }

        protected override void OnDisableControlsForNew()
        {
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

        public int AddNewRow(string desc, int inputType, int inputLen)
        {
            var drw = dgwSegments.AddNewRow(EF_CodeSegment.Segment);

            drw.SetValue<string>(EF_CodeSegment.Description, desc);
            drw.SetValue<int>(EF_CodeSegment.InputType, inputType);
            drw.SetValue<int>(EF_CodeSegment.InputLen, inputLen);

            return drw.GetValue<int>(EF_CodeSegment.Segment);
        }

        private void InitializeAuxComponent()
        {
            cbbManager = new ComboBoxManager();
            cbbManager.CreateList<string>();
            foreach (KeyValuePair<string, string> codes in GlobalInfo.CodeTypes)
                cbbManager.AddValue(codes.Key, codes.Value);
            cbbManager.AttachTo(cbbCode);
            //eSegmentType = new EnumsManager<InputType>(sgmCodeType, Properties.Resources.ResourceManager);
        }

        private void cbbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDescription.Text = cbbCode.Text;
        }
    }

    #region dbmanagerCodes

    internal class dbmanagerCodes : ERPFramework.Data.DBManager
    {
        public dbmanagerCodes(DocumentForm document)
            : base(document)
        { }

        protected override void dAdapter_MasterRowUpdating(object sender, RowUpdatingEventArgs e)
        {
            base.dAdapter_RowUpdating(sender, e);
        }

        protected override void dAdapter_RowUpdating(object sender, RowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row[EF_CodeSegment.CodeType.Name] = GetColumn<string>(EF_Codes.CodeType, Pos);
            }
            base.dAdapter_RowUpdating(sender, e);
        }

        protected override void CreateMasterParam(SqlProxyParameterCollection parameters)
        {
            parameters.Add(
                new SqlProxyParameter("@p1", EF_Codes.CodeType)
                );

        }

        protected override string CreateMasterQuery(SqlProxyParameterCollection parameters)
        {
            return new QueryBuilder()
                .SelectAllFrom<EF_Codes>()
                .Where(EF_Codes.CodeType).IsEqualTo(parameters["@p1"])
                .Query;

        }



        protected override void SetParameters(IRadarParameters key, DataAdapterProperties dataadapterproperties)
        {
            dataadapterproperties.Parameters["@p1"].Value = key[EF_Codes.CodeType];
        }

        protected override string CreateSlaveQuery<T>(SqlProxyParameterCollection parameters)
        {
            if (typeof(T) == typeof(EF_CodeSegment))
            {
                var qb = new QueryBuilder().
                       SelectAllFrom<EF_CodeSegment>().
                        Where(EF_CodeSegment.CodeType).IsEqualTo(parameters["@p1"]).
                        OrderBy(EF_CodeSegment.Segment);

                return qb.Query;
            }

            return "";
        }

        protected override void CreateSlaveParam<T>(SqlProxyParameterCollection parameters)
        {
            if (typeof(T) == typeof(EF_CodeSegment))
            {
                parameters.Add(
                    new SqlProxyParameter("@p2", EF_CodeSegment.CodeType));
            }
        }
    }
    #endregion
}