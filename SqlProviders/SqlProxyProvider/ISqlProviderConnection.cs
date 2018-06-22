using System;
using System.Data;

namespace SqlProxyProvider
{
    public interface ISqlProviderConnection : IDisposable
    {
        System.Data.IDbConnection Connection { get; }

        string ConnectionString { get; set; }

        int ConnectionTimeout { get; }

        string Database { get; }

        ConnectionState State { get; }

        ISqlProviderTransaction BeginTransaction();

        ISqlProviderTransaction BeginTransaction(IsolationLevel il);

        void ChangeDatabase(string databaseName);

        void Close();

        ISqlProviderCommand CreateCommand();
 
        void Open();
    }
}
