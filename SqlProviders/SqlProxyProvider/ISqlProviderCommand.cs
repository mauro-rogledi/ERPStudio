using System;

namespace SqlProxyProvider
{
    public interface ISqlProviderCommand : IDisposable
    {
        System.Data.IDbCommand Command { get; }

        ISqlProviderConnection Connection { get; set; }

        ISqlProviderTransaction Transaction { get; set; }

        string CommandText { get; set; }

        int CommandTimeout { get; set; }

        System.Data.CommandType CommandType { get; set; }

        ISqlProviderParameterCollection Parameters { get; }

        System.Data.UpdateRowSource UpdatedRowSource { get; set; }

        void Cancel();
        ISqlProviderParameter CreateParameter();

        ISqlProviderDataReader ExecuteReader();

        ISqlProviderDataReader ExecuteReader(System.Data.CommandBehavior behavior);

        int ExecuteNonQuery();

        object ExecuteScalar();

        void Prepare();
    }
}
