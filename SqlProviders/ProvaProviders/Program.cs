using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProxyProvider;
using System.Data.SqlClient;
using System.Data;

namespace ProvaProviders
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sqlconnectiostring = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"SALA\SQLEXPRESS",
            //    UserID = "sa",
            //    InitialCatalog = "PLUMBER",
            //    Password = "sa."
            //};

            var sqlconnectiostring = new SqlConnectionStringBuilder
            {
                DataSource = @"USR-ROGLEDIMAU1",
                UserID = "sa",
                InitialCatalog = "NORTHWIND"
            };

            using (var connection = new SqlProxyConnection(sqlconnectiostring.ConnectionString))
            {
                connection.Open();

                {
                    var transaction = connection.BeginTransaction();
                    var p1 = new SqlProxyParameter("@p1", SqlDbType.NVarChar, 8);
                    p1.Value = "ALFKI";
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM PL_MASTERS", connection))
                    using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CUSTOMERS where CustomerID = @p1", connection))
                    {
                        sqlCmd.Transaction = transaction;
                        sqlCmd.Parameters.Add(p1);
                        var sqldatareader = sqlCmd.ExecuteReader();
                        sqldatareader.Read();
                        var l = sqldatareader[0];
                        sqldatareader.Close();
                    }

                    transaction.Commit();
                }
            }
        }
    }
}
