using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ERPFramework;
using ERPFramework.Data;

namespace ERPManager
{
    public static class ModuleManager
    {
        public static List<ApplicationMenuModule> ModuleList;
        public static List<Version> ModuleVersion = new List<Version>();

        public static string ApplicationName { private set; get; }

        public static bool LoadModules()
        {
            string applPath = Path.GetDirectoryName(Application.ExecutablePath);
            string appConfname = Path.Combine(applPath, "ApplicationModules.config");

            if (!File.Exists(appConfname))
            {
                Debug.Assert(false, "Missing ApplicationModules.config");
                return false;
            }
            XmlDocument applModule = new XmlDocument();
            applModule.Load(appConfname);

            XmlNode modules = applModule.SelectSingleNode("modules");
            ApplicationName = modules.Attributes["name"].Value;
            ModuleList = new List<ApplicationMenuModule>(1);

            XmlNodeList moduleList = applModule.SelectNodes("modules/module");
            foreach (XmlNode modNode in moduleList)
            {
                string modulename = modNode.Attributes["name"].Value;
                NameSpace nameSpace = new NameSpace(modNode.Attributes["namespace"].Value);

                if (!LoadMenu(nameSpace))
                    return false;
            }
            return true;
        }

        private static bool LoadMenu(NameSpace nameSpace)
        {
            if (!DllManager.ExistsAssembly(nameSpace)) return false;

            string modulemenu = Path.Combine(Application.StartupPath, nameSpace.Library);
            string localize = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            string menufile = string.Concat("menu.", localize, ".config");

            string modulemenufile = Path.Combine(modulemenu, menufile);
            if (!File.Exists(modulemenufile))
                modulemenufile = Path.Combine(modulemenu, "menu.config");

            if (!File.Exists(modulemenufile))
            {
                Debug.Assert(false, "Missing menu.config");
                return false;
            }

            XmlDocument moduleMenu = new XmlDocument();
            moduleMenu.Load(modulemenufile);

            ApplicationMenuModule appModule;

            XmlNodeList moduleList = moduleMenu.SelectNodes("menu/module");
            foreach (XmlNode moduleNode in moduleList)
            {
                string menu = moduleNode.Attributes["name"].Value;
                string icon = moduleNode.Attributes["icon"].Value;
                string modulensc = moduleNode.Attributes["namespace"].Value;
                NameSpace nsc = new NameSpace(nameSpace.Folder, nameSpace.Library, modulensc);
                if (!RegisterModule(nsc))
                    continue;

                appModule = ModuleList.Find(p => { return p.Menu == menu; });
                if (appModule == null)
                {
                    appModule = new ApplicationMenuModule(nsc, menu, icon);
                    ModuleList.Add(appModule);
                }

                XmlNodeList folderList = moduleNode.SelectNodes("folder");
                LoadFolder(appModule, folderList);
            }
            return true;
        }

        private static void LoadFolder(ApplicationMenuModule appModuleOri, XmlNodeList folderList)
        {
            ApplicationMenuFolder appFolder;
            ApplicationMenuModule appModule = appModuleOri;

            foreach (XmlNode folderNode in folderList)
            {
                string folder = folderNode.Attributes["name"].Value;
                if (folderNode.Attributes["namespace"] != null)
                    appModule = ModuleList.Find(p => { return p.Namespace.Module == folderNode.Attributes["namespace"].Value; });
                else
                    appModule = appModuleOri;

                Debug.Assert(appModule != null, "implementare");

                appFolder = appModule.MenuFolders.Find(p => { return p.Folder == folder; });
                if (appFolder == null)
                {
                    appFolder = new ApplicationMenuFolder(folder);
                    appModule.MenuFolders.Add(appFolder);
                }

                XmlNodeList itemList = folderNode.SelectNodes("item");
                foreach (XmlNode menuNode in itemList)
                {
                    string itemName = menuNode.Attributes["name"].Value;
                    NameSpace nSpace = new NameSpace(menuNode.SelectSingleNode("namespace").InnerText);
                    string formtype = menuNode.SelectSingleNode("formtype").InnerText;
                    string usergrant = menuNode.SelectSingleNode("usergrant").InnerText;

                    DocumentType formType = (DocumentType)Enum.Parse(typeof(DocumentType), formtype);
                    UserType userType = (UserType)Enum.Parse(typeof(UserType), usergrant);

                    if (GlobalInfo.UserInfo.userType < userType)
                        continue;

                    appFolder.MenuItems.Add(new ApplicationMenuItem(itemName, nSpace, formType, userType));
                }
            }
        }

        private static bool RegisterModule(NameSpace nspace)
        {
            nspace.Application = "ModuleData.RegisterModule";
            RegisterModule registerTable = (RegisterModule)DllManager.CreateIstance(nspace, null);

            if (registerTable != null)
            {
                if (SerialManager.IsActivate(registerTable.Application(), registerTable.Module()))
                {
                    bool bOk = registerTable.CreateTable(GlobalInfo.DBaseInfo.dbManager.DB_Connection, GlobalInfo.UserInfo.userType);
                    if (bOk)
                        registerTable.RegisterCountersAndCodes();
                }
                else
                    return false;
            }
            return true;
        }
    }
}