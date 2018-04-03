using SqlProxyProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProvider
{
    internal class SqlProviderParameter : ISqlProviderParameter
    {
        private SqlParameter sqlParameter;
        public IDbDataParameter Parameter => sqlParameter;

        public SqlProviderParameter() => sqlParameter = new SqlParameter();
        public SqlProviderParameter(string parameterName, SqlDbType dbType) => new SqlParameter(parameterName, dbType);
        public SqlProviderParameter(string parameterName, object value) => new SqlParameter(parameterName, value);
        public SqlProviderParameter(string parameterName, SqlDbType dbType, int size) => new SqlParameter(parameterName, dbType, size);
        public SqlProviderParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn) => new SqlParameter(parameterName, dbType, size, sourceColumn);

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
