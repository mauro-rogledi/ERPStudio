using System;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Windows.Forms;

namespace ERPManager.Library
{
    public class InfoData
    {
        public string Version { get; set; }
        public string Filename { get; set; }
    }

    class CheckNewVersion
    {
        public static event EventHandler OpenForm;
        public static event EventHandler GiveMessage;
        public class parameters
        {
            public string ApplicationName { get; set; }
            public Version Version { get; set; }
            public bool Verbose { get; set; }
        }

        static public int CheckVersion(object a)
        {
            parameters param = a as parameters;
            string filename = ReadVersionFromFTP(param.ApplicationName, param.Version, param.Verbose);
            if (filename != string.Empty)
            {
                LaunchDownloader(param.ApplicationName, filename);
                return 1;
            }

            return 1;
        }

        static private string ReadVersionFromFTP(string appName, Version version, bool verbose)
        {
            string fileinfo = Path.ChangeExtension(appName, ".inf");
            MemoryStream ms = new MemoryStream();
            FtpUploader.DownloadFileInMemory(appName, fileinfo, ms);
            ms.Seek(0, SeekOrigin.Begin);
            XmlSerializer xSer = new XmlSerializer(typeof(InfoData));
            InfoData sData = (InfoData)xSer.Deserialize(ms);
            ms.Close();

            if (version < new Version(sData.Version))
            {
                if (OpenForm != null && !verbose)
                    OpenForm(null, EventArgs.Empty);
                if (verbose)
                    MessageBox.Show(Properties.Resources.Msg_NewVersion, appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (GiveMessage != null)
                    GiveMessage(null, EventArgs.Empty);
                return sData.Filename;
            }
            else
            {
                if (verbose)
                    MessageBox.Show(Properties.Resources.Msg_NoVersion, appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (GiveMessage != null)
                    GiveMessage(null, EventArgs.Empty);
                return string.Empty;
            }
        }

        static private void LaunchDownloader(string appname, string filename)
        {
            if (!Process.GetProcesses().Any(p => p.ProcessName.Equals("Updater")))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = string.Concat("\"", appname, "\" \"", filename, "\"");
                start.FileName = "Updater.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;
                start.UseShellExecute = true;
                start.Verb = "runas";
                Process pro = new Process();
                pro.StartInfo = start;
                pro.Start();
            }
        }
    }
}