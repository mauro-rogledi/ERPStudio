using ERPFramework.Forms;
using ERPFramework.Preferences;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPFramework.Data
{
    abstract public class RegisterModule : Object
    {
        #region Abstract Methods

        abstract public int CurrentVersion();

        abstract public string Module();

        abstract public string Application();

        abstract protected bool CreateDBTables();

        abstract protected bool UpdateDBTables();

        abstract public Version DllVersion { get; }

        abstract public void Addon(IDocument frm, NameSpace nameSpace);

        #endregion

        virtual public void RegisterCountersAndCodes()
        {
        }

        virtual public PreferencePanel[] RegisterPreferences()
        {
            return null;
        }

        protected SqlProxyConnection SqlProxyConnection;
        protected ProviderType ProviderType;
        protected int dbVersion;

        public void AddTable<T>(bool toExport = true)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var table = new TableDefinition { Application = Application(), Module = Module(), Table = typeof(T), ToExport = toExport };
            if (!GlobalInfo.Tables.ContainsKey(table.Table.Name))
                GlobalInfo.Tables.Add(table.Table.Name, table);

            if (SqlProxyDatabaseHelper.SearchTable<T>(GlobalInfo.SqlConnection))
                SqlCreateTable.CreateTable<T>();
        }

        public bool CreateTable(SqlProxyConnection Connection, UserType user)
        {

            SqlProxyConnection = Connection;
            CreateDBTables();

            dbVersion = ReadDBVersion();
            if (dbVersion == -1)
            {
                InsertDBVersion(Application(), Module(), CurrentVersion());
                dbVersion = CurrentVersion();
            }

            if (dbVersion > CurrentVersion())
            {
                var message = string.Format("Wrong version.\nThe Current Database version form module {0} is {1}, while the program version is {2}",
                    Module(), dbVersion, CurrentVersion());
                MessageBox.Show(message, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (dbVersion < CurrentVersion())
            {
                if ((user != UserType.Administrator && user != UserType.SuperUser) || !UpdateDBTables())
                {
                    MessageBox.Show("Utente senza privilegi per l'upgrade");
                    return false;
                }

                UpdateDBVersion(Application(), Module(), CurrentVersion());
            }

            return true;
        }

        private int ReadDBVersion()
        {
            SqlProxyDataReader dr;
            int current = -1;
            var p1 = new SqlProxyParameter("@p1", AM_Version.Application);
            var p2 = new SqlProxyParameter("@p2", AM_Version.Module);
            try
            {
                QueryBuilder qb = new QueryBuilder().
                    Select(AM_Version.Version).
                    From<AM_Version>().
                    Where(AM_Version.Application).IsEqualTo(p1).
                    And(AM_Version.Module).IsEqualTo(p2);

                using (SqlProxyCommand cmd = new SqlProxyCommand(qb.Query, SqlProxyConnection))
                {
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    p1.Value = Application();
                    p2.Value = Module();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                        current = dr.GetInt32(0);
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

        private bool InsertDBVersion(string application, string module, int version)
        {
            try
            {
                SqlProxyParameter dbApplication = new SqlProxyParameter("@p1", AM_Version.Application);
                SqlProxyParameter dbModule = new SqlProxyParameter("@p2", AM_Version.Module);
                SqlProxyParameter dbVersion = new SqlProxyParameter("@p3", AM_Version.Version);

                QueryBuilder qb = new QueryBuilder().
                                InsertInto<AM_Version>(AM_Version.Application, AM_Version.Module, AM_Version.Version).
                                Values(dbApplication, dbModule, dbVersion);

                using (SqlProxyCommand cmd = new SqlProxyCommand(qb.Query, SqlProxyConnection))
                {
                    cmd.Parameters.Add(dbApplication);
                    cmd.Parameters.Add(dbVersion);
                    cmd.Parameters.Add(dbModule);

                    dbApplication.Value = application;
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

        private bool UpdateDBVersion(string application, string module, int version)
        {
            try
            {
                var dbVersion = new SqlProxyParameter("@p1", AM_Version.Version);
                var dbApplication = new SqlProxyParameter("@p2", AM_Version.Application);
                var dbModule = new SqlProxyParameter("@p3", AM_Version.Module);

                var qb = new QueryBuilder().
                    Update<AM_Version>().
                    Set<SqlProxyParameter>(AM_Version.Version, dbVersion).
                    Where(AM_Version.Module).IsEqualTo(dbModule);

                //qb.AddManualQuery("UPDATE {0} SET {1}=@p1 WHERE {2}=@p2 AND {3}=@p3",
                //                       AM_Version.Name, AM_Version.Version, AM_Version.Application, AM_Version.Module);

                using (SqlProxyCommand cmd = new SqlProxyCommand(qb.Query, SqlProxyConnection))
                {
                    cmd.Parameters.Add(dbVersion);
                    cmd.Parameters.Add(dbApplication);
                    cmd.Parameters.Add(dbModule);

                    dbVersion.Value = version;
                    dbApplication.Value = application;
                    dbModule.Value = module;
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
    }
}