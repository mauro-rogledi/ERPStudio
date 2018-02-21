using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace ERPFramework.Controls
{
    public partial class BreadCrumbs : MetroUserControl
    {
        public event BreadCrumbsEventHandler BreadClick;
        public delegate void BreadCrumbsEventHandler(object sender, BreadCrumbsEventArgs e);
        public List<KeyValuePair<string, string>> Crumbs = new List<KeyValuePair<string, string>>();

        public BreadCrumbs()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            Crumbs.Clear();
            RemoveCrumbs();
        }

        public void AddCrumb(string key, string value)
        {
            Crumbs.Add(new KeyValuePair<string,string>(key, value));
            RefreshCrumbs();
        }

        private void RemoveCrumbs()
        {
            for(int t=Controls.Count-1; t>0 ;t--)
                Controls.RemoveAt(t);
        }

        private void RefreshCrumbs()
        {
            RemoveCrumbs();

            bool firstTime = true;
            foreach(KeyValuePair<string,string> kvp in Crumbs)
            {
                int cntr = Controls.Count;
                if (!firstTime)
                {
                    MetroLabel lbl = new MetroLabel() { Text = "", AutoSize = true };
                    lbl.FontSize = MetroFramework.MetroLabelSize.Medium;
                    lbl.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                    lbl.Location = new Point(Controls[cntr - 1].Width + Controls[cntr - 1].Location.X, Controls[cntr - 1].Location.Y);
                    Controls.Add(lbl);
                }

                firstTime = false;
                MetroLabel lnklbl = new MetroLabel() { Text = kvp.Value, Tag = kvp.Key, AutoSize = true };
                lnklbl.DoubleClick += Lnklbl_DoubleClick;
                if (cntr > 1)
                    lnklbl.Location = new Point(Controls[cntr].Width + Controls[cntr].Location.X, 3);
                else
                    lnklbl.Location = new Point(3, 3);
                Controls.Add(lnklbl);
            }
            ResumeLayout();
        }

        private void Lnklbl_DoubleClick(object sender, EventArgs e)
        {
            if (BreadClick != null)
                BreadClick(sender, new BreadCrumbsEventArgs(((MetroLabel)sender).Tag.ToString()));
        }

        private void lblHeader_Click(object sender, EventArgs e)
        {
            if (BreadClick != null)
                BreadClick(sender, new BreadCrumbsEventArgs(""));
        }
    }
}

public class BreadCrumbsEventArgs : EventArgs
{
    public string Key { get; private set; }
    public BreadCrumbsEventArgs(string key)
    {
        Key = key;
    }
}
