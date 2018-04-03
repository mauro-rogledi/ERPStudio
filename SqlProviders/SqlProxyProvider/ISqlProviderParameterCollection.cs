using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProviderParameterCollection : IDataParameterCollection
    {

        System.Data.IDbCommand Command { get; }
        void Add(ISqlProviderParameter param);

        void AddRange(ISqlProviderParameter[] param);
        void AddRange(List<ISqlProviderParameter> param);

        //object this[int index] { get; set; }
    }
}
