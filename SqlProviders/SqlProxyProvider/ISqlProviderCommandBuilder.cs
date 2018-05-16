using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderCommandBuilder
    {
        string QuoteSuffix { get; set; }
        string QuotePrefix { get; set; }
        IDbDataAdapter DataAdapter { get; set; }
        IDbCommand GetDeleteCommand();
        IDbCommand GetDeleteCommand(bool useColumnsForParameterNames);
        IDbCommand GetInsertCommand();
        IDbCommand GetInsertCommand(bool useColumnsForParameterNames);
        IDbCommand GetUpdateCommand(bool useColumnsForParameterNames);
        IDbCommand GetUpdateCommand();

       System.Data.ConflictOption ConflictOption { get; set; }
    }
}
