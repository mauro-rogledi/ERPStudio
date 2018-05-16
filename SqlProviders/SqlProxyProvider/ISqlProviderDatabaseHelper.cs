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

        Task<List<string>> GetServers();
        Task<List<string>> ListDatabase(string server);

    }
}
