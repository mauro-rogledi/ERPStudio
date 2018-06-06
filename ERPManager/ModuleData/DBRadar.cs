using ERPFramework;
using ERPFramework.Controls;
using ERPFramework.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPManager.ModuleData
{
    internal class RadarUsersParam : RadarParameters
    {
        public RadarUsersParam(string code)
        {
            Add(EF_Users.Username, code);
        }
    }

    #region Radar Users

    internal class RadarUsers : RadarForm
    {
        private QueryBuilder qb = new QueryBuilder();

        public RadarUsers()
            : base()
        {
            rdrCodeColumn = EF_Users.Username;
            rdrDescColumn = EF_Users.Surname;
        }

        protected override void PrepareFindParameters()
        {
            rdrParameters.Add(
                EF_Users.Username,
                new SqlProxyParameter("@p1", EF_Users.Username)
                );
        }

        protected override bool DefineFindQuery(SqlProxyCommand sqlCmd)
        {
            qb.Clear();
            qb.SelectAllFrom<EF_Users>().
                Where(EF_Users.Username).IsEqualTo(rdrParameters[EF_Users.Username]);

            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(rdrParameters);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters param)
        {
            rdrParameters[EF_Users.Username].Value = param[EF_Users.Username];
        }

        protected override void OnFound(SqlProxyDataReader sqlReader)
        {
            Description = sqlReader[EF_Users.Surname].ToString();
        }

        protected override string DefineBrowseQuery(SqlProxyCommand sqlCmd, string findQuery)
        {
            qb.Clear();
            qb.SelectAllFrom<EF_Users>().
                AddFilter(findQuery);

            return qb.Query;
        }

        protected override void PrepareBrowseQuery()
        {
        }

        protected override IRadarParameters PrepareRadarParameters(DataGridViewRow row)
        {
            return new RadarUsersParam(row.GetValue<string>(EF_Users.Username));
        }

        public override IRadarParameters GetRadarParameters(string text)
        {
            return new RadarUsersParam(text);
        }

        public override string GetCodeFromParameters(IRadarParameters param)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}