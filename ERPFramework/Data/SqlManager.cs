#region Using directives

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.SqlServer.Management.Smo;
using System.Xml.Serialization;
using ERPFramework.Data;
using ERPFramework.Login;

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
        private bool connected;
        protected SqlProxyConnection MyConnection;
        private int currentVersion = 8;

        #region Public Variables

        public bool isconnected { get { return connected; } }

        public string DB_ConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }

        public SqlProxyConnection DB_Connection
        {
            get { return MyConnection; }
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
            bool configFound = true;

            configFound = ReadConfigFile();

            if (configFound)
                ConnectToDatabase();
            else
                CreateNewConnection();

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
            LoginInfo lI = GlobalInfo.LoginInfo;
            string connectionString = GetConnectionString();

            try
            {
                MyConnection = new SqlProxyConnection(connectionString);
                MyConnection.Open();
                connected = (MyConnection.State == ConnectionState.Open);
                if (connected)
                    MyConnection.ChangeDatabase(lI.Datasource);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), lI.Datasource, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (SearchTable(AM_Version.Name))
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
                    RsT.CreateTable(MyConnection, GlobalInfo.UserInfo.userType);
                    AddAdminUser();
                }
            }
            else
            {
                RegisterModule RsT = new ERPFramework.ModuleData.RegisterModule();
                RsT.CreateTable(MyConnection, GlobalInfo.UserInfo.userType);
            }

            return connected;
        }

        /// <summary>
        /// CreateNewConnection.
        /// Apre la finestra di connessione al database
        /// </summary>
        private bool CreateNewConnection()
        {
            connected = false;

            myConnectionForm = new ConnectionForm();

            DialogResult Result = myConnectionForm.ShowDialog();
            if (Result == DialogResult.OK)
            {
                SetDataBaseParameter();

                if (myConnectionForm.DataBase_NewDatabase)
                {
                    if (CreateNewDatabase())
                        connected = ConnectToDatabase();
                }
                else
                    connected = ConnectToDatabase();
            }

            myConnectionForm.Dispose();
            return connected;
        }

        /// <summary>
        /// CreateNewDatabase.
        /// Crea un nuovo database
        /// </summary>
        private bool CreateNewDatabase()
        {
            SqlProxyDatabaseHelper.DataSource = GlobalInfo.LoginInfo.Datasource;
            SqlProxyDatabaseHelper.UserID = GlobalInfo.LoginInfo.UserID;
            SqlProxyDatabaseHelper.Password = GlobalInfo.LoginInfo.Password;
            SqlProxyDatabaseHelper.IntegratedSecurity = GlobalInfo.LoginInfo.AuthenicationMode == AuthenticationMode.Windows;
            SqlProxyDatabaseHelper.CreateDatabase();

            try
            {

                var connectionString = GetConnectionString();
                MyConnection = new SqlProxyConnection(connectionString);
                MyConnection.Open();
                if (MyConnection.State == ConnectionState.Open)
                    MessageBox.Show(Properties.Resources.Database_Create,
                                    GlobalInfo.LoginInfo.Datasource,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyConnection.ChangeDatabase(GlobalInfo.LoginInfo.Datasource);
                RegisterModule RsT = new ERPFramework.ModuleData.RegisterModule();
                RsT.CreateTable(MyConnection, GlobalInfo.UserInfo.userType);
                AddAdminUser();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), GlobalInfo.LoginInfo.Datasource, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (MyConnection.State == ConnectionState.Open)
                CloseConnection(MyConnection);

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
                connected = (newConn.State == ConnectionState.Open);
                if (connected)
                    newConn.ChangeDatabase(GlobalInfo.LoginInfo.Datasource);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), GlobalInfo.LoginInfo.Datasource, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            li.ProviderType = myConnectionForm.DataBase_Provider;
            li.Host = myConnectionForm.DataBase_Host;
            li.Datasource = myConnectionForm.DataBase_Name;
            li.AuthenicationMode = myConnectionForm.DataBase_Authentication;
            li.UserID = myConnectionForm.DataBase_Username;
            li.Password = myConnectionForm.DataBase_Password;
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
            int version = ReadDBVersion();
            if (version > currentVersion)
            {
                string message = string.Format(Properties.Resources.Database_WrongVersion,
                                                version, currentVersion);

                MessageBox.Show(message, Properties.Resources.Attention, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                connected = false;
                return false;
            }
            return true;
        }

        private int ReadDBVersion()
        {
            int current = -1;
            try
            {
                var qb = new QueryBuilder().
                    Select(AM_Version.Version).
                    From<AM_Version>();

                using (var cmd = new SqlProxyCommand(qb.Query, MyConnection))
                {
                    var dr = cmd.ExecuteReader();
                    dr.Read();
                    current = dr.GetValue<int>(AM_Version.Version);
                    dr.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return current;
            }
            return current;
        }

        protected bool SearchTable(string tablename)
        {
            bool notfound = false;
            try
            {
                using (var cmd = new SqlProxyCommand(SqlProxyDatabaseHelper.QuerySearchTable(tablename), MyConnection))
                {
                    var dr = cmd.ExecuteReader();

                    notfound = !dr.Read();
                    dr.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return true;
            }
            return notfound;
        }

        private bool InsertDBVersion(string module, string version)
        {
            try
            {
                var dbApplication = new SqlProxyParameter("@p1", AM_Version.Application);
                var dbModule = new SqlProxyParameter("@p2", AM_Version.Module);
                var dbVersion = new SqlProxyParameter("@p3", AM_Version.Version);

                var qb = new QueryBuilder().
                    InsertInto<AM_Version>(AM_Version.Module).Values(dbModule).
                    Where(AM_Version.Application).IsEqualTo(dbApplication).
                    And(AM_Version.Version).IsEqualTo(dbVersion);

                using (var cmd = new SqlProxyCommand(qb.Query, MyConnection))
                {
                    cmd.Parameters.Add(dbModule);
                    cmd.Parameters.Add(dbVersion);

                    dbModule.Value = module;
                    dbVersion.Value = version;
                    cmd.ExecuteScalar();
                }
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            return true;
        }

        private bool UpdateDBVersion(string application, string module, string version)
        {
            try
            {
                SqlProxyParameter dbApplication = new SqlProxyParameter("@p1", AM_Version.Application);
                SqlProxyParameter dbVersion = new SqlProxyParameter("@p2", AM_Version.Version);
                SqlProxyParameter dbModule = new SqlProxyParameter("@p3", AM_Version.Module);

                QueryBuilder qb = new QueryBuilder().
                    Update<AM_Version>().
                    Set<SqlProxyParameter>(AM_Version.Version, dbVersion).
                    Where(AM_Version.Application).IsEqualTo(dbApplication).
                    And(AM_Version.Module).IsEqualTo(dbVersion);

                //qb.AddManualQuery("UPDATE {0} SET {1}=@p1 WHERE {2}=@p2",
                //                       AM_Version.Name, AM_Version.Version, AM_Version.Module);

                SqlProxyCommand cmd = new SqlProxyCommand(qb.Query, MyConnection);
                cmd.Parameters.Add(dbApplication);
                cmd.Parameters.Add(dbVersion);

                dbApplication.Value = application;
                dbVersion.Value = version;
                dbModule.Value = module;
                cmd.ExecuteScalar();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            return true;
        }

        private bool CreateDBVersion()
        {
            try
            {
                SqlProxyParameter dbVersion = new SqlProxyParameter("@p1", AM_Version.Version);
                string command = "INSERT INTO " + AM_Version.Name + " ( " +
                    AM_Version.Version + " ) " +
                    "VALUES (@p1)";

                SqlProxyCommand cmd = new SqlProxyCommand(command, MyConnection);
                cmd.Parameters.Add(dbVersion);

                dbVersion.Value = currentVersion;
                cmd.ExecuteScalar();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            return true;
        }

        private bool AddAdminUser()
        {
            return AddUser("Administrator", "a", "", UserType.Administrator);
        }

        public bool AddUser(string username, string password, string surname, UserType usertype)
        {
            try
            {
                SqlProxyParameter dbUser = new SqlProxyParameter("@p1", AM_Users.Username);
                SqlProxyParameter dbPass = new SqlProxyParameter("@p2", AM_Users.Password);
                SqlProxyParameter dbSurn = new SqlProxyParameter("@p3", AM_Users.Surname);
                SqlProxyParameter dbPriv = new SqlProxyParameter("@p4", AM_Users.UserType);
                SqlProxyParameter dbExp = new SqlProxyParameter("@p5", AM_Users.Expired);
                SqlProxyParameter dbExpD = new SqlProxyParameter("@p6", AM_Users.ExpireDate);
                SqlProxyParameter dbCPwd = new SqlProxyParameter("@p7", AM_Users.ChangePassword);
                SqlProxyParameter dbLock = new SqlProxyParameter("@p8", AM_Users.Blocked);

                string command = "INSERT INTO " + AM_Users.Name + " ( " +
                    AM_Users.Username + ", " +
                    AM_Users.Password + ", " +
                    AM_Users.Surname + ", " +
                    AM_Users.UserType + ", " +
                    AM_Users.Expired + ", " +
                    AM_Users.ExpireDate + ", " +
                    AM_Users.ChangePassword + ", " +
                    AM_Users.Blocked + " ) " +
                    "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7, @p8)";

                SqlProxyCommand cmd = new SqlProxyCommand(command, MyConnection);
                cmd.Parameters.Add(dbUser);
                cmd.Parameters.Add(dbPass);
                cmd.Parameters.Add(dbSurn);
                cmd.Parameters.Add(dbPriv);
                cmd.Parameters.Add(dbExp);
                cmd.Parameters.Add(dbExpD);
                cmd.Parameters.Add(dbCPwd);
                cmd.Parameters.Add(dbLock);

                dbUser.Value = username;
                dbPass.Value = Cryption.Encrypt(password);
                dbSurn.Value = surname;
                dbPriv.Value = usertype;
                dbExp.Value = 0;
                dbExpD.Value = DateTime.Today;
                dbCPwd.Value = 1;
                dbLock.Value = 0;

                cmd.ExecuteScalar();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }
            return true;
        }

        private bool ReadConfigFile()
        {
            if (File.Exists(ConfigFilename))
            {
                Stream stream = null;
                try
                {
                    stream = File.Open(ConfigFilename, FileMode.Open);
                    XmlSerializer xmlSer = new XmlSerializer(typeof(LoginInfo));

                    GlobalInfo.LoginInfo = (LoginInfo)xmlSer.Deserialize(stream);
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
            XmlSerializer xmlSer = new XmlSerializer(typeof(LoginInfo));
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
                return System.IO.Path.Combine(ConfigDirectory, SS.LoginFile);
            }
        }

        private string ConfigDirectory
        {
            get
            {
                string directory = GetApplicationName();
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

        public string GetApplicationName()
        {
            string applPath = Path.GetDirectoryName(Application.ExecutablePath);
            string appConfname = Path.Combine(applPath, "ApplicationModules.config");
            string appName = string.Empty;
            if (!File.Exists(appConfname))
            {
                Debug.Assert(false, "Missing ApplicationModules.config");
                return "";
            }
            XmlDocument applModule = new XmlDocument();
            applModule.Load(appConfname);
            XmlNode moduleName = applModule.SelectSingleNode("modules");
            if (moduleName != null)
            {
                appName = moduleName.Attributes["name"].Value;
#if (DEBUG)
                Version vrs = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                appName = string.Concat(appName, "_", vrs.Major.ToString(), vrs.Minor.ToString());
#endif
            }

            return appName;
        }

        private string GetConnectionString()
        {
            LoginInfo li = GlobalInfo.LoginInfo;
            var sqlconnectionstring = new SqlProxyConnectionStringbuilder
            {
                DataSource = li.Datasource,
                UserID = li.UserID,
                Password = li.Password,
                IntegratedSecurity = li.AuthenicationMode == AuthenticationMode.Windows
            };

            return sqlconnectionstring.ConnectionString;
        }
    }

    public class NameSolverDatabaseStrings
    {
        public const string SQLSqlProvider = "SQLSERVER";

        public const string ProviderConnAttribute = "Provider={0}; ";
        public const string UnknownDBMS = "Unknown DBMS {0}; ";

        public const string SQLWinNtConnection = "Data Source={0};Initial Catalog='{1}';Integrated Security='SSPI';Connect Timeout=30; Pooling= false ";
        public const string SQLConnection = "Data Source={0};Initial Catalog='{1}'; MultipleActiveResultSets=True; User ID='{2}';Password='{3}';";
        public const string SQLCompactConnection = "Data Source={0};Password='{1}';";
        public const string SQLiteConnectionNew = "Data Source={0};Version=3;New={1};Compress=True;";
        public const string SQLiteConnection = "Data Source={0};Version=3;Compress=True;";

        // Data Source=mydb.db;Version=3;New=True;

        public const string SQLCreationRemote = "CREATE DATABASE {0}";
    }

    public struct SS
    {
        public const string LoginFile = "loginsetting.config";
        public const string ProviderType = "ProviderType";
        public const string AuthenticationType = "AuthenticationType";
        public const string Login = "Login";
        public const string Host = "Host";
        public const string DataSource = "DataSource";
        public const string Directory = "ApplicationBuilder";

        public const string Users = "Users";
        public const string User = "User";
        public const string UserName = "UserName";
        public const string Password = "Password";
        public const string UsersList = "UsersList";
        public const string Remember = "RememberPassword";
        public const string LastUser = "LastUser";
        public const string LastPassword = "LastPassword";

        public const string IdApplication = "GuidApplication";
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