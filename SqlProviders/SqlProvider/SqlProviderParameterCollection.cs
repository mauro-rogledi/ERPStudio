using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderParameterCollection : ISqlProviderParameterCollection
    {
        List<ISqlProviderParameter> parameterCollection = new List<ISqlProviderParameter>();
        SqlCommand sqlCommand;
        public ISqlProviderCommand Command { get; private set; }
        public SqlProviderParameterCollection() { }

        public SqlProviderParameterCollection(ISqlProviderCommand command)
        {
            Command = command;
            sqlCommand = command.Command as SqlCommand;
        }

        public bool IsReadOnly => sqlCommand.Parameters.IsReadOnly;

        public bool IsFixedSize => sqlCommand.Parameters.IsFixedSize;

        public int Count => sqlCommand.Parameters.Count;

        public object SyncRoot => sqlCommand.Parameters.SyncRoot;

        public bool IsSynchronized => sqlCommand.Parameters.IsSynchronized;

        public ISqlProviderParameter this[string parameterName]
        {
            get => parameterCollection.Find(x => x.ParameterName == parameterName);
        }

        public ISqlProviderParameter this[int index] { get => parameterCollection[index]; }


        public ISqlProviderParameter Add(ISqlProviderParameter parameter)
        {
            parameterCollection.Add(parameter);
            sqlCommand.Parameters.Add(parameter.Parameter);

            return parameter;
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
        public bool Contains(object value) => sqlCommand.Parameters.Contains(value);
        public bool Contains(ISqlProviderParameter param) => sqlCommand.Parameters.Contains(param.Parameter);

        public int IndexOf(string parameterName) => sqlCommand.Parameters.IndexOf(parameterName);
        public int IndexOf(object value) => sqlCommand.Parameters.IndexOf(value);
        public int IndexOf(ISqlProviderParameter param) => sqlCommand.Parameters.IndexOf(param.ParameterName);

        public void RemoveAt(string parameterName)
        {
            var param = parameterCollection.Find(x => x.ParameterName == parameterName);
            parameterCollection.Remove(param);
            sqlCommand.Parameters.RemoveAt(parameterName);
        }

        //@@TODO
        public void Clear()
        {
            sqlCommand.Parameters.Clear();
            parameterCollection.Clear();
        }


        public void Insert(int index, object value) => sqlCommand.Parameters.Insert(index, value);
        public void Insert(int index, ISqlProviderParameter param) => sqlCommand.Parameters.Insert(index, param.Parameter);

        public void Remove(object value) => sqlCommand.Parameters.Remove(value);
        public void Remove(ISqlProviderParameter param) => sqlCommand.Parameters.Remove(param.Parameter);

        public void RemoveAt(int index) => sqlCommand.Parameters.RemoveAt(index);

        public void CopyTo(Array array, int index) => sqlCommand.Parameters.CopyTo(array, index);

        public IEnumerator GetEnumerator() => sqlCommand.Parameters.GetEnumerator();

        public int Add(object value) => sqlCommand.Parameters.Add(value);
    }
}
