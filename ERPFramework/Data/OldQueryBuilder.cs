using ApplicationLibrary.Data;
using System;

namespace ApplicationLibrary.Data
{
    #region QueryBuilder

    [Obsolete("Use QueryBuilder")]
    public class OldQueryBuilder : IDisposable
    {
        public bool SetQualified { get; set; }

        public enum JoinType { INNER, OUTER_LEFT, OUTER_RIGHT };

        private enum QueryType { SELECT, UPDATE, DELETE, MANUAL };

        private string sSelect = string.Empty;
        private string sSet = string.Empty;
        private string sFrom = string.Empty;
        private string sWhere = string.Empty;
        private string sOrder = string.Empty;
        private string sGroup = string.Empty;
        private string sHaving = string.Empty;
        private QueryType queryType = QueryType.SELECT;
        private bool hasExecuter = false;

        private Data.SqlABCommand scc = null;

        public string Select { get { return sSelect; } set { sSelect = value; } }

        public string Set { get { return sSet; } set { sSet = value; } }

        public string From { get { return sFrom; } set { sFrom = value; } }

        public string Where { get { return sWhere; } set { sWhere = value; } }

        public string Order { get { return sOrder; } set { sOrder = value; } }

        public string Group { get { return sGroup; } set { sGroup = value; } }

        public string Having { get { return sHaving; } set { sHaving = value; } }

        public string AND_OR = "AND";

        public OldQueryBuilder()
        {
        }

        public OldQueryBuilder(bool qualified)
        {
            SetQualified = qualified;
        }

        public OldQueryBuilder(bool qualified, bool hasExecuter)
        {
            SetQualified = qualified;
            this.hasExecuter = hasExecuter;
            scc = new SqlABCommand(GlobalInfo.LoginInfo.ProviderType, GlobalInfo.SqlConnection);
        }

        public void Dispose()
        {
            scc.Dispose();
            scc = null;
        }

        public void Clear()
        {
            sSelect = string.Empty;
            sFrom = string.Empty;
            sWhere = string.Empty;
            sOrder = string.Empty;
            sGroup = string.Empty;
            sHaving = string.Empty;
        }

        public void AddManualQuery(string query, params object[] param)
        {
            string[] parameters = new string[param.Length];
            for (int t = 0; t < param.Length; t++)
                parameters[t] = param[t].ToString();
            sSelect = string.Format(query, parameters);

            queryType = QueryType.MANUAL;
        }

        public string Query
        {
            get
            {
                //Debug.Assert(sSelect != string.Empty || sDelete != string.Empty, "SelectString is Empty");
                //Debug.Assert(sFrom != string.Empty || queryType == QueryType.UPDATE, "FromString is Empty");

                string myQuery = string.Empty;
                switch (queryType)
                {
                    case QueryType.MANUAL:
                        myQuery = Select;
                        break;

                    case QueryType.SELECT:
                        myQuery = sSelect.Length > 0 || sFrom.Length > 0
                                    ? string.Format("SELECT {0} FROM {1}", sSelect, sFrom)
                                    : string.Empty;
                        break;

                    case QueryType.UPDATE:
                        myQuery = string.Format("UPDATE {0}", sFrom);
                        break;

                    case QueryType.DELETE:
                        myQuery = string.Format("DELETE FROM {0}", sFrom);
                        break;
                }

                if (sSet != string.Empty)
                    myQuery += " SET " + sSet;

                if (sWhere != string.Empty)
                    myQuery += " WHERE " + sWhere;

                if (sGroup != string.Empty)
                    myQuery += " GROUP BY " + sGroup;

                if (sOrder != string.Empty)
                    myQuery += " ORDER BY " + sOrder;

                if (sHaving != string.Empty)
                    myQuery += " HAVING " + sHaving;

                return myQuery;
            }
        }

        public string AddFilter(string sFilter)
        {
            if (!string.IsNullOrEmpty(sFilter))
                sWhere = AddString(sWhere, sFilter, AND_OR);
            return sWhere;
        }

        public void AddManualFilter(string query, params object[] param)
        {
            string[] parameters = new string[param.Length];
            for (int t = 0; t < param.Length; t++)
                parameters[t] = param[t].ToString();
            AddFilter(string.Format(query, parameters));
        }

        public string AddFunction(string sfunc, IColumn sColumn)
        {
            string function = string.Format(sfunc, SetQualified ? sColumn.QualifyName : sColumn.Name);
            sSelect = AddString(sSelect, function);
            return sSelect;
        }

        public string AddFunctionAs(string sfunc, IColumn sColumn)
        {
            string function = string.Format(sfunc, SetQualified ? sColumn.QualifyName : sColumn.Name);
            function = string.Format("{0} AS {1}", function, sColumn.Name);
            sSelect = AddString(sSelect, function);
            return sSelect;
        }

        public string AddFunction(string sfunc, IColumn sColumn, string value)
        {
            string function = string.Format(sfunc, SetQualified ? sColumn.QualifyName : sColumn.Name, value);
            sSelect = AddString(sSelect, function);
            return sSelect;
        }

        public SqlABDataReader ExecuteReader()
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

        public void Update(string table)
        {
            sFrom = table;
            queryType = QueryType.UPDATE;
        }

        #region AddSet

        public string AddSet<T>(IColumn sColumn, T value)
        {
            if (typeof(T) == typeof(System.Boolean))
            {
                bool bValue = (bool)Convert.ChangeType(value, typeof(bool));
                string sVal = bValue ? "1" : "0";
                return AddSet<string>(sColumn, sVal);
            }
            else if (typeof(T).BaseType == typeof(System.Enum))
            {
                int nVal = (int)Convert.ChangeType(value, typeof(int));
                return AddSet<string>(sColumn, nVal.ToString());
            }
            else if (typeof(T) == typeof(System.DateTime))
            {
                DateTime dVal = (DateTime)Convert.ChangeType(value, typeof(DateTime));
                return AddSet<string>(sColumn, string.Format("{0}{1:00}{2:00}", dVal.Year, dVal.Month, dVal.Day));
            }
            else if (typeof(T) == typeof(string))
            {
                sSet = string.Format("{0} = '{1}'", sColumn.Name, value);
                return sSet;
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(double))
            {
                sSet = string.Format("{0} = {1}", sColumn.Name, value);
                return sSet;
            }

            return sSet;
        }
        public string AddSetFromColumn(IColumn sColumnTo, IColumn sColumnFrom)
        {
            sSet = string.Format("[{0}] = [{1}]", sColumnTo.Name, sColumnFrom.Name);
            return sSet;
        }
        #endregion

        #region AddSelect

        public string AddSelect()
        {
            sSelect = AddString(sSelect, "*");
            return sSelect;
        }

        public string AddDelete()
        {
            queryType = QueryType.DELETE;
            return string.Empty;
        }

        public string AddSelect(IColumn sColumn)
        {
            sSelect = AddString(sSelect, SetQualified ? sColumn.QualifyName : sColumn.Name);
            return sSelect;
        }

        public string AddSelect(string value)
        {
            sSelect = AddString(sSelect, value);
            return sSelect;
        }

        public string AddSelectDistinct(IColumn sColumn)
        {
            sSelect = AddString(sSelect, string.Format("DISTINCT({0})", SetQualified ? sColumn.QualifyName : sColumn.Name));
            return sSelect;
        }

        public string AddSelectAS(string sColumn, string sColumnAs)
        {
            sSelect = AddString(sSelect, AddString("'" + sColumn + "'", sColumnAs, "AS"));
            return sSelect;
        }

        public string AddSelectAS(string sColumn, IColumn sColumnAs)
        {
            sSelect = AddString(sSelect, AddString("'" + sColumn + "'", SetQualified ? sColumnAs.QualifyName : sColumnAs.Name, "AS"));
            return sSelect;
        }

        //TODO change for sqLite with no qualiify column name
        public string AddInnerQueryAS(string sColumn, IColumn sColumnAs)
        {
            sSelect = AddString(sSelect, AddString("(" + sColumn + ")", SetQualified ? sColumnAs.QualifyName : sColumnAs.Name, "AS"));
            return sSelect;
        }
        
        public string AddSelectAS(IColumn sColumn, string sColumnAs)
        {
            sSelect = AddString(sSelect, AddString(SetQualified ? sColumn.QualifyName : sColumn.Name, "'" + sColumnAs + "'", "AS"));
            return sSelect;
        }

        public string AddSelectAS(IColumn sColumn, IColumn sColumnAs)
        {
            sSelect = AddString(sSelect, AddString(SetQualified ? sColumn.QualifyName : sColumn.Name,
                                                    SetQualified ? sColumnAs.QualifyName : sColumnAs.Name, "AS"));
            return sSelect;
        }

        public string AddSelectAll(string sTable)
        {
            sSelect = AddString(sSelect, sTable + ".*");
            return sSelect;
        }

        #endregion

        #region AddFrom

        public string AddFrom(string sfrom)
        {
            sFrom = AddString(sFrom, sfrom);
            return sFrom;
        }

        public string AddFrom(Table table)
        {
            sFrom = AddString(sFrom, table.Tablename);
            return sFrom;
        }

        #endregion

        #region AddJoin

        public string AddJoin(JoinType join, IColumn column1, IColumn column2)
        {
            return AddJoin(join, column1, column2, "=");
        }

        public string AddJoin(JoinType join, IColumn column1, IColumn column2, string oper)
        {
            string sJoin = string.Empty;

            switch (join)
            {
                case JoinType.INNER:
                    sJoin = " INNER JOIN";
                    break;

                case JoinType.OUTER_LEFT:
                    sJoin = " LEFT OUTER JOIN";
                    break;

                case JoinType.OUTER_RIGHT:
                    sJoin = " RIGHT OUTER JOIN";
                    break;
            }
            sFrom += string.Format("{0} {1} ON {2} {3} {4}", sJoin, column1.Table,
                                                            SetQualified ? column1.QualifyName : column1.Name, oper,
                                                            SetQualified ? column2.QualifyName : column2.Name);

            return sFrom;
        }

        public string AddJoinAnd<T>(IColumn column1, T sValue)
        {
            return AddJoinAnd<T>(column1, sValue, "=");
        }

        public string AddJoinAnd<T>(IColumn column1, T sValue, string sOperator)
        {
            if (typeof(T) == typeof(System.Boolean))
            {
                bool bValue = (bool)Convert.ChangeType(sValue, typeof(bool));
                string sVal = bValue ? "1" : "0";
                return AddJoinAnd(SetQualified ? column1.QualifyName : column1.Name, sVal, sOperator);
            }
            else if (typeof(T).BaseType == typeof(System.Enum))
            {
                int nVal = (int)Convert.ChangeType(sValue, typeof(int));
                return AddJoinAnd(SetQualified ? column1.QualifyName : column1.Name, nVal.ToString(), sOperator);
            }
            else if (typeof(T) == typeof(System.DateTime))
            {
                DateTime dVal = (DateTime)Convert.ChangeType(sValue, typeof(DateTime));
                return AddJoinAnd(SetQualified ? column1.QualifyName : column1.Name, string.Format("'{0}{1:00}{2:00}'", dVal.Year, dVal.Month, dVal.Day), sOperator);
            }

            return AddJoinAnd(SetQualified ? column1.QualifyName : column1.Name, sValue.ToString(), sOperator); ;
        }

        public string AddColumnJoinAnd(IColumn column1, IColumn column2)
        {
            return AddJoinAnd(column1, column2, "=");
        }

        public string AddJoinAnd(IColumn column1, IColumn column2, string oper)
        {
            return AddJoinAnd(SetQualified ? column1.QualifyName : column1.Name,
                                SetQualified ? column2.QualifyName : column2.Name, oper);
        }

        public string AddJoinAnd(string column1, string column2, string oper)
        {
            sFrom += string.Format(" AND {0} {1} {2}", column1, oper, column2);
            return sFrom;
        }

        #endregion

        #region AddCompare

        public string AddCompare(IColumn sColumn1, SqlABParameter param)
        {
            if (hasExecuter && scc != null)
                scc.Parameters.Add(param);

            return AddCompare(sColumn1, param.ParameterName, "=");
        }

        public string AddCompare(IColumn sColumn1, SqlABParameter param, string sOperator)
        {
            if (hasExecuter && scc != null)
                scc.Parameters.Add(param);

            return AddCompare(sColumn1, param.ParameterName, sOperator);
        }

        public string AddCompareColumn(IColumn sColumn1, IColumn sColumn2)
        {
            return AddCompareColumn(sColumn1, sColumn2, "=");
        }

        public string AddCompareColumn(IColumn sColumn1, IColumn sColumn2, string sOperator)
        {
            return AddCompare(SetQualified ? sColumn1.QualifyName : sColumn1.Name,
                                SetQualified ? sColumn2.QualifyName : sColumn2.Name, sOperator);
        }

        public string AddCompare(IColumn sColumn1, IColumn sColumn2, string sOperator)
        {
            return AddCompare(SetQualified ? sColumn1.QualifyName : sColumn1.Name,
                                SetQualified ? sColumn2.QualifyName : sColumn2.Name, sOperator);
        }

        public string AddCompare(IColumn sColumn1, string sval)
        {
            return AddCompare(SetQualified ? sColumn1.QualifyName : sColumn1.Name, sval, "=");
        }

        public string AddCompare(IColumn sColumn1, string sColumn2, string sOperator)
        {
            return AddCompare(SetQualified ? sColumn1.QualifyName : sColumn1.Name, sColumn2, sOperator);
        }

        public string AddCompare<T>(IColumn sColumn, T sValue)
        {
            return AddCompare<T>(sColumn, sValue, "=");
        }

        public string AddCompare<T>(IColumn sColumn, T sValue, string sOperator)
        {
            if (typeof(T) == typeof(System.Boolean))
            {
                bool bValue = (bool)Convert.ChangeType(sValue, typeof(bool));
                string sVal = bValue ? "1" : "0";
                return AddCompare(sColumn.QualifyName, sVal, sOperator);
            }
            else if (typeof(T).BaseType == typeof(System.Enum))
            {
                int nVal = (int)Convert.ChangeType(sValue, typeof(int));
                return AddCompare(sColumn.QualifyName, nVal.ToString(), sOperator);
            }
            else if (typeof(T) == typeof(System.DateTime))
            {
                DateTime dVal = (DateTime)Convert.ChangeType(sValue, typeof(DateTime));
                return AddCompare(sColumn.QualifyName, string.Format("'{0}{1:00}{2:00}'", dVal.Year, dVal.Month, dVal.Day), sOperator);
            }

            return AddCompare(sColumn.QualifyName, string.Concat("'", sValue.ToString(), "'"), sOperator);
        }

        public string AddCompare(string sColumn, string sValue)
        {
            return AddCompare(sColumn, string.Concat("'", sValue, "'"), "=");
        }

        public string AddCompare(string sColumn, string sValue, string sOperator)
        {
            string where = sColumn + " " + sOperator + " " + sValue;
            sWhere = AddString(sWhere, where, AND_OR);
            return where;
        }

        public string AddCompareIsNull(IColumn sColumn)
        {
            return AddCompare(sColumn.Name, "NULL", "IS");
        }

        public string AddCompareIsNull(IColumn sColumn, string isNot)
        {
            return AddCompare(sColumn.Name, "NULL", isNot);
        }

        #endregion

        #region AddBetween

        public string AddBetween(IColumn sColumn1, SqlABParameter bFromParam, SqlABParameter bToParam)
        {
            if (hasExecuter && scc != null)
            {
                scc.Parameters.Add(bFromParam);
                scc.Parameters.Add(bToParam);
            }
            return AddBetween(SetQualified ? sColumn1.QualifyName : sColumn1.Name, bFromParam.ParameterName, bToParam.ParameterName);
        }

        public string AddBetween<T>(IColumn sColumn, T sFrom, T sTo)
        {
            if (typeof(T) == typeof(System.Boolean))
            {
                string bFrom = ((bool)Convert.ChangeType(sFrom, typeof(bool))) ? "1" : "0";
                string bTo = ((bool)Convert.ChangeType(sTo, typeof(bool))) ? "1" : "0";
                return AddCompare(sColumn.QualifyName,
                                    string.Concat("'", bFrom, "'"),
                                    string.Concat("'", bTo, "'"));
            }

            return AddBetween(SetQualified ? sColumn.QualifyName : sColumn.Name,
                                    string.Concat("'", sFrom.ToString(), "'"),
                                    string.Concat("'", sTo.ToString(), "'"));
        }

        public string AddBetween(string sColumn, string sFrom, string sTo)
        {
            string where = sColumn + " BETWEEN " + sFrom + " AND " + sTo;
            if (sWhere.LastIndexOf("( ") == sWhere.Length - 2)
                sWhere = AddString(sWhere, where, "");
            else
                sWhere = AddString(sWhere, where, AND_OR);
            return where;
        }

        #endregion

        #region AddLike

        public string AddLike(IColumn sColumn1, SqlABParameter param)
        {
            if (hasExecuter && scc != null)
                scc.Parameters.Add(param);

            return AddLike(SetQualified ? sColumn1.QualifyName : sColumn1.Name, param.ParameterName);
        }

        public string AddLike(IColumn sColumn1, IColumn sColumn2)
        {
            return AddLike(SetQualified ? sColumn1.QualifyName : sColumn1.Name,
                            SetQualified ? sColumn2.QualifyName : sColumn2.Name);
        }

        public string AddLike(IColumn sColumn1, string sValue)
        {
            return AddLike(SetQualified ? sColumn1.QualifyName : sColumn1.Name,
                            string.Concat("'", sValue, "'"));
        }

        public string AddLike(string sColumn, string sValue)
        {
            string where = sColumn + " LIKE " + sValue;
            sWhere = AddString(sWhere, where, AND_OR);
            return where;
        }

        #endregion

        #region AddOrder

        public string AddOrder(string sColumnName)
        {
            sOrder = AddString(sOrder, sColumnName);
            return sFrom;
        }

        public string AddOrder(IColumn columnName)
        {
            sOrder = AddString(sOrder, SetQualified ? columnName.QualifyName : columnName.Name);
            return sFrom;
        }

        public string AddOrder(string sColumnName, bool desc)
        {
            sOrder = AddString(sOrder, sColumnName);
            if (desc)
                sOrder += " DESC";
            return sFrom;
        }

        public string AddOrder(IColumn columnName, bool desc)
        {
            sOrder = AddString(sOrder, SetQualified ? columnName.QualifyName : columnName.Name);
            if (desc)
                sOrder += " DESC";
            return sFrom;
        }

        #endregion

        #region AddGroup

        public string AddGroup(string sgroup)
        {
            sGroup = AddString(sGroup, sgroup);
            return sGroup;
        }

        public string AddGroup(IColumn sgroup)
        {
            sGroup = AddString(sGroup, SetQualified ? sgroup.QualifyName : sgroup.Name);
            return sGroup;
        }

        #endregion

        #region AddHaving

        public string AddHaving(IColumn sColumn1, IColumn sColumn2)
        {
            return AddHaving(SetQualified ? sColumn1.QualifyName : sColumn1.Name,
                                SetQualified ? sColumn2.QualifyName : sColumn2.Name, "=");
        }

        public string AddHaving(IColumn sColumn1, IColumn sColumn2, string sOperator)
        {
            return AddHaving(SetQualified ? sColumn1.QualifyName : sColumn1.Name,
                                SetQualified ? sColumn2.QualifyName : sColumn2.Name, sOperator);
        }

        public string AddHaving(IColumn sColumn, string sValue)
        {
            return AddHaving(SetQualified ? sColumn.QualifyName : sColumn.Name, sValue, "=");
        }

        public string AddHaving(string sColumn, string sValue)
        {
            return AddHaving(sColumn, sValue, "=");
        }

        public string AddHaving(string sColumn, string sValue, string sOperator)
        {
            string having = sColumn + " " + sOperator + " " + sValue;
            sHaving = AddString(sHaving, having, AND_OR);
            return having;
        }

        #endregion

        #region private methods

        public string AddString(string source, string token)
        {
            if (source != string.Empty) source += ", ";
            source += token;

            return source;
        }

        public string AddString(string source, string token, string divid)
        {
            if (source != string.Empty) source += " " + divid + " ";
            source += token;

            return source;
        }

        #endregion
    }

    #endregion
}