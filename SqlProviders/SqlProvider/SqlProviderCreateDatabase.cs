using System;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderCreateDatabase : SqlProxyProvider.ISqlProxyCreateDatabase
    {
        public string DataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string InitialCatalog { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IntegratedSecurity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CreateDatabase()
        {
            var connectionstringBuilder = new SqlProviderConnectionStringBuilder
            {
                DataSource = this.DataSource,
                UserID = this.UserID,
                Password = this.Password,
                IntegratedSecurity = this.IntegratedSecurity
            };

            try
            {
                using (var connection = new SqlConnection(connectionstringBuilder.ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"CREATE DATABASE {InitialCatalog}";
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
