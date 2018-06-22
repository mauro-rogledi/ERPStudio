using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ERPFramework.Data;

namespace ERPFramework.ModulesHelper
{
    public static class ModuleManager
    {
        public static List<ApplicationMenuModule> ModuleList = new List<ApplicationMenuModule>();
        public static List<Version> ModuleVersion = new List<Version>();

        public static bool LoadModules()
        {
            var lang = GlobalInfo.globalPref.Language;
            ActivationManager.Modules.
                Where(k => (k.Value.State & ActivationState.Activate) == ActivationState.Activate).ToList().
                ForEach(module => LoadMenu(module.Value)
            );
            return true;
        }

        private static bool LoadMenu(ActivationModuleMemory module)
        {
            var applPath = Path.GetDirectoryName(Application.ExecutablePath);
            var localize = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            var menuFolder = Path.Combine(applPath, module.ModulePath, "menu");
            var menufile = string.Concat("menu.", localize, ".config");

            var modulemenufile = Path.Combine(menuFolder, menufile);
            if (!File.Exists(modulemenufile))
                modulemenufile = Path.Combine(menuFolder, "menu.config");

            if (!File.Exists(modulemenufile))
            {
                Debug.Assert(false, "Missing menu.config");
                return false;
            }

            var moduleMenu = new XmlDocument();
            moduleMenu.Load(modulemenufile);

            ApplicationMenuModule appModule;

            var moduleList = moduleMenu.SelectNodes("menu/module");
            moduleList.Cast<XmlNode>().ToList().ForEach(moduleNode =>
           {
               if (RegisterModule(module.NameSpace))
               {
                   var menu = moduleNode.Attributes["name"].Value;
                   var icon = moduleNode.Attributes["icon"].Value;
                   appModule = ModuleList.Find(p => { return p.Menu == menu; });
                   if (appModule == null)
                   {
                       appModule = new ApplicationMenuModule(module.NameSpace, menu, icon);
                       ModuleList.Add(appModule);
                   }

                   var folderList = moduleNode.SelectNodes("folder");
                   LoadFolder(appModule, folderList);
               }

           });
            return true;
        }

        private static void LoadFolder(ApplicationMenuModule appModuleOri, XmlNodeList folderList)
        {
            ApplicationMenuFolder appFolder;
            var appModule = appModuleOri;

            folderList.Cast<XmlNode>().ToList().ForEach(folderNode =>
           {
               var folder = folderNode.Attributes["name"].Value;
               appModule = folderNode.Attributes["namespace"] != null
                                ? ModuleList.Find(p => { return p.Namespace.Module == folderNode.Attributes["namespace"].Value; })
                                : appModuleOri;

               Debug.Assert(appModule != null, "implementare");

               appFolder = appModule.MenuFolders.Find(p => { return p.Folder == folder; });
               if (appFolder == null)
               {
                   appFolder = new ApplicationMenuFolder(folder);
                   appModule.MenuFolders.Add(appFolder);
               }

               folderNode.SelectNodes("item").Cast<XmlNode>().ToList().ForEach(menuNode =>
               {

                   var itemName = menuNode.Attributes["name"].Value;
                   var nSpace = new NameSpace(menuNode.SelectSingleNode("namespace").InnerText);
                   var formtype = menuNode.SelectSingleNode("formtype").InnerText;
                   var usergrant = menuNode.SelectSingleNode("usergrant").InnerText;

                   var formType = (DocumentType)Enum.Parse(typeof(DocumentType), formtype);
                   var userType = (UserType)Enum.Parse(typeof(UserType), usergrant);

                   if (GlobalInfo.UserInfo.userType >= userType)
                       appFolder.MenuItems.Add(new ApplicationMenuItem(itemName, nSpace, formType, userType));
               });
           });
        }

        private static bool RegisterModule(NameSpace nameSpace)
        {
            var nspace = new NameSpace(nameSpace)
            {
                Application = "ModuleData.RegisterModule"
            };

            var registerTable = (RegisterModule)DllManager.CreateIstance(nspace, null);

            var bOk = true;
            if (registerTable != null)
            {
                if (ActivationManager.IsActivate(registerTable.Module()) != ActivationState.NotActivate)
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