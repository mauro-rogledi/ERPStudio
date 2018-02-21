using ERPFramework;
using ERPFramework.ModulesHelper;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    class FavoriteButton : MetroLink
    {
        public FavoritesMenu FavoritesMenu { get; set; }
        public event EventHandler<bool> FavoriteClick;

        MetroLink minusBtn = null;

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

        public FavoriteButton()
            : base()
        {
            TextImageRelation = TextImageRelation.TextBeforeImage;
            ImageAlign = ContentAlignment.MiddleRight;
            UseStyleColors = true;
            FontSize = MetroFramework.MetroLinkSize.Medium;
            FontWeight = MetroLinkWeight.Regular;
            Size = new System.Drawing.Size(170, 24);
            ImageSize = 24;
            Margin = new Padding(0);
            Dock = DockStyle.Top;
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        }

        private void CreateFavoritesButton()
        {
            minusBtn = new MetroLink
            {
                ImageSize = 16,
                Size = new Size(16, 16),
                Visible = false,
                NoFocusImage = Properties.Resources.Delete16g,
                Image = Properties.Resources.Delete16,
                Location = new Point(Width - 40, 4)
            };
            minusBtn.Click += MinusBtn_Click;
            Controls.Add(minusBtn);
        }

        private void MinusBtn_Click(object sender, EventArgs e)
        {
            FavoriteClick?.Invoke(this, false);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (minusBtn != null)
                minusBtn.Visible = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            System.Threading.Thread.Sleep(30);
            if (minusBtn != null)
                minusBtn.Visible = ClientRectangle.Contains(PointToClient(Control.MousePosition));
        }

        protected override void OnResize(EventArgs e)
        {
            if (minusBtn != null)
                minusBtn.Location = new Point(Width - 40, 4);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            OpenDocument.Show(Item.Namespace);
            Cursor = Cursors.Default;
            Application.DoEvents();
        }
    }
}
