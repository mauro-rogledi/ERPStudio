using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ERPFramework.Data;

namespace ERPFramework.ModulesHelper
{
    public static class ModuleManager
    {
        public static List<ApplicationMenuModule> ModuleList;
        public static List<Version> ModuleVersion = new List<Version>();

        public static string ApplicationName { private set; get; }

        public static bool LoadModules()
        {
            var applPath = Path.GetDirectoryName(Application.ExecutablePath);
            var appConfname = Path.Combine(applPath, "ApplicationModules.config");

            if (!File.Exists(appConfname))
            {
                Debug.Assert(false, "Missing ApplicationModules.config");
                return false;
            }
            var applModule = new XmlDocument();
            applModule.Load(appConfname);

            var modules = applModule.SelectSingleNode("modules");
            ApplicationName = modules.Attributes["name"].Value;
            ModuleList = new List<ApplicationMenuModule>(1);

            var moduleList = applModule.SelectNodes("modules/module");
            foreach (XmlNode modNode in moduleList)
            {
                string modulename = modNode.Attributes["name"].Value;
                var nameSpace = new NameSpace(modNode.Attributes["namespace"].Value);

                if (!LoadMenu(nameSpace))
                    return false;
            }
            return true;
        }

        private static bool LoadMenu(NameSpace nameSpace)
        {
            if (!DllManager.ExistsAssembly(nameSpace)) return false;

            var modulemenu = Path.Combine(Application.StartupPath, nameSpace.Library);
            var localize = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            var menufile = string.Concat("menu.", localize, ".config");

            var modulemenufile = Path.Combine(modulemenu, menufile);
            if (!File.Exists(modulemenufile))
                modulemenufile = Path.Combine(modulemenu, "menu", "menu.config");

            if (!File.Exists(modulemenufile))
            {
                Debug.Assert(false, "Missing menu.config");
                return false;
            }

            var moduleMenu = new XmlDocument();
            moduleMenu.Load(modulemenufile);

            ApplicationMenuModule appModule;

            var moduleList = moduleMenu.SelectNodes("menu/module");
            foreach (XmlNode moduleNode in moduleList)
            {
                var menu = moduleNode.Attributes["name"].Value;
                var icon = moduleNode.Attributes["icon"].Value;
                var modulensc = moduleNode.Attributes["namespace"].Value;
                var nsc = new NameSpace(nameSpace.Folder, nameSpace.Library, modulensc);
                if (!RegisterModule(nsc))
                    continue;

                appModule = ModuleList.Find(p => { return p.Menu == menu; });
                if (appModule == null)
                {
                    appModule = new ApplicationMenuModule(nsc, menu, icon);
                    ModuleList.Add(appModule);
                }

                var folderList = moduleNode.SelectNodes("folder");
                LoadFolder(appModule, folderList);
            }
            return true;
        }

        private static void LoadFolder(ApplicationMenuModule appModuleOri, XmlNodeList folderList)
        {
            ApplicationMenuFolder appFolder;
            var appModule = appModuleOri;

            foreach (XmlNode folderNode in folderList)
            {
                var folder = folderNode.Attributes["name"].Value;
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

                var itemList = folderNode.SelectNodes("item");
                foreach (XmlNode menuNode in itemList)
                {
                    var itemName = menuNode.Attributes["name"].Value;
                    var nSpace = new NameSpace(menuNode.SelectSingleNode("namespace").InnerText);
                    var formtype = menuNode.SelectSingleNode("formtype").InnerText;
                    var usergrant = menuNode.SelectSingleNode("usergrant").InnerText;

                    var formType = (DocumentType)Enum.Parse(typeof(DocumentType), formtype);
                    var userType = (UserType)Enum.Parse(typeof(UserType), usergrant);

                    if (GlobalInfo.UserInfo.userType < userType)
                        continue;

                    appFolder.MenuItems.Add(new ApplicationMenuItem(itemName, nSpace, formType, userType));
                }
            }
        }

        private static bool RegisterModule(NameSpace nspace)
        {
            nspace.Application = "ModuleData.RegisterModule";
            var registerTable = (RegisterModule)DllManager.CreateIstance(nspace, null);

            var bOk = true;
            if (registerTable != null)
            {
                if (SerialManager.IsActivate(registerTable.Application(), registerTable.Module()) != ActivationState.NotActivate)
                {
                    bOk = registerTable.CreateTable(GlobalInfo.DBaseInfo.dbManager.DB_Connection, GlobalInfo.UserInfo.userType);
                    if (bOk)
                        registerTable.RegisterCountersAndCodes();
                }
                else
                    return false;
            }
            return bOk;
        }
    }
}