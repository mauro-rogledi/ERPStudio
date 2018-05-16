using System;
using System.Data;
using System.IO;
using System.Windows;

namespace ERPFramework.Data
{
    public static class ImportExportData
    {
        public static event EventHandler<string> RowToElaborate;
        public static event EventHandler<Tuple<int, bool>> RowElaborated;

        public static bool ExportData(string folder)
        {
            foreach (var table in GlobalInfo.Tables)
            {
                if (!table.Value.ToExport)
                    continue;

                var tableName = table.Value.Table.GetField("Name").GetValue(null).ToString();
                using (var dataset = ReadData(tableName))
                {
                    if (dataset.Tables[tableName].Rows.Count == 0)
                        continue;

                    var filename = Path.ChangeExtension(Path.Combine(folder, tableName), ".xml");

                    RowToElaborate?.Invoke(null, tableName);

                    dataset.WriteXml(filename, XmlWriteMode.WriteSchema);

                    RowElaborated?.Invoke(null, new Tuple<int, bool>(dataset.Tables[tableName].Rows.Count, true));
                }
            }
            return true;
        }

        public static bool ImportData(string folder, bool clearTable)
        {
            foreach (var table in GlobalInfo.Tables)
            {
                if (!table.Value.ToExport)
                    continue;

                var tableName = table.Value.Table.GetField("Name").GetValue(null).ToString();
                var filename = Path.ChangeExtension(Path.Combine(folder, tableName), ".xml");
                if (!File.Exists(filename))
                    continue;

                if (clearTable)
                    ImportExportData.DeleteData(tableName);

                using (var dataset = new DataSet())
                {
                    dataset.ReadXml(filename, XmlReadMode.ReadSchema);
                    RowToElaborate?.Invoke(null, tableName);
                    var result = WriteData(tableName, dataset);
                    RowElaborated?.Invoke(null, new Tuple<int, bool>(dataset.Tables[tableName].Rows.Count, result));
                }

            }
            return true;
        }

        private static DataSet ReadData(string tableName)
        {
            var dataSet = new DataSet();
            var query = $"SELECT * FROM {tableName}";

            using (var sqlCM = new SqlProxyCommand(query, GlobalInfo.DBaseInfo.dbManager.DB_Connection))
            {

                var sqlDA = new SqlProxyDataAdapter(sqlCM);
                sqlDA.Fill(dataSet, tableName);
            }

            return dataSet;
        }

        private static bool WriteData(string tableName, DataSet dataSet)
        {
            var query = $"SELECT * FROM {tableName}";
            using (var sqlCM = new SqlProxyCommand(query, GlobalInfo.DBaseInfo.dbManager.DB_Connection))
            {

                var adp = new SqlProxyDataAdapter(sqlCM);
                var scb = new SqlProxyCommandBuilder(adp);
                try
                {
                    adp.InsertCommand = scb.GetInsertCommand();
                    var rows = adp.Update(dataSet, dataSet.Tables[0].TableName);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace, e.Message);
                    MessageBox.Show(e.StackTrace, e.Message);
                }
                finally
                {

                }
            }

            return true;
        }

        private static void DeleteData(string tableName)
        {
            var query = $"DELETE FROM {tableName}";
            using (var sqlCM = new SqlProxyCommand(query, GlobalInfo.DBaseInfo.dbManager.DB_Connection))
            {
                try
                {
                    sqlCM.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace, e.Message);
                    MessageBox.Show(e.StackTrace, e.Message);
                }
                finally
                {

                }
            }
        }
    }
}
