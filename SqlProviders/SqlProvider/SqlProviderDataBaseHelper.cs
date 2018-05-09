using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    internal static class ConvertDbType
    {
        static Dictionary<DbType, SqlDbType> dbTypeSqlType = new Dictionary<DbType, SqlDbType>
        {
            { DbType.String, SqlDbType.NVarChar },
            { DbType.Int16, SqlDbType.SmallInt },
            { DbType.Int32, SqlDbType.Int },
            { DbType.Int64, SqlDbType.BigInt },
            { DbType.Boolean, SqlDbType.Bit },
            { DbType.Date, SqlDbType.Date },
            { DbType.DateTime, SqlDbType.DateTime },
            { DbType.Double, SqlDbType.Float },
            { DbType.Decimal, SqlDbType.Decimal },
            { DbType.Currency, SqlDbType.Money }
        };

        static public SqlDbType GetSqlDbType(DbType dbType)
        {
            return dbTypeSqlType[dbType];
        }
    }

    class SqlProviderDataBaseHelper : SqlProxyProvider.ISqlProxyDataBaseHelper
    {
        public string DataSource { get; set; } = "";
        public string UserID { get; set; } = "";
        public string InitialCatalog { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IntegratedSecurity { get; set; } = false;

        public void CreateDatabase()
        {
            var connectionstringBuilder = new SqlProviderConnectionStringBuilder
            {
                DataSource = this.DataSource,
                UserID = this.UserID,
                Password = this.Password,
                IntegratedSecurity = this.IntegratedSecurity
            };
            CreateDatabase(connectionstringBuilder.ConnectionString);
        }

        public void CreateDatabase(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"CREATE DATABASE {InitialCatalog}";
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"can't create db {e.Message}");
            }
        }

        public string QuerySearchTable(string tableName) => $"select table_name from information_schema.tables where table_name = '{tableName}";

        public bool SearchColumn(string srcTable, string  srcColumn, IDbConnection connection)
        {
            var found = false;
            try
            {
                var command = $"select table_name from information_schema.columns where table_name = '{srcTable}' and column_name = '{srcColumn}'";

                using (var cmd = new SqlCommand(command, connection as SqlConnection))
                {
                    var dr = cmd.ExecuteReader();

                    found = dr.HasRows;
                    dr.Close();
                }
            }
            catch (Exception )
            {
                return false;
            }
            return found;
        }

    }
}
