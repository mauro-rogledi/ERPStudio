using System;
using System.ComponentModel;
using System.Drawing;

namespace ERPFramework.Controls
{
    /// <summary>
    /// Summary description for ProgressStatusBar.
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.StatusBar))]
    public class ProgressStatusBar : System.Windows.Forms.StatusBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;

        private System.Windows.Forms.ProgressBar myProgressBar;
        private ProgressStatusBarPanel myStatusPanelProgressBar;

        public ProgressStatusBar()
        {
            InitializeComponent();
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

        public ProgressStatusBarPanel ProgressBarState
        {
            get { return this.myStatusPanelProgressBar; }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Layout += new System.Windows.Forms.LayoutEventHandler(ProgressStatusBar_Layout);
        }

        private void ProgressStatusBar_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            int pos = 0;

            foreach (ProgressStatusBarPanel myPanel in this.Panels)
            {
                if (myPanel.ProgressBarPanel)
                {
                    if (myStatusPanelProgressBar == null)
                    {
                        myStatusPanelProgressBar = myPanel;
                    }
                    break;
                }
                pos = pos + myPanel.Width;
            }

            if (myStatusPanelProgressBar != null)
            {
                if (myProgressBar == null)
                {
                    myProgressBar = new System.Windows.Forms.ProgressBar();
                    myStatusPanelProgressBar.Cambia += new EventHandler(myStatusPanelProgressBar_Cambia);
                }

                myProgressBar.Top = 5;
                myProgressBar.Left = pos + 3;
                myProgressBar.Width = myStatusPanelProgressBar.Width - 6;
                myProgressBar.Height = this.Height - 6;
                myProgressBar.Visible = myStatusPanelProgressBar.ProgressBarVisible && this.ShowPanels;
                myProgressBar.Minimum = myStatusPanelProgressBar.Min;
                myProgressBar.Maximum = myStatusPanelProgressBar.Max;
                myProgressBar.Value = myStatusPanelProgressBar.Value;
                this.Controls.Add(myProgressBar);
            }
        }

        private void myStatusPanelProgressBar_Cambia(object sender, System.EventArgs e)
        {
            myProgressBar.Value = myStatusPanelProgressBar.Value;
            myProgressBar.Minimum = myStatusPanelProgressBar.Min;
            myProgressBar.Maximum = myStatusPanelProgressBar.Max;

            myProgressBar.Visible = myStatusPanelProgressBar.ProgressBarVisible && this.ShowPanels;
        }

        #endregion
    }

    [ToolboxBitmap(typeof(System.Windows.Forms.StatusBarPanel))]
    public class ProgressStatusBarPanel : System.Windows.Forms.StatusBarPanel
    {
        private bool isProgresBarPanel;
        private bool isProgresBarVisible;
        private int minValue;
        private int maxValue = 100;
        private int currentValue;

        public event System.EventHandler Cambia = null;

        [
        Category("ProgressBar"),
        Description("Indica il panel che contiene una progressbar"),
        DefaultValue(false)
        ]
        public bool ProgressBarPanel
        {
            get { return isProgresBarPanel; }
            set { isProgresBarPanel = value; }
        }

        [
        Category("ProgressBar"),
        Description("Indica se la progressbar è visibile"),
        DefaultValue(false)
        ]
        public bool ProgressBarVisible
        {
            get { return isProgresBarVisible; }
            set
            {
                isProgresBarVisible = value;
                if (Cambia != null)
                    Cambia(this, new System.EventArgs());
            }
        }

        public ProgressStatusBarPanel()
        {
        }

        [
        Category("ProgressBar"),
        Description("Valore minimo della statusbar"),
        DefaultValue(10)
        ]
        public int Min
        {
            get { return minValue; }
            set
            {
                minValue = value;
                if (Cambia != null)
                    Cambia(this, new System.EventArgs());
            }
        }

        [
        Category("ProgressBar"),
        Description("Valore massimo della statusbar"),
        DefaultValue(100)
        ]
        public int Max
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                currentValue = 0;
                if (Cambia != null)
                    Cambia(this, new System.EventArgs());
            }
        }

        public int Value
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                if (Cambia != null)
                    Cambia(this, new System.EventArgs());
            }
        }
    }
}