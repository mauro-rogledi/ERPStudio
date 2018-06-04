using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProxyDataBaseHelper
    {
        string DataSource { get; set; }
        string UserID { get; set; }
        string InitialCatalog { get; set; }
        string Password { get; set; }
        bool IntegratedSecurity { get; set; }

        void CreateDatabase();
        void CreateDatabase(string connectionString);

        string QuerySearchTable(string tableName);
        bool SearchColumn(string srcTable, string srcColumn, System.Data.IDbConnection connection);
        DateTime GetServerDate(string connectionString);

        Task<List<string>> GetServers();
        Task<List<string>> ListDatabase(string server, string userID, string password, bool integratedSecurity);

        string ConvertDate(DateTime date);
        string GetYear(string date);
        string GetMonth(string date);
        string GetDay(string date);
        string GetWeekOfYear(string date);
        string DayOfYear(string date);
        string DayOfWeek(string date);

    }
}
