using ERPFramework.Data;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPFramework.Login
{
    /// <summary>
    /// Summary description for PasswordManager.
    /// </summary>
    /// 
    public class PasswordManager : IDisposable
    {
        private SqlABConnection sCon;
        private SqlABCommand sC;
        private SqlABParameter sP;
        private SqlABDataReader sR;
        private DateTime ExpireDate = DateTime.Today;
        private UserStatus status = UserStatus.Found;

        private bool changePwd;

        public UserStatus Status { get { return status; } }

        public bool HasToChangePassword { get { return changePwd; } }

        public string Password { get { return (sR != null) ? (string)sR[AM_Users.Password] : string.Empty; } }

        public UserType UserType
        {
            get
            { return (sR != null) ? (UserType)Enum.Parse(typeof(UserType), sR[AM_Users.UserType].ToString()) : UserType.Guest; }
        }

        public PasswordManager()
        {
            try
            {
                sCon = new SqlABConnection(GlobalInfo.LoginInfo.ProviderType,
                                            GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString);
                sCon.Open();
                sP = new SqlABParameter("@p1", AM_Users.Username);

                QueryBuilder qb = new QueryBuilder();

                string select =  qb.SelectAllFrom<AM_Users>().
                    Where(AM_Users.Username).IsEqualTo(sP).
                    Query;

                sC = new SqlABCommand(select, sCon);
                sC.Parameters.Add(sP);

                string getDate = "";
                switch (GlobalInfo.LoginInfo.ProviderType)
                {
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        getDate = "SELECT GETDATE()";
                        break;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        getDate = "SELECT GETDATE()";
                        break;
#endif
#if (SQLite)
                    case ProviderType.SQLite:
                        getDate = "SELECT date('now');";
                        break;
#endif
                }
                using (SqlABCommand sc1 = new SqlABCommand(getDate, sCon))
                {
                    SqlABDataReader dr = sc1.ExecuteReader();
                    if (dr.Read())
                        ExpireDate = dr.GetDateTime(0);
                    dr.Close();
                }
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            try
            {
                if (sR != null && !sR.IsClosed)
                    sR.Close();
                if (sCon != null)
                    sCon.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public UserStatus SelectUser(string username)
        {
            try
            {
                sP.Value = username;
                if (sR != null && !sR.IsClosed)
                    sR.Close();

                sR = sC.ExecuteReader();

                if (sR.Read())
                    status = UserStatus.Found;
                else
                {
                    status = UserStatus.NotFound;
                    MessageBox.Show(
                        "Check username or contact the Administrator",
                        "User not found",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                if (status != UserStatus.Found) return status;

                if (HasExpired())
                {
                    status = UserStatus.Expired;
                    MessageBox.Show(
                        "Please contact the Administrator",
                        "The User has expired",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return status;
                }

                if ((bool)sR[AM_Users.Blocked])
                {
                    status = UserStatus.Locked;
                    MessageBox.Show(
                        "Please contact the Administrator",
                        "The User has blocked",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return status;
                }

                changePwd = (bool)sR[AM_Users.ChangePassword];
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return status;
        }

        private bool HasExpired()
        {
            if (sR == null) return false;

            if (!((bool)sR[AM_Users.Expired])) return false;
            if (sR[AM_Users.ExpireDate] == null) return false;

            DateTime expireUser = (DateTime)sR[AM_Users.ExpireDate];

            return (expireUser > ExpireDate);
        }

        public bool CheckPassword(string pass)
        {
            if (Cryption.Encrypt(pass) != sR.GetValue<string>(AM_Users.Password))
            {
                MessageBox.Show(
                    "Please contact the Administrator",
                    "Wrong Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        public void ChangePassword(string pass)
        {
            try
            {
                //UPDATE USERS SET password='aa' WHERE username='MAURO'

                string update = string.Format
                    (
                    "UPDATE {0} SET {1}=@p2, {2}=@p3 WHERE {3} = @p4",
                    AM_Users.Name,
                    AM_Users.Password,
                    AM_Users.ChangePassword,
                    AM_Users.Username
                    );

                SqlABCommand sc = new SqlABCommand(update, sCon);
                SqlABParameter p2 = new SqlABParameter("@p2", AM_Users.Password);
                SqlABParameter p3 = new SqlABParameter("@p3", AM_Users.ChangePassword);
                SqlABParameter p4 = new SqlABParameter("@p4", AM_Users.Username);
                sc.Parameters.Add(p2);
                sc.Parameters.Add(p3);
                sc.Parameters.Add(p4);

                p2.Value = Cryption.Encrypt(pass);
                p3.Value = (int)0;
                p4.Value = sR[AM_Users.Username];

                if (sR != null && !sR.IsClosed)
                    sR.Close();

                sc.ExecuteScalar();
                SelectUser((string)p4.Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}