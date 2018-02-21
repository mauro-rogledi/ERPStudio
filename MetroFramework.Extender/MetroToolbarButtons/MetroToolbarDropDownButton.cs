using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{
    public class MetroToolbarDropDownButton : MetroLink, IMetroToolBarButton
    {
        private MetroContextMenu menu = null;

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public new MetroContextMenu ContextMenuStrip
        {
            get
            {
                if (menu == null)
                {
                    menu = new MetroContextMenu(null);
                }
                return menu;
            }
            set { menu = value; }
        }

        private MetroToolbarButtonType buttonType = MetroToolbarButtonType.New;
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(MetroToolbarButtonType.New)]
        public MetroToolbarButtonType ButtonType { get { return buttonType; } set { buttonType = value; } }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public ToolStripItemCollection Items
        {
            get
            {
                return ContextMenuStrip.Items;
            }
        }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public void AddDropDownItem(ToolStripMenuItem tsmi)
        {
            Items.Add(tsmi);
            tsmi.Click += Tsmi_Click;
        }

        [Category(ExtenderDefaults.PropertyCategory.Event)]
        public event EventHandler DropDownItemClicked = null;
        private void Tsmi_Click(object sender, EventArgs e)
        {
            DropDownItemClicked?.Invoke(sender, e);
        }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ToolBarButtonStyle.PushButton)]
        public bool IsVisible
        {
            get { return Visible; }
            set { ChangeVisible(value); }
        }

        [Browsable(false)]
        public void ShowMenu()
        {
            //menu.Font = MetroFonts.Link(FontSize, FontWeight);
            menu.Show(this, 0, Height);
        }
        protected override void OnPaintForeground(PaintEventArgs e)
        {
            base.OnPaintForeground(e);

            if (Items.Count > 1)
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
        Image _verticalLine = null;

        public MetroToolbarDropDownButton() : base()
        {
            createimages();
            Size = new Size(40, 34);
            Dock = DockStyle.Left;
            Text = string.Empty;
            ImageSize = 32;
            Margin = new Padding(6);
        }

        private void createimages()
        {
            _nofocus = Properties.Resources.TriangleFillDown8g;
            _image = Properties.Resources.TriangleFillDown8;
            _verticalLine = Properties.Resources.VerticalLine32;

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

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Parent is MetroToolbar)
                (Parent as MetroToolbar).ToolbarDropDownClicked(this);
        }

        #endregion

        private void DrawTriangle(Graphics g)
        {

            var iconLocation = new Point(ClientRectangle.Width - 8, ClientRectangle.Height / 2 - 4);
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

        private void ChangeVisible(bool value)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (value == Visible)
                return;
            Visible = value;

            if (Parent is MetroToolbar)
                (Parent as MetroToolbar).ButtonVisibleChanged();
        }
    }
}
