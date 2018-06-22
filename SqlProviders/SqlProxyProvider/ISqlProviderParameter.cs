using System.Data;

namespace SqlProxyProvider
{
    public interface ISqlProviderParameter : IDbDataParameter
    {
        IDbDataParameter Parameter { get; set; }


    }
}
