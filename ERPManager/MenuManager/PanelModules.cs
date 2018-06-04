using ERPFramework;
using MetroFramework.Animation;
using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    class PanelModules : MetroPanel
    {
        public event EventHandler<ApplicationMenuItem> ItemSelected;
        public event EventHandler<bool> FavoriteClick;

        public FavoritesMenu FavoritesMenu { get; set; }

        private int _panelHeight = 0;

        public PanelModules()
            : base()
        {
        }

        public void SelectModule(ApplicationMenuModule module)
        {
            Controls.Clear();

            module.MenuFolders.ForEach(fld =>
            {
                var folderPanel = new PanelFolder
                {
                    FavoritesMenu = this.FavoritesMenu
                };

                Controls.Add(folderPanel);
                Controls.SetChildIndex(folderPanel, 0);
                folderPanel.FavoriteClick += Folderpanel_FavoriteClick;
                folderPanel.Folder = fld;

            });
        }

        private void Folderpanel_FavoriteClick(object sender, bool e)
        {
            FavoriteClick?.Invoke(sender, e);
        }

        private void Menubtn_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            ItemSelected?.Invoke(sender, ((MenuButton)sender).Item);
            Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void ModuleBtn_Click(object sender, EventArgs e)
        {
            var ex = new ExpandAnimation();
            if (Height == _panelHeight)
                ex.Start(this, new Size(Width, 24), TransitionType.Linear, 7);
            else
                ex.Start(this, new Size(Width, _panelHeight), TransitionType.Linear, 7);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            //this.Size = new Size(Parent.Width - 6, Size.Height);

        }
    }
}
