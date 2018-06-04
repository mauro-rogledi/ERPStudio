using System.Collections.Generic;
using System.Windows.Forms;
using ERPFramework.Controls;

namespace ERPFramework.Data
{
    public class RadarCodesParam : RadarParameters
    {
        public RadarCodesParam(string code)
        {
            Params = new Dictionary<string, object>();
            Params.Add(code);
        }
    }

    public class RadarCounterParam : RadarParameters
    {
        public RadarCounterParam(int year, int type)
        {
            Params = new List<object>();
            Params.Add(year);
            Params.Add(type);
        }
    }

    #region RadarCodes

    public class RadarCodes : RadarForm
    {
        private QueryBuilder qb = new QueryBuilder();
        private SqlProxyParameter p1;

        public RadarCodes()
            : base()
        {
            rdrCodeColumn = EF_Codes.CodeType;
            rdrDescColumn = EF_Codes.Description;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationFramework.CounterManager.codesForm");
        }

        protected override bool DefineFindQuery(SqlProxyCommand sqlCmd)
        {
            p1 = new SqlProxyParameter("@p1", EF_Codes.CodeType);

            qb.Clear();
            qb.SelectAllFrom<EF_Codes>().
                Where(EF_Codes.CodeType).IsEqualTo(p1);


            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(p1);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters param)
        {
        }

        protected override void OnFound(SqlProxyDataReader sqlReader)
        {
            Description = sqlReader[EF_Codes.Description.Name].ToString();
        }

        protected override string DefineBrowseQuery(SqlProxyCommand sqlCmd, string findQuery)
        {
            qb.Clear();
            qb.SelectAllFrom<EF_Codes>().
                AddFilter(findQuery);

            return qb.Query;
        }

        protected override void PrepareBrowseQuery()
        {
        }

        public override IRadarParameters GetRadarParameters(string text)
        {
            return new RadarCodesParam(text);
        }

        protected override IRadarParameters PrepareRadarParameters(DataGridViewRow row)
        {
            return new RadarCodesParam(row.GetValue<string>(EF_Codes.CodeType));
        }

        public override string GetCodeFromParameters(IRadarParameters param)
        {
            return param.GetValue<string>(0);
        }
    }

    #endregion

    #region Radar Counter

    public class RadarCounter : RadarForm
    {
        private QueryBuilder qb = new QueryBuilder();
        private SqlProxyParameter p1;
        private SqlProxyParameter p2;

        public RadarCounter()
            : base()
        {
            rdrCodeColumn = EF_Counter.Year;
            rdrDescColumn = EF_Counter.Description;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationFramework.CounterManager.counterForm");
        }

        protected override bool DefineFindQuery(SqlProxyCommand sqlCmd)
        {
            p1 = new SqlProxyParameter("@p1", EF_Counter.Year);
            p2 = new SqlProxyParameter("@p2", EF_Counter.Type);

            qb.Clear();
            qb.SelectAllFrom<EF_Counter>().
                Where(EF_Counter.Year).IsEqualTo(p1).
                And(EF_Counter.Type).IsEqualTo(p2);


            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(p1);
            sqlCmd.Parameters.Add(p2);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters param)
        {
            p1.Value = param[0];
            p2.Value = param[1];
        }

        protected override void OnFound(SqlProxyDataReader sqlReader)
        {
            Description = sqlReader[EF_Counter.Description.Name].ToString();
        }

        protected override string DefineBrowseQuery(SqlProxyCommand sqlCmd, string findQuery)
        {
            qb.Clear();
            qb.SelectAllFrom<EF_Counter>().
                AddFilter(findQuery);

            return qb.Query;
        }

        protected override void PrepareBrowseQuery()
        {
        }

        protected override IRadarParameters PrepareRadarParameters(DataGridViewRow row)
        {
            return new RadarCounterParam(row.GetValue<int>(EF_Counter.Year),
                                            row.GetValue<int>(EF_Counter.Type));
        }

        public override IRadarParameters GetRadarParameters(string text)
        {
            return new RadarCounterParam(GlobalInfo.CurrentDate.Year, int.Parse(text));
        }

        public override string GetCodeFromParameters(IRadarParameters param)
        {
            return param.GetValue<string>(0);
        }
    }

    #endregion
}