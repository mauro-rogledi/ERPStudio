using System;

namespace ERPFramework.Data
{
    #region AM_Version

    public class AM_Version : Table
    {
        public static string Name = "AM_Version";

        public static Column<AM_Version,string> Application = new Column<AM_Version,string>("Application", 32, "") { EnableNull = false };
        public static Column<AM_Version,string> Module = new Column<AM_Version,string>("Module", 32, "") { EnableNull = false };
        public static Column<AM_Version,int> Version = new Column<AM_Version,int>("Version", description: "");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { Application, Module }; } }

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
        public static string Name = "AM_Users";

        public static Column<AM_Users,string> Username     = new Column<AM_Users,string>("Username", 32, "") { EnableNull = false };
        public static Column<AM_Users,string> Password     = new Column<AM_Users,string>("Password", 64, "");
        public static Column<AM_Users,string> Surname      = new Column<AM_Users, string>("Surname", 32, "Surname");
        public static Column<AM_Users,UserType> UserType   = new Column<AM_Users,UserType>("UserType");
        public static Column<AM_Users,bool> Expired        = new Column<AM_Users,bool>("Expired", description: "Expired");
        public static Column<AM_Users,DateTime> ExpireDate = new Column<AM_Users,DateTime>("ExpDate", description: "ExpDate");
        public static Column<AM_Users,bool> ChangePassword = new Column<AM_Users,bool>("ChangePassword");
        public static Column<AM_Users,bool> Blocked        = new Column<AM_Users,bool>("Blocked");

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
        public static string Name = "AM_Descriptions";

        public static Column<AM_Descriptions,Int32> ID           = new Column<AM_Descriptions, Int32>("ID") { EnableNull = false, AutoIncrement = true };
        public static Column<AM_Descriptions,string> Category    = new Column<AM_Descriptions, string>("Category", 35, "Category");
        public static Column<AM_Descriptions,string> Description = new Column<AM_Descriptions, string>("Description", 35, "Description");

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
        public static string Name = "AM_Counter";

        public static Column<AM_Counter,int> Year           = new Column<AM_Counter,int>("Year") { EnableNull = false };
        public static Column<AM_Counter,int> Type           = new Column<AM_Counter,int>("Type") { EnableNull = false };
        public static Column<AM_Counter,string> Description = new Column<AM_Counter,string>("Description", 35, "Description");
        public static Column<AM_Counter,bool> HasPrefix     = new Column<AM_Counter,bool>("HasPrefix");
        public static Column<AM_Counter,bool> PrefixRO      = new Column<AM_Counter,bool>("PrefixRO");
        public static Column<AM_Counter,string> PrefixValue = new Column<AM_Counter,string>("PrefixValue", 4);
        public static Column<AM_Counter,int> PrefixType     = new Column<AM_Counter,int>("PrefixType");
        public static Column<AM_Counter,string> PrefixSep   = new Column<AM_Counter,string>("PrefixSep", 1);

        public static Column<AM_Counter,bool> HasSuffix     = new Column<AM_Counter,bool>("HasSuffix");
        public static Column<AM_Counter,bool> SuffixRO      = new Column<AM_Counter,bool>("SuffixRO");
        public static Column<AM_Counter,string> SuffixValue = new Column<AM_Counter,string>("SuffixValue", 4);
        public static Column<AM_Counter,int> SuffixType     = new Column<AM_Counter,int>("SuffixType");
        public static Column<AM_Counter,string> SuffixSep   = new Column<AM_Counter,string>("SuffixSep", 1);

        public static Column<AM_Counter,int> CodeLen        = new Column<AM_Counter,int>("CodeLen");
        public static Column<AM_Counter,int> CodeKey        = new Column<AM_Counter,int>("CodeKey");


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
        public static string Name = "AM_CounterValue";

        public static Column<AM_CounterValue,int> Type           = new Column<AM_CounterValue,int>("Type") { EnableNull = false };
        public static Column<AM_CounterValue,string> Code        = new Column<AM_CounterValue,string>("Code", 15) { EnableNull = false };
        public static Column<AM_CounterValue,string> Description = new Column<AM_CounterValue,string>("Description", 35);
        public static Column<AM_CounterValue,int> NumericValue   = new Column<AM_CounterValue,int>("NumericValue");

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
        public static string Name = "AM_Codes";
        public static Column<AM_Codes,string> CodeType    = new Column<AM_Codes,string>("CodeType", 8, "CodeType") { EnableNull = false };
        public static Column<AM_Codes,string> Description = new Column<AM_Codes,string>("Description", 35, "Description");

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
        public static string Name = "AM_CodeSegment";
        public static Column<AM_CodeSegment,string> CodeType    = new Column<AM_CodeSegment, string>("Type", 8) { EnableNull = false };
        public static Column<AM_CodeSegment,int> Segment        = new Column<AM_CodeSegment, int>("Segment") { EnableNull = false };
        public static Column<AM_CodeSegment,string> Description = new Column<AM_CodeSegment, string>("Description", 35);
        public static Column<AM_CodeSegment,int> InputType      = new Column<AM_CodeSegment, int>("InputType");
        public static Column<AM_CodeSegment,int> InputLen       = new Column<AM_CodeSegment, int>("InputLen");

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
        public static string Name = "AM_Preferences";
        public static Column<AM_Preferences, string> PrefType    = new Column<AM_Preferences,string>("PrefType", 32, "PrefType") { EnableNull = false };
        public static Column<AM_Preferences, string> Computer    = new Column<AM_Preferences,string>("Computer", 32, "Computer") { EnableNull = false };
        public static Column<AM_Preferences, string> Username    = new Column<AM_Preferences,string>("Username", 32, "Username") { EnableNull = false };
        public static Column<AM_Preferences, string> Application = new Column<AM_Preferences,string>("ApplicationName", 32, "ApplicationName") { EnableNull = false };
        public static Column<AM_Preferences, string> Preferences = new Column<AM_Preferences,string>("Preferences", 2048, "Preferences");

        public override IColumn[] PrimaryKey { get { return new IColumn[] { PrefType, Computer, Username, Application }; } }

        public new static IColumn ForeignKey = Application;

        public AM_Preferences() : base()
        {
        }
    }

    #endregion
}