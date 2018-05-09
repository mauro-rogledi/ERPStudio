using SqlProxyProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderParameterCollection : ISqlProviderParameterCollection
    {
        SQLiteCommand sqliteCommand;
        public IDbCommand Command => sqliteCommand;
        public SqlProviderParameterCollection() { }
        public SqlProviderParameterCollection(IDbCommand command) => sqliteCommand = command as SQLiteCommand;

        public bool IsReadOnly => sqliteCommand.Parameters.IsReadOnly;

        public bool IsFixedSize => sqliteCommand.Parameters.IsFixedSize;

        public int Count => sqliteCommand.Parameters.Count;

        public object SyncRoot => sqliteCommand.Parameters.SyncRoot;

        public bool IsSynchronized => sqliteCommand.Parameters.IsSynchronized;

        public object this[string parameterName] { get => sqliteCommand.Parameters[parameterName].Value; set => sqliteCommand.Parameters[parameterName].Value = value; }


        public object this[int index] { get => sqliteCommand.Parameters[index].Value; set => sqliteCommand.Parameters[index].Value = value; }


        public ISqlProviderParameter Add(ISqlProviderParameter parameter)
        {
            var param = parameter.Parameter as ISqlProviderParameter;

            sqliteCommand.Parameters.Add(param.Parameter);

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

        public bool Contains(string parameterName) => sqliteCommand.Parameters.Contains(parameterName);

        public int IndexOf(string parameterName) => sqliteCommand.Parameters.IndexOf(parameterName);

        public void RemoveAt(string parameterName) => sqliteCommand.Parameters.RemoveAt(parameterName);

        public bool Contains(object value) => sqliteCommand.Parameters.Contains(value);

        public void Clear() => sqliteCommand.Parameters.Clear();

        public int IndexOf(object value) => sqliteCommand.Parameters.IndexOf(value);

        public void Insert(int index, object value) => sqliteCommand.Parameters.Insert(index, value);

        public void Remove(object value) => sqliteCommand.Parameters.Remove(value);

        public void RemoveAt(int index) => sqliteCommand.Parameters.RemoveAt(index);

        public void CopyTo(Array array, int index) => sqliteCommand.Parameters.CopyTo(array, index);

        public IEnumerator GetEnumerator() => sqliteCommand.Parameters.GetEnumerator();

        public int Add(object value) => sqliteCommand.Parameters.Add(value);
    }
}
