using ERPFramework;
using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.Forms;
using ERPManager.ModuleData;
using System.Collections.Generic;

namespace ERPManager.Forms
{
    public partial class usersForm : DocumentForm
    {
        BindableObject<string> password = new BindableObject<string>();
        EnumsManager<UserType> PrivilegeTypes = null;

        public usersForm()
            : base("Users")
        {
            InitializeComponent();
        }

        protected override void OnInitializeData()
        {
            dbManager = new dbUserManagement("userForm", this);
            dbManager.AttachRadar<RadarUsers>();
            dbManager.AddMaster<AM_Users>();

            PrivilegeTypes = new EnumsManager<UserType>(Properties.Resources.ResourceManager);
            PrivilegeTypes.AttachTo(cbbPrivilege);
        }

        protected override void OnBindData()
        {
            BindControl(txtUserName, AM_Users.Username);
            BindControl(txtName, AM_Users.Surname);
            BindControl(cbbPrivilege, AM_Users.UserType);
            BindControl(mtgForceChange, AM_Users.ChangePassword);
            BindControl(mtgBlocked, AM_Users.Blocked);

            BindObject(password, AM_Users.Password);

            BindControl(dtbExpire, AM_Users.ExpireDate);
            BindControl(rdbDate, AM_Users.Expired);
            BindControl(rdbNever);
        }
    }

    /// <summary>
    /// Classe derivata per gestioni particolari
    /// </summary>
    internal class dbUserManagement : DBManager
    {
        public dbUserManagement(string name, DocumentForm document)
            : base(name, document)
        {
        }

        protected override string CreateMasterQuery(ref List<SqlABParameter> dParam)
        {
            var qb = new QueryBuilder();

            return qb.SelectAll<AM_Users>().
                From<AM_Users>().
                Where(AM_Users.Username).IsEqualTo(dParam[0]).
                Query;
        }

        protected override List<SqlABParameter> CreateMasterParam()
        {
            var PList = new List<SqlABParameter>();

            var sParam = new SqlABParameter("@p1", AM_Users.Username) { Value = "" };
            PList.Add(sParam);
            return PList;
        }

        protected override void SetParameters(IRadarParameters key, DBCollection collection)
        {
            collection.Parameter[0].Value = key.Params[0];
        }
    }
}
