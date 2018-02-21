using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{

    public class MetroToolbarButton : MetroLink
    {
        private MetroContextMenu menu = null;

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public new MetroContextMenu ContextMenuStrip
        {
            get
            {
                if (menu == null)
                    menu = new MetroContextMenu(null);
                return menu;
            }
            set { menu = value; }
        }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroToolbarButtonDock ButtonDock { get; set; } = MetroToolbarButtonDock.Left;

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroToolbarButtonType ButtonType { get; set; }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public ToolStripItemCollection Items
        {
            get
            {
                return ContextMenuStrip.Items;
            }
        }

        private bool showTriangle = false;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowTriangle { get { return showTriangle; } set { showTriangle = value; } }

        [Browsable(false)]
        public void ShowMenu()
        {
            menu.Font = MetroFonts.Link(FontSize, FontWeight);
            menu.Show(this, 0, Height);
        }
        protected override void OnPaintForeground(PaintEventArgs e)
        {
            base.OnPaintForeground(e);

            if (showTriangle)
                DrawTriangle(e.Graphics);
        }

        #region Focus Methods

        private bool isHovered = false;
        private bool isPressed = false;

        Image _lightlightimg = null;
        Image _darklightimg = null;
        Image _lightimg = null;
        Image _darkimg = null;
        Image _image = null;
        Image _nofocus = null;

        public MetroToolbarButton() :base()
        {
            createimages();
        }

        private void createimages()
        {
            _nofocus = Properties.Resources.TriangleFillDown8g;
            _image = Properties.Resources.TriangleFillDown8;
            _lightimg = _image;
            _darkimg = ApplyInvert(new Bitmap(_image));

            _darklightimg = ApplyLight(new Bitmap(_darkimg));
            _lightlightimg = ApplyLight(new Bitmap(_lightimg));
        }

        protected override void OnGotFocus(EventArgs e)
        {
            // isHovered = true;
            isPressed = false;
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isPressed = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            isPressed = false;

            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isPressed = true;
            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        #endregion

        private void DrawTriangle(Graphics g)
        {

            Point iconLocation = new Point(ClientRectangle.Width - 8, ClientRectangle.Height / 2-4);
            if (_nofocus == null)
            {
                if (Theme == MetroThemeStyle.Dark)
                {
                    g.DrawImage((isHovered && !isPressed) ? _darkimg : _darklightimg, new Rectangle(iconLocation, new Size(8, 8)));
                }
                else
                {
                    g.DrawImage((isHovered && !isPressed) ? _lightimg : _lightlightimg, new Rectangle(iconLocation, new Size(8, 8)));
                }
            }
            else
            {
                if (Theme == MetroThemeStyle.Dark)
                {
                    g.DrawImage((isHovered && !isPressed) ? _darkimg : _nofocus, new Rectangle(iconLocation, new Size(8, 8)));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(isHovered);
                    g.DrawImage((isHovered && !isPressed) ? _image : _nofocus, new Rectangle(iconLocation, new Size(8, 8)));
                }
            }
        }
    }
}
