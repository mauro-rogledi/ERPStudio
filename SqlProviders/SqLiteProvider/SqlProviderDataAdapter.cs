using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderDataAdapter : ISqlProviderDataAdapter
    {
        SQLiteDataAdapter sqliteDataAdapter;
        public IDbDataAdapter DataAdapter => sqliteDataAdapter;

        public event EventHandler<RowUpdatingEventArgs> RowUpdating;
        public event EventHandler<RowUpdatedEventArgs> RowUpdated;

        public SqlProviderDataAdapter() => sqliteDataAdapter = new SQLiteDataAdapter();

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

        public int Fill(DataTable dataTable) => sqliteDataAdapter.Fill(dataTable);
        public int Fill(DataSet dataSet) => sqliteDataAdapter.Fill(dataSet);
        public int Fill(DataSet dataSet, string tableName) => sqliteDataAdapter.Fill(dataSet, tableName);
        public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable) => sqliteDataAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => sqliteDataAdapter.FillSchema(dataSet, schemaType);

        public IDataParameter[] GetFillParameters() => sqliteDataAdapter.GetFillParameters();

        public int Update(DataSet dataSet) => sqliteDataAdapter.Update(dataSet);
        public int Update(DataSet dataSet, string srcTable) => sqliteDataAdapter.Update(dataSet, srcTable);

        private void SqlDataAdapter_RowUpdated(object sender, RowUpdatedEventArgs e) =>
            RowUpdated?.Invoke(sender, e);

        private void SqlDataAdapter_RowUpdating(object sender, RowUpdatingEventArgs e) =>
            RowUpdating?.Invoke(sender, e);
    }
}
