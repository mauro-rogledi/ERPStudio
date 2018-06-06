using System.Collections.Generic;
using System.Windows.Forms;
using ERPFramework.Controls;

namespace ERPFramework.Data
{
    public class RadarCodesParam : RadarParameters
    {
        public RadarCodesParam(string code)
        {
            Add(EF_Codes.CodeType, code);
        }
    }

    public class RadarCounterParam : RadarParameters
    {
        public RadarCounterParam(int year, int type)
        {
            Add(EF_Counter.Year, year);
            Add(EF_Counter.Type, type);
        }
    }

    #region RadarCodes

    public class RadarCodes : RadarForm
    {
        private QueryBuilder qb = new QueryBuilder();

        public RadarCodes()
            : base()
        {
            rdrCodeColumn = EF_Codes.CodeType;
            rdrDescColumn = EF_Codes.Description;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationFramework.CounterManager.codesForm");
        }

        protected override void PrepareFindParameters()
        {
            rdrParameters.Add(
                EF_Codes.CodeType,
                new SqlProxyParameter("@p1", EF_Codes.CodeType)
                );
        }

        protected override bool DefineFindQuery(SqlProxyCommand sqlCmd)
        {
            qb.Clear();
            qb.SelectAllFrom<EF_Codes>().
                Where(EF_Codes.CodeType).IsEqualTo(rdrParameters[EF_Codes.CodeType]);


            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(rdrParameters[EF_Codes.CodeType]);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters parameter)
        {
        }

        protected override void OnFound(SqlProxyDataReader sqlReader)
        {
            Description = sqlReader.GetValue<string>(EF_Codes.Description);
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
            return param.GetValue<string>(EF_Codes.CodeType);
        }
    }

    #endregion

    #region Radar Counter

    public class RadarCounter : RadarForm
    {
        private QueryBuilder qb = new QueryBuilder();

        public RadarCounter()
            : base()
        {
            rdrCodeColumn = EF_Counter.Year;
            rdrDescColumn = EF_Counter.Description;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationFramework.CounterManager.counterForm");
        }

        protected override void PrepareFindParameters()
        {
            rdrParameters.Add(
                EF_Counter.Year,
                new SqlProxyParameter("@p1", EF_Counter.Year));

            rdrParameters.Add(
                EF_Counter.Type,
                new SqlProxyParameter("@p2", EF_Counter.Type));
        }

        protected override bool DefineFindQuery(SqlProxyCommand sqlCmd)
        {
            qb.Clear();
            qb.SelectAllFrom<EF_Counter>().
                Where(EF_Counter.Year).IsEqualTo(rdrParameters[EF_Counter.Year]).
                And(EF_Counter.Type).IsEqualTo(rdrParameters[EF_Counter.Type]);


            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(rdrParameters);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters param)
        {
            rdrParameters[EF_Counter.Year].Value = param[EF_Counter.Year];
            rdrParameters[EF_Counter.Type].Value = param[EF_Counter.Type];
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
            return param.GetValue<string>(EF_Counter.Year);
        }
    }

    #endregion
}