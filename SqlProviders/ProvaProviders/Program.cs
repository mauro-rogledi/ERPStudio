using System.Data;
using System.Data.SqlClient;

namespace ProvaProviders
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProxyProviderLoader.UseProvider = ProviderType.SQL;
            //var sqlconnectiostring = new SqlProxyConnectionStringbuilder
            //{
            //    DataSource = @"SALA\SQLEXPRESS",
            //    //UserID = "sa",
            //    InitialCatalog = "PLUMBER",
            //    //Password = "sa.",
            //    IntegratedSecurity = true
            //};

            //var creadb = new SqlProxyDataBaseHelper
            //{
            //    DataSource = @"C:\Users\Rogledi\Desktop\Clothes.db"
            //};
            //creadb.CreateDatabase();

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

            ProxyProviderLoader.UseProvider = ProviderType.SQLite;
            var sqlconnectiostring = new SqlProxyConnectionStringbuilder
            {
                DataSource = @"D:\Utenti\Mauro\Desktop\Clothes.db"
            };

            using (var connection = new SqlProxyConnection(sqlconnectiostring.ConnectionString))
            {
                connection.Open();

                {
                    //var transaction = connection.BeginTransaction();
                    //var p1 = new SqlProxyParameter("@p1", DbType.String, 16)
                    //{
                    //    Value = "ARDUINO"
                    //};

                    var p1 = new SqlProxyParameter("@p1", DbType.Int16)
                    {
                        Value = 1
                    };

                    var dataSet = new DataSet();
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CUSTOMERS where CustomerID = @p1", connection))
                    using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CL_MASTER where ID = @p1", connection))
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM PL_MASTERS where Code = @p1", connection))
                    {
                        sqlCmd.Parameters.Add(p1);
                        var dAdapter = new SqlProxyDataAdapter
                        {
                            SelectCommand = sqlCmd
                        };
                        dAdapter.Fill(dataSet);


                        //sqlCmd.Transaction = transaction;
                        //var sqldatareader = sqlCmd.ExecuteReader();
                        //sqldatareader.Read();
                        //var l = sqldatareader[3];
                        //sqldatareader.Close();
                    }

                    //transaction.Commit();
                }

                connection.Close();
            }
        }
    }
}
