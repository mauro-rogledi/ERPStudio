using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderDataBaseHelper : SqlProxyProvider.ISqlProxyDataBaseHelper
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
    }
}
