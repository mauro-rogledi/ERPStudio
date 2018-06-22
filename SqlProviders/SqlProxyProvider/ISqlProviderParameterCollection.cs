using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SqlProxyProvider
{
    public interface ISqlProviderParameterCollection
    {
        ISqlProviderCommand Command { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        ISqlProviderParameter this[string parameterName] { get;  }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        ISqlProviderParameter this[int index] { get;  }

        object SyncRoot { get; }
        bool IsReadOnly { get; }
        bool IsFixedSize { get; }
        
        int Count { get; }
        bool IsSynchronized { get; }

        
        [EditorBrowsable(EditorBrowsableState.Never)]
        //int Add(object value);
        ISqlProviderParameter Add(ISqlProviderParameter value);
        [EditorBrowsable(EditorBrowsableState.Never)]

        //ISqlProviderParameter Add(string parameterName, SqlDbType sqlDbType);
        
        //ISqlProviderParameter Add(string parameterName, SqlDbType sqlDbType, int size);
        //ISqlProviderParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn);

        //void AddRange(Array values);
        void AddRange(ISqlProviderParameter[] param);
        void AddRange(List<ISqlProviderParameter> param);

        // ISqlProviderParameter AddWithValue(string parameterName, object value);
        void Clear();
        bool Contains(string value);
        bool Contains(object value);
        bool Contains(ISqlProviderParameter value);
//        void CopyTo(ISqlProviderParameter[] array, int index);
  //      void CopyTo(Array array, int index);

        int IndexOf(ISqlProviderParameter value);
        int IndexOf(string parameterName);
        int IndexOf(object value);
        void Insert(int index, object value);
        void Insert(int index, ISqlProviderParameter value);
        void Remove(object value);
        void Remove(ISqlProviderParameter value);
        void RemoveAt(int index);
        
        void RemoveAt(string parameterName);
        //ISqlProviderParameter GetParameter(string parameterName);
        //ISqlProviderParameter GetParameter(int index);
        //void SetParameter(int index, ISqlProviderParameter value);
        //void SetParameter(string parameterName, ISqlProviderParameter value);

    }
}
