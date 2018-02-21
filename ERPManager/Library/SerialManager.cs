using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ERPFramework;

namespace ERPManager
{
    [Flags]
    public enum SerialType : ushort
    {
        ONLY_NAME = 0x0000,
        MAC_ADDRESS = 0x0001,
        LICENSE_NAME = 0x0002,
        EXPIRATION_DATE = 0x0004,
        PEN_DRIVE = 0x0008
    }

    [Serializable()]
    public class SerialModule
    {
        public string Enable { get; set; }

        public string Application { get; set; }

        public string Module { get; set; }

        public string SerialType { get; set; }

        public string Expiration { get; set; }

        public string SerialNo { get; set; }
    }

    [Serializable()]
    public class SerialData
    {
        public string License { get; set; }

        public string PenDrive { get; set; }

        public List<SerialModule> Modules = new List<SerialModule>(1);
    }

    public static class SerialManager
    {
        public static SerialData SerialData = new SerialData();
        public static string macAddres = ReadMacAddress();

        public static void Clear()
        {
            SerialData.Modules.Clear();
        }

        public static void AddModule(string application, bool enable, string module, SerialType sType, DateTime expiration, string serial)
        {
            SerialModule sd = new SerialModule();
            sd.Application = application;
            sd.Module = module;
            sd.Enable = enable.ToString();
            sd.SerialType = sType.ToString();
            sd.Expiration = expiration.ToShortDateString();
            sd.SerialNo = serial;

            SerialData.Modules.Add(sd);
        }

        public static bool IsActivate(string application, string module)
        {
            SerialModule sm = SerialData.Modules.Find(p => (p.Application == application && p.Module == module));
            if (sm == null || sm.Enable == bool.FalseString)
                return false;

            if (!SerialFormatIsOk(sm.SerialNo, sm.Application, sm.Module))
                return false;

            if (!CheckSerialType(sm))
                return false;

            return true;
        }

        private static string ConvertTo64(string text)
        {
            char[] textchar = text.ToCharArray();
            byte[] textbyte = new byte[textchar.Length];

            for (int t = 0; t < textchar.Length; t++)
                textbyte[t] = (byte)textchar[t];
            return Convert.ToBase64String(textbyte);
        }

        public static string CreateSerial(string license, string macAddress, string application, string module, SerialType sType, DateTime expiration, string pendrive)
        {
            string serial = string.Empty;

            concat(ref serial, ConvertString(application) + ConvertString(module));
            if (sType.HasFlag(SerialType.LICENSE_NAME))
                concat(ref serial, ConvertString(license));
            if (sType.HasFlag(SerialType.MAC_ADDRESS))
                concat(ref serial, ConvertMacAddress(macAddress));

            if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                concat(ref serial, ConvertToString((UInt64)(expiration.Year * 365 + expiration.Month * 31 + expiration.Day)));

            if (sType.HasFlag(SerialType.PEN_DRIVE))
            {
                string letter = USBSerialNumber.GetDriveLetterFromName(pendrive);
                concat(ref serial, ConvertSerialNumber(USBSerialNumber.getSerialNumberFromDriveLetter(letter)));
            }

            // Checksum
            concat(ref serial, ConvertString(serial));
            return serial;
        }

        private static bool SerialFormatIsOk(string serial, string application, string module)
        {
            string serialCheck = string.Empty;
            string[] parts = serial.Split(new char[] { '-' });
            for (int t = 0; t < parts.Length - 1; t++)
                concat(ref serialCheck, parts[t]);

            if (ConvertString(serialCheck) != parts[parts.Length - 1])
                return false;

            if (ConvertString(application) + ConvertString(module) != parts[0])
                return false;

            return true;
        }

        private static bool CheckSerialType(SerialModule sm)
        {
            int pos = 1;
            string[] parts = sm.SerialNo.Split(new char[] { '-' });

            SerialType sType = (SerialType)Enum.Parse(typeof(SerialType), sm.SerialType);
            if (sType.HasFlag(SerialType.LICENSE_NAME))
                if (parts[pos++] != ConvertString(SerialData.License))
                    return false;

            if (sType.HasFlag(SerialType.MAC_ADDRESS))
                if (parts[pos++] != ConvertMacAddress(macAddres))
                    return false;

            if (sType.HasFlag(SerialType.EXPIRATION_DATE))
                if (ConvertFromString(parts[pos++]) < (UInt64)(GlobalInfo.CurrentDate.Year * 365 + GlobalInfo.CurrentDate.Month * 31 + GlobalInfo.CurrentDate.Day))
                    return false;

            if (sType.HasFlag(SerialType.PEN_DRIVE))
            {
                string letter = USBSerialNumber.GetDriveLetterFromName(SerialData.PenDrive);
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
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
            ("Select MACAddress,PNPDeviceID FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL AND PNPDeviceID IS NOT NULL");
            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                string pnp = obj["PNPDeviceID"].ToString();
                if (pnp.Contains("PCI\\"))
                    return obj["MACAddress"].ToString(); ;
            }
            return "";
        }

        private static string ConvertMacAddress(string mac)
        {
            UInt64 value = 0;

            string[] exa = mac.Split(new char[] { ':' });

            for (int t = 0; t < exa.Length; t++)
                value = value * (UInt64)4 + ConvertFromEx(exa[t]);

            return ConvertToString(value);
        }

        private static UInt64 ConvertFromEx(string exa)
        {
            string exad = "0123456789ABCDEF";
            char[] ex = exa.ToCharArray();
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
            char[] testo = text.Normalize().ToCharArray();
            for (int c = 0; c < testo.Length; c++)
                value = value * 2 + text[c];

            return ConvertToString(value);
        }

        private static string ConvertToString(UInt64 value)
        {
            string convert = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string converted = "";

            UInt64 bbase = (UInt64)convert.Length;

            while (value > bbase)
            {
                UInt64 rest = value % bbase;
                value = value / bbase;
                converted = convert.Substring((int)rest, 1) + converted;
            }
            converted = convert.Substring((int)value, 1) + converted;

            return converted;
        }

        private static UInt64 ConvertFromString(string exa)
        {
            string exad = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] ex = exa.ToCharArray();
            UInt64 value = 0;

            for (int t = 0; t < ex.Length; t++)
                value = value * (UInt64)exad.Length + (UInt64)exad.IndexOf(ex[t]);

            return value;
        }

        private static string filekey()
        {
            string directory = GlobalInfo.DBaseInfo.dbManager.GetApplicationName();
            if (string.IsNullOrEmpty(directory))
            {
                if (Directory.GetParent(Directory.GetCurrentDirectory()).FullName.EndsWith("bin", StringComparison.CurrentCultureIgnoreCase))
                    directory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Name;
                else
                    directory = new DirectoryInfo(Environment.CurrentDirectory).Name;
            }
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), directory);
            return Path.Combine(path, "key.bin");
        }

        public static bool Load()
        {
            if (File.Exists(filekey()))
            {
                MemoryStream memStr = new MemoryStream();
                using (FileStream myFS = new FileStream(filekey(), FileMode.Open))
                using (GZipStream gZip = new GZipStream(myFS, CompressionMode.Decompress))
                    gZip.CopyTo(memStr);
                memStr.Position = 0;
                BinaryFormatter myBF = new BinaryFormatter();
                SerialData = (SerialData)myBF.Deserialize(memStr);
            }
            else
                return false;

            SerialData.License = ConvertFrom64(SerialData.License);
            SerialData.PenDrive = ConvertFrom64(SerialData.PenDrive);

            foreach (SerialModule Module in SerialData.Modules)
            {
                Module.Application = ConvertFrom64(Module.Application);
                Module.Module = ConvertFrom64(Module.Module);
                Module.Expiration = ConvertFrom64(Module.Expiration);
                Module.SerialNo = ConvertFrom64(Module.SerialNo);
                Module.SerialType = ConvertFrom64(Module.SerialType);
            }
            return true;
        }

        public static void Save()
        {
            string directory = Path.GetDirectoryName(filekey());
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            MemoryStream memStr = new MemoryStream();
            BinaryFormatter myBF = new BinaryFormatter();
            SerialData.License = ConvertTo64(SerialData.License);
            SerialData.PenDrive = ConvertTo64(SerialData.PenDrive);

            foreach (SerialModule Module in SerialData.Modules)
            {
                Module.SerialType = ConvertTo64(Module.SerialType);
                Module.Application = ConvertTo64(Module.Application);
                Module.Module = ConvertTo64(Module.Module);
                Module.Expiration = ConvertTo64(Module.Expiration);
                Module.SerialNo = ConvertTo64(Module.SerialNo);
            }
            myBF.Serialize(memStr, SerialData);

            using (FileStream myFS = new FileStream(filekey(), FileMode.Create))
            using (GZipStream gzip = new GZipStream(myFS, CompressionMode.Compress, false))
                gzip.Write(memStr.ToArray(), 0, (int)memStr.Length);
        }

        private static string ConvertFrom64(string text)
        {
            if (text.Length == 0)
                return "";

            char[] textchar = text.ToCharArray();

            byte[] serialByte = Convert.FromBase64CharArray(textchar, 0, textchar.Length);

            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetString(serialByte);
        }
    }
}