using SqlProxyProvider;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderConnection : ISqlProviderConnection
    {
        SqlConnection sqlConnection;

        public IDbConnection Connection => sqlConnection;

        public SqlProviderConnection() => sqlConnection = new SqlConnection();


        public SqlProviderConnection(string connectionString) => sqlConnection = new SqlConnection(connectionString);

        public SqlProviderConnection(string connectionString, SqlCredential credential) => sqlConnection = new SqlConnection(connectionString, credential);

        public string ConnectionString { get => sqlConnection.ConnectionString; set => sqlConnection.ConnectionString = value; }

        public int ConnectionTimeout => sqlConnection.ConnectionTimeout;

        public string Database => sqlConnection.Database;

        public ConnectionState State => sqlConnection.State;

        public ISqlProviderTransaction BeginTransaction()
        {
            return new SqlProviderTransaction(this, sqlConnection.BeginTransaction());
        }

        public ISqlProviderTransaction BeginTransaction(IsolationLevel il)
        {
            return new SqlProviderTransaction(this, sqlConnection.BeginTransaction(il));
        }

        public void ChangeDatabase(string databaseName)
        {
            sqlConnection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            sqlConnection.Close();
        }

        public void Dispose()
        {
            sqlConnection.Dispose();
        }

        public void Open()
        {
            sqlConnection.Open();
        }

        public ISqlProviderCommand CreateCommand()
        {
            return new SqlProviderCommand(sqlConnection.CreateCommand());
        }
    }
}
