using ERPFramework.Data;
using ERPFramework.Preferences;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ERPFramework
{
    public static class GlobalInfo
    {
        public static DateTime CurrentDate;

        public static Dictionary<int, string> CounterTypes = new Dictionary<int, string>();
        public static Dictionary<string, string> CodeTypes = new Dictionary<string, string>();

        public static UserInfo UserInfo = new UserInfo();
        public static DBaseInfo DBaseInfo = new DBaseInfo();
        public static LoginInfo LoginInfo = new LoginInfo();
        public static ComputerInfo ComputerInfo = new ComputerInfo();
        public static GlobalPreferences globalPref;

        public static Dictionary<string,TableDefinition> Tables = new Dictionary<string, TableDefinition>();

        public static SqlProxyConnection SqlConnection
        {
            get
            {
                System.Diagnostics.Debug.Assert(DBaseInfo.dbManager != null);
                System.Diagnostics.Debug.Assert(DBaseInfo.dbManager.DB_Connection != null);

                return DBaseInfo.dbManager.DB_Connection;
            }
        }

        public delegate MetroUserControl GlobalInfoControlEventHandler(object sender, GlobalInfoFormArgs pe);
        public delegate MetroForm GlobalInfoFormEventHandler(object sender, GlobalInfoFormArgs pe);

        public static Form MainForm { get; set; }
        public static MetroFramework.Components.MetroStyleManager StyleManager { get; set; }

        public static void AddCounter(int key, string value)
        {
            System.Diagnostics.Debug.Assert(!CounterTypes.ContainsKey(key), "Counter duplicated");
            CounterTypes.Add(key, value);
        }

        public static void AddCodeType(string key, string value)
        {
            System.Diagnostics.Debug.Assert(key.Length <= 8, "Counterkey too long");
            System.Diagnostics.Debug.Assert(!CodeTypes.ContainsKey(key), "Counter duplicated");
            CodeTypes.Add(key, value);
        }

        public static int GetCounter(string value)
        {
            System.Diagnostics.Debug.Assert(CounterTypes.ContainsValue(value), "Unknown counter");

            foreach (KeyValuePair<int, string> kvp in CounterTypes)
                if (kvp.Value == value)
                    return kvp.Key;
            return -1;
        }

        public static string GetCodeType(string value)
        {
            System.Diagnostics.Debug.Assert(CodeTypes.ContainsValue(value), "Unknown codetype");

            foreach (KeyValuePair<string, string> kvp in CodeTypes)
                if (kvp.Value == value)
                    return kvp.Key;
            return null;
        }
    }

    public class GlobalInfoFormArgs : EventArgs
    {
        private NameSpace nameSpace;
        private bool modal;
        private bool showDialog;
        private DocumentType formType;
        private MetroUserControl userControl;
        private MetroForm userForm;

        public NameSpace Namespace { get { return nameSpace; } }

        public bool Modal { get { return modal; } }

        public bool ShowDialog { get { return showDialog; } }

        public DocumentType FormType { get { return formType; } }

        public MetroUserControl MetroUserControl { get { return userControl; } }
        public MetroForm MetroForm { get { return userForm; } }

        public GlobalInfoFormArgs(MetroForm userform, bool modal)
        {
            userForm = userform;
            this.modal = modal;
        }

        public GlobalInfoFormArgs(MetroUserControl usercontrol, bool modal)
        {
            userControl = usercontrol;
            this.modal = modal;
        }

        public GlobalInfoFormArgs(DocumentType formtype, string nameSpace, bool modal, bool showdialog)
        {
            this.nameSpace = new NameSpace(nameSpace);
            this.modal = modal;
            formType = formtype;
            this.showDialog = showdialog;
        }
    }

    public class UserInfo
    {
        public string User = string.Empty;
        public UserType userType = UserType.Administrator;
    }

    public class DBaseInfo
    {
        public SqlManager dbManager;
        public UserType userType = UserType.Administrator;
    }

    [Serializable()]
    public class LoginInfo
    {
        [XmlElement()]
        public ProviderType ProviderType { get; set; }
        [XmlElement()]
        public string DataSource { get; set; }
        [XmlElement()]
        public string InitialCatalog { get; set; }
        [XmlElement()]
        public AuthenticationMode AuthenicationMode { get; set; }
        [XmlElement()]
        public string UserID { get; set; }
        [XmlElement()]
        public string Password { get; set; }
        [XmlElement()]
        public bool RememberLastLogin { get; set; }
        [XmlElement()]
        public string LastUser { get; set; }
        [XmlElement()]
        public string LastPassword { get; set; }
        [XmlArrayItem()]
        public List<string> UserList = new List<string>();
        //[XmlIgnore]
        //public string ConnectionString { get; set; }
    }

    public class PrintInfo
    {
        public string ReportName { get; set; }

        public PrintType PrintType { get; set; }

        public PrintInfo(string reportName, PrintType printType)
        {
            ReportName = reportName;
            PrintType = printType;
        }
    }

    public class ComputerInfo
    {
        private System.Drawing.Printing.PrinterSettings oPS = new System.Drawing.Printing.PrinterSettings();

        public string[] Printers;

        public ComputerInfo()
        {
            Printers = new string[System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count];
            System.Drawing.Printing.PrinterSettings.InstalledPrinters.CopyTo(Printers, 0);
        }

        public string ComputerName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        public string UserName
        {
            get
            {
                return System.Environment.UserName;
            }
        }

        public string DefaultPrinter
        {
            get
            {
                return oPS.PrinterName;
            }
        }

        public short DefaultCopies
        {
            get
            {
                return oPS.Copies;
            }
        }
    }

    public class TableDefinition
    {
        public string Module { get; set; }
        public Type Table { get; set; }

        public bool ToExport { get; set; }

        public bool IsVirtual { get; set; }
    }

    public class NameSpace
    {
        public string Folder { get; set; }

        public string Library { get; set; }

        public string Module { get; set; }

        public string Application { get; set; }

        public NameSpace() { }

        public NameSpace(NameSpace nspace)
        {
            this.Folder = nspace.Folder;
            this.Library = nspace.Library;
            this.Module = nspace.Module;
            this.Application = nspace.Application;
        }

        public NameSpace(string Folder, string Library)
        {
            this.Folder = Folder;
            this.Library = Library;
            this.Application = "";
            this.Module = "";
        }

        public NameSpace(string Folder, string Library, string Module)
        {
            this.Folder = Folder;
            this.Library = Library;
            this.Module = Module;
        }

        public NameSpace(string Folder, string Library, string Module, string Application)
        {
            this.Folder = Folder;
            this.Library = Library;
            this.Module = Module;
            this.Application = Application;
        }

        public NameSpace(string ns)
        {
            var splitted = ns.Split(new char[] { '.' });

            if (splitted.Length > 0) Folder = splitted[0];
            if (splitted.Length > 1) Library = splitted[1];
            if (splitted.Length > 2) Module = splitted[2];
            if (splitted.Length > 3) Application += string.Concat(splitted[3]);
            if (splitted.Length > 4) Application += string.Concat(".", splitted[4]);
            if (splitted.Length > 5) Application += string.Concat(".", splitted[5]);
        }

        public string ModulePath
        {
            get { return string.Concat(Folder, ".", Library, ".", Module); }
        }

        public string ApplicationPath
        {
            get
            {
                var val = string.Empty;
                if (Library.Length != 0)
                    val = string.Concat(Library, ".");
                if (Module.Length != 0)
                    val = string.Concat(val, Module, ".");
                if (Application.Length != 0)
                    val = string.Concat(val, Application);
                return val;
            }
        }

        public override string ToString()
        {
            var ns = string.Concat(Library, ".", Module);
            if (Application.Length != 0)
                ns = string.Concat(ns, ".", Application);
            return ns;
        }
    }

    #region Menu Collection

    public class ApplicationMenuModule : Object
    {
        public List<ApplicationMenuFolder> MenuFolders;

        public string Menu { get; private set; }

        public string Image { get; private set; }

        public NameSpace Namespace { get; private set; }

        public ApplicationMenuModule(NameSpace nameSpace, string menu, string image)
        {
            MenuFolders = new List<ApplicationMenuFolder>(1);
            this.Namespace = nameSpace;
            this.Menu = menu;
            this.Image = image;
        }
    }

    public class ApplicationMenuFolder : Object
    {
        public List<ApplicationMenuItem> MenuItems;

        public string Folder { get; private set; }

        public ApplicationMenuFolder(string folder)
        {
            this.Folder = folder;
            MenuItems = new List<ApplicationMenuItem>(1);
        }
    }

    [CollectionDataContract()]
    public class ApplicationMenuItem : Object
    {
        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Namespace")]
        public NameSpace Namespace { get; set; }

        [DataMember(Name = "DocumentType")]
        public DocumentType DocumentType { get; set; }

        public UserType UserGrant { get; set; }

        public ApplicationMenuItem()
        { }

        public ApplicationMenuItem(string Title, NameSpace Namespace, DocumentType documentType, UserType userType)
        {
            this.Title = Title;
            this.Namespace = Namespace;
            this.DocumentType = documentType;
            this.UserGrant = userType;
        }
    }

    #endregion

    public static class ERPFrameworkTranslator
    {
        public static string Translate(string val)
        {
            return Properties.Resources.ResourceManager.GetString(val);
        }
    }

    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter)]
    internal sealed class LocalizedCategory : CategoryAttribute
    {
        public LocalizedCategory(string value) : base(value)
        {

        }
        protected override string GetLocalizedString(string value)
        {
            // TODO: lookup from resx, perhaps with cache etc
            return ERPFrameworkTranslator.Translate(value);
        }
    }

    internal sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string resourceName;
        public LocalizedDisplayNameAttribute(string resourceName)
            : base()
        {
            this.resourceName = resourceName;
        }

        public override string DisplayName
        {
            get
            {
                try
                {
                    var val = ERPFrameworkTranslator.Translate(resourceName);
                    return val;
                }
                catch (Exception)
                {
                    return this.resourceName;
                }
            }
        }
    }
}