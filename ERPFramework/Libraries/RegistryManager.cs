using System.Diagnostics;
using Microsoft.Win32;

namespace ERPFramework.Libraries
{
    /// <summary>
    /// Summary description for RegistryManager.
    /// </summary>
    ///
    public class RegistryManager
    {
        private RegistryKey registrykey = Registry.CurrentUser;
        private string applicationName = string.Empty;
        private string applicationPath = string.Empty;

        public RegistryManager()
        {
        }

        public RegistryKey registryKey { set { registrykey = value; } get { return registrykey; } }

        public string ApplicationName { set { applicationName = value; } get { return applicationName; } }

        public string ApplicationPath { set { applicationPath = value; } get { return applicationPath; } }

        #region Enable/Disable AutoStart

        /// <summary>
        /// SetAutoStart
        /// Enable or Disable Autostart
        /// </summary>
        /// <param name="autostart"></param>
        public void SetAutoStart(bool autostart)
        {
            bool result = (autostart) ? EnableAutostart() : DisableAutostart();
        }

        public bool IsAutostartEnable()
        {
            Debug.Assert(applicationName.Length != 0, ERROR.NoApplicationName);
            using (RegistryKey myVal = registrykey.OpenSubKey(SS.AutoRunSubKey, true))
            {
                if (myVal.GetValue(applicationName) != null)
                    return true;
                else
                    return false;
            }
        }

        private bool EnableAutostart()
        {
            Debug.Assert(applicationName.Length != 0, ERROR.NoApplicationName);
            Debug.Assert(applicationPath.Length != 0, ERROR.NoApplicationPath);

            using (RegistryKey myVal = registrykey.OpenSubKey(SS.AutoRunSubKey, true))
            {
                myVal.SetValue(applicationName, applicationPath);
            }
            return true;
        }

        private bool DisableAutostart()
        {
            Debug.Assert(applicationName.Length != 0, ERROR.NoApplicationName);

            using (RegistryKey myVal = registrykey.OpenSubKey(SS.AutoRunSubKey, true))
            {
                if (myVal.GetValue(applicationName) != null)
                    myVal.DeleteValue(applicationName);
            }
            return true;
        }

        #endregion

        private struct SS
        {
            public const string AutoRunSubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        }

        private struct ERROR
        {
            public const string NoApplicationName = "Manca il nome dell'applicazione";
            public const string NoApplicationPath = "Manca il path dell'applicazione";
        }
    }
}