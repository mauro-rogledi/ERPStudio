using SqlProxyProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace ERPFramework.Data
{
    #region ProxyProviderLoader

    public enum ProviderType { SQLServer, SQLite, SQLCompact };

    public static class ProxyProviderLoader
    {
        public static ProviderType UseProvider { get; set; } = ProviderType.SQLServer;
        public static Assembly assembly;

        public static void LoadProvider()
        {
            var path = Application.StartupPath;
            switch (UseProvider)
            {
                case ProviderType.SQLServer:
                    assembly = Assembly.LoadFrom(Path.Combine(Application.StartupPath, @"SqlProviders\SQLServerProvider.dll"));
                    break;
                case ProviderType.SQLite:
                    assembly = Assembly.LoadFrom(Path.Combine(Application.StartupPath, @"SqlProviders\SqLiteProvider.dll"));
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

        public static bool HasProvider(ProviderType provider)
        {
            var fileName = string.Empty;
            switch (provider)
            {
                case ProviderType.SQLServer:
                    fileName = Path.Combine(Application.StartupPath, @"SqlProviders\SQLServerProvider.dll");
                    break;
                case ProviderType.SQLite:
                    fileName = Path.Combine(Application.StartupPath, @"SqlProviders\SqLiteProvider.dll");
                    break;
                default:
                    break;
            }

            return File.Exists(fileName);
        }
    }
    #endregion

    #region SqlProxyConnection
    public sealed class SqlProxyConnection : ICloneable, IDisposable
    {
        public ISqlProviderConnection Connection { get; private set; }

        public SqlProxyConnection(string connection = "")
        {
            if (!string.IsNullOrEmpty(connection))
                Connection = ProxyProviderLoader.CreateInstance<ISqlProviderConnection>("SqlProvider.SqlProviderConnection", connection);
        }

        public string ConnectionString
        {
            get => Connection.ConnectionString;
            set => Connection.ConnectionString = value;
        }

        public int ConnectionTimeout => Connection.ConnectionTimeout;

        public string Database => Connection.Database;

        public ConnectionState State => Connection.State;

        public SqlProxyTransaction BeginTransaction()
        {
            return new SqlProxyTransaction(Connection.BeginTransaction());
        }

        public SqlProxyTransaction BeginTransaction(IsolationLevel il)
        {
            return new SqlProxyTransaction(Connection.BeginTransaction(il));
        }

        public void ChangeDatabase(string databaseName)
        {
            Connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            Connection.Close();
        }

        public SqlProxyCommand CreateCommand()
        {
            return new SqlProxyCommand(Connection.CreateCommand());
        }

        public void Open()
        {
            Connection.Open();
        }

        public object Clone()
        {
            var Cloned = new SqlProxyConnection
            {
                Connection = this.Connection
            };

            return Cloned;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
    #endregion

    #region SqlProxyCommand
    public sealed class SqlProxyCommand : IDisposable, ICloneable
    {
        public ISqlProviderCommand Command { get; set; }
        SqlProxyConnection dbConnection;
        SqlProxyTransaction dbTransaction;

        public SqlProxyParameterCollection Parameters { get; }

        public SqlProxyCommand()
        {
            Command = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand");
            Parameters = new SqlProxyParameterCollection(Command);
        }

        public SqlProxyCommand(ISqlProviderCommand command)
        {
            Command = command;
            Parameters = new SqlProxyParameterCollection(Command);
        }

        public SqlProxyCommand(SqlProxyConnection connection)
            : this()
        {
            Connection = connection;
        }

        public SqlProxyCommand(IDbCommand command)
        {
            Command = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", command);
            Parameters = new SqlProxyParameterCollection(Command);
        }

        public SqlProxyCommand(string cmdText)
        {
            Command = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText);
            Parameters = new SqlProxyParameterCollection(Command);
        }

        public SqlProxyCommand(string cmdText, SqlProxyConnection connection)
        {
            dbConnection = connection;
            Command = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText, connection.Connection);
            Parameters = new SqlProxyParameterCollection(Command);
        }
        public SqlProxyCommand(string cmdText, SqlProxyConnection connection, SqlProxyTransaction transaction)
        {
            Command = ProxyProviderLoader.CreateInstance<ISqlProviderCommand>("SqlProvider.SqlProviderCommand", cmdText, connection.Connection, transaction.Transaction);
            Parameters = new SqlProxyParameterCollection(Command);
        }

        public SqlProxyConnection Connection
        {
            get => dbConnection;
            set { dbConnection = value; Command.Connection = dbConnection.Connection; }
        }

        public SqlProxyTransaction Transaction
        {
            get => dbTransaction;
            set { dbTransaction = value; Command.Transaction = dbTransaction; }
        }
        public string CommandText { get => Command.CommandText; set => Command.CommandText = value; }
        public int CommandTimeout { get => Command.CommandTimeout; set => Command.CommandTimeout = value; }
        public CommandType CommandType { get => Command.CommandType; set => Command.CommandType = value; }

        public UpdateRowSource UpdatedRowSource { get => Command.UpdatedRowSource; set => Command.UpdatedRowSource = value; }

        public void Cancel()
        {
            Command.Cancel();
        }

        public SqlProxyParameter CreateParameter()
        {
            return new SqlProxyParameter(Command.CreateParameter());
        }

        public void Dispose()
        {
            Command.Dispose();
        }

        public int ExecuteNonQuery()
        {
            return Command.ExecuteNonQuery();
        }

        public SqlProxyDataReader ExecuteReader()
        {
            var exeread = Command.ExecuteReader();
            return new SqlProxyDataReader(exeread);
        }

        public SqlProxyDataReader ExecuteReader(CommandBehavior behavior)
        {
            return new SqlProxyDataReader(Command.ExecuteReader(behavior));
        }

        public object ExecuteScalar()
        {
            return Command.ExecuteScalar();
        }

        public void Prepare()
        {
            Command.Prepare();
        }

        object ICloneable.Clone()
        {
            var Cloned = new SqlProxyCommand()
            {
                Command = this.Command
            };

            return Cloned;
        }
    }
    #endregion

    #region SqlProxyTransaction
    public sealed class SqlProxyTransaction : ISqlProviderTransaction
    {
        public IDbTransaction Transaction { get; }
        ISqlProviderTransaction dbTransaction;

        public SqlProxyTransaction(ISqlProviderTransaction dbTransaction)
        {
            Transaction = dbTransaction.Transaction;
            this.dbTransaction = dbTransaction;
        }

        public ISqlProviderConnection Connection => dbTransaction.Connection;

        public IsolationLevel IsolationLevel => Transaction.IsolationLevel;

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Dispose()
        {
            Transaction.Dispose();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }
    }
    #endregion

    #region SqlProxyDataReader
    public sealed class SqlProxyDataReader
    {
        IDataReader dbDataReader;
        public IDataReader DataReader => dbDataReader;

        public SqlProxyDataReader(ISqlProviderDataReader datareader) => dbDataReader = datareader.DataReader;

        public object this[int i] => dbDataReader[i];

        public object this[string name] => dbDataReader[name];

        public object this[IColumn column] => dbDataReader[column.Name];

        public int Depth => dbDataReader.Depth;

        public bool IsClosed => dbDataReader.IsClosed;

        public int RecordsAffected => dbDataReader.RecordsAffected;

        public int FieldCount => dbDataReader.FieldCount;

        public void Close() => dbDataReader.Close();

        public void Dispose() => dbDataReader.Dispose();

        public T GetValue<T>(IColumn column) => GetValue<T>(column.Name);

        public T GetValue<T>(string column)
        {
            return typeof(T).BaseType == typeof(Enum)
                ? dbDataReader[column] != DBNull.Value
                    ? (T)Enum.ToObject(typeof(T), dbDataReader[column])
                    : (T)Enum.ToObject(typeof(T), 0)
                : dbDataReader[column] != System.DBNull.Value
                    ? (T)Convert.ChangeType(dbDataReader[column], typeof(T))
                    : (T)Convert.ChangeType("", typeof(T));
        }

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

        public string GetString(string name) => dbDataReader.GetString(GetOrdinal(name));

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
        public ISqlProviderParameter Parameter { get; set; }

        public SqlProxyParameter()
        {
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter");
        }

        public SqlProxyParameter(string parameterName, IColumn column)
        {
            var dbType = ConvertColumnType.GetDBType(column.ColType);
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType);
            Parameter.Value = column.DefaultValue;
        }

        public SqlProxyParameter(string parameterName, IColumn column, object value)
        {
            var dbType = ConvertColumnType.GetDBType(column.ColType);
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType);
            Parameter.Value = value;
        }

        public SqlProxyParameter(IDbDataParameter parameter)
        {
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameter);
        }

        public SqlProxyParameter(string parameterName, DbType dbType)
        {
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType);
        }
        public SqlProxyParameter(string parameterName, object value)
        {
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, value);
        }
        public SqlProxyParameter(string parameterName, DbType dbType, int size)
        {
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType, size);
        }
        public SqlProxyParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            Parameter = ProxyProviderLoader.CreateInstance<ISqlProviderParameter>("SqlProvider.SqlProviderParameter", parameterName, dbType, size, sourceColumn);
        }

        public byte Precision { get => Parameter.Precision; set => Parameter.Precision = value; }
        public byte Scale { get => Parameter.Scale; set => Parameter.Scale = value; }
        public int Size { get => Parameter.Size; set => Parameter.Size = value; }
        public DbType DbType { get => Parameter.DbType; set => Parameter.DbType = value; }
        public ParameterDirection Direction { get => Parameter.Direction; set => Parameter.Direction = value; }

        public bool IsNullable => Parameter.IsNullable;

        public string ParameterName { get => Parameter.ParameterName; set => Parameter.ParameterName = value; }
        public string SourceColumn { get => Parameter.SourceColumn; set => Parameter.SourceColumn = value; }
        public DataRowVersion SourceVersion { get => Parameter.SourceVersion; set => Parameter.SourceVersion = value; }
        public object Value
        {
            // TODO
            get => Parameter.Value; set => Parameter.Value = (value is Enum)
               ? ((Enum)value)//.Int()
               : value;
        }

        IDbDataParameter ISqlProviderParameter.Parameter { get; set; }
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
                    if (_databaseHelper == null)
                    {
                        Monitor.Enter(lockWasTaken);
                        _databaseHelper = ProxyProviderLoader.CreateInstance<ISqlProxyDataBaseHelper>("SqlProvider.SqlProviderDatabaseHelper");
                    }
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


        public static bool SearchColumn(IColumn column, SqlProxyConnection connection) => databaseHelper.SearchColumn(column.Tablename, column.Name, connection.Connection.Connection);

        public static bool SearchTable<T>(SqlProxyConnection connection)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            var notfound = false;
            try
            {
                using (var cmd = new SqlProxyCommand(QuerySearchTable(tableName), connection))
                {
                    var dr = cmd.ExecuteReader();

                    notfound = !dr.Read();
                    dr.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return true;
            }
            return notfound;
        }

        public static async Task<List<string>> GetServers()
        {
            return await databaseHelper.GetServers();
        }

        public static async Task<List<string>> ListDatabase(string server, string userID, string password, bool integratedSecurity)
        {
            return await databaseHelper.ListDatabase(server, userID, password, integratedSecurity);
        }

        public static string ConvertDate(DateTime datetime) => databaseHelper.ConvertDate(datetime);
        public static string GetYear(string date) => databaseHelper.GetYear(date);
        public static string GetMonth(string date) => databaseHelper.GetMonth(date);
        public static string GetDay(string date) => databaseHelper.GetDay(date);
        public static string GetWeekOfYear(string date) => databaseHelper.GetWeekOfYear(date);
        public static string DayOfYear(string date) => databaseHelper.DayOfYear(date);
        public static string DayOfWeek(string date) => databaseHelper.DayOfWeek(date);
        public static DateTime GetServerDate(string connectionString) => databaseHelper.GetServerDate(connectionString);

        private static string QuerySearchTable(string tableName) => databaseHelper.QuerySearchTable(tableName);

    }
    #endregion

    #region SqlProxyDataAdapter
    public class SqlProxyDataAdapter
    {
        public IDbDataAdapter DataAdapter => dbDataAdapter.DataAdapter;
        public ISqlProviderDataAdapter dbDataAdapter;

        EventHandler<RowUpdatingEventArgs> RowUpdatingEventHandler;
        EventHandler<RowUpdatedEventArgs> RowUpdatedEventHandler;

        SqlProxyCommand selectCommand, insertCommand, updateCommand, deleteCommand;

        public SqlProxyDataAdapter()
        {
            dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter");
        }

        public SqlProxyDataAdapter(SqlProxyCommand selectCommand) => dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter", selectCommand.Command);

        public SqlProxyDataAdapter(string selectCommandText, string selectConnectionString) => dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter", selectCommandText, selectConnectionString);

        public SqlProxyDataAdapter(string selectCommandText, ISqlProviderConnection selectConnection) => dbDataAdapter = ProxyProviderLoader.CreateInstance<ISqlProviderDataAdapter>("SqlProvider.SqlProviderDataAdapter", selectConnection.Connection);

        public SqlProxyCommand SelectCommand
        {
            get => selectCommand;
            set
            {
                selectCommand = value;
                DataAdapter.SelectCommand = value.Command.Command;
            }
        }

        public SqlProxyCommand InsertCommand
        {
            get => insertCommand;
            set
            {
                insertCommand = value;
                DataAdapter.InsertCommand = value.Command.Command;
            }
        }
        public SqlProxyCommand UpdateCommand
        {
            get => updateCommand;
            set
            {
                updateCommand = value;
                DataAdapter.UpdateCommand = value.Command.Command;
            }
        }
        public SqlProxyCommand DeleteCommand
        {
            get => deleteCommand;
            set
            {
                deleteCommand = value;
                DataAdapter.DeleteCommand = value.Command.Command;
            }
        }
        public MissingMappingAction MissingMappingAction { get => DataAdapter.MissingMappingAction; set => DataAdapter.MissingMappingAction = value; }
        public MissingSchemaAction MissingSchemaAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ITableMappingCollection TableMappings => DataAdapter.TableMappings;

        public int Fill(DataTable dataTable) => dbDataAdapter.Fill(dataTable);
        public int Fill(DataSet dataSet, string srcTable) => dbDataAdapter.Fill(dataSet, srcTable);
        public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable) => dbDataAdapter.Fill(dataSet, startRecord, maxRecords, srcTable);

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType) => DataAdapter.FillSchema(dataSet, schemaType);

        public IDataParameter[] GetFillParameters() => DataAdapter.GetFillParameters();

        public int Update(DataSet dataSet) => DataAdapter.Update(dataSet);
        public int Update(DataSet dataSet, string table) => dbDataAdapter.Update(dataSet, table);

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
            CommandBuilder = ProxyProviderLoader.CreateInstance<ISqlProviderCommandBuilder>("SqlProvider.SqlProviderCommandBuilder", dataAdapter.dbDataAdapter);
        }

        public string QuoteSuffix { get => CommandBuilder.QuoteSuffix; set => CommandBuilder.QuoteSuffix = value; }
        public string QuotePrefix { get => CommandBuilder.QuotePrefix; set => CommandBuilder.QuotePrefix = value; }
        public SqlProxyDataAdapter DataAdapter
        {
            get => dataAdapter;
            set
            {
                dataAdapter = value;
                CommandBuilder.DataAdapter = dataAdapter.dbDataAdapter;
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

    #region SqlProxyParameterCollection
    public class SqlProxyParameterCollection
    {
        ISqlProviderParameterCollection Parameters { get; }
        public SqlProxyParameterCollection(SqlProxyCommand command)
        {
            Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", command.Command);
        }
        public SqlProxyParameterCollection(ISqlProviderCommand command)
        {
            Parameters = ProxyProviderLoader.CreateInstance<ISqlProviderParameterCollection>("SqlProvider.SqlProviderParameterCollection", command);
        }

        public ISqlProviderParameter this[string parameterName] { get => Parameters[parameterName];  }
        public ISqlProviderParameter this[int index] { get => Parameters[index]; }

        public ISqlProviderCommand Command => Parameters.Command;

        public bool IsReadOnly => Parameters.IsReadOnly;

        public bool IsFixedSize => Parameters.IsFixedSize;

        public int Count => Parameters.Count;

        public object SyncRoot => Parameters.SyncRoot;

        public bool IsSynchronized => Parameters.IsSynchronized;

        public void Add(SqlProxyParameter param)
        {
            Parameters.Add(param.Parameter);
        }

        public void Add(ISqlProviderParameter param)
        {
            Parameters.Add(param);
        }

        //public int Add(object value) => Parameters.Add(value);

        //public SqlProxyParameter Add(SqlProxyParameter param)
        //{
        //    Parameters.Add(param.Parameter);
        //    return param as SqlProxyParameter;
        //}

        public void AddRange(ISqlProviderParameterCollection param) => throw new Exception("missing");
          

        public void AddRange(ISqlProviderParameter[] param) => Parameters.AddRange(param);

        public void AddRange(List<ISqlProviderParameter> param) => Parameters.AddRange(param);

        public void Clear() => Parameters.Clear();

        public bool Contains(string parameterName) => Parameters.Contains(parameterName);

        public bool Contains(object value) => Parameters.Contains(value);

        //public void CopyTo(Array array, int index) => Parameters.CopyTo(array, index);
        //public IEnumerator GetEnumerator() => Parameters.GetEnumerator();

        public int IndexOf(string parameterName) => Parameters.IndexOf(parameterName);

        public int IndexOf(object value) => Parameters.IndexOf(value);

        public void Insert(int index, object value) => Parameters.Insert(index, value);

        public void Remove(object value) => Parameters.Remove(value);

        public void RemoveAt(string parameterName) => Parameters.RemoveAt(parameterName);

        public void RemoveAt(int index) => Parameters.RemoveAt(index);
    }
    #endregion
}