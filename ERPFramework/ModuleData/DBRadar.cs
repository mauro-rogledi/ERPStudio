using System.Collections.Generic;
using System.Windows.Forms;
using ERPFramework.Controls;
using ERPFramework.Data;

namespace ERPFramework.Data
{
    public class RadarCodesParam : RadarParameters
    {
        public RadarCodesParam(string code)
        {
            Params = new List<object>();
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
        private SqlABParameter p1;

        public RadarCodes()
            : base()
        {
            rdrCodeColumn = AM_Codes.CodeType;
            rdrDescColumn = AM_Codes.Description;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationFramework.CounterManager.codesForm");
        }

        protected override bool DefineFindQuery(SqlABCommand sqlCmd)
        {
            p1 = new SqlABParameter("@p1", AM_Codes.CodeType);

            qb.Clear();
            qb.SelectAllFrom<AM_Codes>().
                Where(AM_Codes.CodeType).IsEqualTo(p1);


            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(p1);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters param)
        {
        }

        protected override void OnFound(SqlABDataReader sqlReader)
        {
            Description = sqlReader[AM_Codes.Description.Name].ToString();
        }

        protected override string DefineBrowseQuery(SqlABCommand sqlCmd, string findQuery)
        {
            qb.Clear();
            qb.SelectAllFrom<AM_Codes>().
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
            return new RadarCodesParam(row.GetValue<string>(AM_Codes.CodeType));
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
        private SqlABParameter p1;
        private SqlABParameter p2;

        public RadarCounter()
            : base()
        {
            rdrCodeColumn = AM_Counter.Year;
            rdrDescColumn = AM_Counter.Description;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationFramework.CounterManager.counterForm");
        }

        protected override bool DefineFindQuery(SqlABCommand sqlCmd)
        {
            p1 = new SqlABParameter("@p1", AM_Counter.Year);
            p2 = new SqlABParameter("@p2", AM_Counter.Type);

            qb.Clear();
            qb.SelectAllFrom<AM_Counter>().
                Where(AM_Counter.Year).IsEqualTo(p1).
                And(AM_Counter.Type).IsEqualTo(p2);


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

        protected override void OnFound(SqlABDataReader sqlReader)
        {
            Description = sqlReader[AM_Counter.Description.Name].ToString();
        }

        protected override string DefineBrowseQuery(SqlABCommand sqlCmd, string findQuery)
        {
            qb.Clear();
            qb.SelectAllFrom<AM_Counter>().
                AddFilter(findQuery);

            return qb.Query;
        }

        protected override void PrepareBrowseQuery()
        {
        }

        protected override IRadarParameters PrepareRadarParameters(DataGridViewRow row)
        {
            return new RadarCounterParam(row.GetValue<int>(AM_Counter.Year),
                                            row.GetValue<int>(AM_Counter.Type));
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