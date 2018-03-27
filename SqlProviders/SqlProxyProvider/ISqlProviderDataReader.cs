using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderDataReader : System.Data.IDataReader
    {
        System.Data.IDataReader DataReader { get; }
    }
}
