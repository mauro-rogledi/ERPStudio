using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace ERPManager.Library
{
    class FtpUploader
    {
        public static void UploadFile(string folder, string filename)
        {
            string myFile = string.Concat("maurorogledi.it//ApplicationBuilder//", folder, "//", filename);

            string ftpUserID;
            string ftpPassword;
            string ftpServerIP;
            ftpServerIP = "ftp.maurorogledi.it";
            ftpUserID = "1553343@aruba.it";
            ftpPassword = "a4669463d4";
            FileInfo fileInf = new FileInfo(filename);
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
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            Byte[] buff;
            buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                long filesize = fs.Length;
                int i = 0;
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    Application.DoEvents();
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    i = i + 1;
                    //Double percentComp = (i * buffLength) * 100 / filesize;
                    //ProgressBar1.Value = (int)percentComp;
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }
        }

        public static void DownloadFile(string folder, string filename)
        {
            string myFile = string.Concat("maurorogledi.it//ApplicationBuilder//", folder, "//", filename);

            string ftpUserID;
            string ftpPassword;
            string ftpServerIP;
            ftpServerIP = "ftp.maurorogledi.it";
            ftpUserID = "1553343@aruba.it";
            ftpPassword = "a4669463d4";
            //ftpUserID = "mauro";
            //ftpPassword = "1Tvlgalc.";
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

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = new FileStream(filename, FileMode.Create);
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
                    fs.Write(buffer, 0, bytesRead);
                    bytesRead = strm.Read(buffer, 0, 1024);
                }
                //Chiudo gli oggetti
                fs.Close();
                response.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }

        public static void DownloadFileInMemory(string folder, string filename, MemoryStream ms)
        {
            string myFile = string.Concat("ERPStudio//", folder, "//", filename);

            string ftpUserID;
            string ftpPassword;
            string ftpServerIP;
            ftpServerIP = "maurorogledi.myftp.org";
            ftpUserID = "ErpStudio";
            ftpPassword = "ErpManager";
            //ftpUserID = "mauro";
            //ftpPassword = "1Tvlgalc.";
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
