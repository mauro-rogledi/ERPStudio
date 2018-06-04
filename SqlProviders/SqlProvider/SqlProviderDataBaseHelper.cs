using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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

    class SqlProviderDatabaseHelper : SqlProxyProvider.ISqlProxyDataBaseHelper
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

        public string QuerySearchTable(string tableName) => $"select table_name from information_schema.tables where table_name = '{tableName}'";

        public bool SearchColumn(string srcTable, string srcColumn, IDbConnection connection)
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
            catch (Exception)
            {
                return false;
            }
            return found;
        }

        public string ConvertDate(DateTime datetime)=> $"'{datetime.Year}{datetime.Month:00}{datetime.Day:00}' ";
        
        public string GetYear(string date) => $"YEAR({date}) ";
        public string GetMonth(string date) => $"MONTH({date}) ";
        public string GetDay(string date) => $"DAY({date}) ";
        public string GetWeekOfYear(string date) => $"DATEPART(week, {date}) ";
        public string DayOfYear(string date) => $"DATEPART(dayofyear, {date}) ";
        public string DayOfWeek(string date) => $"DATEPART(weekday, {date}) ";

        public async Task<List<string>> GetServers()
        {
            var instances =  SqlDataSourceEnumerator.Instance;
            var dt = await Task.Run(
                () => instances.GetDataSources()
            );

            var rows = dt.Rows.OfType<DataRow>();

            var localInstance = ListLocalSqlInstances().ToList();

            var serverList = rows.Aggregate<DataRow, List<string>>(new List<string>(), (acc, x) =>
            {
                var srv = x["ServerName"].ToString();
                if (acc.Count == 0 && localInstance != null)
                {
                    acc.AddRange(
                          localInstance.Select((string instance) =>
                          {
                              return instance == "."
                              ? srv
                              : $"{srv}{instance}";
                          }));
                }
                else
                    acc.Add(srv);

                return acc;
            });

            return serverList;
        }

        public async Task<List<string>> ListDatabase(string server, string userID, string password, bool integratedSecurity)
        {
            var connectionstringBuilder = new SqlProviderConnectionStringBuilder
            {
                DataSource = server,
                UserID = userID,
                Password = password,
                IntegratedSecurity = integratedSecurity
            };
            var dbList = new List<string>();


            return await Task.Run(
                () =>
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(connectionstringBuilder.ConnectionString))
                        {
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand("SELECT * from sys.databases where database_id > 6", con))
                            {
                                using (IDataReader dr = cmd.ExecuteReader())
                                {
                                    while (dr.Read())
                                    {
                                        dbList.Add(dr[0].ToString());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }

                    return dbList;
                });
        }

        public DateTime GetServerDate(string connectionString)
        {
            var expireDate = DateTime.Today;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT GETDATE()";
                    var dr = command.ExecuteReader();
                    if (dr.Read())
                        expireDate = dr.GetDateTime(0);
                    dr.Close();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception($"can't read server date");
            }

            return expireDate;
        }


        private static IEnumerable<string> ListLocalSqlInstances()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    foreach (string item in ListLocalSqlInstances(hive))
                    {
                        yield return item;
                    }
                }

                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    foreach (string item in ListLocalSqlInstances(hive))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                foreach (string item in ListLocalSqlInstances(Registry.LocalMachine))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<string> ListLocalSqlInstances(RegistryKey hive)
        {
            const string keyName = @"Software\Microsoft\Microsoft SQL Server";
            const string valueName = "InstalledInstances";
            const string defaultName = "MSSQLSERVER";

            using (var key = hive.OpenSubKey(keyName, false))
            {
                if (key == null) return Enumerable.Empty<string>();

                var value = key.GetValue(valueName) as string[];
                if (value == null) return Enumerable.Empty<string>();

                for (int index = 0; index < value.Length; index++)
                {
                    if (string.Equals(value[index], defaultName, StringComparison.OrdinalIgnoreCase))
                    {
                        value[index] = ".";
                    }
                    else
                    {
                        value[index] = @"\" + value[index];
                    }
                }
                return value;
            }
        }

    }
}
