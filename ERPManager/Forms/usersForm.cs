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
#pragma warning disable CC0033 // Dispose Fields Properly
        BindableObject<string> password = new BindableObject<string>();
#pragma warning restore CC0033 // Dispose Fields Properly
        EnumsManager<UserType> PrivilegeTypes = null;

        public usersForm()
            : base("Users")
        {
            InitializeComponent();
        }

        protected override void OnInitializeData()
        {
            dbManager = new dbUserManagement(this)
                .MasterTable<EF_Users>()
                .Radar<RadarUsers>();

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
        public dbUserManagement(DocumentForm document)
            : base(document)
        {
        }

        protected override string CreateMasterQuery(SqlProxyParameterCollection parameters)
        {
            var qb = new QueryBuilder();

            return new QueryBuilder()
                .SelectAll<EF_Users>()
                .From<EF_Users>()
                .Where(EF_Users.Username).IsEqualTo(parameters["@p1"])
                .Query;
        }

        protected override void CreateMasterParam(SqlProxyParameterCollection parameters)
        {
            parameters.Add(
                new SqlProxyParameter("@p1", EF_Users.Username));
        }

        protected override void SetParameters(IRadarParameters key, DataAdapterProperties dataadapterproperties)
        {
            dataadapterproperties.Parameters["@p1"].Value = key[EF_Users.Username];
        }
    }
}
