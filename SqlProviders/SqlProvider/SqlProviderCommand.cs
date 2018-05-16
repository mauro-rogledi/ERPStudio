using SqlProxyProvider;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderCommand : SqlProxyProvider.ISqlProviderCommand
    {
        SqlCommand sqlCommand;

        public SqlProviderCommand() => sqlCommand = new SqlCommand();
        public SqlProviderCommand(IDbCommand command) => sqlCommand = command as SqlCommand;
        public SqlProviderCommand(string cmdText) => sqlCommand = new SqlCommand(cmdText);
        public SqlProviderCommand(string cmdText, ISqlProviderConnection connection) => sqlCommand = new SqlCommand(cmdText, connection.Connection as SqlConnection);
        public SqlProviderCommand(string cmdText, ISqlProviderConnection connection, ISqlProviderTransaction transaction) => sqlCommand = new SqlCommand(cmdText, connection.Connection as SqlConnection, transaction.Transaction as SqlTransaction);
        public IDbCommand Command => sqlCommand;

        public string CommandText { get => sqlCommand.CommandText; set => sqlCommand.CommandText = value; }
        public int CommandTimeout { get => sqlCommand.CommandTimeout; set => sqlCommand.CommandTimeout = value; }
        public CommandType CommandType { get => sqlCommand.CommandType; set => sqlCommand.CommandType = value; }
        //public SqlProviderCommand(string cmdText, SqlConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting);

        public IDbConnection Connection
        {
            get => sqlCommand.Connection;
            set
            {
                var connection = ((SqlProviderConnection)value).Connection;
                sqlCommand.Connection = (SqlConnection)connection;
            }
        }
        public IDbTransaction Transaction
        {
            get => sqlCommand.Transaction;
            set
            {
                var transaction = ((SqlProviderTransaction)value).Transaction;
                sqlCommand.Transaction = (SqlTransaction)transaction;
            }
        }

        public UpdateRowSource UpdatedRowSource { get => sqlCommand.UpdatedRowSource; set => sqlCommand.UpdatedRowSource = value; }

        IDataParameterCollection IDbCommand.Parameters => throw new NotImplementedException();

        public void Cancel() => sqlCommand.Cancel();

        public IDbDataParameter CreateParameter() => sqlCommand.CreateParameter();

        public void Dispose() => sqlCommand.Dispose();

        public int ExecuteNonQuery() => sqlCommand.ExecuteNonQuery();

        public IDataReader ExecuteReader() => sqlCommand.ExecuteReader();

        public IDataReader ExecuteReader(CommandBehavior behavior) => sqlCommand.ExecuteReader(behavior);

        public object ExecuteScalar() => sqlCommand.ExecuteScalar();

        public void Prepare() => sqlCommand.Prepare();
    }
}
