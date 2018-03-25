using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    #region ProxyProviderLoader

    public static class ProxyProviderLoader
    {
        public enum ProviderType { SQL, SQLite };
        public static ProviderType ProviderTypes { get; set; } = ProviderType.SQL;
        public static Assembly assembly;

        public static void LoadProvider()
        {
            switch (ProviderTypes)
            {
                case ProviderType.SQL:
                    assembly = Assembly.LoadFrom(@"SQLProvider.dll");
                    break;
            }
        }

        public static T CreateInstance<T>(string nameSpace, params object[] parameters)
        {
            if (assembly == null)
                LoadProvider();

            Type classType = assembly.GetType(nameSpace);
            return (T)Activator.CreateInstance(classType, parameters);
        }
    }
    #endregion
}
