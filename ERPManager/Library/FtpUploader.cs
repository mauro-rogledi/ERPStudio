using System;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace ERPManager.Library
{
    class FtpUploader
    {
        public static void DownloadFileInMemory(string folder, string filename, MemoryStream ms)
        {
            string myFile = string.Concat("ERPStudio//", folder, "//", filename);

            string ftpUserID;
            string ftpPassword;
            string ftpServerIP;
            ftpServerIP = Properties.Settings.Default.ftpServer;
            ftpUserID = Properties.Settings.Default.ftpUser;
            ftpPassword = Properties.Settings.Default.ftpPassword;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)(FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "//" + myFile)));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // bypass default lan settings
            reqFTP.Proxy = null;

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            reqFTP.UsePassive = false;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = response.GetResponseStream();

                //Creo l’array di byte
                byte[] buffer = new byte[2048];
                //Carico la prima serie di byte nell’array
                int bytesRead = strm.Read(buffer, 0, 2048);
                //ciclo fino alla fine dello stream di byte
                while (bytesRead != 0)
                {
                    //Scrivo l’array di byte
                    ms.Write(buffer, 0, bytesRead);
                    bytesRead = strm.Read(buffer, 0, 1024);
                }
                //Chiudo gli oggetti
                response.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }

    }
}
