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
            var sqlconnectiostring = new SqlConnectionStringBuilder
            {
                DataSource = @"SALA\SQLEXPRESS",
                UserID = "sa",
                InitialCatalog = "PLUMBER",
                Password = "sa."
            };

            //var sqlconnectiostring = new SqlConnectionStringBuilder
            //{
            //    DataSource = @"USR-ROGLEDIMAU1",
            //    UserID = "sa",
            //    InitialCatalog = "NORTHWIND"
            //};

            using (var connection = new SqlProxyConnection(sqlconnectiostring.ConnectionString))
            {
                connection.Open();

                {
                    var transaction = connection.BeginTransaction();
                    using (var sqlCmd = new SqlProxyCommand("SELECT * FROM PL_MASTERS", connection))
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CUSTOMERS", connection))
                    {
                        sqlCmd.Transaction = transaction;
                        //sqlCmd.Parameters.Add()
                        var sqldatareader = sqlCmd.ExecuteReader();
                        sqldatareader.Read();
                        var l = sqldatareader[0];
                    }

                    transaction.Commit();
                }
            }
        }
    }
}
