using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderConnection : IDbConnection
    {
        SqlConnection sqlConnection;


        public SqlProviderConnection() => sqlConnection = new SqlConnection();


        public SqlProviderConnection(string connectionString) => sqlConnection = new SqlConnection(connectionString);

        public SqlProviderConnection(string connectionString, SqlCredential credential) => sqlConnection = new SqlConnection(connectionString, credential);

        public string ConnectionString { get => sqlConnection.ConnectionString; set => sqlConnection.ConnectionString = value; }

        public int ConnectionTimeout => sqlConnection.ConnectionTimeout;

        public string Database => sqlConnection.Database;

        public ConnectionState State => sqlConnection.State;

        public IDbTransaction BeginTransaction()
        {
            return sqlConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return sqlConnection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            sqlConnection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            sqlConnection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return sqlConnection.CreateCommand();
        }

        public void Dispose()
        {
            sqlConnection.Dispose();
        }

        public void Open()
        {
            sqlConnection.Open();
        }
    }
}
