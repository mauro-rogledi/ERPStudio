using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderParameterCollection : ISqlProviderParameterCollection
    {
        SqlCommand sqlCommand;
        public IDbCommand Command => sqlCommand;

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object this[string parameterName] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SqlProviderParameterCollection(IDbCommand command) => sqlCommand = command as SqlCommand;

        public object this[int index] { get => sqlCommand.Parameters[index].Value; set => sqlCommand.Parameters[index].Value = value; }


        public void Add(ISqlProviderParameter parameter)
        {
            sqlCommand.Parameters.Add(parameter);
        }

        public void AddRange(ISqlProviderParameter[] parameters)
        {
            sqlCommand.Parameters.AddRange(parameters);
        }

        public void AddRange(List<ISqlProviderParameter> parameters)
        {
            foreach (var parameter in parameters)
                Add(parameter);
        }

        public bool Contains(string parameterName)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
