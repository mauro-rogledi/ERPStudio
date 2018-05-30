using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Linq;

namespace ERPFramework.ModulesHelper
{
    [Flags]
    public enum SerialType : ushort
    {
        ONLY_NAME = 0x0000,
        MAC_ADDRESS = 0x0001,
        LICENSE_NAME = 0x0002,
        EXPIRATION_DATE = 0x0004,
        PEN_DRIVE = 0x0008,
        TRIAL = 0x0010
    }

    public enum ActivationState
    {
        Activate,
        NotActivate,
        Trial
    }

    public class ActivationDataMemory
    {
        public string Application { get; set; }
        public string License { get; set; }

        public string PenDrive { get; set; }

        public Dictionary<string, ActivationModuleMemory> Modules = new Dictionary<string, ActivationModuleMemory>();
    }

    public class ActivationModuleMemory
    {
        public bool Enabled { get; set; }

        public ActivationState State { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public SerialType SerialType;

        public DateTime Expiration { get; set; }

        public string SerialNo { get; set; }

        public List<string> Functionality { get; set; } = new List<string>();
    }

    [Serializable()]
    public class ActivationDataSave
    {
        public string Application { get; set; }
        public string License { get; set; }

        public string PenDrive { get; set; }

        public Dictionary<string, ActivationModuleSave> Modules = new Dictionary<string, ActivationModuleSave>();

        public void AddModule(string name, bool enabled, string serialNo)
        {
            if (Modules.ContainsKey(name))
                throw new Exception("Duplicate module");

            Modules.Add(name, new ActivationModuleSave { Enabled = enabled, SerialNo = serialNo });
        }
    }

    public class ActivationModuleSave
    {
        public bool Enabled { get; set; }
        public ActivationState State { get; set; }
        public string SerialNo { get; set; }
    }

    public static class ActivationManager
    {
        public static ActivationDataSave activationDataSave;
        public static ActivationDataMemory activationDataMemory = new ActivationDataMemory();
        private static string macAddres = ReadMacAddress();

        public static string ApplicationName { get; private set; }

        public static string License
        {
            get => activationDataMemory.License;
            set => activationDataMemory.License = value;
        }

        public static string PenDrive
        {
            get => activationDataMemory.PenDrive;
            set => activationDataMemory.PenDrive = value;
        }

        public static ActivationModuleMemory Module(string module) => activationDataMemory.Modules[module];
        public static Dictionary<string, ActivationModuleMemory> Modules { get => activationDataMemory.Modules; }
        public static void Clear()
        {
            activationDataSave.Modules.Clear();
            activationDataMemory.Modules.Clear();
        }

        public static bool Load()
        {
            Loadconfiguration();
            var activationFound = LoadFromActivationFile();
            LoadActivationFromModules();
            return activationFound;
        }

        private static bool Loadconfiguration()
        {
            var path = Path.Combine(Application.StartupPath, "applicationConfig.xml");
            if (!File.Exists(path))
                return false;

            var xDoc = new XmlDocument();
            xDoc.Load(path);

            activationDataMemory.Application = xDoc.SelectSingleNode("application").Attributes["name"].Value;
            xDoc.SelectNodes("name/module").Cast<XmlNode>().ToList().ForEach(n =>
            {
                var moduleName = n.InnerText;
                activationDataMemory.Modules.Add(moduleName, new ActivationModuleMemory { Name = moduleName });
            });

            return true;
        }

        private static bool LoadFromActivationFile()
        {
            if (!File.Exists(filekey()))
                return false;

            var memStr = new MemoryStream();
            using (FileStream myFS = new FileStream(filekey(), FileMode.Open))
            {
                using (GZipStream gZip = new GZipStream(myFS, CompressionMode.Decompress))
                    gZip.CopyTo(memStr);
            }
            memStr.Position = 0;
            var myBF = new BinaryFormatter();
            activationDataSave = (ActivationDataSave)myBF.Deserialize(memStr);

            activationDataSave.Application = ConvertFrom64(activationDataSave.Application);
            activationDataSave.License = ConvertFrom64(activationDataSave.License);
            activationDataSave.PenDrive = ConvertFrom64(activationDataSave.PenDrive);

            foreach (var module in activationDataSave.Modules)
                module.Value.SerialNo = ConvertFrom64(module.Value.SerialNo);

            return true;
        }

        public static void Save()
        {
            var directory = Path.GetDirectoryName(filekey());
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var memStr = new MemoryStream();
            var myBF = new BinaryFormatter();
            if (activationDataSave == null)
                activationDataSave = new ActivationDataSave();

            activationDataSave.Application = ConvertTo64(activationDataMemory.Application);
            activationDataSave.License = ConvertTo64(activationDataMemory.License);
            activationDataSave.PenDrive = ConvertTo64(activationDataSave.PenDrive);

            foreach (var Module in activationDataSave.Modules)
                Module.Value.SerialNo = ConvertTo64(Module.Value.SerialNo);

            myBF.Serialize(memStr, activationDataSave);

            using (FileStream myFS = new FileStream(filekey(), FileMode.Create))
            using (GZipStream gzip = new GZipStream(myFS, CompressionMode.Compress, false))
                gzip.Write(memStr.ToArray(), 0, (int)memStr.Length);
        }

        public static void LoadActivationFromModules()
        {
            var applPath = Path.GetDirectoryName(Application.ExecutablePath);
            var dirs = Directory.GetDirectories(applPath);

            foreach (var dir in dirs)
            {
                var menuDir = Path.Combine(dir, "Menu");
                if (Directory.Exists(menuDir))
                {
                    LoadActivatioModule(menuDir);
                    //var res = activationDataSave?.Modules.ContainsKey(module.Code);
                    //if (activationDataSave?.Modules.ContainsKey(module.Code) ?? false)
                    //{
                    //    module.SerialNo = activationDataSave.Modules[module.Code].SerialNo;
                    //    module.Enabled = activationDataSave.Modules[module.Code].Enabled;
                    //}

                    //activationDataMemory.Modules.Add(module.Code, module);
                }
            }

            activationDataMemory.License = activationDataSave?.License;
            activationDataMemory.PenDrive = activationDataSave?.PenDrive;
        }

        private static void LoadActivatioModule(string menuDir)
        {
#if DEBUG
            var activationName = "activation.xml";
#else
            var activationName = "activation.cml";
#endif
            var activationFile = new XmlDocument();
            activationFile.Load(Path.Combine(menuDir, activationName));

            var applicationNode = activationFile.SelectSingleNode("application");
            var application = applicationNode.Attributes["name"];
            var moduleNode = applicationNode.SelectNodes("application/module");

            moduleNode.Cast<XmlNode>().ToList().ForEach(n =>
            {
                var module = new ActivationModuleMemory
                {
                    Name = n.Attributes["name"].Value,
                    Code = n.Attributes["code"].Value,
                    SerialType = (SerialType)Enum.Parse(typeof(SerialType), n.Attributes["serialType"].Value),
                    Enabled = false,
                    SerialNo = ""
                };

                module.Expiration = module.SerialType.HasFlag(SerialType.EXPIRATION_DATE)
                    ? DateTime.Parse(n.Attributes["expirationDate"].Value)
                    : DateTime.MaxValue;

                var functionalityNode = n.SelectNodes("functionality");
                functionalityNode.Cast<XmlNode>().ToList().ForEach(f =>
                    module.Functionality.Add(f.InnerText)
                );

                activationDataMemory.Modules.Add(module.Code, module);
            });


            //var serial = new ActivationModuleMemory
            //var serial = new ActivationModuleMemory
            //{
            //    Name = moduleNode.Attributes["name"].Value,
            //    Code = moduleNode.Attributes["code"].Value,
            //    SerialType = (SerialType)Enum.Parse(typeof(SerialType), moduleNode.Attributes["serialType"].Value),
            //    Enabled = false,
            //    SerialNo = ""
            //};
            //if (serial.SerialType.HasFlag(SerialType.EXPIRATION_DATE))
            //{
            //    if (DateTime.TryParse(moduleNode.Attributes["expirationDate"].Value, out DateTime expirationDate))
            //        serial.Expiration = expirationDate;
            //}
            //var functionality = activationFile.SelectNodes("module/functionality");
            //foreach (XmlNode node in functionality)
            //    serial.Functionality.Add(node.InnerText);

            //return serial;
        }

        //public static void AddModule(bool enable, string module, SerialType sType, DateTime expiration, string serial)
        //{
        //    var sd = new SerialModule
        //    {
        //        SerialType = sType,
        //        SerialNo = serial
        //    };

        //    activationData.Modules.Add(sd);
        //}

        public static ActivationState IsActivate(string name)
        {
            if (!activationDataMemory.Modules.ContainsKey(name))
                return ActivationState.NotActivate;

            var sm = activationDataMemory.Modules[name];
            if (sm == null || !sm.Enabled)
                return ActivationState.NotActivate;

            if (!SerialFormatIsOk(sm.SerialNo, sm.Name))
                return ActivationState.NotActivate;

            if (!CheckSerialType(sm))
                return ActivationState.NotActivate;

            return sm.SerialType.HasFlag(SerialType.TRIAL)
                ? ActivationState.Trial
                : ActivationState.Activate;
        }

        private static string ConvertTo64(string text)
        {
            var textchar = text.ToCharArray();
            var textbyte = new byte[textchar.Length];

            for (int t = 0; t < textchar.Length; t++)
                textbyte[t] = (byte)textchar[t];
            return Convert.ToBase64String(textbyte);
        }

        public static string CreateSerial(string license, string macAddress, string module, SerialType sType, DateTime expiration, string pendrive)
        {
            string serial = string.Empty;

            concat(ref serial, ConvertString(module));
            if (sType.HasFlag(SerialType.LICENSE_NAME))
                concat(ref serial, ConvertString(license));
            if (sType.HasFlag(SerialType.MAC_ADDRESS))
                concat(ref serial, ConvertMacAddress(macAddress));

            if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                concat(ref serial, ConvertToString((UInt64)(expiration.Year * 365 + expiration.Month * 31 + expiration.Day)));

            if (sType.HasFlag(SerialType.PEN_DRIVE))
            {
                var letter = USBSerialNumber.GetDriveLetterFromName(pendrive);
                concat(ref serial, ConvertSerialNumber(USBSerialNumber.getSerialNumberFromDriveLetter(letter)));
            }

            if (sType.HasFlag(SerialType.TRIAL))
                concat(ref serial, ConvertString("TRIAL VERSION"));

            // Checksum
            concat(ref serial, ConvertString(serial));
            return serial;
        }

        private static bool SerialFormatIsOk(string serial, string module)
        {
            var serialCheck = string.Empty;
            var parts = serial.Split(new char[] { '-' });
            for (int t = 0; t < parts.Length - 1; t++)
                concat(ref serialCheck, parts[t]);

            if (ConvertString(serialCheck) != parts[parts.Length - 1])
                return false;

            if (ConvertString(module) != parts[0])
                return false;

            return true;
        }

        private static bool CheckSerialType(ActivationModuleMemory sm)
        {
            var pos = 1;
            var parts = sm.SerialNo.Split(new char[] { '-' });

            if (sm.SerialType.HasFlag(SerialType.LICENSE_NAME))
                if (parts[pos++] != ConvertString(activationDataSave.License))
                    return false;

            if (sm.SerialType.HasFlag(SerialType.MAC_ADDRESS))
                if (parts[pos++] != ConvertMacAddress(macAddres))
                    return false;

            if (sm.SerialType.HasFlag(SerialType.EXPIRATION_DATE))
                if (ConvertFromString(parts[pos++]) < (UInt64)(GlobalInfo.CurrentDate.Year * 365 + GlobalInfo.CurrentDate.Month * 31 + GlobalInfo.CurrentDate.Day))
                    return false;

            if (sm.SerialType.HasFlag(SerialType.PEN_DRIVE))
            {
                var letter = USBSerialNumber.GetDriveLetterFromName(activationDataSave.PenDrive);
                if (letter == string.Empty || parts[pos++] != ConvertSerialNumber(USBSerialNumber.getSerialNumberFromDriveLetter(letter)))
                    return false;
            }

            return true;
        }

        private static void concat(ref string a, string b)
        {
            a = (a == string.Empty)
                ? b
                : string.Concat(a, "-", b);
        }

        public static string ReadMacAddress()
        {
            using (var searcher = new ManagementObjectSearcher
            ("Select MACAddress,PNPDeviceID FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL AND PNPDeviceID IS NOT NULL"))
            {
                var mObject = searcher.Get();

                foreach (ManagementObject obj in mObject)
                {
                    var pnp = obj["PNPDeviceID"].ToString();
                    if (pnp.Contains("PCI\\"))
                        return obj["MACAddress"].ToString(); ;
                }
                return "";
            }
        }

        private static string ConvertMacAddress(string mac)
        {
            UInt64 value = 0;

            var exa = mac.Split(new char[] { ':' });

            for (int t = 0; t < exa.Length; t++)
                value = value * (UInt64)4 + ConvertFromEx(exa[t]);

            return ConvertToString(value);
        }

        private static UInt64 ConvertFromEx(string exa)
        {
            var exad = "0123456789ABCDEF";
            var ex = exa.ToCharArray();
            UInt64 value = 0;

            for (int t = 0; t < ex.Length; t++)
                value = value * 16 + (UInt64)exad.IndexOf(ex[t]);

            return value;
        }

        private static string ConvertSerialNumber(string exa)
        {
            return ConvertToString(ConvertFromEx(exa));
        }

        private static string ConvertString(string text)
        {
            UInt64 value = 0;
            var testo = text.Normalize().ToCharArray();
            for (int c = 0; c < testo.Length; c++)
                value = value * 2 + text[c];

            return ConvertToString(value);
        }

        private static string ConvertToString(UInt64 value)
        {
            var convert = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var converted = "";

            var bbase = (UInt64)convert.Length;

            while (value > bbase)
            {
                var rest = value % bbase;
                value = value / bbase;
                converted = convert.Substring((int)rest, 1) + converted;
            }
            converted = convert.Substring((int)value, 1) + converted;

            return converted;
        }

        private static UInt64 ConvertFromString(string exa)
        {
            var exad = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var ex = exa.ToCharArray();
            UInt64 value = 0;

            for (int t = 0; t < ex.Length; t++)
                value = value * (UInt64)exad.Length + (UInt64)exad.IndexOf(ex[t]);

            return value;
        }

        private static string filekey()
        {
            var directory = GlobalInfo.DBaseInfo.dbManager.GetApplicationName();
            if (string.IsNullOrEmpty(directory))
            {
                if (Directory.GetParent(Directory.GetCurrentDirectory()).FullName.EndsWith("bin", StringComparison.CurrentCultureIgnoreCase))
                    directory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Name;
                else
                    directory = new DirectoryInfo(Environment.CurrentDirectory).Name;
            }
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), directory);
            return Path.Combine(path, "key.bin");
        }



        private static string ConvertFrom64(string text)
        {
            if (text.Length == 0)
                return "";

            var textchar = text.ToCharArray();
            var serialByte = Convert.FromBase64CharArray(textchar, 0, textchar.Length);

            var ascii = new ASCIIEncoding();
            return ascii.GetString(serialByte);
        }
    }
}