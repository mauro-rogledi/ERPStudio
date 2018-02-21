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
            Params = new List<object>();
            Params.Add(code);
        }
    }

    #region Radar Users

    internal class RadarUsers : RadarForm
    {
        private QueryBuilder qb = new QueryBuilder();
        private SqlABParameter p1;

        public RadarUsers()
            : base()
        {
            rdrCodeColumn = AM_Users.Username;
            rdrDescColumn = AM_Users.Surname;
            rdrNameSpace = new NameSpace("Plumber.Plumber.ApplicationManager.Forms.usersForm");
        }

        protected override bool DefineFindQuery(SqlABCommand sqlCmd)
        {
            p1 = new SqlABParameter("@p1", AM_Users.Username);

            qb.Clear();
            qb.SelectAllFrom<AM_Users>().
                Where(AM_Users.Username).IsEqualTo(p1);

            sqlCmd.CommandText = qb.Query;
            sqlCmd.Parameters.Add(p1);

            return true;
        }

        protected override void PrepareFindQuery(IRadarParameters param)
        {
            p1.Value = param[0];
        }

        protected override void OnFound(SqlABDataReader sqlReader)
        {
            Description = sqlReader[AM_Users.Surname].ToString();
        }

        protected override string DefineBrowseQuery(SqlABCommand sqlCmd, string findQuery)
        {
            qb.Clear();
            qb.SelectAllFrom<AM_Users>().
                AddFilter(findQuery);

            return qb.Query;
        }

        protected override void PrepareBrowseQuery()
        {
        }

        protected override IRadarParameters PrepareRadarParameters(DataGridViewRow row)
        {
            return new RadarUsersParam(row.GetValue<string>(AM_Users.Username));
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