using ERPFramework.Forms;
using ERPFramework.Preferences;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERPFramework.Data
{
    public class DataModelManager : IDisposable
    {
        readonly DataSet documentDataSet = null;
        readonly IDataEntryBase dataEntry = null;
        readonly bool useTransaction = false;

        readonly SqlProxyConnection documentConnection = null;

        Action<SqlProxyParameterCollection, DataRow> setMasterParam = null;

        List <IDisposable> disposableObject = new List<IDisposable>();
        SqlProxyCommand masterCommand;
        SqlProxyDataAdapter masterDataAdapter;
        string masterTable;

        Dictionary<string, BindingSource> slaveBinding = new Dictionary<string, BindingSource>();
        Dictionary<string, SqlProxyDataAdapter> slaveDataAdapter = new Dictionary<string, SqlProxyDataAdapter>();

        #region public properties
        public BindingSource BindingMaster { get; private set; }
        public Dictionary<string, BindingSource> BindingSlave { get; private set; }
        #endregion

        #region public methods
        public DataModelManager MasterTable<M>(Func<SqlProxyParameterCollection, string> CreateMasterQuery, Action<SqlProxyParameterCollection> DeclareParam = null, Action<SqlProxyParameterCollection, DataRow> SetParam = null)
        {
            masterTable = typeof(M).GetType().Name;

            masterCommand = new SqlProxyCommand(documentConnection);
            DeclareParam?.Invoke(masterCommand.Parameters);
            masterCommand.CommandText = CreateMasterQuery?.Invoke(masterCommand.Parameters);

            setMasterParam = SetParam;

            masterDataAdapter = CreateDataAdapter(masterTable, masterCommand);

            BindingMaster = new BindingSource(documentDataSet, masterTable);

            disposableObject.Add(masterCommand);

            return this;
        }

        public DataModelManager SlaveTable<M, S>(string relactionname, IColumn master, IColumn slave)
        {
            return this;
        }

        public DataModelManager SlaveTable<M, S>(string relactionname, IColumn[] master, IColumn[] slave)
        {
            return this;
        }

        public bool Find(DataRow dr)
        {
            setMasterParam?.Invoke(masterCommand.Parameters, dr);
            var count = masterDataAdapter.Fill(documentDataSet, masterTable);

            return count > 0;
        }

        public void Edit()
        {
            slaveBinding.Values.ToList().ForEach(bs => bs.AllowNew = true);
        }

        public void Save()
        {
            var dataSet = documentDataSet.GetChanges();
            if (dataSet?.HasChanges() ?? false)
            {
                dataSet.AcceptChanges();
                slaveDataAdapter.ToList().ForEach(da =>
               {
                   da.Value.Update(documentDataSet, da.Key);
               });
            }
        }

        #endregion

        #region constructor
        internal DataModelManager(IDataEntryBase document)
        {
            this.dataEntry = document;
            useTransaction = new PreferencesManager<GlobalPreferences>("", null).ReadPreference().UseTransaction;

            documentConnection = new SqlProxyConnection(GlobalInfo.DBaseInfo.ConnectionString);
            documentDataSet = new DataSet(document.GetType().Name)
            {
                Locale = System.Globalization.CultureInfo.InvariantCulture
            };
        }

        #endregion

        #region Protected virtual method

        //protected abstract string CreateMasterQuery(SqlProxyParameterCollection parameters);

        //protected virtual string CreateSlaveQuery<T>(SqlProxyParameterCollection parameters)
        //{
        //    return string.Empty;
        //}

        //protected virtual void CreateMasterParam(SqlProxyParameterCollection parameters)
        //{
        //}

        //protected virtual void CreateSlaveParam<T>(SqlProxyParameterCollection parameters)
        //{
        //}

        //protected virtual void CreateSlaveParam<T, S>(SqlProxyParameterCollection parameters)
        //{
        //}
        #endregion

        public void Dispose()
        {
            disposableObject.ForEach(
                o => o.Dispose()
                );
            GC.SuppressFinalize(this);
        }

        private SqlProxyDataAdapter CreateDataAdapter(string name, SqlProxyCommand sqlCommand)
        {
            SqlProxyDataAdapter dAdapter = null;
            dAdapter = new SqlProxyDataAdapter(sqlCommand);

            try
            {
                dAdapter.FillSchema(documentDataSet, SchemaType.Source);
                dAdapter.Fill(documentDataSet, name);

                SetDefaultValue(name);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            return dAdapter;
        }


        private void SetDefaultValue(string name)
        {
            documentDataSet.Tables[name].Columns.Cast<DataColumn>().ToList().ForEach(
                col =>
                {
                    col.DefaultValue = ConvertColumnType.DefaultValue(col.DataType);
                });
        }

        #region Column Management

        public DataColumn AddColumn(IColumn column)
        {
            return AddColumn(column.Tablename, column.Name, column.ColType);
        }

        public DataColumn AddColumn(string tableName, string columnName, System.Type columnType)
        {
            try
            {
                return documentDataSet.Tables[tableName].Columns.Add(columnName, columnType);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }
        }

        public void SetColumn<T>(string tableName, string columnName, int row, T val)
        {
            if (row >= 0 && documentDataSet.Tables[tableName].Rows[row].RowState != DataRowState.Deleted)
                documentDataSet.Tables[tableName].Rows[row][columnName] = val;
        }

        public void SetColumn<T>(IColumn column, int row, T val)
        {
            if (row >= 0 && documentDataSet.Tables[column.Tablename].Rows[row].RowState != DataRowState.Deleted)
                documentDataSet.Tables[column.Tablename].Rows[row][column.Name] = val;
        }

        public T GetColumn<T>(IColumn column)
        {
            return GetColumn<T>(column, 0);
        }

        public T GetColumn<T>(IColumn column, int row)
        {
            if (documentDataSet.Tables[column.Tablename].Rows.Count < 1)
                return default(T);

            if (documentDataSet.Tables[column.Tablename].Rows[row].RowState == DataRowState.Deleted)
                return (T)GetColumn<T>(column, row, DataRowVersion.Original);

            if (documentDataSet.Tables[column.Tablename].Rows[row][column.Name] == System.DBNull.Value)
            {
                if (typeof(T) == typeof(string))
                    return (T)Convert.ChangeType("", typeof(T));
                else
                    return default(T);
            }
            if (typeof(T).IsEnum)
                return (T)System.Enum.ToObject(typeof(T), documentDataSet.Tables[column.Tablename].Rows[row][column.Name]);
            else
                return (T)Convert.ChangeType(documentDataSet.Tables[column.Tablename].Rows[row][column.Name], typeof(T));
        }

        public T GetColumn<T>(IColumn column, int row, DataRowVersion drs)
        {
            if (documentDataSet.Tables[column.Tablename].Rows.Count == 0 ||
                documentDataSet.Tables[column.Tablename].Rows[row][column.Name, drs] == System.DBNull.Value)
                return default(T);

            if (typeof(T).IsEnum)
                return (T)System.Enum.ToObject(typeof(T), documentDataSet.Tables[column.Tablename].Rows[row][column.Name, drs]);
            else
                return (T)Convert.ChangeType(documentDataSet.Tables[column.Tablename].Rows[row][column.Name, drs], typeof(T));
        }

        public DataTable GetDataTable<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            return documentDataSet.Tables[tableName];
        }

        public DataColumn GetDataColumn(IColumn column)
        {
            return documentDataSet.Tables[column.Tablename] == null
                ? null
                : documentDataSet.Tables[column.Tablename].Columns[column.Name];
        }

        public DataRow GetDataRow(string tableName, int row)
        {
            return documentDataSet.Tables[tableName].Rows[row];
        }

        public DataRow GetDataRow<T>(int row = 0)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            return documentDataSet.Tables[tableName].Rows[row];
        }

        #endregion
    }
}
