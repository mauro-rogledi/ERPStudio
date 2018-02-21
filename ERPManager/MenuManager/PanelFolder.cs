using System.Windows.Forms;
using ERPFramework;
using ERPFramework.ModulesHelper;
using MetroFramework.Controls;

namespace ERPManager.MenuManager
{
    class PanelFolder : MetroPanel
    {
        int maxHeight = 26;

        public event System.EventHandler<bool> FavoriteClick;
        public FavoritesMenu FavoritesMenu { get; set; }

        private ApplicationMenuFolder folder;
        public ApplicationMenuFolder Folder
        {
            set
            {
                folder = value;
                var btnTitle = new MetroTextBox.MetroTextButton
                {
                    Height = 24,
                    Width = Bounds.Width - 16,
                    Text = folder.Folder,
                    UseStyleColors = true,
                    Dock = System.Windows.Forms.DockStyle.Top,
                    TextAlign = System.Drawing.ContentAlignment.TopCenter
                };
                Controls.Add(btnTitle);
                Controls.SetChildIndex(btnTitle, 0);
                btnTitle.Click += BtnTitle_Click;

                foreach (var item in folder.MenuItems)
                {
                    if (item.UserGrant > GlobalInfo.UserInfo.userType)
                        continue;

                    var itemMenu = new MenuButton()
                    {
                        FavoritesMenu = this.FavoritesMenu
                    };

                    Controls.Add(itemMenu);
                    Controls.SetChildIndex(itemMenu, 0);
                    maxHeight += 24;
                    itemMenu.Click += ItemMenu_Click;
                    itemMenu.FavoriteClick += ItemMenu_FavoriteClick;
                    itemMenu.Item = item;
                }
                Height = maxHeight;
            }
        }

        public PanelFolder()
        {
            Height = 26;
            Dock = System.Windows.Forms.DockStyle.Top;
            Margin = new Padding(0);
        }

        private void ItemMenu_FavoriteClick(object sender, bool e)
        {
            FavoriteClick?.Invoke(sender, e);
        }

        private void ItemMenu_Click(object sender, System.EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var menuItem = sender as MenuButton;
            OpenDocument.Show(menuItem.Item.Namespace);
            Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void BtnTitle_Click(object sender, System.EventArgs e)
        {
            var ex = new MetroFramework.Animation.ExpandAnimation();
            if (Height == maxHeight)
                ex.Start(this, new System.Drawing.Size(Width, 26), MetroFramework.Animation.TransitionType.Linear, 7);
            else
                ex.Start(this, new System.Drawing.Size(Width, maxHeight), MetroFramework.Animation.TransitionType.Linear, 7);
        }
    }
}
