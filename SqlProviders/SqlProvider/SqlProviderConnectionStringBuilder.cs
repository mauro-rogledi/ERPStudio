using SqlProxyProvider;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderConnectionStringBuilder : ISqlProviderConnectionStringBuilder
    {
        SqlConnectionStringBuilder sqlconnectionStringBuilder;

        public SqlProviderConnectionStringBuilder() => sqlconnectionStringBuilder = new SqlConnectionStringBuilder();

        public string ConnectionString => sqlconnectionStringBuilder.ConnectionString;

        public string DataSource { get => sqlconnectionStringBuilder.DataSource; set => sqlconnectionStringBuilder.DataSource = value; }
        public string UserID { get => sqlconnectionStringBuilder.UserID; set => sqlconnectionStringBuilder.UserID = value; }
        public string InitialCatalog { get => sqlconnectionStringBuilder.InitialCatalog; set => sqlconnectionStringBuilder.InitialCatalog = value; }
        public string Password { get => sqlconnectionStringBuilder.Password; set => sqlconnectionStringBuilder.Password = value; }
        public bool IntegratedSecurity { get => sqlconnectionStringBuilder.IntegratedSecurity; set => sqlconnectionStringBuilder.IntegratedSecurity = value; }
    }
}
