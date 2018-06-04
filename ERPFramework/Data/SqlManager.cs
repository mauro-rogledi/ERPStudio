#region Using directives

using ERPFramework.Login;
using ERPFramework.ModulesHelper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

#endregion

namespace ERPFramework.Data
{
    /// <summary>
    /// Summary description for SqlManager.
    /// </summary>
    ///
    public class SqlManager
    {
        public Panel myPanel;
        private ConnectionForm myConnectionForm;
        protected SqlProxyConnection myConnection;
        private int currentVersion = 8;

        #region Public Variables

        //public bool isconnected { get { return connected; } }

        public string DB_ConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }

        public SqlProxyConnection DB_Connection
        {
            get { return myConnection; }
        }

        #endregion

        #region Abstract Method

        //protected abstract bool CreateTable();
        //protected abstract int currentVersion();
        //protected abstract bool UpdateTables(int oldversion, int curversion);

        #endregion

        /// <summary>
        /// SqlManager.
        /// Costruttore per il caso New
        /// </summary>
        ///
        public SqlManager()
        {
        }

        public bool CreateConnection()
        {
            var connected = false;
            var configFound = ReadConfigFile();

            connected = configFound
                ? ConnectToDatabase()
                : CreateNewConnection();

            if (connected && !configFound)
                WriteConfigFile();
            return connected;
        }

        /// <summary>
        /// ConnectToDatabase.
        /// Si connette ad un database esistente
        /// </summary>
        private bool ConnectToDatabase()
        {
            var connected = false;
            var connectionString = GetConnectionString();

            try
            {
                myConnection = new SqlProxyConnection(connectionString);
                myConnection.Open();
                connected = (myConnection.State == ConnectionState.Open);
                // @@ try to remove changedatabase
                //if (connected)
                //    myConnection.ChangeDatabase(lI.InitialCatalog);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), GlobalInfo.LoginInfo.InitialCatalog, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (SqlProxyDatabaseHelper.SearchTable<EF_Version>(myConnection))
            {
                if (MessageBox.Show(Properties.Resources.Database_WrongType,
                                    Properties.Resources.Attention,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    connected = false;
                    return false;
                }
                else
                {
                    RegisterModule RsT = new ERPFramework.ModuleData.RegisterModule();
                    RsT.CreateTable(myConnection, GlobalInfo.UserInfo.userType);
                    AddAdminUser();
                }
            }
            else
            {
                RegisterModule RsT = new ERPFramework.ModuleData.RegisterModule();
                connected = RsT.CreateTable(myConnection, GlobalInfo.UserInfo.userType);
            }

            return connected;
        }

        /// <summary>
        /// CreateNewConnection.
        /// Apre la finestra di connessione al database
        /// </summary>
        private bool CreateNewConnection()
        {
            var connected = false;

            using (myConnectionForm = new ConnectionForm())
            {

                var Result = myConnectionForm.ShowDialog();
                if (Result == DialogResult.OK)
                {
                    SetDataBaseParameter();

                    if (myConnectionForm.NewDatabase)
                    {
                        if (CreateNewDatabase())
                            connected = ConnectToDatabase();
                    }
                    else
                        connected = ConnectToDatabase();
                }
            }
            return connected;
        }

        /// <summary>
        /// CreateNewDatabase.
        /// Crea un nuovo database
        /// </summary>
        private bool CreateNewDatabase()
        {
            SqlProxyDatabaseHelper.DataSource = GlobalInfo.LoginInfo.DataSource;
            SqlProxyDatabaseHelper.UserID = GlobalInfo.LoginInfo.UserID;
            SqlProxyDatabaseHelper.Password = GlobalInfo.LoginInfo.Password;
            SqlProxyDatabaseHelper.InitialCatalog = GlobalInfo.LoginInfo.InitialCatalog;
            SqlProxyDatabaseHelper.IntegratedSecurity = GlobalInfo.LoginInfo.AuthenicationMode == AuthenticationMode.Windows;
            SqlProxyDatabaseHelper.CreateDatabase();

            try
            {

                var connectionString = GetConnectionString();
                myConnection = new SqlProxyConnection(connectionString);
                myConnection.Open();
                if (myConnection.State == ConnectionState.Open)
                    MessageBox.Show(Properties.Resources.Database_Create,
                                    GlobalInfo.LoginInfo.InitialCatalog,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                //myConnection.ChangeDatabase(GlobalInfo.LoginInfo.InitialCatalog);
                RegisterModule RsT = new ERPFramework.ModuleData.RegisterModule();
                RsT.CreateTable(myConnection, GlobalInfo.UserInfo.userType);
                AddAdminUser();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), GlobalInfo.LoginInfo.InitialCatalog, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (myConnection.State == ConnectionState.Open)
                myConnection.Clone();

            return true;
        }


        /// <summary>
        /// NewConnection.
        /// Si connette ad un database esistente
        /// </summary>
        public SqlProxyConnection NewConnection()
        {
            SqlProxyConnection newConn = null;
            string connectionString = GetConnectionString();

            try
            {
                newConn = new SqlProxyConnection(connectionString);
                newConn.Open();
                if (newConn.State == ConnectionState.Open)
                    newConn.ChangeDatabase(GlobalInfo.LoginInfo.InitialCatalog);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), GlobalInfo.LoginInfo.InitialCatalog, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return newConn;
        }

        public void CloseConnection(SqlProxyConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        /// <summary>
        /// SetDataBaseParameter.
        /// Copia i parametri di accesso al database
        /// nelle variabili del SqlConnector
        /// </summary>
        private void SetDataBaseParameter()
        {
            var li = GlobalInfo.LoginInfo;
            li.ProviderType = myConnectionForm.Provider;
            li.DataSource = myConnectionForm.DataSource;
            li.InitialCatalog = myConnectionForm.InitialCatalog;
            li.AuthenicationMode = myConnectionForm.DataBase_Authentication;
            li.UserID = myConnectionForm.Username;
            li.Password = myConnectionForm.Password;
        }

        private void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// UpdateDataBase
        /// Controlla la versione corrente del database
        /// Se diversa da quella del programma chiama
        /// le routine di aggiornamento
        /// </summary>
        private bool UpdateDatabase()
        {
            var version = ReadDBVersion();
            if (version > currentVersion)
            {
                var message = string.Format(Properties.Resources.Database_WrongVersion,
                                                version, currentVersion);

                MessageBox.Show(message, Properties.Resources.Attention, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //connected = false;
                return false;
            }
            return true;
        }

        private int ReadDBVersion()
        {
            //DRVersion dvVersion = new DRVersion();
            int current = -1;
            //try
            //{
            //    var qb = new QueryBuilder().
            //        Select(AM_Version.Version).
            //        From<AM_Version>();

            //    using (var cmd = new SqlProxyCommand(qb.Query, myConnection))
            //    {
            //        var dr = cmd.ExecuteReader();
            //        dr.Read();
            //        current = dr.GetValue<int>(AM_Version.Version);
            //        dr.Close();
            //    }
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message);
            //    return current;
            //}
            return current;
        }



        private bool AddAdminUser()
        {
            return AddUser("Administrator", "a", "", UserType.Administrator);
        }

        public bool AddUser(string username, string password, string surname, UserType usertype)
        {
            var duUsers = new DRUsers();
            duUsers.Find(username);

            var row = duUsers.AddRecord();
            row.SetValue<string>(EF_Users.Username, username);
            row.SetValue<string>(EF_Users.Password, Cryption.Encrypt(password));
            row.SetValue<string>(EF_Users.Surname, surname);
            row.SetValue<UserType>(EF_Users.UserType, usertype);
            row.SetValue<bool>(EF_Users.Expired, false);
            row.SetValue<DateTime>(EF_Users.ExpireDate, DateTime.Today);
            row.SetValue<bool>(EF_Users.ChangePassword, true);
            row.SetValue<bool>(EF_Users.Blocked, false);

            return duUsers.Update();
        }

        private bool ReadConfigFile()
        {
            if (File.Exists(ConfigFilename))
            {
                Stream stream = null;
                try
                {
                    stream = File.Open(ConfigFilename, FileMode.Open);
                    var xmlSer = new XmlSerializer(typeof(LoginInfo));

                    GlobalInfo.LoginInfo = (LoginInfo)xmlSer.Deserialize(stream);
                    ProxyProviderLoader.UseProvider = GlobalInfo.LoginInfo.ProviderType;
                    if (GlobalInfo.LoginInfo.RememberLastLogin)
                        GlobalInfo.LoginInfo.LastPassword = Cryption.Decrypt(GlobalInfo.LoginInfo.LastPassword);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    stream.Close();
                }
                return true;
            }
            return false;
        }

        public void WriteConfigFile()
        {
            if (!System.IO.Directory.Exists(ConfigDirectory))
                System.IO.Directory.CreateDirectory(ConfigDirectory);
            TextWriter stream = new StreamWriter(ConfigFilename);
            var xmlSer = new XmlSerializer(typeof(LoginInfo));
            GlobalInfo.LoginInfo.LastPassword = GlobalInfo.LoginInfo.RememberLastLogin
                    ? Cryption.Encrypt(GlobalInfo.LoginInfo.LastPassword)
                    : string.Empty;

            xmlSer.Serialize(stream, GlobalInfo.LoginInfo);
            if (GlobalInfo.LoginInfo.RememberLastLogin)
                GlobalInfo.LoginInfo.LastPassword = Cryption.Decrypt(GlobalInfo.LoginInfo.LastPassword);
            stream.Close();
        }

        private string ConfigFilename
        {
            get
            {
                return System.IO.Path.Combine(ConfigDirectory, "loginsetting.config");
            }
        }

        private string ConfigDirectory
        {
            get
            {
                var directory = ActivationManager.ApplicationName;
                if (string.IsNullOrEmpty(directory))
                {
                    if (Directory.GetParent(Directory.GetCurrentDirectory()).FullName.EndsWith("bin", StringComparison.CurrentCultureIgnoreCase))
                        directory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Name;
                    else
                        directory = new DirectoryInfo(Environment.CurrentDirectory).Name;
                }

                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), directory);
            }
        }

        private string GetConnectionString()
        {
            LoginInfo li = GlobalInfo.LoginInfo;
            var sqlconnectionstring = new SqlProxyConnectionStringbuilder
            {
                DataSource = li.DataSource,
                UserID = li.UserID,
                Password = li.Password,
                InitialCatalog = li.InitialCatalog,
                IntegratedSecurity = li.AuthenicationMode == AuthenticationMode.Windows
            };

            return sqlconnectionstring.ConnectionString;
        }
    }

    public class DataRowValues
    {
        public static void SetValue<T>(DataRow row, IColumn col, T value)
        {
            row[col.Name] = value;
        }

        public static T GetValue<T>(DataRow row, IColumn col)
        {
            if (row[col.Name] != System.DBNull.Value && row[col.Name] != null)
                return (T)Convert.ChangeType(row[col.Name], typeof(T));
            else
                return default(T);
        }

        public static T GetValue<T>(DataRow row, IColumn col, DataRowVersion version)
        {
            if (row[col.Name, version] != System.DBNull.Value && row[col.Name, version] != null)
                return (T)Convert.ChangeType(row[col.Name, version], typeof(T));
            else
                return default(T);
        }
    }
}