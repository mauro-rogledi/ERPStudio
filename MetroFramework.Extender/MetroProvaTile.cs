using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Extender;
using MetroFramework.Interfaces;

namespace MetroFramework.Controls
{
    [Designer("MetroFramework.Design.Controls.MetroTileDesigner, MetroFramework.Design, Version=1.4.0.0, Culture=neutral, PublicKeyToken=5f91a84759bf584a"), ToolboxBitmap(typeof(Button))]
    public class MetroProvaTile : Button, IContainerControl, IMetroControl
    {
        private MetroColorStyle metroStyle;

        private MetroThemeStyle metroTheme;

        private MetroStyleManager metroStyleManager;

        private bool useCustomBackColor;

        private bool useCustomForeColor;

        private bool useStyleColors;

        private Control activeControl;

        private bool paintTileCount = true;

        private int tileCount;

        private Image tileImage;

        private bool useTileImage;

        private ContentAlignment tileImageAlign = ContentAlignment.TopLeft;

        private MetroTileTextSize tileTextFontSize = MetroTileTextSize.Medium;

        private MetroTileTextWeight tileTextFontWeight;

        private bool isHovered;

        private bool isPressed;

        private bool isFocused;

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        [Category("Metro Appearance"), DefaultValue(MetroColorStyle.Default)]
        public MetroColorStyle Style
        {
            get
            {
                if (base.DesignMode || this.metroStyle != MetroColorStyle.Default)
                {
                    return this.metroStyle;
                }
                if (this.StyleManager != null && this.metroStyle == MetroColorStyle.Default)
                {
                    return this.StyleManager.Style;
                }
                if (this.StyleManager == null && this.metroStyle == MetroColorStyle.Default)
                {
                    return MetroColorStyle.Blue;
                }
                return this.metroStyle;
            }
            set
            {
                this.metroStyle = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(MetroThemeStyle.Default)]
        public MetroThemeStyle Theme
        {
            get
            {
                if (base.DesignMode || this.metroTheme != MetroThemeStyle.Default)
                {
                    return this.metroTheme;
                }
                if (this.StyleManager != null && this.metroTheme == MetroThemeStyle.Default)
                {
                    return this.StyleManager.Theme;
                }
                if (this.StyleManager == null && this.metroTheme == MetroThemeStyle.Default)
                {
                    return MetroThemeStyle.Light;
                }
                return this.metroTheme;
            }
            set
            {
                this.metroTheme = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroStyleManager StyleManager
        {
            get
            {
                return this.metroStyleManager;
            }
            set
            {
                this.metroStyleManager = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(false)]
        public bool UseCustomBackColor
        {
            get
            {
                return this.useCustomBackColor;
            }
            set
            {
                this.useCustomBackColor = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(false)]
        public bool UseCustomForeColor
        {
            get
            {
                return this.useCustomForeColor;
            }
            set
            {
                this.useCustomForeColor = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(false)]
        public bool UseStyleColors
        {
            get
            {
                return this.useStyleColors;
            }
            set
            {
                this.useStyleColors = value;
            }
        }

        [Browsable(false), Category("Metro Behaviour"), DefaultValue(false)]
        public bool UseSelectable
        {
            get
            {
                return base.GetStyle(ControlStyles.Selectable);
            }
            set
            {
                base.SetStyle(ControlStyles.Selectable, value);
            }
        }

        [Browsable(false)]
        public Control ActiveControl
        {
            get
            {
                return this.activeControl;
            }
            set
            {
                this.activeControl = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(true)]
        public bool PaintTileCount
        {
            get
            {
                return this.paintTileCount;
            }
            set
            {
                this.paintTileCount = value;
            }
        }

        [DefaultValue(0)]
        public int TileCount
        {
            get
            {
                return this.tileCount;
            }
            set
            {
                this.tileCount = value;
            }
        }

        [DefaultValue(ContentAlignment.BottomLeft)]
        public new ContentAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                base.TextAlign = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(null)]
        public Image TileImage
        {
            get
            {
                return this.tileImage;
            }
            set
            {
                this.tileImage = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(false)]
        public bool UseTileImage
        {
            get
            {
                return this.useTileImage;
            }
            set
            {
                this.useTileImage = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(ContentAlignment.TopLeft)]
        public ContentAlignment TileImageAlign
        {
            get
            {
                return this.tileImageAlign;
            }
            set
            {
                this.tileImageAlign = value;
            }
        }

        [Category("Metro Appearance"), DefaultValue(MetroTileTextSize.Medium)]
        public MetroTileTextSize TileTextFontSize
        {
            get
            {
                return this.tileTextFontSize;
            }
            set
            {
                this.tileTextFontSize = value;
                this.Refresh();
            }
        }

        [Category("Metro Appearance"), DefaultValue(MetroTileTextWeight.Light)]
        public MetroTileTextWeight TileTextFontWeight
        {
            get
            {
                return this.tileTextFontWeight;
            }
            set
            {
                this.tileTextFontWeight = value;
                this.Refresh();
            }
        }

        //protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        //{
        //    if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaintBackground != null)
        //    {
        //        this.CustomPaintBackground(this, e);
        //    }
        //}

        //protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        //{
        //    if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaint != null)
        //    {
        //        this.CustomPaint(this, e);
        //    }
        //}

        //protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        //{
        //    if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaintForeground != null)
        //    {
        //        this.CustomPaintForeground(this, e);
        //    }
        //}

        public bool ActivateControl(Control ctrl)
        {
            if (base.Controls.Contains(ctrl))
            {
                ctrl.Select();
                this.activeControl = ctrl;
                return true;
            }
            return false;
        }

        public MetroProvaTile()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.TextAlign = ContentAlignment.BottomLeft;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color color = this.BackColor;
                if (!this.useCustomBackColor)
                {
                    color = MetroPaint.GetStyleColor(this.Style);
                }
                if (color.A == 255)
                {
                    e.Graphics.Clear(color);
                }
                else
                {
                    base.OnPaintBackground(e);
                    this.OnCustomPaintBackground(new MetroPaintEventArgs(color, Color.Empty, e.Graphics));
                }
            }
            catch
            {
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (base.GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    this.OnPaintBackground(e);
                }
                this.OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                this.OnPaintForeground(e);
            }
            catch
            {
                base.Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            Color color = MetroPaint.BorderColor.Button.Normal(this.Theme);
            Color foreColor;
            if (this.isHovered && !this.isPressed && base.Enabled)
            {
                foreColor = MetroPaint.ForeColor.Tile.Hover(this.Theme);
            }
            else if (this.isHovered && this.isPressed && base.Enabled)
            {
                foreColor = MetroPaint.ForeColor.Tile.Press(this.Theme);
            }
            else if (!base.Enabled)
            {
                foreColor = MetroPaint.ForeColor.Tile.Disabled(this.Theme);
            }
            else
            {
                foreColor = MetroPaint.ForeColor.Tile.Normal(this.Theme);
            }
            if (this.useCustomForeColor)
            {
                foreColor = this.ForeColor;
            }
            if (this.isPressed || this.isHovered || this.isFocused)
            {
                using (Pen pen = new Pen(color))
                {
                    pen.Width = 3f;
                    Rectangle rect = new Rectangle(1, 1, base.Width - 3, base.Height - 3);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            if (this.useTileImage && this.tileImage != null)
            {
                ContentAlignment contentAlignment = this.tileImageAlign;
                Rectangle rect2;
                if (contentAlignment <= ContentAlignment.MiddleCenter)
                {
                    switch (contentAlignment)
                    {
                        case ContentAlignment.TopLeft:
                            rect2 = new Rectangle(new Point(0, 0), new Size(this.TileImage.Width, this.TileImage.Height));
                            goto IL_430;
                        case ContentAlignment.TopCenter:
                            rect2 = new Rectangle(new Point(base.Width / 2 - this.TileImage.Width / 2, 0), new Size(this.TileImage.Width, this.TileImage.Height));
                            goto IL_430;
                        case (ContentAlignment)3:
                            break;
                        case ContentAlignment.TopRight:
                            rect2 = new Rectangle(new Point(base.Width - this.TileImage.Width, 0), new Size(this.TileImage.Width, this.TileImage.Height));
                            goto IL_430;
                        default:
                            if (contentAlignment == ContentAlignment.MiddleLeft)
                            {
                                rect2 = new Rectangle(new Point(0, base.Height / 2 - this.TileImage.Height / 2), new Size(this.TileImage.Width, this.TileImage.Height));
                                goto IL_430;
                            }
                            if (contentAlignment == ContentAlignment.MiddleCenter)
                            {
                                rect2 = new Rectangle(new Point(base.Width / 2 - this.TileImage.Width / 2, base.Height / 2 - this.TileImage.Height / 2), new Size(this.TileImage.Width, this.TileImage.Height));
                                goto IL_430;
                            }
                            break;
                    }
                }
                else if (contentAlignment <= ContentAlignment.BottomLeft)
                {
                    if (contentAlignment == ContentAlignment.MiddleRight)
                    {
                        rect2 = new Rectangle(new Point(base.Width - this.TileImage.Width, base.Height / 2 - this.TileImage.Height / 2), new Size(this.TileImage.Width, this.TileImage.Height));
                        goto IL_430;
                    }
                    if (contentAlignment == ContentAlignment.BottomLeft)
                    {
                        rect2 = new Rectangle(new Point(0, base.Height - this.TileImage.Height), new Size(this.TileImage.Width, this.TileImage.Height));
                        goto IL_430;
                    }
                }
                else
                {
                    if (contentAlignment == ContentAlignment.BottomCenter)
                    {
                        rect2 = new Rectangle(new Point(base.Width / 2 - this.TileImage.Width / 2, base.Height - this.TileImage.Height), new Size(this.TileImage.Width, this.TileImage.Height));
                        goto IL_430;
                    }
                    if (contentAlignment == ContentAlignment.BottomRight)
                    {
                        rect2 = new Rectangle(new Point(base.Width - this.TileImage.Width, base.Height - this.TileImage.Height), new Size(this.TileImage.Width, this.TileImage.Height));
                        goto IL_430;
                    }
                }
                rect2 = new Rectangle(new Point(0, 0), new Size(this.TileImage.Width, this.TileImage.Height));
                IL_430:
                e.Graphics.DrawImage(this.TileImage, rect2);
            }
            if (this.TileCount > 0 && this.paintTileCount)
            {
                Size size = TextRenderer.MeasureText(this.TileCount.ToString(), MetroFonts.TileCount);
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                TextRenderer.DrawText(e.Graphics, this.TileCount.ToString(), MetroFonts.TileCount, new Point(base.Width - size.Width, 0), foreColor);
                e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            }
            TextRenderer.MeasureText(this.Text, MetroFonts.Tile(this.tileTextFontSize, this.tileTextFontWeight));
            TextFormatFlags flags = MetroPaint.GetTextFormatFlags(this.TextAlign, true) | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.EndEllipsis;
            Rectangle clientRectangle = base.ClientRectangle;
            if (this.isPressed)
            {
                clientRectangle.Inflate(-4, -12);
            }
            else
            {
                clientRectangle.Inflate(-2, -10);
            }
            TextRenderer.DrawText(e.Graphics, this.Text, MetroFonts.Tile(this.tileTextFontSize, this.tileTextFontWeight), clientRectangle, foreColor, flags);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.isFocused = true;
            this.isHovered = true;
            base.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.isFocused = false;
            this.isHovered = false;
            this.isPressed = false;
            base.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            this.isFocused = true;
            this.isHovered = true;
            base.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.isFocused = false;
            this.isHovered = false;
            this.isPressed = false;
            base.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.isHovered = true;
                this.isPressed = true;
                base.Invalidate();
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.Invalidate();
            base.OnKeyUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.isHovered = true;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isPressed = true;
                base.Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.isPressed = false;
            base.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!this.isFocused)
            {
                this.isHovered = false;
            }
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            base.Invalidate();
        }
    }
}
