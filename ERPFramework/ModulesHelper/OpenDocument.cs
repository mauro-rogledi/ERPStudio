using System;
using System.ComponentModel;
using ERPFramework.Data;

namespace ERPFramework.ModulesHelper
{
    public static class OpenDocument
    {
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public static event EventHandler<Tuple<object, bool>> ShowObject;

        public static object Create(NameSpace ns, object[] parameters= null)
        {
            object obj = DllManager.CreateIstance(ns, parameters);
            AttachAddOn(obj, ns);
            AttachPreferences(obj, ns);

            return obj;
        }

        public static object Show(object obj, bool modal = false)
        {
            if (ShowObject != null)
                ShowObject(null, new Tuple<object, bool>(obj, modal));

            return obj;
        }

        public static T Show<T>(object obj, bool modal = false)
        {
            if (ShowObject != null)
                ShowObject(null, new Tuple<object, bool>(obj, modal));

            return (T)System.Convert.ChangeType(obj, typeof(T));
        }

        public static object Show(NameSpace ns, object[] parameters = null, bool modal = false)
        {
            object obj = Create(ns, parameters);
            return Show(obj, modal);
        }

        public static T Show<T>(NameSpace ns, object[] parameters = null, bool modal = true)
        {
            object obj = Create(ns, parameters);
            return Show<T>(obj, modal);
        }

        private static void AttachAddOn(object obj, NameSpace ns)
        {
            if (obj is ERPFramework.Forms.IDocument)
            {
                foreach (ApplicationMenuModule amm in ModuleManager.ModuleList)
                {
                    string module = amm.Namespace.Module;
                    amm.Namespace.Application = "ModuleData.RegisterModule";
                    RegisterModule registerModule = (RegisterModule)DllManager.CreateIstance(amm.Namespace, null);
                    if (registerModule != null)
                    {
                        registerModule.Addon(obj as ERPFramework.Forms.IDocument, ns);
                    }
                }
            }
        }
        private static void AttachPreferences(object obj, NameSpace nameSpace)
        {
            if (obj is ERPFramework.Preferences.PreferenceForm)
            {
                ERPFramework.Preferences.PreferenceForm form = obj as ERPFramework.Preferences.PreferenceForm;
                RegisterModule registerModule = (RegisterModule)DllManager.CreateIstance(new NameSpace("ERPManager.ERPManager.ModuleData.RegisterModule"), null);
                if (registerModule != null)
                    form.AddPanel(registerModule.RegisterPreferences());

                foreach (ApplicationMenuModule amm in ModuleManager.ModuleList)
                {
                    string module = amm.Namespace.Module;
                    amm.Namespace.Application = "ModuleData.RegisterModule";
                    registerModule = (RegisterModule)DllManager.CreateIstance(amm.Namespace, null);
                    if (registerModule != null)
                        form.AddPanel(registerModule.RegisterPreferences());
                }
            }
        }
    }
}
