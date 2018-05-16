using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;

namespace SqlceProviders
{
    public class SqlProviderConnection : IDbConnection
    {
        SqlCeConnection sqlConnection;

        string IDbConnection.ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        int IDbConnection.ConnectionTimeout => throw new NotImplementedException();

        string IDbConnection.Database => throw new NotImplementedException();

        ConnectionState IDbConnection.State => throw new NotImplementedException();

        IDbTransaction IDbConnection.BeginTransaction()
        {
            throw new NotImplementedException();
        }

        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        void IDbConnection.ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        void IDbConnection.Close()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDbConnection.CreateCommand()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        void IDbConnection.Open()
        {
            throw new NotImplementedException();
        }
    }
}
