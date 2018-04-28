using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderParameterCollection : ISqlProviderParameterCollection
    {
        SqlCommand sqlCommand;
        public IDbCommand Command => sqlCommand;
        public SqlProviderParameterCollection() { }
        public SqlProviderParameterCollection(IDbCommand command) => sqlCommand = command as SqlCommand;

        public bool IsReadOnly => sqlCommand.Parameters.IsReadOnly;

        public bool IsFixedSize => sqlCommand.Parameters.IsFixedSize;

        public int Count => sqlCommand.Parameters.Count;

        public object SyncRoot => sqlCommand.Parameters.SyncRoot;

        public bool IsSynchronized => sqlCommand.Parameters.IsSynchronized;

        public object this[string parameterName] { get => sqlCommand.Parameters[parameterName].Value; set => sqlCommand.Parameters[parameterName].Value = value; }


        public object this[int index] { get => sqlCommand.Parameters[index].Value; set => sqlCommand.Parameters[index].Value = value; }


        public void Add(ISqlProviderParameter parameter)
        {
            var param = parameter.Parameter as ISqlProviderParameter;

            sqlCommand.Parameters.Add(param.Parameter);
        }

        public void AddRange(ISqlProviderParameter[] parameters)
        {
            foreach (var parameter in parameters)
                Add(parameter);
        }

        public void AddRange(List<ISqlProviderParameter> parameters)
        {
            foreach (var parameter in parameters)
                Add(parameter);
        }

        public bool Contains(string parameterName) => sqlCommand.Parameters.Contains(parameterName);

        public int IndexOf(string parameterName) => sqlCommand.Parameters.IndexOf(parameterName);

        public void RemoveAt(string parameterName) => sqlCommand.Parameters.RemoveAt(parameterName);

        public bool Contains(object value) => sqlCommand.Parameters.Contains(value);

        public void Clear() => sqlCommand.Parameters.Clear();

        public int IndexOf(object value) => sqlCommand.Parameters.IndexOf(value);

        public void Insert(int index, object value) => sqlCommand.Parameters.Insert(index, value);

        public void Remove(object value) => sqlCommand.Parameters.Remove(value);

        public void RemoveAt(int index) => sqlCommand.Parameters.RemoveAt(index);

        public void CopyTo(Array array, int index) => sqlCommand.Parameters.CopyTo(array, index);

        public IEnumerator GetEnumerator() => sqlCommand.Parameters.GetEnumerator();

        public int Add(object value) => sqlCommand.Parameters.Add(value);
    }
}
