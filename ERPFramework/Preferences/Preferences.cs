using ERPFramework.Data;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using MetroFramework.Extender;

namespace ERPFramework.Preferences
{
    public class PreferencesManager<T> where T : new()
    {
        private DRFindPreference DRFindPref;
        private DUPreference DRPref;
        private RRReadAllPreference RRAllPref;
        private MyToolStripItem currentToolStripItem;
        private Type[] AllTypes;

        public MetroLinkChecked ButtonComputer { set; private get; }

        public MetroLinkChecked ButtonUser { set; private get; }

        public MetroLinkChecked ButtonApplication { set; private get; }

        public string ComputerName { get { return System.Environment.MachineName; } }

        public string UserName { get { return GlobalInfo.UserInfo.User; } }

        public string ApplicationName { get; set; }

        public string PrefType
        {
            get
            {
                NameSpace ns = new NameSpace(typeof(T).ToString());
                return ns.Application != null
                            ? ns.Application
                            : ns.Module;
            }
        }

        public string LockKey
        {
            get
            {
                currentToolStripItem.CreateText();
                return currentToolStripItem.Text;
            }
        }

        public T Preference { get; set; }

        public PreferencesManager(string Application, Type[] types)
        {
            ApplicationName = Application;
            this.AllTypes = types;

            RRAllPref = new RRReadAllPreference(null);
            DRPref = new DUPreference(null);
            DRFindPref = new DRFindPreference(null);
            if (Preference == null)
                Preference = new T();
        }

        public void FillComboBox(MetroToolbarDropDownButton PrefList)
        {
            // For design mode
            if (GlobalInfo.SqlConnection == null)
                return;

            PrefList.Items.Clear();

            RRReadAllPreference DRRead = new RRReadAllPreference();
            if (DRRead.Find(PrefType, ComputerName, UserName, ApplicationName))
            {
                for (int t = 0; t < DRRead.Count; t++)
                {
                    string computer = DRRead.GetValue<string>(EF_Preferences.Computer, t);
                    string user = DRRead.GetValue<string>(EF_Preferences.Username, t);
                    string application = DRRead.GetValue<string>(EF_Preferences.Application, t);

                    MyToolStripItem tsi = new MyToolStripItem(computer, user, application);
                    PrefList.AddDropDownItem(tsi);
                }
            }
        }

        public string Serialize(T Pref)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                DataContractJsonSerializer serializer = null;
                if (AllTypes != null)
                    serializer = new DataContractJsonSerializer(typeof(T), AllTypes);
                else
                    serializer = new DataContractJsonSerializer(typeof(T));

                serializer.WriteObject(memoryStream, Pref);
                memoryStream.Seek(0, 0);
                StreamReader sr = new StreamReader(memoryStream);
                return sr.ReadToEnd();
            }
            catch
            {
                StringBuilder stringBuilder = new StringBuilder();
                System.Xml.Serialization.XmlSerializer x = null;
                if (AllTypes != null)
                    x = new System.Xml.Serialization.XmlSerializer(typeof(T), AllTypes);
                else
                    x = new System.Xml.Serialization.XmlSerializer(typeof(T));

                XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);
                x.Serialize(xmlWriter, Pref);
                return stringBuilder.ToString();
            }
        }

        public T Deserialize(string value)
        {
            T Pref = new T();
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                DataContractJsonSerializer serializer = null;
                if (AllTypes != null)
                    serializer = new DataContractJsonSerializer(typeof(T), AllTypes);
                else
                    serializer = new DataContractJsonSerializer(typeof(T));
                StreamWriter sw = new StreamWriter(memoryStream);
                sw.Write(value);
                sw.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);
                Pref = (T)serializer.ReadObject(memoryStream);
            }
            catch (Exception)
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(value))
                    Pref = (T)x.Deserialize(reader);
            }

            return Pref;
        }

        public T ReadPreference(MyToolStripItem tsi)
        {
            currentToolStripItem = tsi;
            currentToolStripItem.CreateText();
            UpdateToolbarButton(tsi);
            return ReadPreference(tsi.Computer, tsi.User, tsi.Application);
        }

        public bool ExistPreference()
        {
            return DRPref.Find(PrefType, currentToolStripItem.Computer, currentToolStripItem.User, currentToolStripItem.Application);
        }

        public bool SavePreference(T pref)
        {
            if (DRPref.Find(PrefType, currentToolStripItem.Computer, currentToolStripItem.User, currentToolStripItem.Application))
                DRPref.SetValue<string>(EF_Preferences.Preferences, Serialize(pref));
            else
                DRPref.AddRecord(Serialize(pref));
            return DRPref.Update();
        }

        public bool DeletePreference(T pref)
        {
            if (DRPref.Find(PrefType, currentToolStripItem.Computer, currentToolStripItem.User, currentToolStripItem.Application))
                DRPref.Delete();
            return true;
        }

        public T ReadPreference()
        {
            if (RRAllPref.Find(PrefType, GlobalInfo.ComputerInfo.ComputerName, GlobalInfo.UserInfo.User, ApplicationName))
                return Deserialize(RRAllPref.GetValue<string>(EF_Preferences.Preferences));
            return new T();
        }

        private T ReadPreference(string Computer, string User, string Application)
        {
            if (DRPref.Find(PrefType, Computer, User, Application))
                return Deserialize(DRPref.GetValue<string>(EF_Preferences.Preferences));
            return new T();
        }

        public void Clear()
        {
            currentToolStripItem = new MyToolStripItem();
            UpdateToolbarButton(currentToolStripItem);
        }

        public bool ButtonClick(ButtonClicked button)
        {
            switch (button)
            {
                case ButtonClicked.Computer:
                    currentToolStripItem.HasComputer = !currentToolStripItem.HasComputer;
                    ButtonComputer.Checked = currentToolStripItem.HasComputer;
                    currentToolStripItem.Computer = currentToolStripItem.HasComputer
                                ? ComputerName
                                : string.Empty;
                    return currentToolStripItem.HasComputer;

                case ButtonClicked.User:
                    currentToolStripItem.HasUser = !currentToolStripItem.HasUser;
                    ButtonUser.Checked = currentToolStripItem.HasUser;
                    currentToolStripItem.User = currentToolStripItem.HasUser
                            ? UserName
                            : string.Empty;
                    return currentToolStripItem.HasUser;

                case ButtonClicked.Application:
                    currentToolStripItem.HasApplication = !currentToolStripItem.HasApplication;
                    ButtonApplication.Checked = currentToolStripItem.HasApplication;
                    currentToolStripItem.Application = currentToolStripItem.HasApplication
                              ? ApplicationName
                              : string.Empty;
                    return currentToolStripItem.HasApplication;
                default:
                    return false;
            }
        }

        private void UpdateToolbarButton(MyToolStripItem tsi)
        {
            ButtonComputer.Checked = tsi.HasComputer;
            ButtonUser.Checked = tsi.HasUser;
            ButtonApplication.Checked = tsi.HasApplication;
        }
    }

    public class MyToolStripItem : System.Windows.Forms.ToolStripMenuItem
    {
        public bool HasComputer { get; set; }

        public bool HasUser { get; set; }

        public bool HasApplication { get; set; }

        public string Computer { get; set; }

        public string User { get; set; }

        public string Application { get; set; }

        public MyToolStripItem()
        {
            HasComputer = false;
            HasUser = false;
            HasApplication = false;
            Computer = string.Empty;
            User = string.Empty;
            Application = string.Empty;
        }

        public MyToolStripItem(string computer, string user, string application)
        {
            HasComputer = !string.IsNullOrWhiteSpace(computer);
            HasUser = !string.IsNullOrWhiteSpace(user);
            HasApplication = !string.IsNullOrWhiteSpace(application);
            Computer = computer;
            User = user;
            Application = application;

            CreateText();
        }

        public void CreateText()
        {
            Text = string.Empty;
            if (HasComputer)
                Text = Computer;

            if (HasUser)
            {
                Text += (Text.Length == 0)
                    ? User
                    : " " + User;
            }

            if (HasApplication)
            {
                Text += (Text.Length == 0)
                    ? Application
                    : " " + Application;
            }

            if (Text.Length == 0)
                Text = "All Configuration";
        }
    }
}