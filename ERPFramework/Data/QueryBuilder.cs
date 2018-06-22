using SqlProxyProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPFramework.Data
{
    public class QueryBuilder
    {
        #region Private Properties
        private bool _from = true;
        private bool _select = true;
        private bool _where = true;
        private bool _group = true;
        private bool _qualified;
        private readonly bool _hasExecuter;
        private bool _toAppend;
        private string _append = string.Empty;

        private readonly StringBuilder sb = new StringBuilder();
        private SqlProxyCommand scc;

        private Dictionary<string, string> qualifiedMap;
        #endregion

        public string Query
        {
            get
            {
                if (_toAppend)
                    sb.Append(_append);
                _toAppend = false;
                return sb.ToString().TrimEnd(' ') ?? "";
            }
        }

        public override string ToString()
        {
            return sb.ToString().TrimStart(',').TrimAll() ?? "";
        }

        public bool HasWhere { get { return !_where; } }

        public bool AddSelectStatement { set { _select = value; } }

        public QueryBuilder(bool qualified = false, bool hasExecuter = false)
        {
            _qualified = qualified;
            if (hasExecuter)
            {
                _hasExecuter = hasExecuter;
                scc = new SqlProxyCommand(GlobalInfo.SqlConnection);
            }
        }

#pragma warning disable CC0091 // Use static method
        public QueryBuilder SubSelect(bool qualified = false) => new QueryBuilder(qualified);
#pragma warning restore CC0091 // Use static method

        public QueryBuilder AddSelect(bool addSelect)
        {
            AddSelectStatement = addSelect;
            return this;
        }

        public QueryBuilder Qualify<T>(string name)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            if (qualifiedMap == null)
                qualifiedMap = new Dictionary<string, string>();
            else
                System.Diagnostics.Debug.Assert(!qualifiedMap.ContainsKey(tableName));

            qualifiedMap.Add(tableName, name);
            return this;
        }

        public QueryBuilder Append(string text)
        {
            _toAppend = true;
            _append = text;
            return this;
        }

        public void Dispose()
        {
            scc.Dispose();
            scc = null;
        }

        public void Clear()
        {
            sb.Clear();
            _from = true;
            _select = true;
            _where = true;
            _group = true;
            _toAppend = false;
            _append = string.Empty;
        }

        private string DecodeColumn(object column)
        {
            if (column is IColumn)
                return (column as IColumn).ColumnName(_qualified, qualifiedMap);
            else if (column is QueryBuilder)
                return (column as QueryBuilder).ToString();
            else if (column is string)
                return column as string;
            else if (column is DateTime)
            {
                var dVal = (DateTime)Convert.ChangeType(column, typeof(DateTime));
                return SqlProxyDatabaseHelper.ConvertDate(dVal);
            }

            System.Diagnostics.Debug.Assert(false, nameof(DecodeColumn));
            return "";
        }

        #region Select
        public QueryBuilder Select(object column)
        {
            if (_select)
                sb.AppendFormat("SELECT {0} ", DecodeColumn(column));
            else
                sb.AppendFormat(", {0} ", DecodeColumn(column));

            _select = false;

            return this;
        }

        public QueryBuilder Select(params object[] columns)
        {
            foreach (object column in columns)
            {
                if (_select)
                    sb.AppendFormat("SELECT {0} ", DecodeColumn(column));
                else
                    sb.AppendFormat(", {0} ", DecodeColumn(column));

                _select = false;
            }

            return this;
        }

        public QueryBuilder SelectTop(int val, object column = null)
        {
            switch (GlobalInfo.LoginInfo.ProviderType)
            {
#if(SQLite)
                case ProviderType.SQLite:
                    {
                        if (column != null)
                            sb.AppendFormat("SELECT {0} ", DecodeColumn(column));

                        _append = $"LIMIT {val} ";
                        _toAppend = true;
                        break;
                    }
#endif
                default:
                    {
                        if (_select)
                        {
                            if (column == null)
                                sb.AppendFormat("SELECT TOP {0}", val);
                            else
                                sb.AppendFormat("SELECT TOP {0} {1} ", val, DecodeColumn(column));
                        }
                        else
                            System.Diagnostics.Debug.Assert(false);
                        break;
                    }
            }

            _select = column == null;

            return this;
        }

        public QueryBuilder SelectAs(object column, string name)
        {
            if (_select)
                sb.AppendFormat("SELECT {0} as [{1}] ", DecodeColumn(column), name);
            else
                sb.AppendFormat(", {0} as [{1}] ", DecodeColumn(column), name);

            _select = false;

            return this;
        }

        public QueryBuilder SelectFunction(string funct, object column)
        {
            if (_select)
                sb.AppendFormat("SELECT {0}({1}) ", funct, DecodeColumn(column));
            else
                sb.AppendFormat(", {0}({1}) ", funct, DecodeColumn(column));

            _select = false;

            return this;
        }

        public QueryBuilder SelectFunctionAs(string funct, object column, string name = "")
        {
            if (name.IsEmpty() && !(column is IColumn))
            {
                System.Diagnostics.Debug.Assert(false, "Missing name of AS");
                return this;
            }

            if (_select)
                sb.AppendFormat("SELECT {0}({1}) as [{2}] ", funct, DecodeColumn(column), name.IsEmpty() ? (column as IColumn).Name : name);
            else
                sb.AppendFormat(", {0}({1}) as [{2}] ", funct, DecodeColumn(column), name.IsEmpty() ? (column as IColumn).Name : name);

            _select = false;

            return this;
        }

        public QueryBuilder SelectCastAs<T>(object column, int len = 0, int dec = 0, string name = "")
        {
            var castString = string.Empty;

            if (name.IsEmpty() && !(column is IColumn))
            {
                System.Diagnostics.Debug.Assert(false, "Missing name of AS");
                return this;
            }

            var vartype = ConvertColumnType.TypeAsString(typeof(T));
            if (vartype == "int" || vartype == "bit" || vartype == "datetime" || vartype == "text")
                castString += $"{vartype}";
            else
                if (vartype == "decimal")
                castString += $"{vartype}({len},{dec})";
            else
                castString += $"{vartype}({len})";

            if (_select)
                sb.AppendFormat("SELECT CAST({0} as {1}) as [{2}] ", DecodeColumn(column), castString, name.IsEmpty() ? (column as IColumn).Name : name);
            else
                sb.AppendFormat(", CAST({0} as {1}) as [{2}] ", DecodeColumn(column), castString, name.IsEmpty() ? (column as IColumn).Name : name);

            _select = false;

            return this;
        }

        public QueryBuilder SelectCastAs(object column, string name = "")
        {
            if (name.IsEmpty() && !(column is IColumn))
            {
                System.Diagnostics.Debug.Assert(false, "Missing name of AS");
                return this;
            }

            if (_select)
                sb.AppendFormat("SELECT CAST({0} as [{1}]) ", DecodeColumn(column), name.IsEmpty() ? (column as IColumn).Name : name);
            else
                sb.AppendFormat(", CAST({0} as [{1}]) ", DecodeColumn(column), name.IsEmpty() ? (column as IColumn).Name : name);

            _select = false;

            return this;
        }

        public QueryBuilder SelectAllFrom<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            return SelectAllFrom(typeof(T));
        }

        public QueryBuilder SelectAllFrom(Type table)
        {
            System.Diagnostics.Debug.Assert(table.BaseType == typeof(Table));

            var tableName = table.GetField("Name").GetValue(null).ToString();

            if (qualifiedMap == null || !qualifiedMap.ContainsKey(tableName))
            {
                if (_select)
                    sb.AppendFormat("SELECT [{0}].* FROM [{0}] ", tableName);
                else
                    sb.AppendFormat(", [{0}].* FROM [{0}] ", tableName);

            }
            else
            {
                if (_select)
                    sb.AppendFormat("SELECT [{1}].* FROM [{0}] AS [1] ", tableName, qualifiedMap[tableName]);
                else
                    sb.AppendFormat(", [{1}].* FROM [{0}] AS [1] ", tableName, qualifiedMap[tableName]);
            }

            _select = false;
            _from = false;

            return this;
        }

        public QueryBuilder SelectAll()
        {
            if (_select)
                sb.AppendFormat("SELECT * ");
            else
                System.Diagnostics.Debug.Assert(false, "Select without from");

            _select = false;

            return this;
        }

        public QueryBuilder SelectAll<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));

            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            if (qualifiedMap == null || !qualifiedMap.ContainsKey(tableName))
            {
                if (_select)
                    sb.AppendFormat("SELECT [{0}].* ", tableName);
                else
                    sb.AppendFormat(", [{0}].* ", tableName);
            }
            else
            {
                if (_select)
                    sb.AppendFormat("SELECT [{0}].* ", qualifiedMap[tableName]);
                else
                    sb.AppendFormat(", [{0}].* ", qualifiedMap[tableName]);
            }

            _select = false;

            return this;
        }
        #endregion

        #region Math Operations
        public QueryBuilder DividedBy(object val)
        {
            Is("/", val);

            return this;
        }

        public QueryBuilder Plus(object val)
        {
            Is("+", val);

            return this;
        }

        public QueryBuilder Minus(object val)
        {
            Is("-", val);

            return this;
        }

        public QueryBuilder MoltiplicationBy(object val)
        {
            Is("*", val);

            return this;
        }
        #endregion

        #region Date Operation
        public string Year(IColumn val)
        {
            System.Diagnostics.Debug.Assert(val.ColType == typeof(DateTime), "Column type must be DateTime type");
            return SqlProxyDatabaseHelper.GetYear(DecodeColumn(val));
        }

        public string Month(IColumn val)
        {
            System.Diagnostics.Debug.Assert(val.ColType == typeof(DateTime), "Column type must be DateTime type");
            return SqlProxyDatabaseHelper.GetMonth(DecodeColumn(val));
        }

        public string Day(IColumn val)
        {
            System.Diagnostics.Debug.Assert(val.ColType == typeof(DateTime), "Column type must be DateTime type");
            return SqlProxyDatabaseHelper.GetDay(DecodeColumn(val));
        }

        public string WeekOfYear(IColumn val)
        {
            System.Diagnostics.Debug.Assert(val.ColType == typeof(DateTime), "Column type must be DateTime type");
            return SqlProxyDatabaseHelper.GetWeekOfYear(DecodeColumn(val));
        }

        public string DayOfYear(IColumn val)
        {
            System.Diagnostics.Debug.Assert(val.ColType == typeof(DateTime), "Column type must be DateTime type");
            return SqlProxyDatabaseHelper.DayOfYear(DecodeColumn(val));
        }

        public string DayOfWeek(IColumn val)
        {
            System.Diagnostics.Debug.Assert(val.ColType == typeof(DateTime), "Column type must be DateTime type");
            return SqlProxyDatabaseHelper.DayOfWeek(DecodeColumn(val));

        }
        #endregion

        #region Update Delete / Set
        public QueryBuilder Update<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));

            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            sb.AppendFormat("UPDATE {0} ", tableName);
            return this;
        }

        public QueryBuilder Delete<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));

            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            sb.AppendFormat("DELETE FROM [{0}] ", tableName);
            return this;
        }

        public QueryBuilder Set<T>(object column, T val)
        {
            sb.AppendFormat("SET {0}", DecodeColumn(column));
            return Is("=", val);
        }

        public QueryBuilder Set<T>(object column1, object column2)
        {
            sb.AppendFormat("SET {0} = {1}", DecodeColumn(column1), DecodeColumn(column2));

            return this;
        }
        #endregion

        #region Insert Into
        public QueryBuilder InsertInto<T>(params object[] columns)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));

            var tableName = typeof(T).GetField("Name").GetValue(null).ToString();

            var concat = "";
            foreach (object column in columns)
                concat = concat.SeparConcat($"{DecodeColumn(column)}", ", ");

            sb.AppendFormat("INSERT INTO {0} ({1}) ", tableName, concat);
            return this;
        }

        public QueryBuilder Values(params object[] values)
        {
            var concat = "";
            foreach (object param in values)
            {
                if (param is SqlProxyParameter)
                {
                    if (_hasExecuter && scc != null)
                        scc.Parameters.Add((param as SqlProxyParameter));

                    concat = concat.SeparConcat((param as SqlProxyParameter).ParameterName, ",");
                }
                else if (param is double || param is int)
                {
                    concat = concat.SeparConcat(param.ToString(), ",");
                }
                else if (param is Enum)
                {
                    var nVal = (int)Convert.ChangeType(param, typeof(int));
                    concat = concat.SeparConcat(nVal.ToString(), ",");
                }
                else if (param is DateTime)
                {
                    var dVal = (DateTime)Convert.ChangeType(param, typeof(DateTime));
                    concat = concat.SeparConcat(SqlProxyDatabaseHelper.ConvertDate(dVal), ",");
                }
                else
                    concat = concat.SeparConcat($"'{param.ToString()}'", ",");
            }

            sb.AppendFormat("VALUES ( {0} ) ", concat);

            return this;
        }
        #endregion

        #region From
        public QueryBuilder From<T>(string As = "")
        {
            return From(typeof(T), As);
        }

        public QueryBuilder From(Type table, string As = "")
        {
            System.Diagnostics.Debug.Assert(table.BaseType == typeof(Table));
            var tableName = table.GetField("Name").GetValue(null).ToString();

            if (As.IsEmpty())
            {
                if (_from)
                    sb.AppendFormat($"FROM [{tableName}] ");
                else
                    sb.AppendFormat($", [{tableName}] ");
            }
            else
            {
                if (_from)
                    sb.AppendFormat($"FROM [{tableName}] AS [{As}] ");
                else
                    sb.AppendFormat($", [{tableName}]  AS [{As}]");
            }

            _from = false;

            return this;
        }
        #endregion

        #region Join
        public QueryBuilder InnerJoin(IColumn column1, IColumn column2, string oper = "=")
        {
            join("INNER JOIN", column1, column2, oper);
            return this;
        }
        public QueryBuilder OutherJoin(IColumn column1, IColumn column2, string oper = "=")
        {
            join("OUTER JOIN", column1, column2, oper);
            return this;
        }

        public QueryBuilder LeftOutherJoin(IColumn column1, IColumn column2, string oper = "=")
        {
            join("LEFT OUTER JOIN", column1, column2, oper);
            return this;
        }

        public QueryBuilder RightOutherJoin(IColumn column1, IColumn column2, string oper = "=")
        {
            join("RIGHT OUTER JOIN", column1, column2, oper);
            return this;
        }

        private void join(string join, IColumn column1, IColumn column2, string oper = "=")
        {
            sb.AppendFormat("{0} [{1}] ON {2} {3} {4} ", join, column2.Tablename, DecodeColumn(column1), oper, DecodeColumn(column2));
        }
        #endregion

        #region Where
        public QueryBuilder Where(object column)
        {
            if (_where)
                sb.AppendFormat($"WHERE {DecodeColumn(column)} ");

            _where = false;
            return this;
        }

        public QueryBuilder AndOrWhere(object column)
        {
            if (_where)
                sb.AppendFormat($"WHERE {DecodeColumn(column)} ");
            else
                sb.AppendFormat($"AND {DecodeColumn(column)} ");

            _where = false;
            return this;
        }

        public QueryBuilder WhereOpenExpression(object column)
        {
            if (_where)
                sb.AppendFormat($"WHERE ( {DecodeColumn(column)} ");

            _where = false;
            return this;
        }

        #endregion

        #region Compare
        public QueryBuilder IsGreatherThan(object val, string qualified = "")
        {
            Is(">", val, qualified);

            return this;
        }

        public QueryBuilder IsGreatherEqualThan(object val, string qualified = "")
        {
            Is(">=", val, qualified);

            return this;
        }

        public QueryBuilder IsEqualTo(object val, string qualified = "")
        {
            Is("=", val, qualified);
            return this;
        }

        public QueryBuilder IsNotEqualTo(object val, string qualified = "")
        {
            Is("!=", val, qualified);
            return this;
        }

        public QueryBuilder IsLessThan(object val, string qualified = "")
        {
            Is("<", val, qualified);
            return this;
        }

        public QueryBuilder IsLessEqualThan(object val, string qualified = "")
        {
            Is("<=", val, qualified);
            return this;
        }

        public QueryBuilder Like(object val)
        {
            Is("LIKE", val);
            return this;
        }

        public QueryBuilder IsNull()
        {
            sb.Append("IS NULL ");
            return this;
        }

        public QueryBuilder Is(string operators, object val, string qualified = "")
        {
            if (val is ISqlProviderParameter)
            {
                sb.AppendFormat("{0} {1} ", operators, (val as ISqlProviderParameter).ParameterName);
                if (_hasExecuter && scc != null)
                    scc.Parameters.Add((val as ISqlProviderParameter));

            }
            else if (val is double || val is int)
            {
                sb.AppendFormat("{0} {1} ", operators, val);
            }
            else if (val is bool)
            {
                var bValue = (bool)Convert.ChangeType(val, typeof(bool));
                var bVal = bValue ? 1 : 0;
                sb.AppendFormat("{0} {1} ", operators, bVal);
            }
            else if (val is Enum)
            {
                var nVal = (int)Convert.ChangeType(val, typeof(int));
                sb.AppendFormat("{0} {1} ", operators, nVal);
            }
            else if (val is DateTime)
            {
                var dVal = (DateTime)Convert.ChangeType(val, typeof(DateTime));
                sb.AppendFormat("{0} {1} ", operators, $"{SqlProxyDatabaseHelper.ConvertDate(dVal)}");

            }
            else if (val is QueryBuilder)
            {
                var qb = val as QueryBuilder;
                sb.AppendFormat("{0} ({1}) ", operators, qb.ToString());
            }
            else if (val is IColumn)
            {
                if (qualified.IsEmpty())
                    sb.AppendFormat("{0} {1} ", operators, DecodeColumn(val));
                else
                    sb.AppendFormat("{0} [{1}].[{2}] ", operators, qualified, (val as IColumn).Name);
            }
            else
                sb.AppendFormat("{0} '{1}' ", operators, val);

            return this;
        }

        public QueryBuilder Between(object val1, object val2)
        {
            if (val1 is SqlProxyParameter && val2 is SqlProxyParameter)
            {
                sb.AppendFormat("BETWEEN {0} AND {1} ", (val1 as SqlProxyParameter).ParameterName, (val2 as SqlProxyParameter).ParameterName);
                if (_hasExecuter && scc != null)
                {
                    scc.Parameters.Add((val1 as SqlProxyParameter));
                    scc.Parameters.Add((val2 as SqlProxyParameter));
                }

            }
            else if (val1 is double || val1 is int)
            {
                sb.AppendFormat("BETWEEN {0} AND {1} ", val1, val2);
            }
            else if (val1 is Enum && val2 is Enum)
            {
                var nVal1 = (int)Convert.ChangeType(val1, typeof(int));
                var nVal2 = (int)Convert.ChangeType(val1, typeof(int));
                sb.AppendFormat("BETWEEN {0} AND {1} ", val1, val2);
            }
            else if (val1 is DateTime && val2 is DateTime)
            {
                var dVal1 = (DateTime)Convert.ChangeType(val1, typeof(DateTime));
                var dVal2 = (DateTime)Convert.ChangeType(val2, typeof(DateTime));
                sb.AppendFormat("BETWEEN {0} AND {1} ", $"{SqlProxyDatabaseHelper.ConvertDate(dVal1)}", $"{SqlProxyDatabaseHelper.ConvertDate(dVal2)}");
            }
            else
                sb.AppendFormat("BETWEEN '{0}' AND '{1}' ", val1, val2);

            return this;
        }

        public QueryBuilder AddFilter(string filter, string andor = "AND")
        {
            if (filter.IsEmpty())
                return this;

            if (_where)
                sb.AppendFormat("WHERE {0} ", filter);
            else
                sb.AppendFormat("{0} {1} ", andor, filter);

            return this;
        }
        #endregion

        #region IN
        public QueryBuilder In(params object[] values)
        {
            var concat = "";
            foreach (object param in values)
                concat = InConcat(concat, param);

            sb.AppendFormat("IN ( {0} ) ", concat);

            return this;
        }

        private string InConcat(string concat, object param)
        {
            if (param is SqlProxyParameter)
            {
                if (_hasExecuter && scc != null)
                    scc.Parameters.Add((param as SqlProxyParameter));

                concat = concat.SeparConcat((param as SqlProxyParameter).ParameterName, ",");
            }
            else if (param is double || param is int)
            {
                concat = concat.SeparConcat(param.ToString(), ",");
            }
            else if (param is Enum)
            {
                var nVal = (int)Convert.ChangeType(param, typeof(int));
                concat = concat.SeparConcat(nVal.ToString(), ",");
            }
            else if (param is DateTime)
            {
                var dVal = (DateTime)Convert.ChangeType(param, typeof(DateTime));
                concat = concat.SeparConcat($"{SqlProxyDatabaseHelper.ConvertDate(dVal)}", ",");
            }
            else if (param is bool)
            {
                var bVal = ((bool)param) ? 1 : 0;
                concat = concat.SeparConcat(bVal.ToString(), ",");
            }
            else if (param is Array)
            {
                foreach (object p in param as Array)
                    concat = InConcat(concat, p);
            }
            else
                concat = concat.SeparConcat($"'{param.ToString()}'", ",");

            return concat;
        }

        public QueryBuilder In(QueryBuilder query)
        {
            sb.AppendFormat("IN ( {0} ) ", query.Query);
            return this;
        }
        #endregion

        #region OR AND BRACES
        public QueryBuilder OrExpression()
        {
            sb.Append("OR ");
            return this;
        }
        public QueryBuilder AndExpression()
        {
            sb.Append("AND ");
            return this;
        }

        public QueryBuilder NotExpression()
        {
            sb.Append("NOT ");
            return this;
        }

        public QueryBuilder Or(object column)
        {
            sb.AppendFormat("OR {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder And(object column)
        {
            sb.AppendFormat("AND {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder Not(object column)
        {
            sb.AppendFormat("NOT {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder OrOpenExpression(object column)
        {
            sb.AppendFormat("OR ( {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder AndOpenExpression(object column)
        {
            sb.AppendFormat("AND ( {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder NotOpenExpression(object column)
        {
            sb.AppendFormat("NOT ( {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder OpenExpression(object column)
        {
            sb.AppendFormat("( {0} ", DecodeColumn(column));
            return this;
        }

        public QueryBuilder OpenExpression()
        {
            sb.Append("( ");
            return this;
        }

        public QueryBuilder CloseExpression(string As = "")
        {
            if (As.IsEmpty())
                sb.Append(") ");
            else
                sb.AppendFormat(") AS [{0}] ", As);
            return this;
        }
        #endregion

        #region ORDER GROUP HAVING

        public QueryBuilder OrderBy(params object[] parameters)
        {
            var concat = "";
            foreach (object param in parameters)
            {
                if (param is IColumn)
                {
                    var col = param as IColumn;
                    concat = concat.SeparConcat($"{DecodeColumn(col)}", ", ");
                }
                else if (param is Boolean)
                {
                    var desc = (Boolean)param;
                    if (desc)
                        concat = concat.SeparConcat("DESC", " ");
                }
                else if (param is string)
                {
                    concat = concat.SeparConcat($"[{(string)param}]", ",");
                }
                else
                    System.Diagnostics.Debug.Assert(false);
            }

            sb.AppendFormat("ORDER BY {0} ", concat);

            return this;
        }

        public QueryBuilder GroupBy(params object[] columns)
        {
            var concat = "";
            foreach (object column in columns)
                concat = concat.SeparConcat($"{DecodeColumn(column)}", ", ");

            if (_group)
                sb.AppendFormat($"GROUP BY {concat} ");
            else
                sb.AppendFormat($", {concat} ");

            _group = false;

            return this;
        }

        public QueryBuilder GroupBy(object column)
        {
            if (_group)
                sb.AppendFormat($"GROUP BY {DecodeColumn(column)} ");
            else
                sb.AppendFormat($", {DecodeColumn(column)} ");

            _group = false;

            return this;
        }

        public QueryBuilder Having(object column)
        {
            sb.AppendFormat("Having {0} ", DecodeColumn(column));

            return this;
        }
        #endregion

        #region Execute
        public SqlProxyDataReader ExecuteReader()
        {
            scc.CommandText = Query;
            return scc.ExecuteReader();
        }

        public T ExecuteScalar<T>()
        {
            scc.CommandText = Query;
            return (T)scc.ExecuteScalar();
        }

        public int ExecuteNonQuery()
        {
            scc.CommandText = Query;
            return scc.ExecuteNonQuery();
        }
        #endregion
    }
}
