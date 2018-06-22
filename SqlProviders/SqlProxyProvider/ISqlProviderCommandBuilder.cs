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
        ISqlProviderDataAdapter DataAdapter { get; set; }

        string QuoteSuffix { get; set; }
        string QuotePrefix { get; set; }
        ISqlProviderCommand GetDeleteCommand();
        ISqlProviderCommand GetDeleteCommand(bool useColumnsForParameterNames);
        ISqlProviderCommand GetInsertCommand();
        ISqlProviderCommand GetInsertCommand(bool useColumnsForParameterNames);
        ISqlProviderCommand GetUpdateCommand(bool useColumnsForParameterNames);
        ISqlProviderCommand GetUpdateCommand();

       System.Data.ConflictOption ConflictOption { get; set; }
    }
}
