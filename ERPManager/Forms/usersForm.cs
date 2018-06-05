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
            dbManager.AddMaster<EF_Users>();

            PrivilegeTypes = new EnumsManager<UserType>(Properties.Resources.ResourceManager);
            PrivilegeTypes.AttachTo(cbbPrivilege);
        }

        protected override void OnBindData()
        {
            BindControl(txtUserName, EF_Users.Username);
            BindControl(txtName, EF_Users.Surname);
            BindControl(cbbPrivilege, EF_Users.UserType);
            BindControl(mtgForceChange, EF_Users.ChangePassword);
            BindControl(mtgBlocked, EF_Users.Blocked);

            BindObject(password, EF_Users.Password);

            BindControl(dtbExpire, EF_Users.ExpireDate);
            BindControl(rdbDate, EF_Users.Expired);
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

        protected override string CreateMasterQuery(ref List<SqlProxyParameter> dParam)
        {
            var qb = new QueryBuilder();

            return new QueryBuilder()
                .SelectAll<EF_Users>()
                .From<EF_Users>()
                .Where(EF_Users.Username).IsEqualTo(dParam[0])
                .Query;
        }

        protected override void CreateMasterParam(SqlParametersCollection parameters)
        {
            parameters.Add(
                EF_Users.Username,
                new SqlProxyParameter("@p1", EF_Users.Username));

        }

        protected override void SetParameters(IRadarParameters key, DataAdapterProperties dataadapterproperties)
        {
            dataadapterproperties.Parameters[EF_Users.Username].Value = key[EF_Users.Username];
        }
    }
}
