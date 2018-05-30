using ERPFramework.Forms;
using System.Data;

namespace ERPFramework.Data
{
    #region DRCounter
    public class RRCounter : DataReaderUpdater<EF_Counter>
    {
        private int year;
        private int type;
        private SqlProxyParameter sqlP1, sqlP2;

        public RRCounter(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(int year, int type)
        {
            this.year = year;
            this.type = type;
            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_Counter.Year);
            sqlP2 = AddParameters("@p51", EF_Counter.Type);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_Counter>().
                Where(EF_Counter.Year).IsLessEqualThan(sqlP1).
                And(EF_Counter.Type).IsEqualTo(sqlP2).
                OrderBy(EF_Counter.Year, true).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = year;
            sqlP2.Value = type;
        }
    }
    #endregion

    #region DUCounterValue
    public class DUCounterValue : DataReaderUpdater<EF_CounterValue>
    {
        private SqlProxyParameter sqlP1, sqlP2;
        private int type;
        private string code;

        public DUCounterValue(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(int type, string code)
        {
            this.type = type;
            this.code = code;
            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_CounterValue.Type);
            sqlP2 = AddParameters("@p51", EF_CounterValue.Code);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_CounterValue>().
                Where(EF_CounterValue.Type).IsEqualTo(sqlP1).
                And(EF_CounterValue.Code).IsEqualTo(sqlP2).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = type;
            sqlP2.Value = code;
        }

        public override DataRow AddRecord()
        {
            var dr = base.AddRecord();
            dr[EF_CounterValue.Type.Name] = sqlP1.Value;
            dr[EF_CounterValue.Code.Name] = sqlP2.Value;

            return dr;
        }
    }
    #endregion

    #region DRCodes
    public class DUCodes : DataReaderUpdater<EF_CodeSegment>
    {
        private SqlProxyParameter sqlP1;
        private string codetype;

        public DUCodes(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_CodeSegment.CodeType);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_CodeSegment>().
                Where(EF_CodeSegment.CodeType).IsEqualTo(sqlP1).
                OrderBy(EF_CodeSegment.Segment).
                Query;
        }


        public bool Find(string codeType)
        {
            codetype = codeType;
            return base.Find();
        }

        protected override void SetParameters()
        {
            sqlP1.Value = codetype;
        }
    }
    #endregion

    #region DRPreference
    public class DUPreference : DataReaderUpdater<EF_Preferences>
    {
        private SqlProxyParameter sqlP1, sqlP2, sqlP3, sqlP4;
        private string preftype;
        private string computer;
        private string username;
        private string application;

        public DUPreference(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(string PrefType, string Computer, string Username, string Application)
        {
            preftype = PrefType;
            computer = Computer;
            username = Username;
            application = Application;

            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_Preferences.PrefType);
            sqlP2 = AddParameters("@p51", EF_Preferences.Computer);
            sqlP3 = AddParameters("@p52", EF_Preferences.Username);
            sqlP4 = AddParameters("@p53", EF_Preferences.Application);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_Preferences>().
                Where(EF_Preferences.PrefType).IsEqualTo(sqlP1).
                And(EF_Preferences.Computer).IsEqualTo(sqlP2).
                And(EF_Preferences.Username).IsEqualTo(sqlP3).
                And(EF_Preferences.Application).IsEqualTo(sqlP4)
                .Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = preftype;
            sqlP2.Value = computer;
            sqlP3.Value = username;
            sqlP4.Value = application;
        }

        public DataRow AddRecord(string pref)
        {
            var dr = base.AddRecord();
            dr[EF_Preferences.PrefType.Name] = preftype;
            dr[EF_Preferences.Computer.Name] = computer; ;
            dr[EF_Preferences.Username.Name] = username;
            dr[EF_Preferences.Application.Name] = application;
            dr[EF_Preferences.Preferences.Name] = pref;

            return dr;
        }

        public DataRow AddRecord(string preftype, string computername, string username, string application)
        {
            var dr = base.AddRecord();
            dr[EF_Preferences.PrefType.Name] = preftype;
            dr[EF_Preferences.Computer.Name] = computername;
            dr[EF_Preferences.Username.Name] = username;
            dr[EF_Preferences.Application.Name] = application;

            return dr;
        }
    }

    #endregion

    #region DRFindPreferences

    public class DRFindPreference : DataReaderUpdater<EF_Preferences>
    {
        private SqlProxyParameter sqlP1, sqlP2;
        private string preftype;
        private string application;

        public DRFindPreference(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(string preftype, string application)
        {
            this.preftype = preftype;
            this.application = application;

            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_Preferences.PrefType);
            sqlP2 = AddParameters("@p51", EF_Preferences.Application);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_Preferences>().
                Where(EF_Preferences.PrefType).IsEqualTo(sqlP1).
                And(EF_Preferences.Application).IsEqualTo(sqlP2).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = preftype;
            sqlP2.Value = application;
        }
    }
    #endregion

    #region DRReadAllPreference
    public class RRReadAllPreference : DataReaderUpdater<EF_Preferences>
    {
        private SqlProxyParameter sqlP1, sqlP2, sqlP3, sqlP4;
        private string preftype;
        private string computer;
        private string username;
        private string application;

        public RRReadAllPreference(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(string preftype, string computer, string username, string application)
        {
            this.preftype = preftype;
            this.computer = computer;
            this.username = username;
            this.application = application;

            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_Preferences.PrefType);
            sqlP2 = AddParameters("@p51", EF_Preferences.Computer);
            sqlP3 = AddParameters("@p52", EF_Preferences.Username);
            sqlP4 = AddParameters("@p53", EF_Preferences.Application);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_Preferences>().
                Where(EF_Preferences.PrefType).IsEqualTo(sqlP1).
                AndExpression().
                    OpenExpression(EF_Preferences.Computer).IsEqualTo(sqlP2).Or(EF_Preferences.Computer).IsEqualTo("").Or(EF_Preferences.Computer).IsNull().
                CloseExpression().
                AndExpression().
                    OpenExpression(EF_Preferences.Username).IsEqualTo(sqlP3).Or(EF_Preferences.Username).IsEqualTo("").Or(EF_Preferences.Username).IsNull().
                CloseExpression().
                AndExpression().
                    OpenExpression(EF_Preferences.Application).IsEqualTo(sqlP4).Or(EF_Preferences.Application).IsEqualTo("").Or(EF_Preferences.Application).IsNull().
                CloseExpression().
                OrderBy(EF_Preferences.Computer, true, EF_Preferences.Username, true, EF_Preferences.Application, true).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = preftype;
            sqlP2.Value = computer;
            sqlP3.Value = username;
            sqlP4.Value = application;
        }
    }
    #endregion

    #region DRUsers
    public class DRUsers : DataReaderUpdater<EF_Users>
    {
        private SqlProxyParameter sqlP1;
        private string username;

        public DRUsers(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(string userName)
        {
            username = userName;
            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_Users.Username);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_Users>().
                Where(EF_Users.Username).IsEqualTo(sqlP1).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = username;
        }
    }

    #endregion

    #region DRVersion
    public class DRVersion : DataReaderUpdater<EF_Version>
    {
        private SqlProxyParameter sqlP1;
        private string module;

        public DRVersion(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        public bool Find(string module)
        {
            this.module = module;
            return base.Find();
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", EF_Version.Module);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<EF_Version>().
                Where(EF_Version.Module).IsEqualTo(sqlP1).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = module;
        }
    }

    #endregion

}