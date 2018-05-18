using ERPFramework.Forms;
using System.Data;

namespace ERPFramework.Data
{
    #region DRCounter
    public class RRCounter : DataReaderUpdater<AM_Counter>
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
            sqlP1 = AddParameters("@p50", AM_Counter.Year);
            sqlP2 = AddParameters("@p51", AM_Counter.Type);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_Counter>().
                Where(AM_Counter.Year).IsLessEqualThan(sqlP1).
                And(AM_Counter.Type).IsEqualTo(sqlP2).
                OrderBy(AM_Counter.Year, true).
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
    public class DUCounterValue : DataReaderUpdater<AM_CounterValue>
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
            sqlP1 = AddParameters("@p50", AM_CounterValue.Type);
            sqlP2 = AddParameters("@p51", AM_CounterValue.Code);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_CounterValue>().
                Where(AM_CounterValue.Type).IsEqualTo(sqlP1).
                And(AM_CounterValue.Code).IsEqualTo(sqlP2).
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
            dr[AM_CounterValue.Type.Name] = sqlP1.Value;
            dr[AM_CounterValue.Code.Name] = sqlP2.Value;

            return dr;
        }
    }
    #endregion

    #region DRCodes
    public class DUCodes : DataReaderUpdater<AM_CodeSegment>
    {
        private SqlProxyParameter sqlP1;
        private string codetype;

        public DUCodes(IDocumentBase iDocumentBase = null)
            : base(iDocumentBase)
        {
        }

        protected override void AddParameters()
        {
            sqlP1 = AddParameters("@p50", AM_CodeSegment.CodeType);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_CodeSegment>().
                Where(AM_CodeSegment.CodeType).IsEqualTo(sqlP1).
                OrderBy(AM_CodeSegment.Segment).
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
    public class DUPreference : DataReaderUpdater<AM_Preferences>
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
            sqlP1 = AddParameters("@p50", AM_Preferences.PrefType);
            sqlP2 = AddParameters("@p51", AM_Preferences.Computer);
            sqlP3 = AddParameters("@p52", AM_Preferences.Username);
            sqlP4 = AddParameters("@p53", AM_Preferences.Application);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_Preferences>().
                Where(AM_Preferences.PrefType).IsEqualTo(sqlP1).
                And(AM_Preferences.Computer).IsEqualTo(sqlP2).
                And(AM_Preferences.Username).IsEqualTo(sqlP3).
                And(AM_Preferences.Application).IsEqualTo(sqlP4)
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
            dr[AM_Preferences.PrefType.Name] = preftype;
            dr[AM_Preferences.Computer.Name] = computer; ;
            dr[AM_Preferences.Username.Name] = username;
            dr[AM_Preferences.Application.Name] = application;
            dr[AM_Preferences.Preferences.Name] = pref;

            return dr;
        }

        public DataRow AddRecord(string preftype, string computername, string username, string application)
        {
            var dr = base.AddRecord();
            dr[AM_Preferences.PrefType.Name] = preftype;
            dr[AM_Preferences.Computer.Name] = computername;
            dr[AM_Preferences.Username.Name] = username;
            dr[AM_Preferences.Application.Name] = application;

            return dr;
        }
    }

    #endregion

    #region DRFindPreferences

    public class DRFindPreference : DataReaderUpdater<AM_Preferences>
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
            sqlP1 = AddParameters("@p50", AM_Preferences.PrefType);
            sqlP2 = AddParameters("@p51", AM_Preferences.Application);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_Preferences>().
                Where(AM_Preferences.PrefType).IsEqualTo(sqlP1).
                And(AM_Preferences.Application).IsEqualTo(sqlP2).
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
    public class RRReadAllPreference : DataReaderUpdater<AM_Preferences>
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
            sqlP1 = AddParameters("@p50", AM_Preferences.PrefType);
            sqlP2 = AddParameters("@p51", AM_Preferences.Computer);
            sqlP3 = AddParameters("@p52", AM_Preferences.Username);
            sqlP4 = AddParameters("@p53", AM_Preferences.Application);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_Preferences>().
                Where(AM_Preferences.PrefType).IsEqualTo(sqlP1).
                AndExpression().
                    OpenExpression(AM_Preferences.Computer).IsEqualTo(sqlP2).Or(AM_Preferences.Computer).IsEqualTo("").Or(AM_Preferences.Computer).IsNull().
                CloseExpression().
                AndExpression().
                    OpenExpression(AM_Preferences.Username).IsEqualTo(sqlP3).Or(AM_Preferences.Username).IsEqualTo("").Or(AM_Preferences.Username).IsNull().
                CloseExpression().
                AndExpression().
                    OpenExpression(AM_Preferences.Application).IsEqualTo(sqlP4).Or(AM_Preferences.Application).IsEqualTo("").Or(AM_Preferences.Application).IsNull().
                CloseExpression().
                OrderBy(AM_Preferences.Computer, true, AM_Preferences.Username, true, AM_Preferences.Application, true).
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
    public class DRUsers : DataReaderUpdater<AM_Users>
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
            sqlP1 = AddParameters("@p50", AM_Preferences.Username);
        }

        protected override string CreateQuery()
        {
            return new QueryBuilder().
                SelectAllFrom<AM_Users>().
                Where(AM_Users.Username).IsEqualTo(sqlP1).
                Query;
        }

        protected override void SetParameters()
        {
            sqlP1.Value = username;
        }
    }

    #endregion

}