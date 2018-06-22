using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProxyProvider;

namespace SqlProvider
{
    public class SqlProviderDataAdapter : ISqlProviderDataAdapter
    {
        SqlDataAdapter sqlDataAdapter;
        public IDbDataAdapter DataAdapter => sqlDataAdapter;

        EventHandler<RowUpdatingEventArgs> RowUpdatingEventHandler;
        EventHandler<RowUpdatedEventArgs> RowUpdatedEventHandler;

        public SqlProviderDataAdapter() => sqlDataAdapter = new SqlDataAdapter();

        public SqlProviderDataAdapter(ISqlProviderCommand selectCommand) => sqlDataAdapter = new SqlDataAdapter(selectCommand.Command as SqlCommand);

        public SqlProviderDataAdapter(string selectCommandText, string selectConnectionString) => sqlDataAdapter = new SqlDataAdapter(selectCommandText, selectConnectionString);

        public SqlProviderDataAdapter(string selectCommandText, ISqlProviderConnection selectConnection) => sqlDataAdapter = new SqlDataAdapter(selectCommandText, selectConnection.Connection as SqlConnection);

        public IDbCommand SelectCommand { get => sqlDataAdapter.SelectCommand; set => sqlDataAdapter.SelectCommand = value as SqlCommand; }
        public IDbCommand InsertCommand { get => sqlDataAdapter.InsertCommand; set => sqlDataAdapter.InsertCommand = value as SqlCommand; }
        public IDbCommand UpdateCommand { get => sqlDataAdapter.UpdateCommand; set => sqlDataAdapter.UpdateCommand = value as SqlCommand; }
        public IDbCommand DeleteCommand { get => sqlDataAdapter.DeleteCommand; set => sqlDataAdapter.DeleteCommand = value as SqlCommand; }
        public MissingMappingAction MissingMappingAction { get => sqlDataAdapter.MissingMappingAction; set => sqlDataAdapter.MissingMappingAction = value; }
        public MissingSchemaAction MissingSchemaAction { get => sqlDataAdapter.MissingSchemaAction; set => sqlDataAdapter.MissingSchemaAction = value; }

        public ITableMappingCollection TableMappings => sqlDataAdapter.TableMappings;

        ISqlProviderCommand ISqlProviderDataAdapter.SelectCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ISqlProviderCommand ISqlProviderDataAdapter.InsertCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ISqlProviderCommand ISqlProviderDataAdapter.UpdateCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ISqlProviderCommand ISqlProviderDataAdapter.DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Fill(DataTable dataTable) => sqlDataAdapter.Fill(dataTable);
        public int Fill(DataSet dataSet) => sqlDataAdapter.Fill(dataSet);
        public int Fill(DataSet dataSet, string srcTable) => sqlDataAdapter.Fill(dataSet, srcTable);
        public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable) => sqlDataAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => sqlDataAdapter.FillSchema(dataSet, schemaType);

        public IDataParameter[] GetFillParameters() => sqlDataAdapter.GetFillParameters();

        public int Update(DataSet dataSet) => sqlDataAdapter.Update(dataSet);
        public int Update(DataSet dataSet, string srcTable) => sqlDataAdapter.Update(dataSet, srcTable);

        public event EventHandler<RowUpdatingEventArgs> RowUpdating
        {
            add
            {
                sqlDataAdapter.RowUpdating += SqlDataAdapter_RowUpdating;
                RowUpdatingEventHandler += value;
            }

            remove
            {
                sqlDataAdapter.RowUpdating -= SqlDataAdapter_RowUpdating;
                RowUpdatingEventHandler -= value;
            }
        }

        public event EventHandler<RowUpdatedEventArgs> RowUpdated
        {
            add
            {
                sqlDataAdapter.RowUpdated += SqlDataAdapter_RowUpdated;
                RowUpdatedEventHandler += value;
            }

            remove
            {
                sqlDataAdapter.RowUpdated -= SqlDataAdapter_RowUpdated;
                RowUpdatedEventHandler -= value;
            }
        }

        private void SqlDataAdapter_RowUpdated(object sender, SqlRowUpdatedEventArgs e) =>
            RowUpdatedEventHandler?.Invoke(sender, new RowUpdatedEventArgs(e.Row, e.Command, e.StatementType, e.TableMapping));

        private void SqlDataAdapter_RowUpdating(object sender, SqlRowUpdatingEventArgs e) =>
            RowUpdatingEventHandler?.Invoke(sender, new RowUpdatingEventArgs(e.Row, e.Command, e.StatementType, e.TableMapping));
    }

}

