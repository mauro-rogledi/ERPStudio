using System;
using System.ComponentModel;

namespace ERPFramework.Libraries
{
    public class ThreadHelper
    {
        private BackgroundWorker backgroungWorker;
        public Func<object, int> Method;

        public void Execute(object arg)
        {
            if (backgroungWorker == null)
            {
                backgroungWorker = new BackgroundWorker();
                backgroungWorker.WorkerSupportsCancellation = true;
                backgroungWorker.DoWork += new DoWorkEventHandler(backgroungWorker_DoWork);
                backgroungWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroungWorker_RunWorkerCompleted);
                backgroungWorker.ProgressChanged += new ProgressChangedEventHandler(backgroungWorker_ProgressChanged);
            }
            backgroungWorker.RunWorkerAsync(arg);
        }

        public void CancelAsync()
        {
            backgroungWorker.CancelAsync();
        }

        private void backgroungWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroungWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bkw = (BackgroundWorker)sender;
            bkw.Dispose();
            sender = null;
        }

        private void backgroungWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            NiggerWork(Method, e);
            while (!backgroungWorker.CancellationPending)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void NiggerWork(Func<object, int> myMethod, DoWorkEventArgs e)
        {
            myMethod(e.Argument);
        }

        public static void UpdateControl(System.Windows.Forms.Control cntrl, Action myAction)
        {
            if (!cntrl.InvokeRequired)
                myAction();
            else
                cntrl.BeginInvoke(myAction);
        }
    }
}