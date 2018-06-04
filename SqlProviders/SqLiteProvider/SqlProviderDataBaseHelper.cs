using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderDatabaseHelper : SqlProxyProvider.ISqlProxyDataBaseHelper
    {
        public string DataSource { get; set; } = "";
        public string UserID { get; set; } = "";
        public string InitialCatalog { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IntegratedSecurity { get; set; } = false;

        public void CreateDatabase()
        {
            CreateDatabase(DataSource);
        }

        public void CreateDatabase(string connectionString)
        {
            SQLiteConnection.CreateFile(connectionString);
        }

        public string QuerySearchTable(string tableName) => $"select tbl_name from sqlite_master where type = 'table' and tbl_name = '{tableName}";

        public bool SearchColumn(string srcTable, string srcColumn, System.Data.IDbConnection connection)
        {
            var found = false;
            try
            {
                var command = $"PRAGMA table_info('{srcTable}');";

                using (var cmd = new SQLiteCommand(command, connection as SQLiteConnection))
                {

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetString(reader.GetOrdinal("name"));
                            if (name == srcColumn)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return true;
            }
            return found;
        }

        public async Task<List<string>> GetServers()
        {
            return await Task.Run(

                () => new List<string>()
                );
        }

        public async Task<List<string>> ListDatabase(string server, string userID, string password, bool integratedSecurity)
        {
            return await Task.Run(

                () => new List<string>()
                );
        }

        public string ConvertDate(DateTime datetime) => $"'{datetime.Year:0000}-{datetime.Month:00}-{datetime.Day:00} 00:00:00' ";

        public string GetYear(string date) => $"STRFTIME('%Y', {date}) ";
        public string GetMonth(string date) => $"STRFTIME('%m', {date}) ";
        public string GetDay(string date) => $"STRFTIME('%d', {date}) ";
        public string GetWeekOfYear(string date) => $"STRFTIME('%W', {date}) ";
        public string DayOfYear(string date) => $"STRFTIME('%j', {date}) ";
        public string DayOfWeek(string date) => $"STRFTIME('%w', {date}) ";

        public DateTime GetServerDate(string connectionString)
        {
            var expireDate = DateTime.Today;
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT date('now');";
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
    }
}
