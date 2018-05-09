using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ERPFramework.Data
{
    public abstract class Table
    {
        public string Tablename = string.Empty;
        private readonly List<IColumn> visibleInRadar = new List<IColumn>();
        public abstract IColumn[] PrimaryKey { get; }

        public readonly IColumn ForeignKey;

        public override string ToString()
        {
            return Tablename;
        }

        public void VisibleInRadar(params IColumn[] col)
        {
            foreach (IColumn cl in col)
                visibleInRadar.Add(cl);
        }

        public int VisibleInRadarCount { get { return visibleInRadar.Count; } }


        public IColumn VisibleInRadarColumn(int index)
        {
                return visibleInRadar[index];
        }
    }

    public static class SqlCreateTable
    {
        private static string TableName = string.Empty;
        private static bool columnAdded;
        private static bool altermode;
        private static bool firstTime = true;
        private static readonly System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

        public static void DropTable<T>()
        {
            sbuilder.Clear();
            TableName = typeof(T).GetField("Name").GetValue(null).ToString();

            sbuilder.Append($"DROP TABLE {TableName}");
            ExecuteCommand();
        }

        public static void AlterTable<T>()
        {
            sbuilder.Clear();

            TableName = typeof(T).GetField("Name").GetValue(null).ToString();
            sbuilder.Append($"ALTER TABLE {TableName}");
            altermode = true;
            firstTime = true;
        }

        public static void DropColumn(IColumn columnName)
        {
            sbuilder.Clear();

            System.Diagnostics.Debug.Assert(altermode, "DropColumn ", "Manca il nome della tabella");
            var constraint = GetConstraint(columnName);
            sbuilder.Append(firstTime ? " DROP " : ", ");

            if (!constraint.Equals(string.Empty))
                sbuilder.Append($" CONSTRAINT {constraint},");

            sbuilder.Append($" COLUMN {columnName.Name}");
            firstTime = false;
        }

        public static bool CreateTable<T>()
        {
            var tablename = typeof(T).GetField("Name").GetValue(null).ToString();
            NewTable(tablename);
            foreach (MemberInfo mi in typeof(T).GetMembers())
                if (mi.MemberType == MemberTypes.Field)
                {
                    if (((FieldInfo)mi).FieldType.GetInterface(nameof(IColumn)) == typeof(IColumn))
                    {
                        var ob = ((FieldInfo)mi).GetValue((mi));
                        System.Diagnostics.Debug.WriteLine(ob is IColumn);
                        var col = (IColumn)((FieldInfo)mi).GetValue((mi));
                        if (!col.IsVirtual)
                        {
                            AddColumn(col);
                            columnAdded = true;
                        }
                    }
                }

            var table = Activator.CreateInstance<T>() as Table;
            AddPrimaryKey(table.PrimaryKey);
            return Create();
        }

        private static void NewTable(string tableName)
        {
            sbuilder.Clear();
            sbuilder.Append($"CREATE TABLE {tableName} (");
            TableName = tableName.ToString();
            columnAdded = false;
        }

        private static string GetConstraint(IColumn columnName)
        {
#if(SQLCompact)
            if (GlobalInfo.SqlConnection.ProviderType == ProviderType.SQLCompact)
                return "";
#endif

            const string command = "select object_name(cdefault) from syscolumns where [id] = object_id(@tn) and [name] like @cn ";
            using (var cmd = new SqlProxyCommand(command, GlobalInfo.DBaseInfo.dbManager.DB_Connection))
            {
                var tn = new SqlProxyParameter("@tn", DbType.String, 64);
                var cn = new SqlProxyParameter("@cn", DbType.String, 64);
                cmd.Parameters.Add(tn);
                cmd.Parameters.Add(cn);

                tn.Value = "dbo." + columnName.Table;
                cn.Value = columnName.Name;

                var constraint = string.Empty;
                try
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        constraint = result.ToString();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }

                return constraint;
            }
        }

        public static void AlterColumn(IColumn column)
        {
            var constraint = GetConstraint(column);
#if(SQLServer)

            //CreateString += GlobalInfo.SqlConnection.ProviderType == ProviderType.SQLServer
            //                    ? (constraint != ""
            //                            ? " DROP CONSTRAINT " + constraint
            //                            : "")
            //                    : string.Format(" ALTER COLUMN {0} DROP DEFAULT", column.Name);
#else
            sbuilder.Append($" ALTER COLUMN {column.Name} DROP DEFAULT");
#endif

            firstTime = false;
            altermode = true;
            sbuilder.Append(" ALTER COLUMN ");

            AddColumn(column);
        }

        public static void AddColumn(IColumn column)
        {
            AddColumn(column.Name, column.ColType, column.Len, column.Dec, column.EnableNull, column.DefaultValue);
        }

        private static void AddColumn(string columnName, System.Type datatype, int len, int dec, bool EnableNull, object defaultValue)
        {
            var vartype = string.Empty;

            vartype = ConvertColumnType.TypeAsString(datatype);

            if (altermode)
            {
                if (firstTime) sbuilder.Append(" ADD");
                firstTime = false;
            }
            else
                sbuilder.Append(sbuilder.ToString().EndsWith("(") ? "" : ", ");

            if (vartype == "int" || vartype == "bit" || vartype == "datetime" || vartype == "text")
                sbuilder.Append($" [{columnName}] {vartype}");
            else
                if (vartype == "decimal")
                sbuilder.Append($" [{columnName}] {vartype}({len},{dec})");
            else
                sbuilder.Append($" [{columnName}] {vartype}({len})");

            sbuilder.Append(EnableNull || altermode ? " NULL" : " NOT NULL");

            if (EnableNull)
            {
                if (vartype == "decimal" || vartype == "int" || vartype == "tinyint" || vartype == "bit")
                    sbuilder.Append($" DEFAULT {defaultValue}");
                else
                    sbuilder.Append($" DEFAULT '{defaultValue}'");
            }
        }

        public static void AddPrimaryKey(params IColumn[] keys)
        {
            System.Diagnostics.Debug.Assert(columnAdded, "CreateTable ", "Aggiunta chiave prima di colonna");

            for (int i = 0; i < keys.Length; i++)
                sbuilder.Append((i == 0) ?
                    $", PRIMARY KEY ([{keys[i].Name}]" :
                    $",[{keys[i].Name}]");

            sbuilder.Append(")");
        }

        public static void AddIndexes(string keyname, params IColumn[] keys)
        {
            System.Diagnostics.Debug.Assert(columnAdded, "CreateTable ", "Aggiunta chiave prima di colonna");

            for (int i = 0; i < keys.Length; i++)
                sbuilder.Append((i == 0)
                    ? $", KEY {keyname}({keys[i].Name}"
                    : $",{keys[i].Name}");

            sbuilder.Append(")");
        }

        public static bool CreateIndex<T>(string indexname, bool unique, params object[] parameters)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));

            var tablename = typeof(T).GetField("Name").GetValue(null).ToString();

            var concat = "";

            foreach (object param in parameters)
            {
                if (param is IColumn)
                {
                    var col = (param as IColumn).ColumnName();
                    concat = concat.SeparConcat(col, ", ");
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

            sbuilder.Clear();
            var create = unique
                                ? "CREATE UNIQUE INDEX"
                                : "CREATE INDEX";
            sbuilder.Append($"{create}[{indexname}] ON [{tablename}] ({concat})");
            return ExecuteCommand();
        }

        private static bool Create()
        {
            System.Diagnostics.Debug.Assert(columnAdded, "CreateTable ", "Creazione di tabella prima di definizione");
            sbuilder.Append(")");

            columnAdded = false;
            return ExecuteCommand();
        }

        public static bool Alter()
        {
            altermode = false;
            return ExecuteCommand();
        }

        private static bool ExecuteCommand()
        {
            try
            {
                using (SqlProxyCommand MyCommand = new SqlProxyCommand(sbuilder.ToString(), GlobalInfo.DBaseInfo.dbManager.DB_Connection))
                {
                    MyCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.ToString(), sbuilder.ToString());
                return false;
            }
        }
    }

    public static class AddVirtualColumn
    {
        public static void AddColumn(DataTable table, IColumn col)
        {
            table.Columns.Add(col.Name, col.ColType);
        }

        public static void AddColumns<T>(DataTable table)
        {
            foreach (MemberInfo mi in typeof(T).GetMembers())
                if (mi.MemberType == MemberTypes.Field)
                {
                    if (((FieldInfo)mi).FieldType.GetInterface(nameof(IColumn)) == typeof(IColumn))
                    {
                        var ob = ((FieldInfo)mi).GetValue((mi));
                        var col = (IColumn)((FieldInfo)mi).GetValue((mi));
                        if (col.IsVirtual)
                            AddColumn(table, col);
                    }
                }
        }
    }
}
