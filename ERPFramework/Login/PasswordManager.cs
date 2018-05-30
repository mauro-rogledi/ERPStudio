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
    public class PasswordManager
    {
        private DRUsers drUsers;
        private DateTime ExpireDate = DateTime.Today;
        private UserStatus status = UserStatus.Found;

        private bool changePwd;

        public UserStatus Status { get { return status; } }

        public bool HasToChangePassword { get { return changePwd; } }

        public string Password
        {
            get => drUsers.GetValue<string>(EF_Users.Password);
        }

        public UserType UserType
        {
            get => drUsers.GetValue<UserType>(EF_Users.UserType);
        }

        public PasswordManager()
        {
            drUsers = new DRUsers();
            ExpireDate = SqlProxyDatabaseHelper.GetServerDate(GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString);
        }

        public UserStatus SelectUser(string username)
        {
            var userFound = drUsers.Find(username);

            if (userFound)
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

            if (drUsers.GetValue<bool>(EF_Users.Blocked))
            {
                status = UserStatus.Locked;
                MessageBox.Show(
                    "Please contact the Administrator",
                    "The User has blocked",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return status;
            }

            changePwd = drUsers.GetValue<bool>(EF_Users.ChangePassword);
            return status;
        }

        private bool HasExpired()
        {
            if (!drUsers.GetValue<bool>(EF_Users.Expired))
                return false;

            return (drUsers.GetValue<DateTime>(EF_Users.ExpireDate) > ExpireDate);
        }

        public bool CheckPassword(string pass)
        {
            if (Cryption.Encrypt(pass) != drUsers.GetValue<string>(EF_Users.Password))
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
            drUsers.SetValue<string>(EF_Users.Password, Cryption.Encrypt(pass));
            drUsers.SetValue<bool>(EF_Users.ChangePassword, false);
            drUsers.Update();
        }
    }
}