using System;

namespace ERPFramework.Data
{
    #region AM_Version

    public class AM_Version : Table
    {
        public static string Name = nameof(AM_Version);

        public static Column<string> Module = new Column<string>("Module", 32, "") { EnableNull = false };
        public static Column<int> Version = new Column<int>("Version", description: "");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { Module }; } }

        public new static IColumn ForeignKey = Module;

        public AM_Version()
        {
            VisibleInRadar(new IColumn[] { Module, Version });
        }
    }

    #endregion

    #region AM_Users

    public class AM_Users : Table
    {
        public static string Name = nameof(AM_Users);
        public static bool IsVirtual = true;

        public static Column<string> Username = new Column<string>("Username", 32, "") { EnableNull = false };
        public static Column<string> Password = new Column<string>("Password", 64, "");
        public static Column<string> Surname = new Column<string>("Surname", 32, "Surname");
        public static Column<UserType> UserType = new Column<UserType>("UserType");
        public static Column<bool> Expired = new Column<bool>("Expired", description: "Expired");
        public static Column<DateTime> ExpireDate = new Column<DateTime>("ExpDate", description: "ExpDate");
        public static Column<bool> ChangePassword = new Column<bool>("ChangePassword");
        public static Column<bool> Blocked = new Column<bool>("Blocked");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { Username }; } }

        public new static IColumn ForeignKey = Username;

        public AM_Users()
        {
            VisibleInRadar(new IColumn[] { Username, Surname });

        }
    }

    #endregion

    #region AM_Descriptions

    public class AM_Descriptions : Table
    {
        public static string Name = nameof(AM_Descriptions);

        public static Column<Int32> ID = new Column<Int32>("ID") { EnableNull = false, AutoIncrement = true };
        public static Column<string> Category = new Column<string>("Category", 35, "Category");
        public static Column<string> Description = new Column<string>("Description", 35, "Description");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { ID }; } }

        public new static IColumn ForeignKey = ID;

        public AM_Descriptions()
        {
            VisibleInRadar(new IColumn[] { ID, Description });
            Tablename = Name;
        }
    }

    #endregion

    #region AM_Counter

    public class AM_Counter : Table
    {
        public static string Name = nameof(AM_Counter);

        public static Column<int> Year = new Column<int>("Year") { EnableNull = false };
        public static Column<int> Type = new Column<int>("Type") { EnableNull = false };
        public static Column<string> Description = new Column<string>("Description", 35, "Description");
        public static Column<bool> HasPrefix = new Column<bool>("HasPrefix");
        public static Column<bool> PrefixRO = new Column<bool>("PrefixRO");
        public static Column<string> PrefixValue = new Column<string>("PrefixValue", 4);
        public static Column<int> PrefixType = new Column<int>("PrefixType");
        public static Column<string> PrefixSep = new Column<string>("PrefixSep", 1);

        public static Column<bool> HasSuffix = new Column<bool>("HasSuffix");
        public static Column<bool> SuffixRO = new Column<bool>("SuffixRO");
        public static Column<string> SuffixValue = new Column<string>("SuffixValue", 4);
        public static Column<int> SuffixType = new Column<int>("SuffixType");
        public static Column<string> SuffixSep = new Column<string>("SuffixSep", 1);

        public static Column<int> CodeLen = new Column<int>("CodeLen");
        public static Column<int> CodeKey = new Column<int>("CodeKey");


        public override IColumn[] PrimaryKey { get { return new IColumn[] { Year, Type }; } }

        public new static IColumn ForeignKey = Year;

        public AM_Counter()
        {
            VisibleInRadar(new IColumn[] { Year, Type, Description });
            Tablename = Name;
        }
    }

    #endregion

    #region AM_CounterValue

    public class AM_CounterValue : Table
    {
        public static string Name = nameof(AM_CounterValue);

        public static Column<int> Type = new Column<int>("Type") { EnableNull = false };
        public static Column<string> Code = new Column<string>("Code", 15) { EnableNull = false };
        public static Column<string> Description = new Column<string>("Description", 35);
        public static Column<int> NumericValue = new Column<int>("NumericValue");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { Type, Code }; } }

        public new static IColumn ForeignKey = Code;

        public AM_CounterValue()
        {
            Tablename = Name;
        }
    }

    #endregion

    #region AM_Codes

    public class AM_Codes : Table
    {
        public static string Name = nameof(AM_Codes);
        public static Column<string> CodeType = new Column<string>("CodeType", 8, "CodeType") { EnableNull = false };
        public static Column<string> Description = new Column<string>("Description", 35, "Description");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { CodeType }; } }
        public new static IColumn ForeignKey = CodeType;

        public AM_Codes()
        {
            VisibleInRadar(new IColumn[] { CodeType, Description });
            Tablename = Name;
        }
    }

    #endregion

    #region AM_CodeSegment

    public class AM_CodeSegment : Table
    {
        public static string Name = nameof(AM_CodeSegment);
        public static Column<string> CodeType = new Column<string>("Type", 8) { EnableNull = false };
        public static Column<int> Segment = new Column<int>("Segment") { EnableNull = false };
        public static Column<string> Description = new Column<string>("Description", 35);
        public static Column<int> InputType = new Column<int>("InputType");
        public static Column<int> InputLen = new Column<int>("InputLen");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { CodeType, Segment }; } }

        public new static IColumn ForeignKey = CodeType;

        public AM_CodeSegment()
        {
        }
    }

    #endregion

    #region AM_Preferences

    public class AM_Preferences : Table
    {
        public static string Name = nameof(AM_Preferences);
        public static Column<string> PrefType = new Column<string>("PrefType", 32, "PrefType") { EnableNull = false };
        public static Column<string> Computer = new Column<string>("Computer", 32, "Computer") { EnableNull = false };
        public static Column<string> Username = new Column<string>("Username", 32, "Username") { EnableNull = false };
        public static Column<string> Application = new Column<string>("ApplicationName", 32, "ApplicationName") { EnableNull = false };
        public static Column<string> Preferences = new Column<string>("Preferences", 2048, "Preferences");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { PrefType, Computer, Username, Application }; } }

        public new static IColumn ForeignKey = Application;

        public AM_Preferences()
        {
        }
    }

    #endregion
}