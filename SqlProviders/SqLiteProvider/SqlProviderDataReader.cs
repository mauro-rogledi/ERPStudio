using System;
using System.Data;
using System.Data.SQLite;

namespace SqlProvider
{
    class SqlProviderDataReader : SqlProxyProvider.ISqlProviderDataReader
    {
        SQLiteDataReader sqliteDataReader;
        public IDataReader DataReader => sqliteDataReader;

        SqlProviderDataReader(IDataReader idatareader) => sqliteDataReader = idatareader as SQLiteDataReader;

        public object this[int i] => sqliteDataReader[i];

        public object this[string name] => sqliteDataReader[name];

        public int Depth => sqliteDataReader.Depth;

        public bool IsClosed => sqliteDataReader.IsClosed;

        public int RecordsAffected => sqliteDataReader.RecordsAffected;

        public int FieldCount => sqliteDataReader.FieldCount;


        public void Close() => sqliteDataReader.Close();

        public void Dispose() => sqliteDataReader.Dispose();

        public bool GetBoolean(int i) => sqliteDataReader.GetBoolean(i);

        public byte GetByte(int i) => sqliteDataReader.GetByte(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => sqliteDataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

        public char GetChar(int i) => sqliteDataReader.GetChar(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => sqliteDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);

        public IDataReader GetData(int i) => sqliteDataReader.GetData(i);

        public string GetDataTypeName(int i) => sqliteDataReader.GetDataTypeName(i);

        public DateTime GetDateTime(int i) => sqliteDataReader.GetDateTime(i);

        public decimal GetDecimal(int i) => sqliteDataReader.GetDecimal(i);

        public double GetDouble(int i) => sqliteDataReader.GetDouble(i);

        public Type GetFieldType(int i) => sqliteDataReader.GetFieldType(i);

        public float GetFloat(int i) => sqliteDataReader.GetFloat(i);

        public Guid GetGuid(int i) => sqliteDataReader.GetGuid(i);

        public short GetInt16(int i) => sqliteDataReader.GetInt16(i);

        public int GetInt32(int i) => sqliteDataReader.GetInt32(i);

        public long GetInt64(int i) => sqliteDataReader.GetInt64(i);

        public string GetName(int i) => sqliteDataReader.GetName(i);

        public int GetOrdinal(string name) => sqliteDataReader.GetOrdinal(name);

        public DataTable GetSchemaTable() => sqliteDataReader.GetSchemaTable();

        public string GetString(int i) => sqliteDataReader.GetString(i);

        public object GetValue(int i) => sqliteDataReader.GetValue(i);

        public int GetValues(object[] values) => sqliteDataReader.GetValues(values);

        public bool IsDBNull(int i) => sqliteDataReader.IsDBNull(i);

        public bool NextResult() => sqliteDataReader.NextResult();

        public bool Read() => sqliteDataReader.Read();
    }
}
