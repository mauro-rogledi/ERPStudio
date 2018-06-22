namespace SqlProxyProvider
{
    public interface ISqlProviderTransaction 
    {
        System.Data.IDbTransaction Transaction { get; }

        ISqlProviderConnection Connection { get; }
        System.Data.IsolationLevel IsolationLevel { get; }
        void Commit();
        void Rollback();
    }
}
