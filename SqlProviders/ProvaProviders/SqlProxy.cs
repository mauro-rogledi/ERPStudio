using System;
using System.Data;
using System.Reflection;
using SqlProxyProvider;

namespace ProvaProviders
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
                    assembly = Assembly.LoadFrom(@"SQLProvider.dll");
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
    public sealed class SqlProxyConnection : ISqlProviderConnection, ICloneable, IDisposable
    {
        ISqlProviderConnection dbConnection;
        public ISqlProviderConnection Connection => dbConnection;

        public SqlProxyConnection(string connection = "")
        {
            if (!string.IsNullOrEmpty(connection))
                dbConnection = ProxyProviderLoader.CreateInstance<ISqlProviderConnection>("SqlProvider.SqlProviderConnection", connection);
        }

        public string ConnectionString
        {
            get => dbConnection.ConnectionString;
            set => dbConnection.ConnectionString = value;
        }

        public int ConnectionTimeout => dbConnection.ConnectionTimeout;

        public string Database => dbConnection.Database;

        public ConnectionState State => dbConnection.State;

        IDbConnection ISqlProviderConnection.Connection => dbConnection;

        public SqlProxyTransaction BeginTransaction()
        {
            return new SqlProxyTransaction(dbConnection.BeginTransaction());
        }

        public SqlProxyTransaction BeginTransaction(IsolationLevel il)
        {
            return new SqlProxyTransaction(dbConnection.BeginTransaction(il));
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
            var Cloned = new SqlProxyConnection
            {
                dbConnection = this.dbConnection
            };

            return Cloned;
        }

        public void Dispose()
        {
            dbConnection.Dispose();
        }

        IDbTransaction IDbConnection.BeginTransaction() => dbConnection.BeginTransaction();

        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel il) => dbConnection.BeginTransaction(il);
    }
    #endregion

    #region SqlProxyCommand
    public sealed class SqlProxyCommand : ISqlProviderCommand, ICloneable
    {
        ISqlProviderCommand dbCommand;
        ISqlProviderParameterCollection dbParameters;

        public IDbCommand Command => dbCommand as IDbCommand;

        public SqlProxyCommand()
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand");
            dbParameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }

        public SqlProxyCommand(string cmdText)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText);
            dbParameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }

        public SqlProxyCommand(string cmdText, SqlProxyConnection connection)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText, connection.Connection);
            dbParameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }
        public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlProxyTransaction transaction)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText, connection.Connection, transaction.Transaction);
            dbParameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand);
        }
        //public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
        //{

        //}


        public IDbConnection Connection { get => dbCommand.Connection; set => dbCommand.Connection = value; }
        public IDbTransaction Transaction { get => dbCommand.Transaction; set => dbCommand.Transaction = (value as ISqlProviderTransaction).Transaction; }
        public string CommandText { get => dbCommand.CommandText; set => dbCommand.CommandText = value; }
        public int CommandTimeout { get => dbCommand.CommandTimeout; set => dbCommand.CommandTimeout = value; }
        public CommandType CommandType { get => dbCommand.CommandType; set => dbCommand.CommandType = value; }

        public ISqlProviderParameterCollection Parameters => dbParameters;

        public UpdateRowSource UpdatedRowSource { get => dbCommand.UpdatedRowSource; set => dbCommand.UpdatedRowSource = value; }

        IDataParameterCollection IDbCommand.Parameters => throw new NotImplementedException();

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

        public SqlProxyDataReader ExecuteReader()
        {
            var exeread = dbCommand.ExecuteReader();
            return new SqlProxyDataReader(exeread);
        }

        public SqlProxyDataReader ExecuteReader(CommandBehavior behavior)
        {
            return new SqlProxyDataReader(dbCommand.ExecuteReader(behavior) as SqlProxyDataReader);
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

        IDataReader IDbCommand.ExecuteReader()
        {
            throw new NotImplementedException();
        }

        IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region SqlProxyTransaction
    public sealed class SqlProxyTransaction : ISqlProviderTransaction
    {
        IDbTransaction _dbTransaction;
        public IDbTransaction Transaction => _dbTransaction;

        public SqlProxyTransaction(IDbTransaction dbTransaction) => _dbTransaction = dbTransaction;

        public IDbConnection Connection => _dbTransaction.Connection;

        public IsolationLevel IsolationLevel => _dbTransaction.IsolationLevel;


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

    #region SqlProxyDataReader
    public sealed class SqlProxyDataReader : ISqlProviderDataReader
    {
        IDataReader dbDataReader;
        public IDataReader DataReader => dbDataReader;

        public SqlProxyDataReader(IDataReader idatareader) => dbDataReader = idatareader;

        public object this[int i] => dbDataReader[i];

        public object this[string name] => dbDataReader[name];

        //public object this[IColumn column] => _idataReader[column.Name];

        public int Depth => dbDataReader.Depth;

        public bool IsClosed => dbDataReader.IsClosed;

        public int RecordsAffected => dbDataReader.RecordsAffected;

        public int FieldCount => dbDataReader.FieldCount;

        public void Close() => dbDataReader.Close();

        public void Dispose() => dbDataReader.Dispose();

        public bool GetBoolean(int i) => dbDataReader.GetBoolean(i);

        public byte GetByte(int i) => dbDataReader.GetByte(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => dbDataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public char GetChar(int i) => dbDataReader.GetChar(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => dbDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

        public IDataReader GetData(int i) => dbDataReader.GetData(i);

        public string GetDataTypeName(int i) => dbDataReader.GetDataTypeName(i);

        public DateTime GetDateTime(int i) => dbDataReader.GetDateTime(i);

        public decimal GetDecimal(int i) => dbDataReader.GetDecimal(i);

        public double GetDouble(int i) => dbDataReader.GetDouble(i);

        public Type GetFieldType(int i) => dbDataReader.GetFieldType(i);

        public float GetFloat(int i) => dbDataReader.GetFloat(i);

        public Guid GetGuid(int i) => dbDataReader.GetGuid(i);

        public short GetInt16(int i) => dbDataReader.GetInt16(i);

        public int GetInt32(int i) => dbDataReader.GetInt32(i);

        public long GetInt64(int i) => dbDataReader.GetInt64(i);

        public string GetName(int i) => dbDataReader.GetName(i);

        public int GetOrdinal(string name) => dbDataReader.GetOrdinal(name);

        public DataTable GetSchemaTable() => dbDataReader.GetSchemaTable();

        public string GetString(int i) => dbDataReader.GetString(i);

        public object GetValue(int i) => dbDataReader.GetValue(i);

        public int GetValues(object[] values) => dbDataReader.GetValues(values);

        public bool IsDBNull(int i) => dbDataReader.IsDBNull(i);

        public bool NextResult() => dbDataReader.NextResult();

        public bool Read() => dbDataReader.Read();
    }
    #endregion

    #region SqlProxyParameter
    public class SqlProxyParameter : ISqlProviderParameter
    {
        ISqlProviderParameter dbParameter;
        public IDbDataParameter Parameter => dbParameter as IDbDataParameter;

        public SqlProxyParameter()
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter");
        }

        public SqlProxyParameter(string parameterName, SqlDbType dbType)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType);
        }
        public SqlProxyParameter(string parameterName, object value)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, value);
        }
        public SqlProxyParameter(string parameterName, SqlDbType dbType, int size)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType, size);
        }
        public SqlProxyParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType, size, sourceColumn);
        }

        public byte Precision { get => dbParameter.Precision; set => dbParameter.Precision = value; }
        public byte Scale { get => dbParameter.Scale; set => dbParameter.Scale = value; }
        public int Size { get => dbParameter.Size; set => dbParameter.Size = value; }
        public DbType DbType { get => dbParameter.DbType; set => dbParameter.DbType = value; }
        public ParameterDirection Direction { get => dbParameter.Direction; set => dbParameter.Direction = value; }

        public bool IsNullable => dbParameter.IsNullable;

        public string ParameterName { get => dbParameter.ParameterName; set => dbParameter.ParameterName = value; }
        public string SourceColumn { get => dbParameter.SourceColumn; set => dbParameter.SourceColumn = value; }
        public DataRowVersion SourceVersion { get => dbParameter.SourceVersion; set => dbParameter.SourceVersion = value; }
        public object Value
        {
            // TODO
            get => dbParameter.Value; set => dbParameter.Value = (value is Enum)
               ? ((Enum)value)//.Int()
               : value;
        }
    } 
    #endregion
}