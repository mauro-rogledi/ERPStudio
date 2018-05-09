using System.Collections.Generic;
using System.Data;

namespace SqlProxyProvider
{
    public interface ISqlProviderParameterCollection : IDataParameterCollection
    {
        System.Data.IDbCommand Command { get; }
        ISqlProviderParameter Add(ISqlProviderParameter param);
        void AddRange(ISqlProviderParameter[] param);
        void AddRange(List<ISqlProviderParameter> param);
    }
}
