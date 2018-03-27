using System.Data;

namespace SqlProxyProvider
{
    public interface ISqlProviderConnection : System.Data.IDbConnection
    {
        System.Data.IDbConnection Connection { get; }
    }
}
