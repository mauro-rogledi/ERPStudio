using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Extender
{
    public class MetroListBox : Control, IMetroControl
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

        private ListBox baseListBox = null;

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; UpdateListBox(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; UpdateListBox(); }
        }
        #endregion

        #region Routing Fields

        public override ContextMenu ContextMenu
        {
            get { return baseListBox.ContextMenu; }
            set
            {
                ContextMenu = value;
                baseListBox.ContextMenu = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get { return baseListBox.ContextMenuStrip; }
            set
            {
                ContextMenuStrip = value;
                baseListBox.ContextMenuStrip = value;
            }
        }

        public override string Text
        {
            get { return baseListBox.Text; }
            set { baseListBox.Text = value; }
        }

        [DefaultValue(true)]
        public new bool TabStop
        {
            get { return baseListBox.TabStop; }
            set { baseListBox.TabStop = value; }
        }

        public ListBox.ObjectCollection Items
        {
            get { return baseListBox.Items; }
        }

        public int SelectedIndex
        {
            get { return baseListBox.SelectedIndex; }
            set { baseListBox.SelectedIndex = value; }
        }

        public object SelectedItem
        {
            get { return baseListBox.SelectedItem; }
            set { baseListBox.SelectedItem = value; }
        }

        public ListBox.SelectedObjectCollection SelectedItems
        {
            get { return baseListBox.SelectedItems; }
        }

        public event EventHandler SelectedIndexChanged;

        public object DataSource
        {
            get { return baseListBox.DataSource; }
            set { baseListBox.DataSource = value; }
        }

        public string DisplayMember
        {
            get { return baseListBox.DisplayMember; }
            set { baseListBox.DisplayMember = value; }
        }

        public string ValueMember
        {
            get { return baseListBox.ValueMember; }
            set { baseListBox.ValueMember = value; }
        }


        #endregion

        #region Constructor
        public MetroListBox()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            base.TabStop = false;

            CreateListBox();
            UpdateListBox();
            AddEventHandler();
        }
        #endregion

        #region Private Methods

        private void CreateListBox()
        {
            if (baseListBox != null) return;

            baseListBox = new ListBox();

            baseListBox.BorderStyle = BorderStyle.None;
            baseListBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
            baseListBox.Location = new Point(3, 3);
            baseListBox.Size = new Size(Width - 6, Height - 6);

            Size = new Size(baseListBox.Width + 6, baseListBox.Height + 6);

            baseListBox.TabStop = true;

            Controls.Add(baseListBox);
        }

        private void AddEventHandler()
        {
            baseListBox.CausesValidationChanged += BaseListBoxCausesValidationChanged;
            baseListBox.ChangeUICues += BaseListBoxChangeUiCues;
            baseListBox.Click += BaseListBoxClick;
            baseListBox.DoubleClick += BaseListBox_DoubleClick;
            baseListBox.ClientSizeChanged += BaseListBoxClientSizeChanged;
            baseListBox.ContextMenuChanged += BaseListBoxContextMenuChanged;
            baseListBox.ContextMenuStripChanged += BaseListBoxContextMenuStripChanged;
            baseListBox.CursorChanged += BaseListBoxCursorChanged;

            baseListBox.KeyDown += BaseListBoxKeyDown;
            baseListBox.KeyPress += BaseListBoxKeyPress;
            baseListBox.KeyUp += BaseListBoxKeyUp;

            baseListBox.SizeChanged += BaseListBoxSizeChanged;

            baseListBox.TextChanged += BaseListBoxTextChanged;
            baseListBox.GotFocus += baseTextBox_GotFocus;
            baseListBox.LostFocus += baseTextBox_LostFocus;
            baseListBox.MouseDown += BaseListBox_MouseDown;
            baseListBox.MouseUp += BaseListBox_MouseUp;

            baseListBox.SelectedIndexChanged += BaseListBox_SelectedIndexChanged;
        }

        private void BaseListBox_DoubleClick(object sender, EventArgs e)
        {
            base.OnDoubleClick(e);
        }

        private void BaseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        private void BaseListBox_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void BaseListBox_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        void _button_MouseLeave(object sender, EventArgs e)
        {
            UseStyleColors = baseListBox.Focused;
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
           // this.InvokeGotFocus(this, e);
        }
        private void UpdateListBox()
        {
            if (baseListBox == null) return;

            baseListBox.Location = new Point(3, 3);
            baseListBox.Size = new Size(Width - 6, Height - 6);

            baseListBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
        }

        #endregion

        #region Routing Methods

        private void BaseListBoxSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void BaseListBoxCursorChanged(object sender, EventArgs e)
        {
            //base.OnCursorChanged(e);
        }

        private void BaseListBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void BaseListBoxContextMenuChanged(object sender, EventArgs e)
        {
            base.OnContextMenuChanged(e);
        }

        private void BaseListBoxClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void BaseListBoxClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void BaseListBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void BaseListBoxCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void BaseListBoxKeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void BaseListBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void BaseListBoxKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        bool _cleared = false;
        bool _withtext = false;

        private void BaseListBoxTextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);
            OnTextChanged(e);

            if (baseListBox.Text != "" && !_withtext)
            {
                _withtext = true;
                _cleared = false;
                Invalidate();
            }

            if (baseListBox.Text == "" && !_cleared)
            {
                _withtext = false;
                _cleared = true;
                Invalidate();
            }
        }

        void MetroTextBox_GotFocus(object sender, EventArgs e)
        {
            baseListBox.Focus();
        }

        #endregion

        #region Paint Methods
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;
                baseListBox.BackColor = backColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                    baseListBox.BackColor = backColor;
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
            if (useCustomForeColor)
            {
                baseListBox.ForeColor = ForeColor;
            }
            else
            {
                baseListBox.ForeColor = MetroPaint.ForeColor.Button.Normal(Theme);
            }

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
            UpdateListBox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateListBox();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            baseListBox.Focus();
        }
        #endregion
    }
}
