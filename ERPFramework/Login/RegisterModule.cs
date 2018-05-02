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

        protected SqlABConnection SqlABConnection;
        protected ProviderType ProviderType;
        protected int dbVersion;

        public void AddTable<T>(bool toExport = true)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var table = new TableDefinition { Application = Application(), Module = Module(), Table = typeof(T), ToExport = toExport };
            if (!GlobalInfo.Tables.ContainsKey(table.Table.Name))
                GlobalInfo.Tables.Add(table.Table.Name, table);

            if (SearchTable<T>())
                SqlCreateTable.CreateTable<T>();
        }

        public bool CreateTable(SqlProxyConnection Connection, UserType user)
        {

            SqlABConnection = Connection;
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
            SqlABDataReader dr;
            int current = -1;
            var p1 = new SqlABParameter("@p1", AM_Version.Application);
            var p2 = new SqlABParameter("@p2", AM_Version.Module);
            try
            {
                QueryBuilder qb = new QueryBuilder().
                    Select(AM_Version.Version).
                    From<AM_Version>().
                    Where(AM_Version.Application).IsEqualTo(p1).
                    And(AM_Version.Module).IsEqualTo(p2);

                using (SqlABCommand cmd = new SqlABCommand(qb.Query, SqlABConnection))
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
                SqlABParameter dbApplication = new SqlABParameter("@p1", AM_Version.Application);
                SqlABParameter dbModule = new SqlABParameter("@p2", AM_Version.Module);
                SqlABParameter dbVersion = new SqlABParameter("@p3", AM_Version.Version);

                QueryBuilder qb = new QueryBuilder().
                                InsertInto<AM_Version>(AM_Version.Application, AM_Version.Module, AM_Version.Version).
                                Values(dbApplication, dbModule, dbVersion);

                using (SqlABCommand cmd = new SqlABCommand(qb.Query, SqlABConnection))
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
                var dbVersion = new SqlABParameter("@p1", AM_Version.Version);
                var dbApplication = new SqlABParameter("@p2", AM_Version.Application);
                var dbModule = new SqlABParameter("@p3", AM_Version.Module);

                var qb = new QueryBuilder().
                    Update<AM_Version>().
                    Set<SqlABParameter>(AM_Version.Version, dbVersion).
                    Where(AM_Version.Module).IsEqualTo(dbModule);

                //qb.AddManualQuery("UPDATE {0} SET {1}=@p1 WHERE {2}=@p2 AND {3}=@p3",
                //                       AM_Version.Name, AM_Version.Version, AM_Version.Application, AM_Version.Module);

                using (SqlABCommand cmd = new SqlABCommand(qb.Query, SqlABConnection))
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

        protected bool SearchTable<T>()
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType == typeof(Table));
            var tablename = typeof(T).GetField("Name").GetValue(null).ToString();

            SqlABDataReader dr;
            var notfound = false;
            try
            {
                var command = string.Empty; ;
                switch (SqlABConnection.ProviderType)
                {
#if(SQLite)
                    case ProviderType.SQLite:
                        command = string.Format("select tbl_name from sqlite_master where type = 'table' and tbl_name = '{0}'", tablename);
                        break;
#endif
#if(SQLCompact)
                    case ProviderType.SQLCompact:
                        command = string.Format("select table_name from information_schema.tables where table_name = '{0}'", tablename);
                        break;
#endif
#if(SQLServer)
                    case ProviderType.SQLServer:
                        command = string.Format("select table_name from information_schema.tables where table_name = '{0}'", tablename);
                        break;
#endif
                }

                using (SqlABCommand cmd = new SqlABCommand(command, SqlABConnection))
                {
                    dr = cmd.ExecuteReader();

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

        protected bool SearchColumn(IColumn columName)
        {
            switch (SqlABConnection.ProviderType)
            {
#if (SQLServer)
                case ProviderType.SQLServer:
                    return SearchSqlServerColumn(columName);
#endif
#if (SQLCompact)
                case ProviderType.SQLCompact:
                    return SearchSqlServerColumn(columName);
#endif
#if (SQLite)
                case ProviderType.SQLite:
                    return SearchSQLiteColumn(columName);
#endif
            }
            return true;
        }
#if (SQLite)
        private bool SearchSQLiteColumn(IColumn columName)
        {
            var found = false;
            try
            {
                var command = string.Format("PRAGMA table_info('{0}');", columName.Tablename);

                using (SqlABCommand cmd = new SqlABCommand(command, SqlABConnection))
                {

                    using (SqlABDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetString("name");
                            if (name == columName.Name)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return true;
            }
            return found;
        }
#endif
        private bool SearchSqlServerColumn(IColumn columName)
        {
            SqlABDataReader dr;
            var found = false;
            try
            {
                var command = string.Format(
                    "select table_name from information_schema.columns where table_name = '{0}' and column_name = '{1}'",
                    columName.Table, columName.Name);

                using (SqlABCommand cmd = new SqlABCommand(command, SqlABConnection))
                {
                    dr = cmd.ExecuteReader();

                    found = dr.HasRows;
                    dr.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return true;
            }
            return found;
        }
    }
}