using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderDataReader : IDataReader
    {
        SqlDataReader dataReader;

        public object this[int i] => dataReader[i];

        public object this[string name] => dataReader[name];

        public int Depth => dataReader.Depth;

        public bool IsClosed => dataReader.IsClosed;

        public int RecordsAffected => dataReader.RecordsAffected;

        public int FieldCount => dataReader.FieldCount;

        public void Close() => dataReader.Close();

        public void Dispose() => dataReader.Dispose();

        public bool GetBoolean(int i) => dataReader.GetBoolean(i);

        public byte GetByte(int i) => dataReader.GetByte(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => dataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public char GetChar(int i) => dataReader.GetChar(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => dataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
 
        public IDataReader GetData(int i) => dataReader.GetData(i);

        public string GetDataTypeName(int i) => dataReader.GetDataTypeName(i);

        public DateTime GetDateTime(int i) => dataReader.GetDateTime(i);

        public decimal GetDecimal(int i) => dataReader.GetDecimal(i);

        public double GetDouble(int i) => dataReader.GetDouble(i);

        public Type GetFieldType(int i) => dataReader.GetFieldType(i);

        public float GetFloat(int i) => dataReader.GetFloat(i);

        public Guid GetGuid(int i) => dataReader.GetGuid(i);

        public short GetInt16(int i) => dataReader.GetInt16(i);

        public int GetInt32(int i) => dataReader.GetInt32(i);

        public long GetInt64(int i) => dataReader.GetInt64(i);

        public string GetName(int i) => dataReader.GetName(i);

        public int GetOrdinal(string name) => dataReader.GetOrdinal(name);

        public DataTable GetSchemaTable() => dataReader.GetSchemaTable(i);

        public string GetString(int i) => dataReader.GetString(i);

        public object GetValue(int i) => dataReader.GetValue(i);

        public int GetValues(object[] values) => dataReader.GetValues(values);

        public bool IsDBNull(int i) => dataReader.IsDBNull(i);

        public bool NextResult() => dataReader.NextResult();

        public bool Read() => dataReader.Read();
    }
}
