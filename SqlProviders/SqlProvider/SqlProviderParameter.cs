using SqlProxyProvider;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlProvider
{
    internal class SqlProviderParameter : ISqlProviderParameter
    {
        private SqlParameter sqlParameter;
        public IDbDataParameter Parameter { get { return sqlParameter; } set { sqlParameter = value as SqlParameter; } }

        public SqlProviderParameter() => sqlParameter = new SqlParameter();
        public SqlProviderParameter(IDbDataParameter parameter) => sqlParameter = parameter as SqlParameter;
        public SqlProviderParameter(string parameterName, DbType dbType) => sqlParameter = new SqlParameter(parameterName, ConvertDbType.GetSqlDbType(dbType));
        public SqlProviderParameter(string parameterName, object value) => sqlParameter = new SqlParameter(parameterName, value);
        public SqlProviderParameter(string parameterName, DbType dbType, int size) => sqlParameter = new SqlParameter(parameterName, ConvertDbType.GetSqlDbType(dbType), size);
        public SqlProviderParameter(string parameterName, DbType dbType, int size, string sourceColumn) => sqlParameter = new SqlParameter(parameterName, ConvertDbType.GetSqlDbType(dbType), size, sourceColumn);

        public byte Precision { get => sqlParameter.Precision; set => sqlParameter.Precision = value; }
        public byte Scale { get => sqlParameter.Scale; set => sqlParameter.Scale = value; }
        public int Size { get => sqlParameter.Size; set => sqlParameter.Size = value; }
        public DbType DbType { get => sqlParameter.DbType; set => sqlParameter.DbType = value; }
        public ParameterDirection Direction { get => sqlParameter.Direction; set => sqlParameter.Direction = value; }

        public bool IsNullable => sqlParameter.IsNullable;

        public string ParameterName { get => sqlParameter.ParameterName; set => sqlParameter.ParameterName = value; }
        public string SourceColumn { get => sqlParameter.SourceColumn; set => sqlParameter.SourceColumn = value; }
        public DataRowVersion SourceVersion { get => sqlParameter.SourceVersion; set => sqlParameter.SourceVersion = value; }
        public object Value
        {
            // TODO
            get => sqlParameter.Value; set => sqlParameter.Value = (value is Enum)
               ? ((Enum)value)//.Int()
               : value;
        }
    }
}
