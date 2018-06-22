using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    class SqlProviderDataReader : SqlProxyProvider.ISqlProviderDataReader
    {
        SqlDataReader sqlDataReader;
        public IDataReader DataReader => sqlDataReader;

        public SqlProviderDataReader(IDataReader idatareader) => sqlDataReader = idatareader as SqlDataReader;

        public object this[int i] => sqlDataReader[i];

        public object this[string name] => sqlDataReader[name];

        public int Depth => sqlDataReader.Depth;

        public bool IsClosed => sqlDataReader.IsClosed;

        public int RecordsAffected => sqlDataReader.RecordsAffected;

        public int FieldCount => sqlDataReader.FieldCount;


        public void Close() => sqlDataReader.Close();

        public void Dispose() => sqlDataReader.Dispose();

        public bool GetBoolean(int i) => sqlDataReader.GetBoolean(i);

        public byte GetByte(int i) => sqlDataReader.GetByte(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => sqlDataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public char GetChar(int i) => sqlDataReader.GetChar(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => sqlDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

        public IDataReader GetData(int i) => sqlDataReader.GetData(i);

        public string GetDataTypeName(int i) => sqlDataReader.GetDataTypeName(i);

        public DateTime GetDateTime(int i) => sqlDataReader.GetDateTime(i);

        public decimal GetDecimal(int i) => sqlDataReader.GetDecimal(i);

        public double GetDouble(int i) => sqlDataReader.GetDouble(i);

        public Type GetFieldType(int i) => sqlDataReader.GetFieldType(i);

        public float GetFloat(int i) => sqlDataReader.GetFloat(i);

        public Guid GetGuid(int i) => sqlDataReader.GetGuid(i);

        public short GetInt16(int i) => sqlDataReader.GetInt16(i);

        public int GetInt32(int i) => sqlDataReader.GetInt32(i);

        public long GetInt64(int i) => sqlDataReader.GetInt64(i);

        public string GetName(int i) => sqlDataReader.GetName(i);

        public int GetOrdinal(string name) => sqlDataReader.GetOrdinal(name);

        public DataTable GetSchemaTable() => sqlDataReader.GetSchemaTable();

        public string GetString(int i) => sqlDataReader.GetString(i);

        public object GetValue(int i) => sqlDataReader.GetValue(i);

        public int GetValues(object[] values) => sqlDataReader.GetValues(values);

        public bool IsDBNull(int i) => sqlDataReader.IsDBNull(i);

        public bool NextResult() => sqlDataReader.NextResult();

        public bool Read() => sqlDataReader.Read();
    }
}
