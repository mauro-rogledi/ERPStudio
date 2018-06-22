using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaProviders
{
    #region ConvertColumnType

    public static class ConvertColumnType
    {
        public static string TypeAsString(Type typeIn)
        {
            var typeOut = string.Empty;

            if (typeIn == typeof(string) || typeIn == typeof(String))
                typeOut = "nvarchar";
            else
                if (typeIn == typeof(Int32) || typeIn == typeof(int) || typeIn.BaseType == typeof(Enum))
                typeOut = "int";
            else
                    if (typeIn == typeof(bool) || typeIn == typeof(Boolean))
                typeOut = "bit";
            else
                        if (typeIn == typeof(Byte))
                typeOut = "tinyint";
            else
                            if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(Double))
                typeOut = "decimal";
            else
                                if (typeIn == typeof(DateTime))
                typeOut = "datetime";
            else
                Debug.Assert(false, "CreateTable " + typeIn.ToString(), "Tipo colonna sconosciuto");

            return typeOut;
        }

        public static int LenOfType<T>()
        {
            var typeOut = 0;
            var typeIn = typeof(T);

            if (typeIn == typeof(Int32) || typeIn == typeof(int) || typeIn.BaseType == typeof(Enum))
                typeOut = 4;
            else
                if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(double))
                typeOut = 9;

            return typeOut;
        }

        public static int DecOf<T>()
        {
            var typeOut = 0;
            var typeIn = typeof(T);
            if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(double))
                typeOut = 2;

            return typeOut;
        }

        public static object DefaultValue<T>()
        {
            object typeOut = null;
            var typeIn = typeof(T);
            if (typeIn == typeof(string) || typeIn == typeof(String))
                typeOut = "";
            else
                if (typeIn == typeof(Int32) || typeIn == typeof(int) || typeIn.BaseType == typeof(Enum))
                typeOut = 0;
            else
                    if (typeIn == typeof(bool) || typeIn == typeof(Boolean))
                typeOut = 0;
            else
                        if (typeIn == typeof(Byte))
                typeOut = 0;
            else
                            if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(Double))
                typeOut = 0.0;
            else
                if (typeIn == typeof(DateTime))
                typeOut = new DateTime().ToString("dd/MM/yyyy");
            else
                                    if (typeIn == typeof(TimeSpan))
                typeOut = 0;
            else
                Debug.Assert(false, "CreateTable " + typeIn.ToString(), "Tipo colonna sconosciuto");

            return typeOut;
        }

        public static DbType GetDBType(Type typein)
        {
            var dictConver = new Dictionary<Type, DbType>
            {
                {typeof(String), DbType.String},
                {typeof(Int32), DbType.Int32},
                {typeof(Byte), DbType.Byte},
                {typeof(Enum), DbType.UInt16},
                {typeof(Boolean), DbType.Boolean},
                {typeof(Decimal), DbType.Decimal},
                {typeof(Single), DbType.Single},
                {typeof(Double), DbType.Double},
                {typeof(DateTime), DbType.DateTime},
                {typeof(TimeSpan), DbType.DateTime}
            };

            var typeCheck = typein.BaseType == typeof(Enum)
                ? typein.BaseType
                : typein;

            if (dictConver.ContainsKey(typeCheck))
                return dictConver[typein];

            throw new Exception("Unknown Type");
        }

        public static SqlDbType SqlTypeOf(Type typeIn)
        {
            var typeOut = SqlDbType.VarChar;

            if (typeIn == typeof(string) || typeIn == typeof(String))
                typeOut = SqlDbType.VarChar;
            else
                if (typeIn == typeof(Int32) || typeIn == typeof(int) || typeIn.BaseType == typeof(Enum))
                typeOut = SqlDbType.Int;
            else
                    if (typeIn == typeof(bool) || typeIn == typeof(Boolean))
                typeOut = SqlDbType.Bit;
            else
                        if (typeIn == typeof(Byte))
                typeOut = SqlDbType.SmallInt;
            else
                            if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(Double))
                typeOut = SqlDbType.Decimal;
            else
                                if (typeIn == typeof(DateTime))
                typeOut = SqlDbType.DateTime;
            else
                                    if (typeIn == typeof(TimeSpan))
                typeOut = SqlDbType.DateTime;
            else
                Debug.Assert(false, "SqlTypeOf " + typeIn.ToString(), "Tipo colonna sconosciuto");

            return typeOut;
        }

        public static SqlDbType SqlTypeCEOf(Type typeIn)
        {
            var typeOut = SqlDbType.NVarChar;

            if (typeIn == typeof(string) || typeIn == typeof(String))
                typeOut = SqlDbType.NVarChar;
            else
                if (typeIn == typeof(Int32) || typeIn == typeof(int) || typeIn.BaseType == typeof(Enum))
                typeOut = SqlDbType.Int;
            else
                    if (typeIn == typeof(bool) || typeIn == typeof(Boolean))
                typeOut = SqlDbType.Bit;
            else
                        if (typeIn == typeof(Byte))
                typeOut = SqlDbType.SmallInt;
            else
                            if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(Double))
                typeOut = SqlDbType.Decimal;
            else
                                if (typeIn == typeof(DateTime))
                typeOut = SqlDbType.DateTime;
            else
                Debug.Assert(false, "SqlTypeCEOf " + typeIn.ToString(), "Tipo colonna sconosciuto");

            return typeOut;
        }

        public static DbType SqlTypeLITOf(Type typeIn)
        {
            var typeOut = DbType.String;

            if (typeIn == typeof(string) || typeIn == typeof(String))
                typeOut = DbType.String;
            else
                if (typeIn == typeof(Int32) || typeIn == typeof(int) || typeIn.BaseType == typeof(Enum))
                typeOut = DbType.Int32;
            else
                    if (typeIn == typeof(bool) || typeIn == typeof(Boolean))
                typeOut = DbType.Boolean;
            else
                        if (typeIn == typeof(Byte))
                typeOut = DbType.Byte;
            else
                            if (typeIn == typeof(Decimal) || typeIn == typeof(float) || typeIn == typeof(Double))
                typeOut = DbType.Decimal;
            else
                                if (typeIn == typeof(DateTime))
                typeOut = DbType.DateTime;
            else
                Debug.Assert(false, "SqlTypeLITOf " + typeIn.ToString(), "Tipo colonna sconosciuto");

            return typeOut;
        }
    }

    #endregion

    #region Column

    public interface IColumn
    {
        Table Table { get; }
        string Tablename { get; }
        Type TableType { get; set; }

        string Description { get; }

        string Name { get; }

        int Len { get; }

        int Dec { get; }

        object DefaultValue { get; }

        bool EnableNull { get; }

        bool VisibleInRadar { get; }

        Type ColType { get; }

        Type EnumType { get; }

        string QualifyName(Dictionary<string, string> qualifyMap = null);

        bool IsVirtual { get; }

        string ColumnName(bool qualified = false, Dictionary<string, string> qualifyMap = null);

        bool AlignRight { get; }
    }

    public class Column<F> : IColumn
    {
        public string Description { get; private set; }

        public string Name { get; private set; }

        public System.Type ColType { get { return typeof(F); } }

        public int Len { get; private set; }

        public int Dec { get; private set; }

        public object DefaultValue { get; private set; }

        public bool EnableNull { get; set; }

        public bool AutoIncrement { get; set; }

        public bool VisibleInRadar { get; set; } = false;

        public System.Type EnumType { get; private set; }

        public bool IsVirtual { get; set; } = false;

        public bool AlignRight { get; set; } = false;

        public string ColumnName(bool qualified = false, Dictionary<string, string> qualifyMap = null)
        {
            return qualified ? QualifyName(qualifyMap) : $"[{Name}]";
        }

        public string QualifyName(Dictionary<string, string> qualifyMap = null)
        {
            return qualifyMap == null || !qualifyMap.ContainsKey(Tablename)
                ? $"[{Tablename}].[{Name}]"
                : $"[{qualifyMap[Tablename]}].[{Name}]";
        }

        public static F GetValue(object val)
        {
            return (F)Convert.ChangeType(val, typeof(F));
        }

        public override string ToString()
        {
            return Name;
        }

        public Column(string name, string description)
            : this(name, null, description)
        { }

        public Column(string name, int? len = null, string description = "")
        {
            Description = description;
            Name = name;
            EnumType = typeof(F).BaseType == typeof(Enum) ? typeof(F) : typeof(System.DBNull);
            Len = len ?? ConvertColumnType.LenOfType<F>();
            Dec = ConvertColumnType.DecOf<F>();
            EnableNull = true;

            DefaultValue = ConvertColumnType.DefaultValue<F>();
        }

        public Type TableType { get; set; }

        private string tablename;
        public string Tablename
        {
            get
            {
                if (tablename == null)
                {
                    System.Diagnostics.Debug.Assert(TableType.BaseType == typeof(Table));
                    tablename = TableType.GetField("Name").GetValue(null).ToString();
                }
                return tablename;
            }
        }

        private Table table;
        public Table Table
        {
            get
            {
                if (table == null)
                    table = (Table)Activator.CreateInstance(TableType);

                return table;
            }
        }
    }

    #endregion

    #region Table
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

        private void AddColumn(DataTable dt, IColumn col)
        {
            dt.Columns.Add(col.Name, col.ColType);
        }
    }
    #endregion
}
