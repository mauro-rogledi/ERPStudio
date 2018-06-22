using SqlProxyProvider;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderCommand : ISqlProviderCommand , IDisposable
    {
        public IDbCommand Command => sqlCommand;

        SqlCommand sqlCommand;
        ISqlProviderConnection connection;
        ISqlProviderTransaction transaction;

        public SqlProviderCommand()
        {
            sqlCommand = new SqlCommand();
            Parameters = new SqlProviderParameterCollection(this);
        }
        public SqlProviderCommand(IDbCommand command)
        {
            sqlCommand = command as SqlCommand;
            Parameters = new SqlProviderParameterCollection(this);
        }
        public SqlProviderCommand(string cmdText)
        {
            sqlCommand = new SqlCommand(cmdText);
            Parameters = new SqlProviderParameterCollection(this);
        }
        public SqlProviderCommand(string cmdText, ISqlProviderConnection connection)
        {
            this.connection = connection;
            sqlCommand = new SqlCommand(cmdText, connection.Connection as SqlConnection);
            Parameters = new SqlProviderParameterCollection(this);
        }
        public SqlProviderCommand(string cmdText, ISqlProviderConnection connection, ISqlProviderTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
            sqlCommand = new SqlCommand(cmdText, connection.Connection as SqlConnection, transaction.Transaction as SqlTransaction);
            Parameters = new SqlProviderParameterCollection(this);
        }

        public string CommandText { get => sqlCommand.CommandText; set => sqlCommand.CommandText = value; }
        public int CommandTimeout { get => sqlCommand.CommandTimeout; set => sqlCommand.CommandTimeout = value; }
        public CommandType CommandType { get => sqlCommand.CommandType; set => sqlCommand.CommandType = value; }

        public ISqlProviderConnection Connection
        {
            get => connection;
            set
            {
                connection = value;
                sqlCommand.Connection = connection.Connection as SqlConnection;
            }
        }
        public ISqlProviderTransaction Transaction
        {
            get => transaction;
            set
            {
                transaction = value;
                sqlCommand.Transaction = transaction.Transaction as SqlTransaction;
            }
        }

        public UpdateRowSource UpdatedRowSource { get => sqlCommand.UpdatedRowSource; set => sqlCommand.UpdatedRowSource = value; }

        public ISqlProviderParameterCollection Parameters { get; }

        public void Cancel() => sqlCommand.Cancel();

        public ISqlProviderParameter CreateParameter() => new SqlProviderParameter(sqlCommand.CreateParameter());

        public void Dispose() => sqlCommand.Dispose();

        public int ExecuteNonQuery() => sqlCommand.ExecuteNonQuery();

        public ISqlProviderDataReader ExecuteReader() => new SqlProviderDataReader(sqlCommand.ExecuteReader());

        public ISqlProviderDataReader ExecuteReader(CommandBehavior behavior) => new SqlProviderDataReader(sqlCommand.ExecuteReader(behavior));

        public object ExecuteScalar() => sqlCommand.ExecuteScalar();

        public void Prepare() => sqlCommand.Prepare();
    }
}
