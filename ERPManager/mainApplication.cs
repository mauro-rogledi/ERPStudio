using System;
using System.Windows.Forms;

namespace ERPManager
{
    internal class MainClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Forms.frmMain());
            }
            catch (Exception exc)
            {
                if (ERPFramework.Emailer.EmailSender.SendException)
                {
                    ERPFramework.Emailer.EmailSender.ToAddress = "mauro.rogledi@gmail.com";
                    ERPFramework.Emailer.EmailSender.SendMail(exc.Message, exc.StackTrace, "");
                }
                System.Diagnostics.Debug.WriteLine(exc.StackTrace, exc.Message);
                MessageBox.Show(exc.StackTrace, exc.Message);
            }
        }
    }
}