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
        ISqlProviderDataAdapter dataAdapter;

        public SqlProviderCommandBuilder() => sqlCommandBuilder = new SqlCommandBuilder();
        public SqlProviderCommandBuilder(ISqlProviderDataAdapter dataAdapter) => sqlCommandBuilder = new SqlCommandBuilder(dataAdapter.DataAdapter as SqlDataAdapter);

        public string QuoteSuffix { get => sqlCommandBuilder.QuoteSuffix; set => sqlCommandBuilder.QuoteSuffix = value; }
        public string QuotePrefix { get => sqlCommandBuilder.QuotePrefix; set => sqlCommandBuilder.QuotePrefix = value; }
        public ISqlProviderDataAdapter DataAdapter
        {
            get => dataAdapter;
            set { dataAdapter = value; sqlCommandBuilder.DataAdapter = dataAdapter.DataAdapter as SqlDataAdapter; }
        }
        public ConflictOption ConflictOption { get => sqlCommandBuilder.ConflictOption; set => sqlCommandBuilder.ConflictOption = value; }

        public ISqlProviderCommand GetDeleteCommand() => new SqlProviderCommand(sqlCommandBuilder.GetDeleteCommand());
        public ISqlProviderCommand GetDeleteCommand(bool useColumnsForParameterNames) => new SqlProviderCommand(sqlCommandBuilder.GetDeleteCommand(useColumnsForParameterNames));

        public ISqlProviderCommand GetInsertCommand() => new SqlProviderCommand(sqlCommandBuilder.GetInsertCommand());
        public ISqlProviderCommand GetInsertCommand(bool useColumnsForParameterNames) => new SqlProviderCommand(sqlCommandBuilder.GetInsertCommand(useColumnsForParameterNames));

        public ISqlProviderCommand GetUpdateCommand() => new SqlProviderCommand(sqlCommandBuilder.GetUpdateCommand());
        public ISqlProviderCommand GetUpdateCommand(bool useColumnsForParameterNames) => new SqlProviderCommand(sqlCommandBuilder.GetUpdateCommand(useColumnsForParameterNames));
    }
}
