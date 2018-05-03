using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderDataAdapter : SqlProxyProvider.ISqlProviderDataAdapter
    {
        SQLiteDataAdapter sqliteDataAdapter;
        public IDbDataAdapter DataAdapter => sqliteDataAdapter;


        public SqlProviderDataAdapter(ISqlProviderCommand selectCommand) => sqliteDataAdapter = new SQLiteDataAdapter(selectCommand.Command as SQLiteCommand);

        public SqlProviderDataAdapter(string selectCommandText, string selectConnectionString) => sqliteDataAdapter = new SQLiteDataAdapter(selectCommandText, selectConnectionString);

        public SqlProviderDataAdapter(string selectCommandText, ISqlProviderConnection selectConnection) => sqliteDataAdapter = new SQLiteDataAdapter(selectCommandText, selectConnection.Connection as SQLiteConnection);

        public IDbCommand SelectCommand { get => sqliteDataAdapter.SelectCommand; set => sqliteDataAdapter.SelectCommand = value as SQLiteCommand; }
        public IDbCommand InsertCommand { get => sqliteDataAdapter.InsertCommand; set => sqliteDataAdapter.InsertCommand = value as SQLiteCommand; }
        public IDbCommand UpdateCommand { get => sqliteDataAdapter.UpdateCommand; set => sqliteDataAdapter.UpdateCommand = value as SQLiteCommand; }
        public IDbCommand DeleteCommand { get => sqliteDataAdapter.DeleteCommand; set => sqliteDataAdapter.DeleteCommand = value as SQLiteCommand; }
        public MissingMappingAction MissingMappingAction { get => sqliteDataAdapter.MissingMappingAction; set => sqliteDataAdapter.MissingMappingAction = value; }
        public MissingSchemaAction MissingSchemaAction { get => sqliteDataAdapter.MissingSchemaAction; set => sqliteDataAdapter.MissingSchemaAction = value; }

        public ITableMappingCollection TableMappings => sqliteDataAdapter.TableMappings;

        public int Fill(DataSet dataSet) => sqliteDataAdapter.Fill(dataSet);

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => sqliteDataAdapter.FillSchema(dataSet, schemaType);

        public IDataParameter[] GetFillParameters() => sqliteDataAdapter.GetFillParameters();

        public int Update(DataSet dataSet) => sqliteDataAdapter.Update(dataSet);
    }
}
