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
    public sealed class SqlProxyConnection : ICloneable, IDisposable
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

    #region SqlProxyCommand
    public sealed class SqlProxyCommand : ICloneable
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

        public SqlProxyDataReader ExecuteReader(CommandBehavior behavior)
        {
            return new SqlProxyDataReader(dbCommand.ExecuteReader(behavior));
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
    #endregion

    #region SqlProxyTransaction
    public sealed class SqlProxyTransaction : ICloneable
    {
        IDbTransaction _dbTransaction;

        public SqlProxyTransaction(IDbTransaction dbTransaction) => _dbTransaction = dbTransaction;

        public IDbConnection Connection => _dbTransaction.Connection;

        public IsolationLevel IsolationLevel => _dbTransaction.IsolationLevel;

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }
    }
    #endregion

    public sealed class SqlProxyDataReader
    {
        IDataReader _idataReader;

        public SqlProxyDataReader(IDataReader idatareader) => _idataReader = idatareader;

        public object this[int i] => _idataReader[i];

        public object this[string name] => _idataReader[name];

        public object this[IColumn column] => _idataReader[column.Name];

        public int Depth => _idataReader.Depth;

        public bool IsClosed => _idataReader.IsClosed;

        public int RecordsAffected => _idataReader.RecordsAffected;

        public int FieldCount => _idataReader.FieldCount;

        public void Close() => _idataReader.Close();

        public void Dispose() => _idataReader.Dispose();

        public bool GetBoolean(int i) => _idataReader.GetBoolean(i);

        public byte GetByte(int i) => _idataReader.GetByte(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => _idataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public char GetChar(int i) => _idataReader.GetChar(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }
    }
}