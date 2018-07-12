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

        Action<SqlProxyParameterCollection, DataRow> setMasterParam;

        List <IDisposable> disposableObject = new List<IDisposable>();

        Dictionary<string, BindingSource> slaveBinding = new Dictionary<string, BindingSource>();
        Dictionary<string, DataModelProperties> datamodelProperties = new Dictionary<string, DataModelProperties>();
        string masterTable;

        #region public properties
        public BindingSource BindingMaster { get; private set; }
        public Dictionary<string, BindingSource> BindingSlave { get; private set; }
        #endregion

        #region public methods
        public DataModelManager MasterTable<M>(Func<SqlProxyParameterCollection, string> CreateMasterQuery, Action<SqlProxyParameterCollection> DeclareParam = null, Action<SqlProxyParameterCollection, DataRow> SetParam = null)
        {
            masterTable = typeof(M).GetType().Name;
            datamodelProperties[masterTable] = new DataModelProperties(masterTable);

            datamodelProperties[masterTable].Command = new SqlProxyCommand(documentConnection);
            DeclareParam?.Invoke(datamodelProperties[masterTable].Parameters);
            datamodelProperties[masterTable].Command.CommandText = CreateMasterQuery?.Invoke(datamodelProperties[masterTable].Parameters);

            setMasterParam = SetParam;

            CreateDataAdapter(datamodelProperties[masterTable]);

            BindingMaster = new BindingSource(documentDataSet, masterTable);

            disposableObject.Add(datamodelProperties[masterTable].Command);

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
            setMasterParam?.Invoke(datamodelProperties[masterTable].Parameters, dr);
            var count = datamodelProperties[masterTable].DataAdapter.Fill(documentDataSet, masterTable);

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
                datamodelProperties.Values.ToList().ForEach(da =>
               {
                   //if (da.TableName != masterTable)
                   da.DataAdapter.Update(documentDataSet, da.TableName);
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

        private void CreateDataAdapter(DataModelProperties property)
        {
            property.DataAdapter = CreateDataAdapter(property.TableName, property.Command);
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
