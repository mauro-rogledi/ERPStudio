using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderDataAdapter
    {
        System.Data.IDbDataAdapter DataAdapter { get; }

        event EventHandler<RowUpdatingEventArgs> RowUpdating;
        event EventHandler<RowUpdatedEventArgs> RowUpdated;

        int Update(DataSet dataSet, string srcTable);

        int Fill(DataTable dataTable);
        int Fill(DataSet dataSet, string tableName);
        int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable);

        ISqlProviderCommand SelectCommand { get; set; }
        ISqlProviderCommand InsertCommand { get; set; }
        ISqlProviderCommand UpdateCommand { get; set; }
        ISqlProviderCommand DeleteCommand { get; set; }
    }
}
