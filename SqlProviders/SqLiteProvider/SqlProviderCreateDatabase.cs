using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderCreateDatabase : SqlProxyProvider.ISqlProxyCreateDatabase
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
    }
}
