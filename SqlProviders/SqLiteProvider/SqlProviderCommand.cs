using SqlProxyProvider;
using System;
using System.Data;
using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderCommand : SqlProxyProvider.ISqlProviderCommand
    {
        SQLiteCommand sqliteCommand;

        public SqlProviderCommand() => sqliteCommand = new SQLiteCommand();
        public SqlProviderCommand(IDbCommand command) => sqliteCommand = command as SQLiteCommand;
        public SqlProviderCommand(string cmdText) => sqliteCommand = new SQLiteCommand(cmdText);
        public SqlProviderCommand(string cmdText, ISqlProviderConnection connection) => sqliteCommand = new SQLiteCommand(cmdText, connection.Connection as SQLiteConnection);
        public SqlProviderCommand(string cmdText, ISqlProviderConnection connection, ISqlProviderTransaction transaction) => sqliteCommand = new SQLiteCommand(cmdText, connection.Connection as SQLiteConnection, transaction.Transaction as SQLiteTransaction);
        public IDbCommand Command => sqliteCommand;

        public string CommandText { get => sqliteCommand.CommandText; set => sqliteCommand.CommandText = value; }
        public int CommandTimeout { get => sqliteCommand.CommandTimeout; set => sqliteCommand.CommandTimeout = value; }
        public CommandType CommandType { get => sqliteCommand.CommandType; set => sqliteCommand.CommandType = value; }
        //public SqlProviderCommand(string cmdText, SqlConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting);

        public IDbConnection Connection
        {
            get => sqliteCommand.Connection;
            set
            {
                var connection = ((SqlProviderConnection)value).Connection;
                sqliteCommand.Connection = (SQLiteConnection)connection;
            }
        }
        public IDbTransaction Transaction
        {
            get => sqliteCommand.Transaction;
            set
            {
                var transaction = ((SqlProviderTransaction)value).Transaction;
                sqliteCommand.Transaction = (SQLiteTransaction)transaction;
            }
        }

        public UpdateRowSource UpdatedRowSource { get => sqliteCommand.UpdatedRowSource; set => sqliteCommand.UpdatedRowSource = value; }

        IDataParameterCollection IDbCommand.Parameters => throw new NotImplementedException();

        public void Cancel() => sqliteCommand.Cancel();

        public IDbDataParameter CreateParameter() => sqliteCommand.CreateParameter();

        public void Dispose() => sqliteCommand.Dispose();

        public int ExecuteNonQuery() => sqliteCommand.ExecuteNonQuery();

        public IDataReader ExecuteReader() => sqliteCommand.ExecuteReader();

        public IDataReader ExecuteReader(CommandBehavior behavior) => sqliteCommand.ExecuteReader(behavior);

        public object ExecuteScalar() => sqliteCommand.ExecuteScalar();

        public void Prepare() => sqliteCommand.Prepare();
    }
}
