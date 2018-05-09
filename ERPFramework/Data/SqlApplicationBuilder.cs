using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#if(SQLServer)

using System.Data.SqlClient;

#endif
#if(SQLCompact)

using System.Data.SqlServerCe;

#endif
#if (SQLite)

using System.Data.SQLite;

#endif

namespace ERPFramework.Data
{
    #region SqlABConnection

    public sealed class SqlABConnection : ICloneable
    {
        internal ProviderType providerType;// = ProviderType.SQLServer;
#if(SQLServer)
        internal SqlConnection sqlConnection;
#endif
#if(SQLCompact)
        internal SqlCeConnection sqlCeConnection;
#endif
#if(SQLite)
        internal SQLiteConnection sQLiteConnection;
#endif

        public ProviderType ProviderType
        {
            get { return providerType; }
        }

        public SqlABConnection(ProviderType ProviderType)
        {
            this.providerType = ProviderType;
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlConnection = new SqlConnection();
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeConnection = new SqlCeConnection();
                    break;
#endif
#if (SQLite)
                case ProviderType.SQLite:
                    sQLiteConnection = new SQLiteConnection();
                    break;
#endif
            }
        }

        public SqlABConnection()
            : this(GlobalInfo.LoginInfo.ProviderType, GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString)
        { }

        public SqlABConnection(ProviderType ProviderType, string connectionString)
        {
            this.providerType = ProviderType;
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlConnection = new SqlConnection(connectionString);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeConnection = new SqlCeConnection(connectionString);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    sQLiteConnection = new SQLiteConnection(connectionString);
                    break;
#endif
            }
        }

        public ConnectionState State
        {
            get
            {
                switch (providerType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlConnection.State;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return sqlCeConnection.State;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return sQLiteConnection.State;
#endif
                    default:
                        return ConnectionState.Broken;
                }
            }
        }

        public void Close()
        {
            switch (providerType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlConnection.Close();
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeConnection.Close();
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    sQLiteConnection.Close();
                    break;
#endif
            }
        }

        public void Open()
        {
            switch (providerType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlConnection.Open();
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeConnection.Open();
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    sQLiteConnection.Open();
                    break;
#endif
            }
        }

        public void ChangeDatabase(string database)
        {
            switch (providerType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlConnection.ChangeDatabase(database);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeConnection.ChangeDatabase(database);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    break;
#endif
            }
        }

        public SqlABTransaction BeginTransaction()
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return new SqlABTransaction(sqlConnection.BeginTransaction(IsolationLevel.RepeatableRead));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return new SqlABTransaction(sqlCeConnection.BeginTransaction(IsolationLevel.RepeatableRead));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return new SqlABTransaction(sQLiteConnection.BeginTransaction(IsolationLevel.RepeatableRead));
#endif
            }
            return null;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private SqlABConnection Clone()
        {
            var Cloned = new SqlABConnection(this.providerType)
            {

#if (SQLCompact)
                sqlCeConnection = sqlCeConnection,
#endif
#if (SQLServer)
                sqlConnection = sqlConnection,
#endif
#if (SQLite)
                sQLiteConnection = sQLiteConnection,
#endif
                providerType = providerType
            };

            return Cloned;
        }
    }

    #endregion

    #region SqlProxyCommand

    public sealed class SqlProxyCommand : ICloneable, IDisposable
    {
        internal ProviderType ProviderType;//
        internal SqlABConnection connection;
#if(SQLServer)

        internal SqlCommand SqlCommand { get; private set; }

#endif
#if(SQLCompact)

        public SqlCeCommand SqlCeCommand { get; private set; }

#endif
#if(SQLite)

        public SQLiteCommand SQLiteCommand { get; private set; }

#endif

        public SqlABParameterCollection Parameters { get; private set; }

        public SqlABDataReader SqlABDataReader { get; private set; }

        public SqlABConnection Connection
        {
            set
            {
                connection = value;
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        SqlCommand.Connection = value.sqlConnection;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        SqlCeCommand.Connection = value.sqlCeConnection;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        SQLiteCommand.Connection = value.sQLiteConnection;
                        break;
#endif
                }
            }
            get
            {
                return connection;
            }
        }

        public string CommandText
        {
            set
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        SqlCommand.CommandText = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        SqlCeCommand.CommandText = value;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        SQLiteCommand.CommandText = value;
                        break;
#endif
                }
            }

            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return SqlCommand.CommandText;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return SqlCeCommand.CommandText;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return SQLiteCommand.CommandText;
#endif
                    default:
                        return "";
                }
            }
        }

        public SqlProxyCommand()
            : this(GlobalInfo.LoginInfo.ProviderType)
        {
            Connection = GlobalInfo.SqlConnection;
        }

        public SqlProxyCommand(SqlABConnection connection)
            : this(GlobalInfo.LoginInfo.ProviderType)
        {
            Connection = connection;
        }

        public SqlProxyCommand(ProviderType ProviderType, SqlABConnection connection)
            : this(ProviderType)
        {
            this.Connection = connection;
        }

        public SqlProxyCommand(ProviderType ProviderType)
        {
            this.ProviderType = ProviderType;

            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.SqlCommand = new SqlCommand();
                    this.Parameters = new SqlABParameterCollection(SqlCommand);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.SqlCeCommand = new SqlCeCommand();
                    this.Parameters = new SqlABParameterCollection(SqlCeCommand);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.SQLiteCommand = new SQLiteCommand();
                    this.Parameters = new SqlABParameterCollection(SQLiteCommand);
                    break;
#endif
            }
        }

        public SqlProxyCommand(ProviderType ProviderType, string cmdText)
        {
            this.ProviderType = ProviderType;

            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.SqlCommand = new SqlCommand(cmdText);
                    this.Parameters = new SqlABParameterCollection(SqlCommand);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.SqlCeCommand = new SqlCeCommand(cmdText);
                    this.Parameters = new SqlABParameterCollection(SqlCeCommand);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.SQLiteCommand = new SQLiteCommand(cmdText);
                    this.Parameters = new SqlABParameterCollection(SQLiteCommand);
                    break;
#endif
            }
        }

        public SqlProxyCommand(string cmdText, SqlABConnection connection)
        {
            this.ProviderType = connection.providerType;

            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.SqlCommand = new SqlCommand(cmdText, connection.sqlConnection);
                    this.Parameters = new SqlABParameterCollection(SqlCommand);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.SqlCeCommand = new SqlCeCommand(cmdText, connection.sqlCeConnection);
                    this.Parameters = new SqlABParameterCollection(SqlCeCommand);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.SQLiteCommand = new SQLiteCommand(cmdText, connection.sQLiteConnection);
                    this.Parameters = new SqlABParameterCollection(SQLiteCommand);
                    break;
#endif
            }
        }

#if(SQLite)

        public SqlProxyCommand(SQLiteCommand command)
        {
            this.ProviderType = ProviderType.SQLite;
            this.SQLiteCommand = command;
            this.Parameters = new SqlABParameterCollection(SQLiteCommand);
        }

#endif
#if(SQLCompact)

        public SqlProxyCommand(SqlCeCommand command)
        {
            this.ProviderType = ProviderType.SQLCompact;
            this.SqlCeCommand = command;
            this.Parameters = new SqlABParameterCollection(SqlCeCommand);
        }

#endif
#if(SQLServer)

        public SqlProxyCommand(SqlCommand command)
        {
            this.ProviderType = ProviderType.SQLServer;
            this.SqlCommand = command;
            this.Parameters = new SqlABParameterCollection(SqlCommand);
        }

#endif

        // @@TODO Transazioni
        //public SqlProxyCommand(ProviderType ProviderType, string cmdText, SqlABConnection connection, SqlTransaction transaction)
        //{
        //    {
        //        this.ProviderType = ProviderType;

        //        switch (ProviderType)
        //        {
        //            case ProviderType.SQLServer:
        //                this.SqlCommand = new SqlCommand(cmdText, connection.SqlConnection);
        //                break;
        //            case ProviderType.SqlCompact:
        //                this.SqlCeCommand = new SqlCeCommand(cmdText, connection.SqlCeConnection,);
        //                break;
        //        }
        //    }
        //}

        public SqlABDataReader ExecuteReader()
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    SqlABDataReader = new SqlABDataReader(this.SqlCommand.ExecuteReader());
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    SqlABDataReader = new SqlABDataReader(this.SqlCeCommand.ExecuteReader());
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    SqlABDataReader = new SqlABDataReader(this.SQLiteCommand.ExecuteReader());
                    break;
#endif
            }

            return SqlABDataReader;
        }

        public object ExecuteScalar()
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return this.SqlCommand.ExecuteScalar();
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return this.SqlCeCommand.ExecuteScalar();
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return this.SQLiteCommand.ExecuteScalar();
#endif
                default:
                    return null;
            }
        }

        public int ExecuteNonQuery()
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return this.SqlCommand.ExecuteNonQuery();
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return this.SqlCeCommand.ExecuteNonQuery();
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return this.SQLiteCommand.ExecuteNonQuery();
#endif
                default:
                    return -1;
            }
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private SqlProxyCommand Clone()
        {
            var Cloned = new SqlProxyCommand(this.ProviderType);
            return Cloned;
        }

        public void Dispose()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    if (SqlCeCommand != null)
                        SqlCeCommand.Dispose();
                    break;
#endif
#if(SQLServer)
                case ProviderType.SQLServer:
                    if (SqlCommand != null)
                        SqlCommand.Dispose();
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    if (SQLiteCommand != null)
                        SQLiteCommand.Dispose();
                    break;
#endif
            }
        }
        public SqlABTransaction Transaction
        {
            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return new SqlABTransaction(SqlCommand.Transaction);
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return new SqlABTransaction(SqlCeCommand.Transaction);
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return new SqlABTransaction(SQLiteCommand.Transaction);
#endif
                    default:
                        return null;
                }
            }
            set
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        SqlCommand.Transaction = value.sqlTransaction;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        SqlCeCommand.Transaction = value.sqlCeTransaction;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        SQLiteCommand.Transaction = value.sQLiteTransaction;
                        break;
#endif
                    default:
                        break;
                }
            }
        }
    }
    #endregion

    #region SqlABTransaction
    public class SqlABTransaction
    {
#if(SQLCompact)
        public SqlCeTransaction sqlCeTransaction;
#endif
#if(SQLServer)
        public SqlTransaction sqlTransaction;
#endif
#if(SQLite)
        public SQLiteTransaction sQLiteTransaction;
#endif

#if(SQLServer)
        public SqlABTransaction(SqlTransaction trans)
        {
            sqlTransaction = trans;
        }
#endif

#if(SQLCompact)
        public SqlABTransaction(SqlCeTransaction trans)
        {
            sqlCeTransaction = trans;
        }
#endif

#if(SQLite)
        public SqlABTransaction(SQLiteTransaction trans)
        {
            sQLiteTransaction = trans;
        }
#endif

        public void Commit()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlTransaction.Commit();
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeTransaction.Commit();
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    sQLiteTransaction.Commit();
                    break;
#endif
            }
        }

        public void Rollback()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlTransaction.Rollback();
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeTransaction.Rollback();
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    sQLiteTransaction.Rollback();
                    break;
#endif
            }
        }
    }
    #endregion

    #region SqlABDataReader

    public class SqlABDataReader : IDisposable
    {

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private readonly ProviderType ProviderType;
#if(SQLCompact)
        private readonly SqlCeDataReader SqlCeDataReader;
#endif
#if(SQLServer)
        private readonly SqlDataReader SqlDataReader;
#endif
#if(SQLite)
        private readonly SQLiteDataReader SQLiteDataReader;
#endif

#if(SQLite)

        public SqlABDataReader(SQLiteDataReader dr)
        {
            ProviderType = ProviderType.SQLite;
            SQLiteDataReader = dr;
        }

#endif
#if(SQLCompact)

        public SqlABDataReader(SqlCeDataReader dr)
        {
            ProviderType = ProviderType.SQLCompact;
            SqlCeDataReader = dr;
        }

#endif
#if(SQLServer)

        public SqlABDataReader(SqlDataReader dr)
        {
            ProviderType = ProviderType.SQLServer;
            SqlDataReader = dr;
        }

#endif

        public object this[IColumn column]
        {
            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return SqlDataReader[column.Name];
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return SqlCeDataReader[column.Name];
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return SQLiteDataReader[column.Name];
#endif
                    default:
                        return false;
                }
            }
        }

        public object this[string name]
        {
            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return SqlDataReader[name];
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return SqlCeDataReader[name];
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return SQLiteDataReader[name];
#endif
                    default:
                        return false;
                }
            }
        }

        public object this[int i]
        {
            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return SqlDataReader[i];
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return SqlCeDataReader[i];
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return SQLiteDataReader[i];
#endif
                    default:
                        return false;
                }
            }
        }

        public bool IsClosed
        {
            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return SqlDataReader.IsClosed;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return SqlCeDataReader.IsClosed;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return SQLiteDataReader.IsClosed;
#endif
                    default:
                        return false;
                }
            }
        }

        public bool Read()
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.Read();
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.Read();
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.Read();
#endif
                default:
                    return false;
            }
        }

        public bool HasRows
        {
            get
            {
                switch (ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return SqlDataReader.HasRows;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return SqlCeDataReader.HasRows;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return SQLiteDataReader.HasRows;
#endif

                    //return true;
                    default:
                        return false;
                }
            }
        }

        public Int32 GetInt32(int i)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetInt32(i);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetInt32(i);
#endif
#if(SQLite)

                case ProviderType.SQLite:
                    return SQLiteDataReader.GetInt32(i);
#endif
                default:
                    return 0;
            }
        }

        public string GetFormatValue<T>(IColumn column)
        {
            return FormatData.GetFormatData<T>(GetValue<T>(column));
        }

        public T GetValue<T>(string column)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return typeof(T).BaseType == typeof(Enum)
                        ? SqlDataReader[column] != DBNull.Value
                            ? (T)Enum.ToObject(typeof(T), SqlDataReader[column])
                            : (T)Enum.ToObject(typeof(T), 0)
                        : SqlDataReader[column] != System.DBNull.Value
                            ? (T)Convert.ChangeType(SqlDataReader[column], typeof(T))
                            : (T)Convert.ChangeType("", typeof(T));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return typeof(T).BaseType == typeof(Enum)
                        ? SqlCeDataReader[column] != DBNull.Value
                            ? (T)Enum.ToObject(typeof(T), SqlCeDataReader[column])
                            : (T)Enum.ToObject(typeof(T), 0)
                        : SqlCeDataReader.GetValue(SqlCeDataReader.GetOrdinal(column)) != DBNull.Value
                            ? (T)Convert.ChangeType(SqlCeDataReader.GetValue(SqlCeDataReader.GetOrdinal(column)), typeof(T))
                            : (T)Convert.ChangeType("", typeof(T));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return typeof(T).BaseType == typeof(Enum)
                        ? SQLiteDataReader[column] != DBNull.Value
                            ? (T)Enum.ToObject(typeof(T), SQLiteDataReader[column])
                            : (T)Enum.ToObject(typeof(T), 0)
                        : SQLiteDataReader.GetValue(SQLiteDataReader.GetOrdinal(column)) != DBNull.Value
                            ? (T)Convert.ChangeType(SQLiteDataReader.GetValue(SQLiteDataReader.GetOrdinal(column)), typeof(T))
                            : (T)Convert.ChangeType("", typeof(T));
#endif
                default:
                    return default(T);
            }
        }

        public T GetValue<T>(IColumn column)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return typeof(T).BaseType == typeof(Enum)
                        ? SqlDataReader[column.Name] != DBNull.Value
                            ? (T)Enum.ToObject(typeof(T), SqlDataReader[column.Name])
                            : (T)Enum.ToObject(typeof(T), column.DefaultValue)
                        : SqlDataReader[column.Name] != System.DBNull.Value
                            ? (T)Convert.ChangeType(SqlDataReader[column.Name], typeof(T))
                            : (T)Convert.ChangeType(column.DefaultValue, typeof(T));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return typeof(T).BaseType == typeof(Enum)
                        ? SqlCeDataReader[column.Name] != DBNull.Value
                            ? (T)Enum.ToObject(typeof(T), SqlCeDataReader[column.Name])
                            : (T)Enum.ToObject(typeof(T), column.DefaultValue)
                        : SqlCeDataReader.GetValue(SqlCeDataReader.GetOrdinal(column.Name)) != DBNull.Value
                            ? (T)Convert.ChangeType(SqlCeDataReader.GetValue(SqlCeDataReader.GetOrdinal(column.Name)), typeof(T))
                            : (T)Convert.ChangeType(column.DefaultValue, typeof(T));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return typeof(T).BaseType == typeof(Enum)
                        ? SQLiteDataReader[column.Name] != DBNull.Value
                            ? (T)Enum.ToObject(typeof(T), SQLiteDataReader[column.Name])
                            : (T)Enum.ToObject(typeof(T), column.DefaultValue)
                        : SQLiteDataReader.GetValue(SQLiteDataReader.GetOrdinal(column.Name)) != DBNull.Value
                            ? (T)Convert.ChangeType(SQLiteDataReader.GetValue(SQLiteDataReader.GetOrdinal(column.Name)), typeof(T))
                            : (T)Convert.ChangeType(column.DefaultValue, typeof(T));
#endif
                default:
                    return default(T);
            }
        }

        public DateTime GetDateTime(string columnname)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetDateTime(SqlDataReader.GetOrdinal(columnname));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetDateTime(SqlCeDataReader.GetOrdinal(columnname));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetDateTime(SQLiteDataReader.GetOrdinal(columnname));
#endif
                default:
                    return new DateTime();
            }
        }

        public DateTime GetDateTime(int i)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetDateTime(i);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetDateTime(i);
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetDateTime(i);
#endif
                default:
                    return new DateTime();
            }
        }

        public string GetString(int i)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetString(i);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetString(i);
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetString(i);
#endif
                default:
                    return ""; ;
            }
        }

        public string GetString(string columnname)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetString(SqlDataReader.GetOrdinal(columnname));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetString(SqlCeDataReader.GetOrdinal(columnname));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetString(SQLiteDataReader.GetOrdinal(columnname));
#endif
                default:
                    return ""; ;
            }
        }

        public double GetDouble(string columnname)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetDouble(SqlDataReader.GetOrdinal(columnname));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetDouble(SqlCeDataReader.GetOrdinal(columnname));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    try
                    {
                        return SQLiteDataReader.GetDouble(SQLiteDataReader.GetOrdinal(columnname));
                    }
                    catch (Exception)
                    { return 0; }
#endif
                default:
                    return 0.0;
            }
        }

        public Int16 GetInt16(string columnname)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetInt16(SqlDataReader.GetOrdinal(columnname));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetInt16(SqlCeDataReader.GetOrdinal(columnname));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetInt16(SQLiteDataReader.GetOrdinal(columnname));
#endif
                default:
                    return 0;
            }
        }

        public Int32 GetInt32(string columnname)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetInt32(SqlDataReader.GetOrdinal(columnname));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetInt32(SqlCeDataReader.GetOrdinal(columnname));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetInt32(SQLiteDataReader.GetOrdinal(columnname));
#endif
                default:
                    return 0;
            }
        }

        public bool GetBoolean(string columnname)
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return SqlDataReader.GetBoolean(SqlDataReader.GetOrdinal(columnname));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return SqlCeDataReader.GetBoolean(SqlCeDataReader.GetOrdinal(columnname));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return SQLiteDataReader.GetBoolean(SQLiteDataReader.GetOrdinal(columnname));
#endif
                default:
                    return false;
            }
        }

        public void Close()
        {
            switch (ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    SqlDataReader.Close();
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    SqlCeDataReader.Close();
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    SQLiteDataReader.Close();
                    break;
#endif
            }
        }
    }

    #endregion

    #region SqlABParameterCollection

    public class SqlABParameterCollection// : ICloneable
    {
#if(SQLite)
        private readonly SQLiteCommand SQLiteCommand;
#endif
#if(SQLCompact)
        private readonly SqlCeCommand SqlCeCommand;
#endif
#if(SQLServer)
        private readonly SqlCommand SqlCommand;
#endif

#if(SQLServer)

        public SqlABParameterCollection(SqlCommand SqlCommand)
        {
            this.SqlCommand = SqlCommand;
        }

#endif
#if(SQLCompact)


        public SqlABParameterCollection(SqlCeCommand SqlCeCommand)
        {
            this.SqlCeCommand = SqlCeCommand;
        }

#endif
#if(SQLite)

        public SqlABParameterCollection(IDbCommand SQLiteCommand)
        {
            this.SQLiteCommand = (SQLiteCommand)SQLiteCommand;
        }

#endif

        public SqlABParameter Add(SqlABParameter SqlABParameter)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return new SqlABParameter(this.SqlCommand.Parameters.Add(SqlABParameter.sqlParameter));
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return new SqlABParameter(this.SqlCeCommand.Parameters.Add(SqlABParameter.sqlCeParameter));
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.SQLiteCommand.Parameters.Add(SqlABParameter.sQLiteParameter);
                    return new SqlABParameter(SqlABParameter.sQLiteParameter);
#endif
                default:
                    return null;
            }
        }

        public void AddRange(SqlABParameter[] SqlABParameter)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.SqlCommand.Parameters.AddRange(SqlABParameter);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.SqlCeCommand.Parameters.AddRange(SqlABParameter);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    for (int t = 0; t < this.SQLiteCommand.Parameters.Count; t++)
                        this.SQLiteCommand.Parameters.Add(SqlABParameter[t]);
                    break;
#endif
            }
        }

        public object this[int index]
        {
            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return this.SqlCommand.Parameters[index].Value;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return this.SqlCeCommand.Parameters[index].Value;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return this.SQLiteCommand.Parameters[index].Value;
#endif
                    default:
                        return null;
                }
            }

            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.SqlCommand.Parameters[index].Value = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.SqlCeCommand.Parameters[index].Value = value;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.SQLiteCommand.Parameters[index].Value = value;
                        break;
#endif
                }
            }
        }

        public void AddRange(List<SqlABParameter> SqlABParameter)
        {
            foreach (SqlABParameter param in SqlABParameter)
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.SqlCommand.Parameters.Add(param.sqlParameter);
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.SqlCeCommand.Parameters.Add(param.sqlCeParameter);
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.SQLiteCommand.Parameters.Add(param.sQLiteParameter);
                        break;
#endif
                }
            }
        }
    }

    #endregion

    #region SqlABParameter

    public sealed class SqlABParameter
    {
        public override string ToString()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return sqlParameter.ParameterName;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return sqlCeParameter.ParameterName;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return sQLiteParameter.ParameterName;
#endif
            }
            return "";
        }

#if(SQLite)
        internal SQLiteParameter sQLiteParameter;
#endif
#if(SQLCompact)
        internal SqlCeParameter sqlCeParameter;
#endif
#if(SQLServer)
        internal SqlParameter sqlParameter;
#endif

        public object Value
        {
            set
            {
                var val = (value is Enum)
                                 ? ((Enum)value).Int()
                                 : value;
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        sqlParameter.Value = val;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        sqlCeParameter.Value = val;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        sQLiteParameter.Value = val;
                        break;
#endif
                }
            }

            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlParameter.Value;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return sqlCeParameter.Value;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return sQLiteParameter.Value;
#endif
                    default:
                        return null;
                }
            }
        }

        public string ParameterName
        {
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        sqlParameter.ParameterName = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        sqlCeParameter.ParameterName = value;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        sQLiteParameter.ParameterName = value;
                        break;
#endif
                }
            }

            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlParameter.ParameterName;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return sqlCeParameter.ParameterName;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return sQLiteParameter.ParameterName;
#endif
                    default:
                        return null;
                }
            }
        }

#if(SQLServer)

        public SqlABParameter(SqlParameter SqlParameter)
        {
            this.sqlParameter = SqlParameter;
        }

#endif
#if(SQLCompact)

        public SqlABParameter(SqlCeParameter SqlCeParameter)
        {
            this.sqlCeParameter = SqlCeParameter;
        }

#endif
#if(SQLite)

        public SqlABParameter(SQLiteParameter SQLiteParameter)
        {
            this.sQLiteParameter = SQLiteParameter;
        }

#endif

        //public SqlABParameter(string parameterName, SqlDbType dbType)
        //    : this(GlobalInfo.LoginInfo.ProviderType, parameterName, dbType)
        //{
        //}

        //public SqlABParameter(string parameterName, SqlDbType dbType, int len)
        //    : this(GlobalInfo.LoginInfo.ProviderType, parameterName, dbType, len)
        //{
        //}

        private SqlABParameter(ProviderType ProviderType, string parameterName, SqlDbType dbType)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.sqlParameter = new SqlParameter(parameterName, dbType);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.sqlCeParameter = new SqlCeParameter(parameterName, dbType);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.sQLiteParameter = new SQLiteParameter(parameterName, (DbType)dbType);
                    break;
#endif
            }
        }

        public SqlABParameter(string parameterName, IColumn column)
            : this(parameterName, column.ColType, column.Len)
        { }

        public SqlABParameter(string parameterName, Type colType, int len)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    var dbType = ConvertColumnType.SqlTypeOf(colType);
                    this.sqlParameter = new SqlParameter(parameterName, dbType, len);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    var dbTypeCE = ConvertColumnType.SqlTypeCEOf(colType);
                    this.sqlCeParameter = new SqlCeParameter(parameterName, dbTypeCE, len);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    var dbTypeLIT = ConvertColumnType.SqlTypeLITOf(colType);
                    this.sQLiteParameter = new SQLiteParameter(parameterName, dbTypeLIT);
                    break;
#endif
            }
        }

        //        private SqlABParameter(ProviderType ProviderType, string parameterName, SqlDbType dbType, int size)
        //        {
        //            switch (ProviderType)
        //            {
        //#if(SQLServer)
        //                case ProviderType.SQLServer:
        //                    ProviderType = ProviderType.SQLServer;
        //                    this.SqlParameter = new SqlParameter(parameterName, dbType, size);
        //                    break;
        //#endif
        //#if(SQLCompact)
        //                case ProviderType.SQLCompact:
        //                    this.ProviderType = ProviderType.SQLCompact;
        //                    this.SqlCeParameter = new SqlCeParameter(parameterName, dbType, size);
        //                    break;
        //#endif
        //#if(SQLite)
        //                case ProviderType.SQLite:
        //                    this.ProviderType = ProviderType.SQLite;
        //                    DbType dbTypeLT = (DbType)Convert.ChangeType(dbType, typeof(DbType));
        //                    this.SQLiteParameter = new SQLiteParameter(parameterName, dbTypeLT);
        //                    break;
        //#endif
        //            }
        //        }
    }

    #endregion

    #region SqlABDataAdapter

    public delegate void SqlABRowUpdatingEventHandler(object sender, SqlABRowUpdatingEventArgs e);

    public sealed class SqlABDataAdapter
    {
#if(SQLite)
        internal SQLiteDataAdapter sQLiteDataAdapter;
#endif
#if(SQLCompact)
        internal SqlCeDataAdapter sqlCeDataAdapter;
#endif
#if(SQLServer)
        internal SqlDataAdapter sqlDataAdapter;
#endif
        internal SqlProxyCommand SqlProxyCommand;

        public event SqlABRowUpdatedEventHandler RowUpdated;

        public event SqlABRowUpdatingEventHandler RowUpdating;

        public MissingSchemaAction MissingSchemaAction
        {
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.sqlDataAdapter.MissingSchemaAction = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.sqlCeDataAdapter.MissingSchemaAction = value;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.sQLiteDataAdapter.MissingSchemaAction = value;
                        break;
#endif
                }
            }

            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlDataAdapter.MissingSchemaAction;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return this.sqlCeDataAdapter.MissingSchemaAction;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        return this.sQLiteDataAdapter.MissingSchemaAction;
#endif
                    default:
#if(SQLite)
                        return this.sQLiteDataAdapter.MissingSchemaAction;
#elif (SQLCompact)
                        return this.sqlCeDataAdapter.MissingSchemaAction;
#elif (SQLServer)
                        return sqlDataAdapter.MissingSchemaAction;
#endif
                }
            }
        }

        public SqlProxyCommand SelectCommand
        {
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.sqlDataAdapter.SelectCommand = value.SqlCommand;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.sqlCeDataAdapter.SelectCommand = value.SqlCeCommand;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.sQLiteDataAdapter.SelectCommand = (SQLiteCommand)value.SQLiteCommand;
                        break;
#endif
                }
            }

            get
            {
                return SqlProxyCommand;
            }
        }

        public SqlProxyCommand UpdateCommand
        {
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.sqlDataAdapter.UpdateCommand = value.SqlCommand;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.sqlCeDataAdapter.UpdateCommand = value.SqlCeCommand;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.sQLiteDataAdapter.UpdateCommand = (SQLiteCommand)value.SQLiteCommand;
                        break;
#endif
                }
            }

            get
            {
                return SqlProxyCommand;
            }
        }

        public SqlProxyCommand InsertCommand
        {
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.sqlDataAdapter.InsertCommand = value.SqlCommand;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.sqlCeDataAdapter.InsertCommand = value.SqlCeCommand;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.sQLiteDataAdapter.InsertCommand = (SQLiteCommand)value.SQLiteCommand;
                        break;
#endif
                }
            }

            get
            {
                return SqlProxyCommand;
            }
        }

        public SqlProxyCommand DeleteCommand
        {
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLServer)
                    case ProviderType.SQLServer:
                        this.sqlDataAdapter.DeleteCommand = value.SqlCommand;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        this.sqlCeDataAdapter.DeleteCommand = value.SqlCeCommand;
                        break;
#endif
#if(SQLite)
                    case ProviderType.SQLite:
                        this.sQLiteDataAdapter.DeleteCommand = (SQLiteCommand)value.SQLiteCommand;
                        break;
#endif
                }
            }

            get
            {
                return SqlProxyCommand;
            }
        }

        public SqlABDataAdapter()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(SqlDataAdapter_RowUpdated);
                    sqlDataAdapter.RowUpdating += new SqlRowUpdatingEventHandler(SqlDataAdapter_RowUpdating);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.sqlCeDataAdapter = new SqlCeDataAdapter();
                    sqlCeDataAdapter.RowUpdated += new SqlCeRowUpdatedEventHandler(SqlCeDataAdapter_RowUpdated);
                    sqlCeDataAdapter.RowUpdating += new SqlCeRowUpdatingEventHandler(SqlCeDataAdapter_RowUpdating);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.sQLiteDataAdapter = new SQLiteDataAdapter();
                    sQLiteDataAdapter.RowUpdated += new EventHandler<RowUpdatedEventArgs>(SQLiteDataAdapter_RowUpdated);
                    sQLiteDataAdapter.RowUpdating += new EventHandler<RowUpdatingEventArgs>(SQLiteDataAdapter_RowUpdating);
                    //SQLiteDataAdapter.RowUpdated += new SQLiteRowUpdatedEventHandler(SQLiteDataAdapter_RowUpdated);
                    //SQLiteDataAdapter.RowUpdating += new SQLiteRowUpdatingEventHandler(SQLiteDataAdapter_RowUpdating);
                    break;
#endif
            }
        }

        public SqlABDataAdapter(SqlProxyCommand SqlProxyCommand)
        {
            this.SqlProxyCommand = SqlProxyCommand;

            switch (SqlProxyCommand.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    this.sqlDataAdapter = new SqlDataAdapter(SqlProxyCommand.SqlCommand);
                    sqlDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(SqlDataAdapter_RowUpdated);
                    sqlDataAdapter.RowUpdating += new SqlRowUpdatingEventHandler(SqlDataAdapter_RowUpdating);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    this.sqlCeDataAdapter = new SqlCeDataAdapter(SqlProxyCommand.SqlCeCommand);
                    sqlCeDataAdapter.RowUpdated += new SqlCeRowUpdatedEventHandler(SqlCeDataAdapter_RowUpdated);
                    sqlCeDataAdapter.RowUpdating += new SqlCeRowUpdatingEventHandler(SqlCeDataAdapter_RowUpdating);
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    this.sQLiteDataAdapter = new SQLiteDataAdapter(SqlProxyCommand.SQLiteCommand);
                    sQLiteDataAdapter.RowUpdated += new EventHandler<RowUpdatedEventArgs>(SQLiteDataAdapter_RowUpdated);
                    sQLiteDataAdapter.RowUpdating += new EventHandler<RowUpdatingEventArgs>(SQLiteDataAdapter_RowUpdating);
                    break;
#endif
            }
        }

#if(SQLite)

        private void SQLiteDataAdapter_RowUpdating(object sender, RowUpdatingEventArgs e)
        {
            if (RowUpdating != null)
                RowUpdating(sender, new SqlABRowUpdatingEventArgs(e));
        }

        private void SQLiteDataAdapter_RowUpdated(object sender, RowUpdatedEventArgs e)
        {
            if (RowUpdated != null)
                RowUpdated(sender, new SqlABRowUpdatedEventArgs(e));
        }

#endif
#if(SQLCompact)

        private void SqlCeDataAdapter_RowUpdating(object sender, SqlCeRowUpdatingEventArgs e)
        {
            if (RowUpdating != null)
                RowUpdating(sender, new SqlABRowUpdatingEventArgs(e));
        }

        private void SqlCeDataAdapter_RowUpdated(object sender, SqlCeRowUpdatedEventArgs e)
        {
            if (RowUpdated != null)
                RowUpdated(sender, new SqlABRowUpdatedEventArgs(e));
        }

#endif
#if(SQLServer)

        private void SqlDataAdapter_RowUpdating(object sender, SqlRowUpdatingEventArgs e)
        {
            if (RowUpdating != null)
                RowUpdating(sender, new SqlABRowUpdatingEventArgs(e));
        }

        private void SqlDataAdapter_RowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            if (RowUpdated != null)
                RowUpdated(sender, new SqlABRowUpdatedEventArgs(e));
        }

#endif

        public int Fill(DataSet dataSet, string srcTable)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return this.sqlDataAdapter.Fill(dataSet, srcTable);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return this.sqlCeDataAdapter.Fill(dataSet, srcTable);
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return this.sQLiteDataAdapter.Fill(dataSet, srcTable);
#endif
                default:
                    return -1;
            }
        }

        public int Fill(DataTable srcTable)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return this.sqlDataAdapter.Fill(srcTable);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return this.sqlCeDataAdapter.Fill(srcTable);
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return this.sQLiteDataAdapter.Fill(srcTable);
#endif
                default:
                    return -1;
            }
        }

        public int Update(DataSet dataSet, string srcTable)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return sqlDataAdapter.Update(dataSet, srcTable);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return sqlCeDataAdapter.Update(dataSet, srcTable);
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return sQLiteDataAdapter.Update(dataSet, srcTable);
#endif
                default:
                    return -1;
            }
        }

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLServer)
                case ProviderType.SQLServer:
                    return this.sqlDataAdapter.FillSchema(dataSet, schemaType);
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return this.sqlCeDataAdapter.FillSchema(dataSet, schemaType);
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    return this.sQLiteDataAdapter.FillSchema(dataSet, schemaType);
#endif
                default:
                    return null;
            }
        }
    }

    #endregion

    #region SqlABRowUpdatedEventArgs

    public delegate void SqlABRowUpdatedEventHandler(object sender, SqlABRowUpdatedEventArgs e);

    public sealed class SqlABRowUpdatedEventArgs
    {
#if(SQLite)
        private readonly RowUpdatedEventArgs SQLite_e;
#endif
#if(SQLCompact)
        private readonly SqlCeRowUpdatedEventArgs SqlCe_e;
#endif
#if(SQLServer)
        private readonly SqlRowUpdatedEventArgs Sql_e;
#endif

#if(SQLite)

        public SqlABRowUpdatedEventArgs(RowUpdatedEventArgs e)
        {
            SQLite_e = e;
        }

#endif
#if(SQLCompact)

        public SqlABRowUpdatedEventArgs(SqlCeRowUpdatedEventArgs e)
        {
            SqlCe_e = e;
        }

#endif
#if(SQLServer)

        public SqlABRowUpdatedEventArgs(SqlRowUpdatedEventArgs e)
        {
            Sql_e = e;
        }

#endif
    }

    #endregion

    #region SqlABRowUpdatingEventArgs

    public sealed class SqlABRowUpdatingEventArgs
    {
#if(SQLite)
        private readonly RowUpdatingEventArgs SQLite_e;
#endif
#if(SQLCompact)
        private readonly SqlCeRowUpdatingEventArgs SqlCe_e;
#endif
#if(SQLServer)
        private readonly SqlRowUpdatingEventArgs Sql_e;
#endif

#if(SQLite)

        public SqlABRowUpdatingEventArgs(RowUpdatingEventArgs e)
        {
            SQLite_e = e;
        }

#endif
#if(SQLCompact)

        public SqlABRowUpdatingEventArgs(SqlCeRowUpdatingEventArgs e)
        {
            SqlCe_e = e;
        }

#endif
#if(SQLServer)

        public SqlABRowUpdatingEventArgs(SqlRowUpdatingEventArgs e)
        {
            Sql_e = e;
        }

#endif

        public StatementType StatementType
        {
            get
            {
#if(SQLServer)
                if (Sql_e != null)
                    return Sql_e.StatementType;
#endif
#if(SQLCompact)
#if(SQLServer)
                else
#endif
                    if (SqlCe_e != null)
                    return SqlCe_e.StatementType;
#endif
#if (SQLite)
#if (SQLCompact || SQLServer)
                else
#endif
                        if (SQLite_e != null)
                    return SQLite_e.StatementType;
#endif
                return new StatementType();
            }
        }

        public DataRow Row
        {
            get
            {
#if(SQLServer)
                if (Sql_e != null)
                    return Sql_e.Row;
#endif
#if(SQLCompact)
#if(SQLServer)
                else
#endif
                    if (SqlCe_e != null)
                    return SqlCe_e.Row;
#endif
#if (SQLite)
#if (SQLCompact || SQLServer)
                else
#endif
                        if (SQLite_e != null)
                    return SQLite_e.Row;
#endif
                return null;
            }
        }
    }

    #endregion

    #region SqlProxyCommandBuilder

    public class SqlProxyCommandBuilder
    {
#if(SQLite)
        private readonly SQLiteCommandBuilder sqliteCommandBuilder;
#endif
#if(SQLCompact)
        private readonly SqlCeCommandBuilder sqlCeCommandBuilder;
#endif
#if(SQLServer)
        private readonly SqlCommandBuilder sqlCommandBuilder;
#endif

        public SqlProxyCommandBuilder(SqlABDataAdapter dAdapter)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLite)
                case ProviderType.SQLite:
                    sqliteCommandBuilder = new SQLiteCommandBuilder(dAdapter.sQLiteDataAdapter);
                    break;
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    sqlCeCommandBuilder = new SqlCeCommandBuilder(dAdapter.sqlCeDataAdapter);
                    break;
#endif
#if(SQLServer)
                case ProviderType.SQLServer:
                    sqlCommandBuilder = new SqlCommandBuilder(dAdapter.sqlDataAdapter);
                    break;
#endif
            }
        }

        public SqlProxyCommand GetUpdateCommand()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLite)
                case ProviderType.SQLite:
                    return new SqlProxyCommand(sqliteCommandBuilder.GetUpdateCommand());
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return new SqlProxyCommand(sqlCeCommandBuilder.GetUpdateCommand());
#endif
#if(SQLServer)
                case ProviderType.SQLServer:
                    return new SqlProxyCommand(sqlCommandBuilder.GetUpdateCommand());
#endif
                default:
                    return null;
            }
        }

        public SqlProxyCommand GetInsertCommand()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLite)
                case ProviderType.SQLite:
                    return new SqlProxyCommand(sqliteCommandBuilder.GetInsertCommand());
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return new SqlProxyCommand(sqlCeCommandBuilder.GetInsertCommand());
#endif
#if(SQLServer)
                case ProviderType.SQLServer:
                    return new SqlProxyCommand(sqlCommandBuilder.GetInsertCommand());
#endif
                default:
                    return null;
            }
        }

        public SqlProxyCommand GetDeleteCommand()
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLite)
                case ProviderType.SQLite:
                    return new SqlProxyCommand(sqliteCommandBuilder.GetDeleteCommand());
#endif
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    return new SqlProxyCommand(sqlCeCommandBuilder.GetDeleteCommand());
#endif
#if(SQLServer)
                case ProviderType.SQLServer:
                    return new SqlProxyCommand(sqlCommandBuilder.GetDeleteCommand());
#endif
                default:
                    return null;
            }
        }

        public string QuotePrefix
        {
            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        return sqliteCommandBuilder.QuotePrefix;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return sqlCeCommandBuilder.QuotePrefix;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlCommandBuilder.QuotePrefix;
#endif
                    default:
                        return "";
                }
            }
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        sqliteCommandBuilder.QuotePrefix = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        sqlCeCommandBuilder.QuotePrefix = value;
                        break;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        sqlCommandBuilder.QuotePrefix = value;
                        break;
#endif
                }
            }
        }

        public string QuoteSuffix
        {
            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        return sqliteCommandBuilder.QuoteSuffix;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return sqlCeCommandBuilder.QuoteSuffix;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlCommandBuilder.QuoteSuffix;
#endif
                    default:
                        return "";
                }
            }
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        sqliteCommandBuilder.QuoteSuffix = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        sqlCeCommandBuilder.QuoteSuffix = value;
                        break;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        sqlCommandBuilder.QuoteSuffix = value;
                        break;
#endif
                }
            }
        }


        public ConflictOption ConflictOption
        {
            get
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        return sqliteCommandBuilder.ConflictOption;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        return sqlCeCommandBuilder.ConflictOption;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        return sqlCommandBuilder.ConflictOption;
#endif
                    default:
                        return ConflictOption.CompareAllSearchableValues;
                }
            }
            set
            {
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        sqliteCommandBuilder.ConflictOption = value;
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        sqlCeCommandBuilder.ConflictOption = value;
                        break;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        sqlCommandBuilder.ConflictOption = value;
                        break;
#endif
                }
            }
        }
    }

    #endregion

    //public class ABDataRow : DataRow
    //{
    //    public ABDataRow()
    //        :base()

    //    protected internal ABDataRow(DataRowBuilder builder)
    //        : base(builder)
    //    { }

    //    public object this[IColumn column]
    //    {
    //        get { return this[column.Name]; }
    //        set { this[column.Name] = value; }
    //    }
    //}
}