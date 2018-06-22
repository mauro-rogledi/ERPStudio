using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using ERPFramework.Data;
using ERPFramework.DataGridViewControls;
using ERPFramework.Forms;
using System.Linq;

namespace ERPFramework.Controls
{
    public interface IRadarForm
    {
        IRadarParameters GetRadarParameters(string text);
        bool Find(IRadarParameters param);
        bool CanOpenNew { get; }
        void OpenNew(IRadarParameters param);
        string Description { get; set; }
        string Seed { set; get; }
        event RadarForm.RadarFormRowSelectedEventHandler RadarFormRowSelected;

        DialogResult ShowDialog(IWin32Window mainForm);
        void Dispose();
        bool OpenBrowse(IRadarParameters iParam);
    }

    public abstract partial class RadarForm : AskForm, IRadarForm
    {
        private DataSet rdrDataSet;
        private SqlProxyConnection rdrConnection;
        private SqlProxyDataAdapter rdrDataAdapter;
        private string code = string.Empty;

        protected SqlProxyCommand rdrFindSqlCommand;
        protected SqlProxyCommand rdrBrowseSqlCommand;

        protected string strBrowseQuery = string.Empty;
        protected string strFindQuery = string.Empty;
        protected IColumn rdrCodeColumn;
        protected IColumn rdrDescColumn;
        protected NameSpace rdrNameSpace;
        protected SqlProxyParameterCollection rdrParameters;

        abstract protected string DefineBrowseQuery(SqlProxyCommand sqlCmd, string findQuery);

        abstract protected bool DefineFindQuery(SqlProxyCommand sqlCmd);

        abstract protected void PrepareFindQuery(IRadarParameters parameter);

        abstract protected void PrepareFindParameters();

        abstract protected IRadarParameters PrepareRadarParameters(DataGridViewRow row);

        abstract public IRadarParameters GetRadarParameters(string text);

        abstract public string GetCodeFromParameters(IRadarParameters param);

        virtual protected void PrepareBrowseQuery() { }

        virtual protected void OnFound(SqlProxyDataReader sqlReader)
        {
            if (RadarFormFoundRecord != null)
                RadarFormFoundRecord(this, sqlReader);
        }

        /// <summary>
        /// The value contained in description column
        /// </summary>
        ///
        public string FindQuery { get; set; }

        public string FilterType { get; set; }

        public string Description { get; set; }

        public string Seed { set { code = value; } get { return code; } }

        public bool EnableAddOnFly { get; set; }

        public bool MustExistCode { get; set; }

        public System.Resources.ResourceManager ResourceManager { get; set; }

        public delegate void RadarFormRowSelectedEventHandler(object sender, RadarFormRowSelectedArgs pe);

        public event RadarFormRowSelectedEventHandler RadarFormRowSelected;

        public delegate void RadarFormFoundRecordEventHandler(object sender, SqlProxyDataReader sqlReader);

        public event RadarFormFoundRecordEventHandler RadarFormFoundRecord;

        protected RadarForm()
        {
            Left = GlobalInfo.MainForm.Left + 50;
            Top = GlobalInfo.MainForm.Top + 50;
            StartPosition = FormStartPosition.Manual;
            rdrConnection = GlobalInfo.DBaseInfo.dbManager.DB_Connection;
            MustExistCode = true;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =  new System.Drawing.Font("Segoe UI", 11);
        }

        protected override void OnLoad(EventArgs e)
        {
            InitializeConnection();
            AddColumnToGrid();
            if (GlobalInfo.globalPref.ExpandSearchWindow)
                WindowState = FormWindowState.Maximized;

            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (txtFilter != null)
            {
                txtFilter.Width = Width - 244;
                txtFilter.Invalidate();
            }
        }

        public bool CanOpenNew
        {
            get { return !rdrNameSpace.ToString().IsEmpty(); }
        }

        public void OpenNew(IRadarParameters param)
        {
            Debug.Assert(rdrNameSpace.ToString().IsEmpty(), "Missing namespace");
            this.code = GetCodeFromParameters(param);
            if (!rdrNameSpace.ToString().IsEmpty())
            {
                var frm = ModulesHelper.OpenDocument.Create(rdrNameSpace) as DocumentForm;
                frm.PostLoaded += new DocumentForm.PostFormLoadEventHandler(frm_PostLoadedForNew);
                ModulesHelper.OpenDocument.Show(frm);
                Requery();
            }
        }

        public bool OpenBrowse(IRadarParameters param)
        {
            var ok = false;
            Debug.Assert(!rdrNameSpace.ToString().IsEmpty(), "Missing namespace");
            this.code = GetCodeFromParameters(param);
            if (!rdrNameSpace.ToString().IsEmpty())
            {
                var frm = ModulesHelper.OpenDocument.Create(rdrNameSpace) as DocumentForm;
                ModulesHelper.OpenDocument.Show(frm);
                ok =frm.FindRecord(param);
            }
            return ok;
        }

        private void frm_PostLoadedForNew(object sender, ERPFramework.Data.DBManager dbManager)
        {
            var frm = (DocumentForm)sender;
            frm.AddNew();
            Debug.Assert(frm.KeyControl != null, "Missing KeyControl");
            frm.KeyControl.Text = code;
        }

        public bool Find(IRadarParameters param)
        {
            var found = false;
            InitializeConnection();

            PrepareFindQuery(param);

            var reader = rdrFindSqlCommand.ExecuteReader();

            if (reader.Read())
            {
                OnFound(reader);
                found = true;
            }
            reader.Close();
            return found;
        }

        public bool ValidateValue(IRadarParameters code, out string error)
        {
            error = string.Empty;
            if (code != null && !Find(code))
            {
                if (EnableAddOnFly && CanOpenNew)
                {
                    var result = MessageBox.Show(Properties.Resources.Rdr_CodeNotFound,
                    Properties.Resources.Warning,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                        OpenNew(code);
                }
                else if (MustExistCode)
                {
                    error = string.Format(Properties.Resources.Rdr_NotFound, code);
                    MessageBox.Show(error,
                    Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            else
            {
                if (code == null)
                {
                    Description = string.Empty;
                    return true;
                }
            }

            return true;
        }

        private void InitializeConnection()
        {
            if (rdrDataSet != null) return;

            CreateConnection();
            PrepareFindParameters();
            DefineFindQuery(rdrFindSqlCommand);
        }

        private void CreateConnection()
        {
            rdrDataSet = new DataSet("GridDataSet");
            rdrFindSqlCommand = new SqlProxyCommand()
            {
                Connection = rdrConnection
            };

            rdrBrowseSqlCommand = new SqlProxyCommand
            {
                Connection = rdrConnection
            };
            rdrParameters = new SqlProxyParameterCollection(rdrBrowseSqlCommand);

            rdrBrowseSqlCommand.CommandText = DefineBrowseQuery(rdrBrowseSqlCommand, string.Empty);
            rdrDataAdapter = new SqlProxyDataAdapter(rdrBrowseSqlCommand);
        }

        protected override void OnShown(EventArgs e)
        {
            PrepareBrowseQuery();

            Requery();

            rdrDataSet.Clear();
            rdrDataAdapter.Fill(rdrDataSet, "Radar");

            dataGridView1.DataSource = rdrDataSet.Tables["Radar"];

            txtFilter.Focus();
        }

        private void AddColumnToGrid()
        {
            if (dataGridView1.Columns.Count != 0) return;


            var rdrTable = rdrCodeColumn.Table;
            for (int t = 0; t < rdrTable.VisibleInRadarCount; t++)
            {
                var col = rdrTable.VisibleInRadarColumn(t);

                var txtColumn = new TrimMaskedTextBoxDataGridViewColumn
                {
                    Name = col.Name,
                    HeaderText = col.Description,
                    DataPropertyName = col.Name,
                    EnumsType = col.EnumType,
                    SortMode = DataGridViewColumnSortMode.Automatic,
                    AutoSizeMode = t < rdrTable.VisibleInRadarCount - 1
                                            ? DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                                            : DataGridViewAutoSizeColumnMode.Fill,
                    IsVirtual = col.IsVirtual
                };
                txtColumn.DefaultCellStyle.Alignment = col.ColType == typeof(int) || col.ColType == typeof(DateTime)
                                                        ? DataGridViewContentAlignment.MiddleRight
                                                        : DataGridViewContentAlignment.MiddleLeft;

                if (col.AlignRight)
                    txtColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (col.EnumType != typeof(System.DBNull) || col.IsVirtual)
                {
                    dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
                    txtColumn.ResourceManager = ResourceManager;
                }
                if (col.Description == string.Empty)
                    txtColumn.Visible = false;
                txtColumn.ReadOnly = true;
                dataGridView1.Columns.Add(txtColumn);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] != null && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                if (((TrimMaskedTextBoxDataGridViewColumn)dataGridView1.Columns[e.ColumnIndex]).IsVirtual)
                    FormatColumns(dataGridView1.Rows[e.RowIndex], dataGridView1.Columns[e.ColumnIndex].DataPropertyName);
            }
        }

        protected virtual void FormatColumns(DataGridViewRow row, string columnName)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SelectRow(e.RowIndex);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public override bool OnOk()
        {
            if (dataGridView1.CurrentRow != null)
            {
                SelectRow(dataGridView1.CurrentRow.Index);
                this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;

            return base.OnOk();
        }

        private void SelectRow(int rowIndex)
        {
            if (rdrDescColumn != null)
                Description = dataGridView1.GetValue<string>(rowIndex, rdrDescColumn);

            if (rdrCodeColumn != null)
                Seed = dataGridView1.GetValue<string>(rowIndex, rdrCodeColumn);

            if (rowIndex >= 0)
                RadarFormRowSelected?.Invoke(this, new RadarFormRowSelectedArgs(PrepareRadarParameters(dataGridView1[rowIndex])));
        }

        private void Requery()
        {
            var requery = new SqlProxyCommand()
            {
                Connection = rdrConnection
            };

            var strSelect = string.IsNullOrEmpty(FindQuery) ? string.Empty : FindQuery;
            strSelect = DefineBrowseQuery(requery, strSelect);
            PrepareBrowseQuery();

            rdrDataSet.Clear();
            rdrDataAdapter.SelectCommand = requery;
            requery.CommandText = strSelect;
            rdrDataAdapter.Fill(rdrDataSet, "Radar");
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    SelectRow(dataGridView1.CurrentRow.Index);
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.Cancel;

                this.Close();
            }
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text.IsEmpty())
            {
                dataGridView1.DataView.RowFilter = "";
            }
            else
                if (e.KeyCode == Keys.Enter)
                dataGridView1.DataView.RowFilter = CreateFilter(txtFilter.Text.Split(' '));
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text.EndsWith(" "))
                dataGridView1.DataView.RowFilter = CreateFilter(txtFilter.Text.Split(' '));
        }

        private string CreateFilter(string[] filters)
        {
            var result = string.Empty;
            var concat = "AND ";
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                foreach (string filter in filters)
                {
                    if (col.Visible && filter.Length > 0)
                    {
                        result = result.SeparConcat(col.Name + " LIKE '%" + filter + "%'", concat);
                        concat = "AND ";
                    }
                }
                concat = "OR ";
            }
            return result;
        }

    }

    public class RadarFormRowSelectedArgs : EventArgs
    {
        public IRadarParameters parameters;

        public RadarFormRowSelectedArgs(IRadarParameters parameters)
        {
            this.parameters = parameters;
        }
    }

    public interface IRadarParameters
    {
        object this[string index] { get; }
        object this[IColumn col] { get; }

        Dictionary<string, object> Params { get; set; }

        string GetLockKey();

        T GetValue<T>(string key);
        T GetValue<T>(IColumn col);
    }

    public class RadarParameters : IRadarParameters
    {
        public object this[string index]
        {
            get { return Params[index]; }
        }

        public object this[IColumn col]
        {
            get { return Params[col.Name]; }
        }

        public void Add(IColumn col, object obj)
        {
            Params.Add(col.Name, obj);
        }

        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();

        public string GetLockKey()
        {
            var builder = new System.Text.StringBuilder();

            Params.Values.ToList().ForEach(v => builder.Append(v.ToString()+"\t"));

            return builder.ToString();
        }

        public T GetValue<T>(string key)
        {
            return (T)Convert.ChangeType(Params[key], typeof(T));
        }

        public T GetValue<T>(IColumn col)
        {
            return (T)Convert.ChangeType(Params[col.Name], typeof(T));
        }
    }
}