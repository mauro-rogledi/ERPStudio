using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using SqlProxyProvider;

namespace ProvaProviders
{
    #region ProxyProviderLoader

    public enum ProviderType { SQL, SQLite };

    public static class ProxyProviderLoader
    {
        public static ProviderType UseProvider { get; set; } = ProviderType.SQL;
        public static Assembly assembly;

        public static void LoadProvider()
        {
            switch (UseProvider)
            {
                case ProviderType.SQL:
                    assembly = Assembly.LoadFrom(@"..\..\..\..\ERPManager\bin\Debug\SqlProviders\SQLServerProvider.dll");
                    break;
                case ProviderType.SQLite:
                    assembly = Assembly.LoadFrom(@"..\..\..\..\ERPManager\bin\Debug\SqlProviders\SqLiteProvider.dll");
                    break;
            }
        }

        public static T CreateInstance<T>(string nameSpace, params object[] parameters)
        {
            if (assembly == null)
                LoadProvider();

            Type classType = assembly.GetType(nameSpace);
            var tutti = assembly.GetTypes();
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

        public IDbCommand Command => dbCommand as IDbCommand;

        public SqlProxyCommand()
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand");
            Parameters = new SqlProxyParameterCollection(dbCommand);
            //Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }

        public SqlProxyCommand(IDbCommand command)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", command);
            Parameters = new SqlProxyParameterCollection(dbCommand);
            //Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }

        public SqlProxyCommand(string cmdText)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText);
            Parameters = new SqlProxyParameterCollection(dbCommand);
            //Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }

        public SqlProxyCommand(string cmdText, SqlProxyConnection connection)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText, connection.Connection);
            Parameters = new SqlProxyParameterCollection(dbCommand.Command);
            //Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand.Command);
        }
        public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlProxyTransaction transaction)
        {
            dbCommand = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText, connection.Connection, transaction.Transaction);
            Parameters = new SqlProxyParameterCollection(dbCommand.Command);
            //ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", dbCommand);
        }
        //public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlTransaction transaction, SqlCommandColumnEncryptionSetting columnEncryptionSetting)
        //{

        //}


        public IDbConnection Connection { get => dbCommand.Connection; set => dbCommand.Connection = value; }
        public IDbTransaction Transaction { get => dbCommand.Transaction; set => dbCommand.Transaction = (value as ISqlProviderTransaction).Transaction; }
        public string CommandText { get => dbCommand.CommandText; set => dbCommand.CommandText = value; }
        public int CommandTimeout { get => dbCommand.CommandTimeout; set => dbCommand.CommandTimeout = value; }
        public CommandType CommandType { get => dbCommand.CommandType; set => dbCommand.CommandType = value; }

        public SqlProxyParameterCollection Parameters { get; }
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

        public SqlProxyParameter(string parameterName, DbType dbType)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType);
        }
        public SqlProxyParameter(string parameterName, object value)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, value);
        }
        public SqlProxyParameter(string parameterName, DbType dbType, int size)
        {
            dbParameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType, size);
        }
        public SqlProxyParameter(string parameterName, DbType dbType, int size, string sourceColumn)
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

    #region SqlProxyConnectionStringbuilder
    public class SqlProxyConnectionStringbuilder : ISqlProviderConnectionStringBuilder
    {
        ISqlProviderConnectionStringBuilder sqlProviderConnectionStringBuilder;

        public SqlProxyConnectionStringbuilder()
        {
            sqlProviderConnectionStringBuilder = ProxyProviderLoader.CreateInstance<ISqlProviderConnectionStringBuilder>("SqlProvider.SqlProviderConnectionStringBuilder");
        }

        public string ConnectionString => sqlProviderConnectionStringBuilder.ConnectionString;

        public string DataSource { get => sqlProviderConnectionStringBuilder.DataSource; set => sqlProviderConnectionStringBuilder.DataSource = value; }
        public string UserID { get => sqlProviderConnectionStringBuilder.UserID; set => sqlProviderConnectionStringBuilder.UserID = value; }
        public string InitialCatalog { get => sqlProviderConnectionStringBuilder.InitialCatalog; set => sqlProviderConnectionStringBuilder.InitialCatalog = value; }
        public string Password { get => sqlProviderConnectionStringBuilder.Password; set => sqlProviderConnectionStringBuilder.Password = value; }
        public bool IntegratedSecurity { get => sqlProviderConnectionStringBuilder.IntegratedSecurity; set => sqlProviderConnectionStringBuilder.IntegratedSecurity = value; }
    }
    #endregion

    #region SqlProxyDatabaseHelper
    public static class SqlProxyDatabaseHelper
    {
        static bool lockWasTaken = false;
        static ISqlProxyDataBaseHelper _databaseHelper;
        static ISqlProxyDataBaseHelper databaseHelper
        {
            get
            {
                try
                {
                    Monitor.Enter(lockWasTaken);
                    if (_databaseHelper == null)
                        _databaseHelper = ProxyProviderLoader.CreateInstance<ISqlProxyDataBaseHelper>("SqlProvider.SqlProviderDatabaseHelper");
                }
                // body
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(lockWasTaken);
                    }
                }
                return _databaseHelper;
            }
        }

        public static string DataSource { get => databaseHelper.DataSource; set => databaseHelper.DataSource = value; }
        public static string UserID { get => databaseHelper.UserID; set => databaseHelper.UserID = value; }
        public static string InitialCatalog { get => databaseHelper.InitialCatalog; set => databaseHelper.InitialCatalog = value; }
        public static string Password { get => databaseHelper.Password; set => databaseHelper.Password = value; }
        public static bool IntegratedSecurity { get => databaseHelper.IntegratedSecurity; set => databaseHelper.IntegratedSecurity = value; }

        public static void CreateDatabase() => databaseHelper.CreateDatabase();

        public static void CreateDatabase(string connectionString) => databaseHelper.CreateDatabase(connectionString);

        public static string QuerySearchTable(string tableName) => databaseHelper.QuerySearchTable(tableName);

        //public static bool SearchColumn(IColumn column, SqlProxyConnection connection) => databaseHelper.SearchColumn(column.Tablename, column.Name, connection.Connection.Connection);

    }
    #endregion

    #region SqlProxyDataAdapter
    public class SqlProxyDataAdapter
    {
        ISqlProviderDataAdapter dbDataAdapter;
        public IDbDataAdapter DataAdapter => dbDataAdapter;

        EventHandler<RowUpdatingEventArgs> RowUpdatingEventHandler;
        EventHandler<RowUpdatedEventArgs> RowUpdatedEventHandler;

        SqlProxyCommand selectCommand, insertCommand, updateCommand, deleteCommand;

        public SqlProxyDataAdapter()
        {
            dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter");
        }

        public SqlProxyDataAdapter(ISqlProviderCommand selectCommand) => dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter", selectCommand.Command);

        public SqlProxyDataAdapter(string selectCommandText, string selectConnectionString) => dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter", selectCommandText, selectConnectionString);

        public SqlProxyDataAdapter(string selectCommandText, ISqlProviderConnection selectConnection) => dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter", selectConnection.Connection);

        public SqlProxyCommand SelectCommand
        {
            get => selectCommand;
            set
            {
                selectCommand = value;
                dbDataAdapter.SelectCommand = (value.Command as ISqlProviderCommand).Command;
            }
        }

        public SqlProxyCommand InsertCommand
        {
            get => insertCommand;
            set
            {
                insertCommand = value;
                dbDataAdapter.InsertCommand = (value.Command as ISqlProviderCommand).Command;
            }
        }
        public SqlProxyCommand UpdateCommand
        {
            get => updateCommand;
            set
            {
                updateCommand = value;
                dbDataAdapter.UpdateCommand = (value.Command as ISqlProviderCommand).Command;
            }
        }
        public SqlProxyCommand DeleteCommand
        {
            get => deleteCommand;
            set
            {
                deleteCommand = value;
                dbDataAdapter.DeleteCommand = (value.Command as ISqlProviderCommand).Command;
            }
        }
        public MissingMappingAction MissingMappingAction { get => dbDataAdapter.MissingMappingAction; set => dbDataAdapter.MissingMappingAction = value; }
        public MissingSchemaAction MissingSchemaAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ITableMappingCollection TableMappings => dbDataAdapter.TableMappings;

        public int Fill(DataTable dataTable) => dbDataAdapter.Fill(dataTable);
        public int Fill(DataSet dataSet) => dbDataAdapter.Fill(dataSet);
        public int Fill(DataSet dataSet, string srcTable) => dbDataAdapter.Fill(dataSet, srcTable);
        public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable) => dbDataAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => dbDataAdapter.FillSchema(dataSet, schemaType);

        public IDataParameter[] GetFillParameters() => dbDataAdapter.GetFillParameters();

        public int Update(DataSet dataSet) => dbDataAdapter.Update(dataSet);

        public event EventHandler<RowUpdatingEventArgs> RowUpdating
        {
            add
            {
                dbDataAdapter.RowUpdating += SqlDataAdapter_RowUpdating;
                RowUpdatingEventHandler += value;
            }

            remove
            {
                dbDataAdapter.RowUpdating -= SqlDataAdapter_RowUpdating;
                RowUpdatingEventHandler -= value;
            }
        }

        public event EventHandler<RowUpdatedEventArgs> RowUpdated
        {
            add
            {
                dbDataAdapter.RowUpdated += SqlDataAdapter_RowUpdated;
                RowUpdatedEventHandler += value;
            }

            remove
            {
                dbDataAdapter.RowUpdated -= SqlDataAdapter_RowUpdated;
                RowUpdatedEventHandler -= value;
            }
        }

        private void SqlDataAdapter_RowUpdating(object sender, RowUpdatingEventArgs e)
        {
            RowUpdatingEventHandler?.Invoke(sender, e);
        }

        private void SqlDataAdapter_RowUpdated(object sender, RowUpdatedEventArgs e)
        {
            RowUpdatedEventHandler?.Invoke(sender, e);
        }
    }
    #endregion

    #region SqlProxyCommandBuilder
    public class SqlProxyCommandBuilder
    {
        public ISqlProviderCommandBuilder CommandBuilder { get; }

        SqlProxyDataAdapter dataAdapter;

        public SqlProxyCommandBuilder()
        {
            CommandBuilder = ProxyProviderLoader.CreateInstance<ISqlProviderCommandBuilder>("SqlProvider.SqlProviderCommandBuilder");
        }

        public SqlProxyCommandBuilder(SqlProxyDataAdapter dataAdapter)
        {
            CommandBuilder = ProxyProviderLoader.CreateInstance<ISqlProviderCommandBuilder>("SqlProvider.SqlProviderCommandBuilder", dataAdapter.DataAdapter);
        }

        public string QuoteSuffix { get => CommandBuilder.QuoteSuffix; set => CommandBuilder.QuoteSuffix = value; }
        public string QuotePrefix { get => CommandBuilder.QuotePrefix; set => CommandBuilder.QuotePrefix = value; }
        public SqlProxyDataAdapter DataAdapter
        {
            get => dataAdapter;
            set
            {
                dataAdapter = value;
                CommandBuilder.DataAdapter = value.DataAdapter;
            }
        }
        public ConflictOption ConflictOption { get => CommandBuilder.ConflictOption; set => CommandBuilder.ConflictOption = value; }

        public SqlProxyCommand GetDeleteCommand() => new SqlProxyCommand(CommandBuilder.GetDeleteCommand());

        public SqlProxyCommand GetDeleteCommand(bool useColumnsForParameterNames) => new SqlProxyCommand(CommandBuilder.GetDeleteCommand(useColumnsForParameterNames));

        public SqlProxyCommand GetInsertCommand() => new SqlProxyCommand(CommandBuilder.GetInsertCommand());
        public SqlProxyCommand GetInsertCommand(bool useColumnsForParameterNames) => new SqlProxyCommand(CommandBuilder.GetInsertCommand(useColumnsForParameterNames));

        public SqlProxyCommand GetUpdateCommand() => new SqlProxyCommand(CommandBuilder.GetUpdateCommand());
        public SqlProxyCommand GetUpdateCommand(bool useColumnsForParameterNames) => new SqlProxyCommand(CommandBuilder.GetUpdateCommand(useColumnsForParameterNames));
    }
    #endregion

    public class SqlProxyParameterCollection
    {
        ISqlProviderParameterCollection Parameters { get;  }
        public SqlProxyParameterCollection(IDbCommand command)
        {
            Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", command);
        }

        public object this[string parameterName] { get => Parameters[parameterName]; set => Parameters[parameterName] = value; }
        public object this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDbCommand Command => Parameters.Command;

        public bool IsReadOnly => Parameters.IsReadOnly;

        public bool IsFixedSize => Parameters.IsFixedSize;

        public int Count => Parameters.Count;

        public object SyncRoot => Parameters.SyncRoot;

        public bool IsSynchronized => Parameters.IsSynchronized;

        public ISqlProviderParameter Add(ISqlProviderParameter param) => Parameters.Add(param);

        public int Add(object value) => Parameters.Add(value);

        public SqlProxyParameter Add(SqlProxyParameter param) => Parameters.Add(param) as SqlProxyParameter;

        public void AddRange(ISqlProviderParameter[] param) => Parameters.AddRange(param);

        public void AddRange(List<ISqlProviderParameter> param) => Parameters.AddRange(param);

        public void Clear() => Parameters.Clear();

        public bool Contains(string parameterName) => Parameters.Contains(parameterName);

        public bool Contains(object value) => Parameters.Contains(value);

        public void CopyTo(Array array, int index) => Parameters.CopyTo(array, index);
        public IEnumerator GetEnumerator() => Parameters.GetEnumerator();

        public int IndexOf(string parameterName) => Parameters.IndexOf(parameterName);

        public int IndexOf(object value) => Parameters.IndexOf(value);

        public void Insert(int index, object value) => Parameters.Insert(index, value);

        public void Remove(object value) => Parameters.Remove(value);

        public void RemoveAt(string parameterName) => Parameters.RemoveAt(parameterName);

        public void RemoveAt(int index) => Parameters.RemoveAt(index);
    }
}