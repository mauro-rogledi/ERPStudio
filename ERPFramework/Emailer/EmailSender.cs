using System;
using System.Net;
using System.Net.Mail;
using ERPFramework.Preferences;
using ERPFramework.Data;

namespace ERPFramework.Emailer
{
    public static class EmailSender
    {
        public static bool UseDefaultCredentials { get; set; }

        public static string Host { get; set; }

        public static int Port { get; set; }

        public static string UserName;
        public static string Password;

        public static bool EnableSsl { get; set; }

        public static string FromAddress { get; set; }

        public static string FromDisplay { get; set; }

        public static string ToAddress { get; set; }

        public static bool SendException { get; set; }

        public static string Error { get; set; }

        public static void ReadDataClient()
        {
            PreferencesManager<MailPref> myMailPref = new PreferencesManager<MailPref>("", null);
            MailPref mailPref = myMailPref.ReadPreference();

            if (mailPref != null)
            {
                FromDisplay = mailPref.Name;
                FromAddress = mailPref.EmailAddress;
                UseDefaultCredentials = mailPref.UseDefaultCredentials;
                Host = mailPref.Host;
                Port = mailPref.Port;
                UserName = mailPref.UserName;
                Password = Cryption.Decrypt(mailPref.Password);
                EnableSsl = mailPref.EnableSsl;
            }
        }

        public static bool SendMail(string subject, string body, string attachment)
        {
            if (string.IsNullOrEmpty(Host) || Port == 0)
                ReadDataClient();

            if (string.IsNullOrEmpty(Host) || Port == 0 || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(FromAddress) || string.IsNullOrEmpty(ToAddress))
            {
                Error = Properties.Resources.Msg_EmailDataMissing;
                return false;
            }

            MailAddress from = new MailAddress(FromAddress, FromDisplay);
            MailAddress to = new MailAddress(ToAddress);

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            if (!string.IsNullOrEmpty(attachment))
                message.Attachments.Add(new Attachment(attachment));

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = UseDefaultCredentials;
            smtpClient.Host = Host;
            smtpClient.Port = Port;
            smtpClient.Credentials = new NetworkCredential(UserName, Password);
            smtpClient.EnableSsl = EnableSsl;
            try
            {
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
                Error = e.Message;
                return false;
            }

            return true;
        }
    }
}