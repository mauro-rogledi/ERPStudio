using SqlProxyProvider;
using System;
using System.Data;
using System.Data.SQLite;

namespace SqlProvider
{
    internal class SqlProviderParameter : ISqlProviderParameter
    {
        private SQLiteParameter SQLiteParameter;
        public IDbDataParameter Parameter => SQLiteParameter;

        public SqlProviderParameter() => SQLiteParameter = new SQLiteParameter();
        public SqlProviderParameter(string parameterName, DbType dbType) => SQLiteParameter = new SQLiteParameter(parameterName, dbType);
        public SqlProviderParameter(string parameterName, object value) => SQLiteParameter = new SQLiteParameter(parameterName, value);
        public SqlProviderParameter(string parameterName, DbType dbType, int size) => SQLiteParameter = new SQLiteParameter(parameterName, dbType, size);
        public SqlProviderParameter(string parameterName, DbType dbType, int size, string sourceColumn) => SQLiteParameter = new SQLiteParameter(parameterName, dbType, size, sourceColumn);

        public byte Precision { get => SQLiteParameter.Precision; set => SQLiteParameter.Precision = value; }
        public byte Scale { get => SQLiteParameter.Scale; set => SQLiteParameter.Scale = value; }
        public int Size { get => SQLiteParameter.Size; set => SQLiteParameter.Size = value; }
        public DbType DbType { get => SQLiteParameter.DbType; set => SQLiteParameter.DbType = value; }
        public ParameterDirection Direction { get => SQLiteParameter.Direction; set => SQLiteParameter.Direction = value; }

        public bool IsNullable => SQLiteParameter.IsNullable;

        public string ParameterName { get => SQLiteParameter.ParameterName; set => SQLiteParameter.ParameterName = value; }
        public string SourceColumn { get => SQLiteParameter.SourceColumn; set => SQLiteParameter.SourceColumn = value; }
        public DataRowVersion SourceVersion { get => SQLiteParameter.SourceVersion; set => SQLiteParameter.SourceVersion = value; }
        public object Value
        {
            // TODO
            get => SQLiteParameter.Value; set => SQLiteParameter.Value = (value is Enum)
               ? ((Enum)value)//.Int()
               : value;
        }
    }
}
