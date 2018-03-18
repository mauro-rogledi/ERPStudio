using System;
using System.Data;
using System.Reflection;

namespace ERPFramework.Data
{
    #region ProxyProviderLoader

    public static class ProxyProviderLoader
    {
        public enum ProviderType { SQL, SQLite };
        public static ProviderType ProviderTypes { get; set; } = ProviderType.SQL;
        public static Assembly assembly;

        public static void LoadProvider()
        {
            switch (ProviderTypes)
            {
                case ProviderType.SQL:
                    assembly = Assembly.LoadFrom(@"SqlProviders\SQLProvider.dll");
                    break;
            }
        }

        public static T CreateInstance<T>(string nameSpace, params object[] parameters)
        {
            if (assembly == null)
                LoadProvider();

            Type classType = assembly.GetType(nameSpace);
            return (T)Activator.CreateInstance(classType, parameters);
        }
    }
    #endregion

    #region SqlProxyConnection
    public sealed class SqlProxyConnection : IDbConnection, ICloneable, IDisposable
    {
        IDbConnection dbConnection;

        public SqlProxyConnection(string connection = "")
        {
            if (!connection.IsEmpty())
                dbConnection = ProxyProviderLoader.CreateInstance<IDbConnection>("SqlProvider.SqlProviderConnection", connection);
        }

        public string ConnectionString
        {
            get => dbConnection.ConnectionString;
            set => dbConnection.ConnectionString = value;
        }

        public int ConnectionTimeout => dbConnection.ConnectionTimeout;

        public string Database => dbConnection.Database;

        public ConnectionState State => dbConnection.State;

        public IDbTransaction BeginTransaction()
        {
            return dbConnection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return dbConnection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            dbConnection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            dbConnection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return dbConnection.CreateCommand();
        }

        public void Open()
        {
            dbConnection.Open();
        }

        public object Clone()
        {
            var Cloned = new SqlProxyConnection()
            {
                dbConnection = this.dbConnection
            };

            return Cloned;
        }

        public void Dispose()
        {
            dbConnection.Dispose();
        }
    }
    #endregion

    public sealed class SqlProxyCommand : IDbCommand, ICloneable
    {
        IDbCommand dbCommand;

        public SqlProxyCommand()
        {
            dbCommand = ProxyProviderLoader.CreateInstance<IDbCommand>("SqlProvider.SqlProviderCommand");
        }

        public SqlProxyCommand(string cmdText)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<IDbCommand>("SqlProvider.SqlProviderCommand", cmdText);
        }

        public SqlProxyCommand(string cmdText, SqlProxyConnection connection)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<IDbCommand>("SqlProvider.SqlProviderCommand", cmdText, connection);
        }
        //public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlTransaction transaction)
        //{

        //}
        //public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
        //{

        //}


        public IDbConnection Connection { get => dbCommand.Connection; set => dbCommand.Connection = value; }
        public IDbTransaction Transaction { get => dbCommand.Transaction; set => dbCommand.Transaction = value; }
        public string CommandText { get => dbCommand.CommandText; set => dbCommand.CommandText = value; }
        public int CommandTimeout { get => dbCommand.CommandTimeout; set => dbCommand.CommandTimeout = value; }
        public CommandType CommandType { get => dbCommand.CommandType; set => dbCommand.CommandType = value; }

        public IDataParameterCollection Parameters => dbCommand.Parameters;

        public UpdateRowSource UpdatedRowSource { get => dbCommand.UpdatedRowSource; set => dbCommand.UpdatedRowSource = value; }

        public void Cancel()
        {
            dbCommand.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return dbCommand.CreateParameter();
        }

        public void Dispose()
        {
            dbCommand.Dispose();
        }

        public int ExecuteNonQuery()
        {
            return dbCommand.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            return dbCommand.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return dbCommand.ExecuteReader(behavior);
        }

        public object ExecuteScalar()
        {
            return dbCommand.ExecuteScalar();
        }

        public void Prepare()
        {
            dbCommand.Prepare();
        }

        object ICloneable.Clone()
        {
            var Cloned = new SqlProxyCommand()
            {
                dbCommand = this.dbCommand
            };

            return Cloned;
        }
    }
}