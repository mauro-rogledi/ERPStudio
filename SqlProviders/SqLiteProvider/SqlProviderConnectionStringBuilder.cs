using SqlProxyProvider;
using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderConnectionStringBuilder : ISqlProviderConnectionStringBuilder
    {
        string userID, initialCatalog;
        bool integratedSecurity;
        SQLiteConnectionStringBuilder sqlconnectionStringBuilder;

        public SqlProviderConnectionStringBuilder() => sqlconnectionStringBuilder = new SQLiteConnectionStringBuilder();

        public string ConnectionString => sqlconnectionStringBuilder.ConnectionString;

        public string DataSource { get => sqlconnectionStringBuilder.DataSource; set => sqlconnectionStringBuilder.DataSource = value; }
        public string UserID { get => userID; set => userID = value; }
        public string InitialCatalog { get => initialCatalog; set => initialCatalog = value; }
        public string Password { get => sqlconnectionStringBuilder.Password; set => sqlconnectionStringBuilder.Password = value; }
        public bool IntegratedSecurity { get => integratedSecurity; set => integratedSecurity = value; }
    }
}
