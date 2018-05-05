using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderDataAdapter : System.Data.IDbDataAdapter
    {
        System.Data.IDbDataAdapter DataAdapter { get; }

        int Fill(DataTable dataTable);
        int Fill(DataSet dataSet, string tableName);
        int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable);
    }
}
