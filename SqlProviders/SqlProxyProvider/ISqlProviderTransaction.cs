using System;

namespace SqlProxyProvider
{
    public interface ISqlProviderTransaction : IDisposable
    {
        System.Data.IDbTransaction Transaction { get; }

        ISqlProviderConnection Connection { get; }
        System.Data.IsolationLevel IsolationLevel { get; }
        void Commit();
        void Rollback();
    }
}
