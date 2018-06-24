#region Using directives

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#endregion

namespace ERPFramework.Data
{
    #region ConvertColumnType

    public static class ConvertColumnType
    {
        public static string TypeAsString(Type typeIn)
        {
            var dictConver = new Dictionary<Type, string>
            {
                {typeof(String), "nvarchar" },
                {typeof(Int32), "int" },
                {typeof(Enum), "int" },
                {typeof(Decimal), "decimal" },
                {typeof(Double), "decimal" },
                {typeof(Boolean), "bit" },
                {typeof(Byte), "tinyint" },
                {typeof(DateTime), "datetime" }
            };

            var typeCheck = typeIn.BaseType == typeof(Enum)
                ? typeIn.BaseType
                : typeIn;

            if (dictConver.ContainsKey(typeCheck))
                return dictConver[typeCheck];

            throw new Exception($"CreateTable {typeIn.ToString()} Tipo colonna sconosciuto");
        }

        public static int LenOfType<T>()
        {
            var dictConver = new Dictionary<Type, int>
            {
                {typeof(Int32), 4 },
                {typeof(Enum), 4 },
                {typeof(Decimal), 9 }
            };

            if (dictConver.ContainsKey(typeof(T)))
                return dictConver[typeof(T)];

            return 0;
        }

        public static int DecOf<T>()
        {
            var typeOut = 0;
            var typeIn = typeof(T);
            if (typeIn == typeof(Decimal))
                typeOut = 2;

            return typeOut;
        }

        public static object DefaultValue<T>()
        {
            var dictConver = new Dictionary<Type, Object>
            {
                {typeof(String), "" },
                {typeof(Int32), 0 },
                {typeof(Enum), 0 },
                {typeof(Boolean), 0 },
                {typeof(Decimal), 0 },
                {typeof(Single), 0},
                {typeof(Double), 0},
                {typeof(DateTime), new DateTime().EmptyDate().ToString("dd/MM/yyyy")},
                {typeof(TimeSpan), 0}
            };

            var typeCheck = typeof(T).BaseType == typeof(Enum)
                ? typeof(T).BaseType
                : typeof(T);

            if (dictConver.ContainsKey(typeCheck))
                return dictConver[typeCheck];

            throw new Exception("Unknown Type");
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
                    tablename = TableType.GetField(nameof(Name)).GetValue(null).ToString();
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
}