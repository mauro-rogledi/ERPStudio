using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ProvaProviders
{
    class Program
    {
        public static DbType GetDBType(Type typein)
        {
            var dictConver = new Dictionary<Type, DbType>
            {
                {typeof(String), DbType.String},
                {typeof(Int32), DbType.Int32},
                {typeof(Byte), DbType.Byte},
                {typeof(Enum), DbType.UInt16},
                {typeof(Boolean), DbType.Boolean},
                {typeof(Decimal), DbType.Decimal},
                {typeof(Single), DbType.Single},
                {typeof(Double), DbType.Double},
                {typeof(DateTime), DbType.DateTime},
                {typeof(TimeSpan), DbType.DateTime}
            };
            var labase = typein.BaseType;

            if (dictConver.ContainsKey(typein))
                return dictConver[typein];

            throw new Exception("Unknown Type");
        }

        static void Main(string[] args)
        {

            var result = GetDBType(typeof(String));
            ProxyProviderLoader.UseProvider = ProviderType.SQL;
            //var list = SqlProxyDatabaseHelper.ListDatabase("USR-ROGLEDIMAU1", "sa", "", true);

            var sqlconnectiostring = new SqlProxyConnectionStringbuilder
            {
                DataSource = @"SALA\SQLEXPRESS",
                //UserID = "sa",
                InitialCatalog = "PLUMBER",
                //Password = "sa.",
                IntegratedSecurity = true
            };

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

            SqlProxyDatabaseHelper.Password = "aa";

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
            //};

            using (var connection = new SqlProxyConnection(sqlconnectiostring.ConnectionString))
            {
                connection.Open();

                {
                   // var transaction = connection.BeginTransaction();
                    var p1 = new SqlProxyParameter("@p1", DbType.String, 16)
                    {
                        Value = "ARDUINO"
                    };

                    //var p1 = new SqlProxyParameter("@p1", DbType.Int16)
                    //{
                    //    Value = 1
                    //};

                    //var p1 = new SqlProxyParameter("@p1", DbType.String, 5)
                    //{
                    //    Value = "ALFKI"
                    //};

                    var dataSet = new DataSet();
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CUSTOMERS where CustomerID = @p1"))
                    //using (var sqlCmd = new SqlProxyCommand("SELECT * FROM CL_MASTER where ID = @p1", connection))
                    using (var sqlCmd = new SqlProxyCommand(connection))
                    {
                        var p2 = sqlCmd.Parameters.Add(p1);
                        sqlCmd.CommandText = "SELECT * FROM PL_MASTERS where Code = @p1";
                        var dAdapter = new SqlProxyDataAdapter()
                        {
                            SelectCommand = sqlCmd
                        };

                        var cBuilder = new SqlProxyCommandBuilder(dAdapter)
                        {
                            QuotePrefix = "[",
                            QuoteSuffix = "]",
                            ConflictOption = ConflictOption.CompareRowVersion
                        };
                        //dAdapter.UpdateCommand = cBuilder.GetUpdateCommand();
                        dAdapter.InsertCommand = cBuilder.GetInsertCommand();
                        //dAdapter.DeleteCommand = cBuilder.GetDeleteCommand();


                        //dAdapter.RowUpdating += DAdapter_RowUpdating;
                        dAdapter.Fill(dataSet, "tablename");
                        var e = new RowUpdatedEventArgs(dataSet.Tables[0].Rows[0], new SqlCommand(), StatementType.Batch, new DataTableMapping());

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

        private static void DAdapter_RowUpdating(object sender, RowUpdatingEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
