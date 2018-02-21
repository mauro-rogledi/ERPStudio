using ERPFramework;
using MetroFramework.Controls;
using System;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    class PanelPreferences : MetroPanel
    {
        int maxHeight = 0;

        private FavoritesMenu favoritesMenu;
        public FavoritesMenu FavoritesMenu
        {
            get
            { return favoritesMenu; }
            set
            {
                favoritesMenu = value;
                if (favoritesMenu.Count > 0)
                    CreateMenu();
            }
        }


        public PanelPreferences()
        {
            Margin = new Padding(0);
        }

        private void CreateMenu()
        {
            AddTitleButton();
            foreach (ApplicationMenuItem item in favoritesMenu)
            {
                if (item.UserGrant > GlobalInfo.UserInfo.userType)
                    continue;
                AddMenuButton(item);
            }

            Visible = true;
        }

        public void AddRemoveButton(ApplicationMenuItem item, bool isFavorite)
        {
            if (isFavorite)
            {
                if (!Visible)
                    AddTitleButton();

                FavoritesMenu.AddItem(item);
                AddMenuButton(item);
            }
            else
            {
                Controls.RemoveByKey(item.Namespace.ToString());
                Height -= 24;
                FavoritesMenu.RemoveItem(item);
                Visible = favoritesMenu.Count > 0;
                if (!Visible)
                    Controls.Clear();
            }
        }

        private void AddMenuButton(ApplicationMenuItem item)
        {
            SuspendLayout();

            var itemMenu = new FavoriteButton
            {
                Item = item,
                Name = item.Namespace.ToString(),
                AllowDrop = true
            };
            Controls.Add(itemMenu);
            Controls.SetChildIndex(itemMenu, 0);
            maxHeight += 24;
            itemMenu.FavoriteClick += ItemMenu_FavoriteClick;
            itemMenu.MouseDown += ItemMenu_MouseDown;
            itemMenu.DragOver += ItemMenu_DragOver;
            itemMenu.DragDrop += ItemMenu_DragDrop;
            Height = maxHeight;
            ResumeLayout();
        }

        private void ItemMenu_DragDrop(object sender, DragEventArgs e)
        {
            var source = e.Data.GetData(typeof(FavoriteButton)) as FavoriteButton;
            var dest = sender as FavoriteButton;

            var sourceIdx = Controls.GetChildIndex(source);
            var destIdx = Controls.GetChildIndex(dest);

            Controls.SetChildIndex(source, destIdx);
            FavoritesMenu.ChangeOrder(Controls);
        }

        private static void ItemMenu_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                var a = e.Data.GetData(typeof(FavoriteButton));
                if (a != null && a != sender)
                    e.Effect = DragDropEffects.Move;
            }
        }

        private void ItemMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoDragDrop(sender, DragDropEffects.Move);
        }

        private void ItemMenu_FavoriteClick(object sender, bool e)
        {
            var button = sender as FavoriteButton;
            AddRemoveButton(button.Item, false);
        }

        private void AddTitleButton()
        {
            var btnTitle = new MetroTextBox.MetroTextButton
            {
                Height = 24,
                Width = Bounds.Width - 34,
                Text = Properties.Resources.Favorites,
                UseStyleColors = true,
                Dock = System.Windows.Forms.DockStyle.Top,
                TextAlign = System.Drawing.ContentAlignment.TopCenter,
                Margin = new Padding(0)
            };
            maxHeight = 24;

            Controls.Add(btnTitle);
            Controls.SetChildIndex(btnTitle, 0);
            btnTitle.Click += BtnTitle_Click;
            Visible = true;
        }

        private void BtnTitle_Click(object sender, EventArgs e)
        {
            var ex = new MetroFramework.Animation.ExpandAnimation();
            if (Height == maxHeight)
                ex.Start(this, new System.Drawing.Size(Width, 26), MetroFramework.Animation.TransitionType.Linear, 7);
            else
                ex.Start(this, new System.Drawing.Size(Width, maxHeight), MetroFramework.Animation.TransitionType.Linear, 7);
        }
    }
}
