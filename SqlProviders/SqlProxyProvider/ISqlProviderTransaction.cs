namespace SqlProxyProvider
{
    public interface ISqlProviderTransaction : System.Data.IDbTransaction
    {
        System.Data.IDbTransaction Transaction { get; }
    }
}
