using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ERPFramework.ModulesHelper
{
    public static class DllManager
    {
        private static string StartupPath = Application.StartupPath;

        public static Object CreateIstance(NameSpace ns, object[] parameters = null)
        {
            Type Obj = LoadApplication(ns);
            if (Obj != null)
                return Activator.CreateInstance(Obj, parameters);
            else
            {
                Debug.Assert(false, string.Format("{0} not found", ns));
            }

            return null;
        }

        public static bool ExistsAssembly(NameSpace ns)
        {
            string dllName = Path.Combine(StartupPath, ns.Folder);
            dllName = Path.ChangeExtension(Path.Combine(dllName, ns.Library), ".dll");
            return File.Exists(dllName);
        }

        public static Type LoadApplication(NameSpace ns)
        {
            Assembly ass = System.Reflection.Assembly.Load(ns.Library);
            if (ass != null)
                return ass.GetType(ns.ApplicationPath);
            else
                return null;
        }
    }
}