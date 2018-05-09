using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProxyProvider;

namespace SqlProvider
{
    class SqlProviderCommandBuilder : ISqlProviderCommandBuilder
    {
        SqlCommandBuilder sqlCommandBuilder;

        public SqlProviderCommandBuilder() => sqlCommandBuilder = new SqlCommandBuilder();
        public SqlProviderCommandBuilder(SqlProviderDataAdapter dataAdapter) => sqlCommandBuilder = new SqlCommandBuilder(dataAdapter.DataAdapter as SqlDataAdapter);

        public string QuoteSuffix { get => sqlCommandBuilder.QuoteSuffix; set => sqlCommandBuilder.QuoteSuffix = value; }
        public string QuotePrefix { get => sqlCommandBuilder.QuotePrefix; set => sqlCommandBuilder.QuotePrefix = value; }
        public IDbDataAdapter DataAdapter { get => sqlCommandBuilder.DataAdapter; set => sqlCommandBuilder.DataAdapter = value as SqlDataAdapter; }
        public ConflictOption ConflictOption { get => sqlCommandBuilder.ConflictOption; set => sqlCommandBuilder.ConflictOption = value; }

        public IDbCommand GetDeleteCommand() => sqlCommandBuilder.GetDeleteCommand();
        public IDbCommand GetDeleteCommand(bool useColumnsForParameterNames) => sqlCommandBuilder.GetDeleteCommand(useColumnsForParameterNames);

        public IDbCommand GetInsertCommand() => sqlCommandBuilder.GetInsertCommand();
        public IDbCommand GetInsertCommand(bool useColumnsForParameterNames) => sqlCommandBuilder.GetInsertCommand(useColumnsForParameterNames);

        public IDbCommand GetUpdateCommand() => sqlCommandBuilder.GetUpdateCommand();
        public IDbCommand GetUpdateCommand(bool useColumnsForParameterNames) => sqlCommandBuilder.GetUpdateCommand(useColumnsForParameterNames);
    }
}
