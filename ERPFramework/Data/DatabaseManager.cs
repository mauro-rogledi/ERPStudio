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
    public abstract class DatabaseManager : IDisposable
    {
        readonly DataSet documentDataSet = null;
        readonly IDocument document = null;
        readonly bool useTransaction = false;

        readonly SqlProxyConnection documentConnection = null;

        List<IDisposable> disposableObject = new List<IDisposable>();

        #region public properties
        public BindingSource BindingMaster { get; private set; }
        public Dictionary<string, BindingSource> BindingSlave { get; private set; }
        #endregion


        #region public methods
        public DatabaseManager AddMaster<M>()
        {
            var tableName = typeof(M).GetType().Name;

            var sqlCommand = new SqlProxyCommand(documentConnection);
            sqlCommand.CommandText = CreateMasterQuery(sqlCommand.Parameters);

            var dataAdapter = CreateDataAdapter(tableName, sqlCommand);

            BindingMaster = new BindingSource(documentDataSet, tableName);

            disposableObject.Add(sqlCommand);

            return this;
        }

        public DatabaseManager AddSlave<M, S>(string relactionname, IColumn master, IColumn slave)
        {
            return this;
        }

        public DatabaseManager AddSlave<M, S>(string relactionname, IColumn[] master, IColumn[] slave)
        {
            return this;
        }

        #endregion

        #region constructor
        protected DatabaseManager(IDocument document)
        {
            this.document = document;
            useTransaction = new PreferencesManager<GlobalPreferences>("", null).ReadPreference().UseTransaction;

            documentConnection = new SqlProxyConnection(GlobalInfo.DBaseInfo.ConnectionString);
            documentDataSet = new DataSet(document.GetType().Name)
            {
                Locale = System.Globalization.CultureInfo.InvariantCulture
            };
        }

        #endregion

        #region Protected virtual method

        protected abstract string CreateMasterQuery(SqlProxyParameterCollection parameters);

        protected virtual string CreateSlaveQuery<T>(SqlProxyParameterCollection parameters)
        {
            return string.Empty;
        }

        protected virtual void CreateMasterParam(SqlProxyParameterCollection parameters)
        {
        }

        protected virtual void CreateSlaveParam<T>(SqlProxyParameterCollection parameters)
        {
        }

        protected virtual void CreateSlaveParam<T, S>(SqlProxyParameterCollection parameters)
        {
        }
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
                col => {
                    col.DefaultValue = ConvertColumnType.DefaultValue(col.DataType);
                });
        }
    }
}
