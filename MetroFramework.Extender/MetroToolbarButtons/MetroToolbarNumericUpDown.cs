using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Extender
{
    public class MetroToolbarNumericUpDown : Control, IMetroToolBarButton, IMetroControl
    {
        #region Interface

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
                CustomPaintBackground?.Invoke(this, e);
        }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
                CustomPaint?.Invoke(this, e);
        }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
                CustomPaintForeground?.Invoke(this, e);
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
        private MetroToolbarButtonType buttonType = MetroToolbarButtonType.New;
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(MetroToolbarButtonType.New)]
        public MetroToolbarButtonType ButtonType { get { return buttonType; } set { buttonType = value; } }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(true)]
        public bool IsVisible
        {
            get { return Visible; }
            set { ChangeVisible(value); }
        }

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; UpdateMetroNumericUpDown(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; UpdateMetroNumericUpDown(); }
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return metroNumericUpDown.ReadOnly; }
            set
            {
                metroNumericUpDown.ReadOnly = value;
            }
        }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public int Value
        {
            get
            {
                return metroNumericUpDown.Value;
            }
            set
            {
                metroNumericUpDown.Value = value;
            }
        }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public int MaxValue
        {
            get
            {
                return metroNumericUpDown.MaxValue;
            }
            set
            {
                metroNumericUpDown.MaxValue = value;
            }
        }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public int MinValue
        {
            get
            {
                return metroNumericUpDown.MinValue;
            }
            set
            {
                metroNumericUpDown.MinValue = value;
            }
        }
        #endregion

        #region Routing Fields

        public override ContextMenu ContextMenu
        {
            get { return metroNumericUpDown.ContextMenu; }
            set
            {
                ContextMenu = value;
                metroNumericUpDown.ContextMenu = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get { return metroNumericUpDown.ContextMenuStrip; }
            set
            {
                ContextMenuStrip = value;
                metroNumericUpDown.ContextMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get { return metroNumericUpDown.Multiline; }
            set { metroNumericUpDown.Multiline = value; }
        }

        public override string Text
        {
            get { return metroNumericUpDown.Text; }
            set { metroNumericUpDown.Text = value; }
        }

        public string[] Lines
        {
            get { return metroNumericUpDown.Lines; }
            set { metroNumericUpDown.Lines = value; }
        }

        [Browsable(false)]
        public string SelectedText
        {
            get { return metroNumericUpDown.SelectedText; }
            set { metroNumericUpDown.Text = value; }
        }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; UpdateMetroNumericUpDown(); }
        }

        public int SelectionStart
        {
            get { return metroNumericUpDown.SelectionStart; }
            set { metroNumericUpDown.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return metroNumericUpDown.SelectionLength; }
            set { metroNumericUpDown.SelectionLength = value; }
        }

        [DefaultValue(true)]
        public new bool TabStop
        {
            get { return metroNumericUpDown.TabStop; }
            set { metroNumericUpDown.TabStop = value; }
        }

        public ScrollBars ScrollBars
        {
            get { return metroNumericUpDown.ScrollBars; }
            set { metroNumericUpDown.ScrollBars = value; }
        }
        public bool ShortcutsEnabled
        {
            get { return metroNumericUpDown.ShortcutsEnabled; }
            set { metroNumericUpDown.ShortcutsEnabled = value; }
        }
        #endregion

        #region Routing Methods

        public event EventHandler AcceptsTabChanged;
        private void metroNumericUpDownAcceptsTabChanged(object sender, EventArgs e)
        {
            if (AcceptsTabChanged != null)
                AcceptsTabChanged(this, e);
        }

        private void metroNumericUpDownSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void metroNumericUpDownCursorChanged(object sender, EventArgs e)
        {
            //base.OnCursorChanged(e);
        }

        private void metroNumericUpDownContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void metroNumericUpDownContextMenuChanged(object sender, EventArgs e)
        {
            base.OnContextMenuChanged(e);
        }

        private void metroNumericUpDownClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void metroNumericUpDownClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void metroNumericUpDownChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void metroNumericUpDownCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void metroNumericUpDownKeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void metroNumericUpDownKeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void metroNumericUpDownKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        bool _cleared = false;
        bool _withtext = false;

        private void metroNumericUpDownTextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);
            OnTextChanged(e);

            if (metroNumericUpDown.Text != "" && !_withtext)
            {
                _withtext = true;
                _cleared = false;
                Invalidate();
            }

            if (metroNumericUpDown.Text == "" && !_cleared)
            {
                _withtext = false;
                _cleared = true;
                Invalidate();
            }
        }

        public void Select(int start, int length)
        {
            metroNumericUpDown.Select(start, length);
        }

        public void SelectAll()
        {
            metroNumericUpDown.SelectAll();
        }

        public void Clear()
        {
            metroNumericUpDown.Clear();
        }

        void MetroTextBox_GotFocus(object sender, EventArgs e)
        {
            metroNumericUpDown.Focus();
        }

        public void AppendText(string text)
        {
            metroNumericUpDown.AppendText(text);
        }

        #endregion

        private MetroNumericUpDown metroNumericUpDown;

        public MetroToolbarNumericUpDown() : base()
        {
            metroNumericUpDown = new MetroNumericUpDown();
            Size = new Size(90, 34);
            Dock = DockStyle.Left;
            BackColor = Color.White;
            TabStop = false;

            metroNumericUpDown.UseStyleColors = true;
            Controls.Add(metroNumericUpDown);

            AddEventHandler();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            metroNumericUpDown.Size = new Size(Width-6, 29);
            metroNumericUpDown.Location = new Point(3, (Height - 29) / 2);
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

        private void UpdateMetroNumericUpDown()
        {
            //metroNumericUpDown.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
            metroNumericUpDown.FontSize = metroTextBoxSize;
            metroNumericUpDown.FontWeight = metroTextBoxWeight;
            metroNumericUpDown.Enabled = Enabled;
        }

        private void AddEventHandler()
        {
            metroNumericUpDown.AcceptsTabChanged += metroNumericUpDownAcceptsTabChanged;

            metroNumericUpDown.CausesValidationChanged += metroNumericUpDownCausesValidationChanged;
            metroNumericUpDown.ChangeUICues += metroNumericUpDownChangeUiCues;
            metroNumericUpDown.Click += metroNumericUpDownClick;
            metroNumericUpDown.ClientSizeChanged += metroNumericUpDownClientSizeChanged;
            metroNumericUpDown.ContextMenuChanged += metroNumericUpDownContextMenuChanged;
            metroNumericUpDown.ContextMenuStripChanged += metroNumericUpDownContextMenuStripChanged;
            metroNumericUpDown.CursorChanged += metroNumericUpDownCursorChanged;
            metroNumericUpDown.Validating += metroNumericUpDown_Validating;

            metroNumericUpDown.KeyDown += metroNumericUpDownKeyDown;
            metroNumericUpDown.KeyPress += metroNumericUpDownKeyPress;
            metroNumericUpDown.KeyUp += metroNumericUpDownKeyUp;

            metroNumericUpDown.SizeChanged += metroNumericUpDownSizeChanged;

            metroNumericUpDown.TextChanged += metroNumericUpDownTextChanged;
            metroNumericUpDown.GotFocus += metroNumericUpDown_GotFocus;
            metroNumericUpDown.LostFocus += metroNumericUpDown_LostFocus;
            metroNumericUpDown.MouseDown += metroNumericUpDown_MouseDown;
            metroNumericUpDown.MouseUp += metroNumericUpDown_MouseUp;
        }

        private void metroNumericUpDown_Validating(object sender, CancelEventArgs e)
        {
            base.OnValidating(e);
        }

        private void metroNumericUpDown_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void metroNumericUpDown_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        void metroNumericUpDown_LostFocus(object sender, EventArgs e)
        {
            UseStyleColors = false;
            Invalidate();
            this.InvokeLostFocus(this, e);
        }

        void metroNumericUpDown_GotFocus(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
            this.InvokeGotFocus(this, e);
        }
    }
}
