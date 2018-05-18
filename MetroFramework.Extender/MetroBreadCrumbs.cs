using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using MetroFramework.Controls;
using System.Collections.Generic;

namespace MetroFramework.Extender
{
    public class MetroBreadCrumbs : Control, IMetroControl
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

        #region Fields

        public List<KeyValuePair<string, string>> Crumbs = new List<KeyValuePair<string, string>>();

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; RefreshCrumbs(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; RefreshCrumbs(); }
        }

        #endregion

        #region Routing Fields
        [Category(Extender.ExtenderDefaults.PropertyCategory.Event)]
        public event EventHandler<string> BreadClick;

        #endregion

        #region Constructor
        public MetroBreadCrumbs()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            base.TabStop = false;

            RefreshCrumbs();
            AddEventHandler();
        }
        #endregion

        #region Public Methods
        public void Clear()
        {
            Crumbs.Clear();
            RemoveCrumbs();
        }

        public void AddCrumb(string key, string value)
        {
            Crumbs.Add(new KeyValuePair<string, string>(key, value));
            RefreshCrumbs();
        }

        private void RemoveCrumbs()
        {
            Controls.Clear();
        }
        #endregion

        #region Private Methods

        private void RefreshCrumbs()
        {
            SuspendLayout();
            RemoveCrumbs();

            bool firstTime = true;
            foreach (KeyValuePair<string, string> kvp in Crumbs)
            {
                int cntr = Controls.Count;
                if (!firstTime)
                {
                    MetroLabel lbl = new MetroLabel() { Text = "", AutoSize = true };
                    lbl.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
                    lbl.Location = new Point(Controls[cntr - 1].Width + Controls[cntr - 1].Location.X, Controls[cntr - 1].Location.Y);
                    Controls.Add(lbl);
                }

                firstTime = false;
                MetroLabel lnklbl = new MetroLabel() { Text = kvp.Value, Tag = kvp.Key, AutoSize = true };
                lnklbl.DoubleClick += Lnklbl_DoubleClick;
                if (cntr > 0)
                    lnklbl.Location = new Point(Controls[cntr].Width + Controls[cntr].Location.X, 3);
                else
                    lnklbl.Location = new Point(3, 3);
                Controls.Add(lnklbl);
            }
            ResumeLayout();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
                if (BreadClick != null)
                    BreadClick(this, "");

        }
        private void AddEventHandler()
        {
            DoubleClick += MetroBreadCrumbs_DoubleClick;
        }

        private void Lnklbl_DoubleClick(object sender, EventArgs e)
        {
            if (BreadClick != null)
                BreadClick(sender, this.Text/* ((MetroLabel)sender).Tag.ToString()*/);
        }

        private void MetroBreadCrumbs_DoubleClick(object sender, EventArgs e)
        {
            if (BreadClick != null)
                BreadClick(sender, this.Text);
        }

        private void BaseTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void BaseTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        void _button_MouseLeave(object sender, EventArgs e)
        {
           // UseStyleColors = baseTextBox.Focused;
            Invalidate();
        }

        void _button_MouseEnter(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
        }

        void _button_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }
        void baseTextBox_LostFocus(object sender, EventArgs e)
        {
            UseStyleColors = false;
            Invalidate();
            this.InvokeLostFocus(this, e);
        }

        void baseTextBox_GotFocus(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
            this.InvokeGotFocus(this, e);
        }
        #endregion

        #region Routing Methods

        public event EventHandler AcceptsTabChanged;
        private void BaseTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            if (AcceptsTabChanged != null)
                AcceptsTabChanged(this, e);
        }

        private void BaseTextBoxSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void BaseTextBoxCursorChanged(object sender, EventArgs e)
        {
            //base.OnCursorChanged(e);
        }

        private void BaseTextBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void BaseTextBoxContextMenuChanged(object sender, EventArgs e)
        {
            base.OnContextMenuChanged(e);
        }

        private void BaseTextBoxClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void BaseTextBoxClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void BaseTextBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void BaseTextBoxCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void BaseTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void BaseTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void BaseTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
        #endregion

        #region Paint Methods
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;
                //baseTextBox.BackColor = backColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                    //baseTextBox.BackColor = backColor;
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
            //if (useCustomForeColor)
            //{
            //    baseTextBox.ForeColor = ForeColor;
            //}
            //else
            //{
            //    baseTextBox.ForeColor = MetroPaint.ForeColor.Button.Normal(Theme);
            //}

            Color borderColor = MetroPaint.BorderColor.ComboBox.Normal(Theme);

            if (useStyleColors)
                borderColor = MetroPaint.GetStyleColor(Style);

            using (Pen p = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 2, Height - 1));
            }
        }
        #endregion

        #region Overridden Methods

        public override void Refresh()
        {
            base.Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshCrumbs();
        }
        #endregion
    }
}
