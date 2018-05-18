using ERPFramework.Forms;
using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    class ApplicationTabControl : MetroFramework.Controls.MetroTabControl
    {
        public void Open(IDocumentBase uc)
        {
            MetroTabPage pg = new MetroFramework.Controls.MetroTabPage();
            pg.StyleManager = ERPFramework.GlobalInfo.StyleManager;
            pg.UseStyleColors = true;
            pg.AutoScroll = false;
            pg.SizeChanged += Pg_SizeChanged;

            pg.Text = uc.Title;
            uc.Exit += Uc_Exit;

            MetroUserControl muc = uc as MetroUserControl;
            muc.UseStyleColors = true;
            ERPFramework.GlobalInfo.StyleManager.Clone(muc);
            pg.Controls.Add(muc);

            TabPages.Add(pg);
            int p = TabPages.Count - 1;
            SelectedTab = pg;
            muc.Size = new Size(pg.Width, pg.Height-5);
            pg.StyleManager.Update();
            muc.Focus();
        }

        private void Pg_SizeChanged(object sender, EventArgs e)
        {
            MetroTabPage pg = sender as MetroTabPage;
            foreach(Control ctrl in pg.Controls)
            {
                if (ctrl is MetroUserControl)
                    ctrl.Size = new Size(pg.Width, pg.Height - 5);
            }
        }

        private void Uc_Exit(object sender, EventArgs e)
        {
            TabPages.Remove(sender as TabPage);
        }

        public bool CanClose()
        {
            foreach(MetroTabPage pg in Controls)
            {
                foreach (Control ctrl in pg.Controls)
                {
                    if (ctrl is DocumentForm)
                    {
                        DocumentForm df = ctrl as DocumentForm;
                        if (df.DocumentMode == ERPFramework.Data.DBMode.Edit ||
                            df.DocumentMode == ERPFramework.Data.DBMode.Run)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
