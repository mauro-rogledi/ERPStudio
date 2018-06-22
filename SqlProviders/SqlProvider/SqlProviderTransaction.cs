using System.Data;
using System.Data.SqlClient;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderTransaction : SqlProxyProvider.ISqlProviderTransaction
    {
        SqlTransaction sqlTransaction;
        public IDbTransaction Transaction => sqlTransaction;
        public ISqlProviderConnection connection;

        public SqlProviderTransaction(ISqlProviderConnection connection, IDbTransaction isqlTransaction)
        {
            this.connection = connection;
            sqlTransaction = isqlTransaction as SqlTransaction;
        }

        public IsolationLevel IsolationLevel => sqlTransaction.IsolationLevel;

        public ISqlProviderConnection Connection => connection;

        public void Commit() => sqlTransaction.Commit();

        public void Dispose() => sqlTransaction.Dispose();

        public void Rollback() => sqlTransaction.Rollback();
    }
}
