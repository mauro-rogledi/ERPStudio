using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderRowUpdatingEventArgs
    {
        StatementType StatementType { get; }
        DataRow DataRow { get; }
    }

    public interface ISqlProviderRowUpdatedEventArgs
    {
        IDbCommand Command { get; }
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlClient.SqlRowUpdatedEventArgs
    //     class.
    //
    // Parameters:
    //   row:
    //     The System.Data.DataRow sent through an System.Data.Common.DbDataAdapter.Update(System.Data.DataSet).
    //
    //   command:
    //     The System.Data.IDbCommand executed when System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)
    //     is called.
    //
    //   statementType:
    //     One of the System.Data.StatementType values that specifies the type of query
    //     executed.
    //
    //   tableMapping:
    //     The System.Data.Common.DataTableMapping sent through an System.Data.Common.DbDataAdapter.Update(System.Data.DataSet).
}
