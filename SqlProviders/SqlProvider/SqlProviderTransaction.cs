using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderTransaction : SqlProxyProvider.ISqlProviderTransaction
    {
        SqlTransaction sqlTransaction;
        public IDbTransaction Transaction => sqlTransaction;

        public SqlProviderTransaction(IDbTransaction isqlTransaction) => sqlTransaction = isqlTransaction as SqlTransaction;

        public IDbConnection Connection => sqlTransaction.Connection;

        public IsolationLevel IsolationLevel => sqlTransaction.IsolationLevel;

        public void Commit() => sqlTransaction.Commit();

        public void Dispose() => sqlTransaction.Dispose();

        public void Rollback() => sqlTransaction.Rollback();
    }
}
