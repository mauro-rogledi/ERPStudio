using ERPFramework;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    class MenuButton : MetroLink
    {
        public FavoritesMenu FavoritesMenu { get; set; }
        public event EventHandler<bool> FavoriteClick;

        MetroLink starBtn = null;

        private ApplicationMenuItem item;
        public ApplicationMenuItem Item
        {
            get { return item; }
            set
            {
                item = value;
                Text = item.Title;
                switch (item.DocumentType)
                {

                    case DocumentType.FastDocument:
                        Image = Properties.Resources.Form24;
                        NoFocusImage = Properties.Resources.Form24g;
                        break;

                    case DocumentType.Document:
                        Image = Properties.Resources.Form24;
                        NoFocusImage = Properties.Resources.Form24g;
                        break;

                    case DocumentType.Batch:
                        Image = Properties.Resources.Batch24;
                        NoFocusImage = Properties.Resources.Batch24g;
                        break;

                    case DocumentType.Report:
                        Image = Properties.Resources.Print24;
                        NoFocusImage = Properties.Resources.Print24g;
                        break;
                }

                CreateFavoritesButton();
            }
        }

        public MenuButton()
            : base()
        {
            TextImageRelation = TextImageRelation.TextBeforeImage;
            ImageAlign = ContentAlignment.MiddleRight;
            UseStyleColors = true;
            UseSelectable = false;
            FontSize = MetroFramework.MetroLinkSize.Medium;
            FontWeight = MetroLinkWeight.Regular;
            Size = new System.Drawing.Size(170, 24);
            ImageSize = 24;
            Dock = DockStyle.Top;
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        }

        private void CreateFavoritesButton()
        {
            starBtn = new MetroLink
            {
                ImageSize = 16,
                Size = new Size(16, 16),
                Location = new Point(Width - 40, 4),
                Visible = false,
                NoFocusImage = FavoritesMenu.IsFavorite(item) ? Properties.Resources.StarFilled16g : Properties.Resources.ChristmasStar16g,
                Image = FavoritesMenu.IsFavorite(item) ? Properties.Resources.StarFilled16g : Properties.Resources.ChristmasStar16g
            };
            starBtn.Click += FavoritesBtn_Click;
            Controls.Add(starBtn);
        }

        private void FavoritesBtn_Click(object sender, EventArgs e)
        {
            var isFavorite = !FavoritesMenu.IsFavorite(item);
            ChangeImage(isFavorite);
            FavoriteClick?.Invoke(Item, isFavorite);
        }

        private void ChangeImage(bool isFavorite)
        {
            starBtn.NoFocusImage = isFavorite ? Properties.Resources.StarFilled16g : Properties.Resources.ChristmasStar16g;
            starBtn.Image = isFavorite ? Properties.Resources.StarFilled16g : Properties.Resources.ChristmasStar16g;

            starBtn.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            ChangeImage(FavoritesMenu.IsFavorite(item));

            if (starBtn != null)
                starBtn.Visible = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            System.Threading.Thread.Sleep(30);
            if (starBtn != null)
                starBtn.Visible = ClientRectangle.Contains(PointToClient(Control.MousePosition));
        }

        protected override void OnResize(EventArgs e)
        {
            if (starBtn != null)
                starBtn.Location = new Point(Width - 40, 4);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
    }
}
