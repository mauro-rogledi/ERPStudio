using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderDataAdapter : SqlProxyProvider.ISqlProviderDataAdapter
    {
        SqlDataAdapter sqlDataAdapter;
        public IDbDataAdapter DataAdapter => sqlDataAdapter;

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

        public int Fill(DataSet dataSet) => sqlDataAdapter.Fill(dataSet);

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => sqlDataAdapter.FillSchema(dataSet, schemaType);

        public IDataParameter[] GetFillParameters() => sqlDataAdapter.GetFillParameters();

        public int Update(DataSet dataSet) => sqlDataAdapter.Update(dataSet);
    }
}
