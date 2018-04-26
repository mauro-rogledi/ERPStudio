using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderConnection : ISqlProviderConnection
    {
        SQLiteConnection sqlConnection;

        public IDbConnection Connection => sqlConnection;

        public SqlProviderConnection() => sqlConnection = new SQLiteConnection();


        public SqlProviderConnection(string connectionString) => sqlConnection = new SQLiteConnection(connectionString);

        public string ConnectionString { get => sqlConnection.ConnectionString; set => sqlConnection.ConnectionString = value; }

        public int ConnectionTimeout => sqlConnection.ConnectionTimeout;

        public string Database => sqlConnection.Database;

        public ConnectionState State => sqlConnection.State;

        public ISqlProviderTransaction BeginTransaction()
        {
            return new SqlProviderTransaction(sqlConnection.BeginTransaction());
        }

        public ISqlProviderTransaction BeginTransaction(IsolationLevel il)
        {
            return new SqlProviderTransaction(sqlConnection.BeginTransaction(il));
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

        IDbTransaction IDbConnection.BeginTransaction() => new SqlProviderTransaction(sqlConnection.BeginTransaction());


        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel il) => new SqlProviderTransaction(sqlConnection.BeginTransaction());
    }
}
