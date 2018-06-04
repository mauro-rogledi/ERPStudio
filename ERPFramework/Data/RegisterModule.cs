using ERPFramework.Forms;
using ERPFramework.Preferences;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ERPFramework.Data
{
    abstract public class RegisterModule : Object
    {
        #region Abstract Methods

        abstract public int CurrentVersion();

        abstract public string Module();

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
            var existVirtual = typeof(T).GetField("IsVirtual");
            var isVirtual = existVirtual == null
                                ? false
                                : bool.Parse(existVirtual.GetValue(null).ToString());

            var table = new TableDefinition { Module = Module(), Table = typeof(T), ToExport = toExport, IsVirtual = isVirtual };
            if (!GlobalInfo.Tables.ContainsKey(table.Table.Name))
            {
                GlobalInfo.Tables.Add(table.Table.Name, table);
                AddTableType<T>();
            }

            if (!isVirtual && SqlProxyDatabaseHelper.SearchTable<T>(GlobalInfo.SqlConnection))
                SqlCreateTable.CreateTable<T>();
        }

        private static void AddTableType<T>()
        {
            var col = from mi in typeof(T).GetMembers()
                      where mi.MemberType == MemberTypes.Field && ((FieldInfo)mi).FieldType.GetInterface(nameof(IColumn)) == typeof(IColumn)
                      select (IColumn)((FieldInfo)mi).GetValue((mi));

            col.ToList().ForEach(c => c.TableType = typeof(T));

        }

        public bool CreateTable(SqlProxyConnection Connection, UserType user)
        {

            SqlProxyConnection = Connection;
            CreateDBTables();

            dbVersion = ReadDBVersion;
            if (dbVersion == -1)
            {
                InsertDBVersion(Module(), CurrentVersion());
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

                UpdateDBVersion(Module(), CurrentVersion());
            }

            return true;
        }

        private int ReadDBVersion
        {
            get
            {
                var drVersion = new DRVersion();

                return drVersion.Find(Module())
                    ? drVersion.GetValue<int>(EF_Version.Version)
                    : -1;
            }
        }

        private static bool InsertDBVersion(string module, int version)
        {
            var drVersion = new DRVersion();

            drVersion.Find(module);
            var record = drVersion.AddRecord();

            record.SetValue<string>(EF_Version.Module, module);
            record.SetValue<int>(EF_Version.Version, version);

            return drVersion.Update();
        }

        private static bool UpdateDBVersion(string module, int version)
        {
            var drVersion = new DRVersion();

            if (drVersion.Find(module))
            {
                drVersion.SetValue<int>(EF_Version.Version, version);
                return drVersion.Update();
            }

            return false;
        }
    }
}