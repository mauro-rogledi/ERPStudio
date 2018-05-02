using System.Data;

namespace ProvaProviders
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyProviderLoader.UseProvider = ProviderType.SQLite;
            var sqlconnectiostring = new SqlProxyConnectionStringbuilder
            {
                DataSource = @"SALA\SQLEXPRESS",
                //UserID = "sa",
                InitialCatalog = "PLUMBER",
                //Password = "sa.",
                IntegratedSecurity = true
            };

            var creadb = new SqlProxyDataBaseHelper
            {
                DataSource = @"C:\Users\Rogledi\Desktop\Clothes.db"
            };
            creadb.CreateDatabase();

            //var creadb = new SqlProxyCreateDatabase
            //{
            //    DataSource = "USR-ROGLEDIMAU1",
            //    UserID = "sa",
            //    InitialCatalog = "PROVADB",
            //};
            //creadb.CreateDatabase();

            //var sqlconnectiostring = new SqlProxyConnectionStringbuilder
            //{
            //    DataSource = @"USR-ROGLEDIMAU1",
            //    UserID = "sa",
            //    InitialCatalog = "NORTHWIND"
            //};

            //ProxyProviderLoader.UseProvider = ProviderType.SQLite;
            //var sqlconnectiostring = new SqlProxyConnectionStringbuilder
            //{
            //    DataSource = @"D:\Utenti\Mauro\Desktop\Clothes.db"
            // };

            using (var connection = new SqlProxyConnection(sqlconnectiostring.ConnectionString))
            {
                connection.Open();

                {
                    var transaction = connection.BeginTransaction();
                    var p1 = new SqlProxyParameter("@p1", DbType.String);
                    p1.Value = "ARDUINO";
                    using (var sqlCmd = new SqlProxyCommand("SELECT * FROM PL_MASTERS where CODE = @p1", connection))
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CUSTOMERS where CustomerID = @p1", connection))
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CL_MASTER where ID = @p1", connection))
                    {
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.Add(p1);
                        var sqldatareader = sqlCmd.ExecuteReader();
                        sqldatareader.Read();
                        var l = sqldatareader[3];
                        sqldatareader.Close();
                    }

                    transaction.Commit();
                }

                connection.Close();
            }
        }
    }
}
