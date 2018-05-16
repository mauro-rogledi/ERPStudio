using System.Data;
using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderTransaction : SqlProxyProvider.ISqlProviderTransaction
    {
        SQLiteTransaction sqliteTransaction;
        public IDbTransaction Transaction => sqliteTransaction;

        public SqlProviderTransaction(IDbTransaction isqlTransaction) => sqliteTransaction = isqlTransaction as SQLiteTransaction;

        public IDbConnection Connection => sqliteTransaction.Connection;

        public IsolationLevel IsolationLevel => sqliteTransaction.IsolationLevel;

        public void Commit() => sqliteTransaction.Commit();

        public void Dispose() => sqliteTransaction.Dispose();

        public void Rollback() => sqliteTransaction.Rollback();
    }
}
