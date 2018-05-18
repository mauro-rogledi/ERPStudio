using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public class MetroTreeView : System.Windows.Forms.TreeView, IMetroControl
    {
        #region Interface

        // [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        // [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        // [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private MetroColorStyle metroStyle = MetroColorStyle.Default;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroColorStyle.Default)]
        public MetroColorStyle Style
        {
            get
            {
                if (DesignMode || metroStyle != MetroColorStyle.Default)
                {
                    return metroStyle;
                }

                if (StyleManager != null && metroStyle == MetroColorStyle.Default)
                {
                    return StyleManager.Style;
                }
                if (StyleManager == null && metroStyle == MetroColorStyle.Default)
                {
                    return ExtenderDefaults.Style;
                }

                return metroStyle;
            }
            set { metroStyle = value; }
        }

        private MetroThemeStyle metroTheme = MetroThemeStyle.Default;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroThemeStyle.Default)]
        public MetroThemeStyle Theme
        {
            get
            {
                if (DesignMode || metroTheme != MetroThemeStyle.Default)
                {
                    return metroTheme;
                }

                if (StyleManager != null && metroTheme == MetroThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }
                if (StyleManager == null && metroTheme == MetroThemeStyle.Default)
                {
                    return ExtenderDefaults.Theme;
                }

                return metroTheme;
            }
            set { metroTheme = value; }
        }

        private MetroStyleManager metroStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        #endregion

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; UpdateTreeView(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; UpdateTreeView(); }
        }

        private void UpdateTreeView()
        {
            Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
        }

        public MetroTreeView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
        }

        #region Paint Methods
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                    BackColor = backColor;
                }

                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new MetroPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            if (!useCustomForeColor)
            {
                ForeColor = MetroPaint.ForeColor.Button.Normal(Theme);
            }

            Color borderColor = MetroPaint.BorderColor.ComboBox.Normal(Theme);

            if (useStyleColors)
                borderColor = MetroPaint.GetStyleColor(Style);

            using (Pen p = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 2, Height - 1));
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            int left = e.Bounds.X;
            int top = e.Bounds.Y;
            int height = e.Bounds.Height;
            int nodeleft = e.Node.Bounds.X;
            int nodetop = e.Node.Bounds.Y;
            int nodeheight = e.Node.Bounds.Height;
            int nodewidth = e.Node.Bounds.Width;
            int checkboxwidth = 13;
            int checknodespace = 0;

            int lineleft = nodeleft - checknodespace - checkboxwidth;
            if (e.Node.Nodes.Count > 0)
            {
                if (!e.Node.IsExpanded)
                {
                    e.Graphics.DrawImage(Properties.Resources.SortRight10,
                        lineleft - Properties.Resources.SortRight10.Width / 2, top + (height -
                        Properties.Resources.SortRight10.Height) / 2);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.SortDown10,
                        lineleft - Properties.Resources.SortDown10.Width / 2, top + (height -
                        Properties.Resources.SortDown10.Height) / 2);
                }
            }
            else
                e.Graphics.DrawImage(Properties.Resources.Circle10,
                    lineleft - Properties.Resources.Circle10.Width / 2, top + (height -
                    Properties.Resources.Circle10.Height) / 2);

            using (Brush b = new SolidBrush(BackColor))
            {
                e.Graphics.DrawString(e.Node.Text, Font, b, nodeleft, nodetop + (nodeheight - this.Font.Height) / 2);
                e.Graphics.FillRectangle(b, new Rectangle(nodeleft + 1, nodetop + 1, nodewidth - 2, nodeheight - 2));
            }
            // draw background 
            if (this.Focused && e.Node.IsSelected)
            {
                // draw highlight background
                using (Brush b = new SolidBrush(MetroPaint.GetStyleColor(Style)))
                    e.Graphics.FillRectangle(b, new Rectangle(nodeleft + 1, nodetop + 1, nodewidth - 2, nodeheight - 2));
            }
            else if (!this.Focused && !this.HideSelection && e.Node.IsSelected)
            {
                // draw gray background
                e.Graphics.FillRectangle(SystemBrushes.ControlLight, new Rectangle(nodeleft + 1, nodetop + 1, nodewidth - 2, nodeheight - 2));
            }
            // draw highlight node text  
            int pos = nodetop + (nodeheight - this.Font.Height) / 2;

            if (pos >= 0)
            {
                if (e.Node.IsSelected && this.Focused)
                {
                    using (Brush b = new SolidBrush(SystemColors.HighlightText))
                    {
                        e.Graphics.DrawString(e.Node.Text, Font, b, nodeleft, nodetop + (nodeheight - this.Font.Height) / 2);
                    }
                }
                else
                {
                    using (Brush b = new SolidBrush(ForeColor))
                    {
                        e.Graphics.DrawString(e.Node.Text, Font, b, nodeleft, nodetop + (nodeheight - this.Font.Height) / 2);
                    }
                }
            }
        }
        #endregion
    }
}
