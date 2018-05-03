using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderDataAdapter : System.Data.IDbDataAdapter
    {
        System.Data.IDbDataAdapter DataAdapter { get; }
    }
}
