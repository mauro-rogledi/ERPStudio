using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Win32;

namespace ERPFramework.Libraries
{
    /// <summary>
    /// Summary description for RegistryManager.
    /// </summary>
    ///
    public static class RegistryManager
    {
        private static RegistryKey registrykey = Registry.CurrentUser;
        private static string applicationName = string.Empty;
        private static string applicationPath = string.Empty;

        const string AutoRunSubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static RegistryKey registryKey { set { registrykey = value; } get { return registrykey; } }

        public static string ApplicationName { set { applicationName = value; } get { return applicationName; } }

        public static string ApplicationPath { set { applicationPath = value; } get { return applicationPath; } }

        #region Enable/Disable AutoStart

        /// <summary>
        /// SetAutoStart
        /// Enable or Disable Autostart
        /// </summary>
        /// <param name="autostart"></param>
        public static void SetAutoStart(bool autostart)
        {
            bool result = (autostart) ? EnableAutostart() : DisableAutostart();
        }

        public static bool IsAutostartEnable()
        {
            Debug.Assert(applicationName.Length != 0, Properties.Resources.NoApplicationName);
            using (RegistryKey myVal = registrykey.OpenSubKey(AutoRunSubKey, true))
            {
                if (myVal.GetValue(applicationName) != null)
                    return true;
                else
                    return false;
            }
        }

        private static bool EnableAutostart()
        {
            Debug.Assert(applicationName.Length != 0, Properties.Resources.NoApplicationName);
            Debug.Assert(applicationPath.Length != 0, Properties.Resources.NoApplicationPath);

            using (RegistryKey myVal = registrykey.OpenSubKey(AutoRunSubKey, true))
            {
                myVal.SetValue(applicationName, applicationPath);
            }
            return true;
        }

        private static bool DisableAutostart()
        {
            Debug.Assert(applicationName.Length != 0, Properties.Resources.NoApplicationName);

            using (RegistryKey myVal = registrykey.OpenSubKey(AutoRunSubKey, true))
            {
                if (myVal.GetValue(applicationName) != null)
                    myVal.DeleteValue(applicationName);
            }
            return true;
        }

        #endregion

        public static IEnumerable<string> ListLocalSqlInstances()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    foreach (string item in ListLocalSqlInstances(hive))
                    {
                        yield return item;
                    }
                }

                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    foreach (string item in ListLocalSqlInstances(hive))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                foreach (string item in ListLocalSqlInstances(Registry.LocalMachine))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<string> ListLocalSqlInstances(RegistryKey hive)
        {
            const string keyName = @"Software\Microsoft\Microsoft SQL Server";
            const string valueName = "InstalledInstances";
            const string defaultName = "MSSQLSERVER";

            using (var key = hive.OpenSubKey(keyName, false))
            {
                if (key == null) return Enumerable.Empty<string>();

                var value = key.GetValue(valueName) as string[];
                if (value == null) return Enumerable.Empty<string>();

                for (int index = 0; index < value.Length; index++)
                {
                    if (string.Equals(value[index], defaultName, StringComparison.OrdinalIgnoreCase))
                    {
                        value[index] = ".";
                    }
                    else
                    {
                        value[index] = @"\" + value[index];
                    }
                }

                return value;
            }
        }
    }
}