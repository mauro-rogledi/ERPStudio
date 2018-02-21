using System;
using System.Drawing;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    /// <summary>
    /// User Control che aggiunge l'evento di close
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
    public class PanelUserControl : MetroFramework.Controls.MetroUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public PanelUserControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        public void ChiudiForm(string myParam)
        {
            PanelUserEventArgs evento = new PanelUserEventArgs(myParam);
            Chiudi(this, evento);
        }

        public void ChiudiForm(DialogResult myResult)
        {
            PanelUserEventArgs evento = new PanelUserEventArgs(myResult);
            Chiudi(this, evento);
        }

        #endregion

        #region Event Management

        public delegate void PanelUserEventHandler(object sender, PanelUserEventArgs me);

        public event PanelUserEventHandler Chiudi;

        public class PanelUserEventArgs : EventArgs
        {
            private string myScelta;
            private DialogResult myResult;

            public PanelUserEventArgs(string scelta)
            {
                this.myScelta = scelta;
            }

            public PanelUserEventArgs(DialogResult result)
            {
                this.myResult = result;
            }

            public string Scelta
            {
                get { return myScelta; }
                set { myScelta = value; }
            }

            public DialogResult Result
            {
                get { return myResult; }
                set { myResult = value; }
            }
        }

        #endregion
    }
}