using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderCommand : IDbCommand, ICloneable
    {
        IDbCommand dbCommand;

        public SqlProviderCommand()
        {
            dbCommand = new SqlCommand();
        }

        public SqlProviderCommand(string cmdText)
        {
            dbCommand = new SqlCommand(cmdText);
        }

        public SqlProviderCommand(string cmdText, IDbConnection connection)
        {
            dbCommand = new SqlCommand(cmdText, connection as SqlConnection);
        }

        public IDbConnection Connection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbTransaction Transaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CommandText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CommandType CommandType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDataParameterCollection Parameters => throw new NotImplementedException();

        public UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }
    }
}
