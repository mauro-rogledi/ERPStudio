using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Extender
{
    public class MetroToolbarComboBox : Control, IMetroToolBarButton, IMetroControl
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

        private MetroComboBoxSize metroComboBoxSize = MetroComboBoxSize.Small;
        [DefaultValue(MetroComboBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroComboBoxSize FontSize
        {
            get { return metroComboBoxSize; }
            set { metroComboBoxSize = value; UpdateMetroComboBox(); }
        }

        private MetroComboBoxWeight metroComboBoxWeight = MetroComboBoxWeight.Regular;
        [DefaultValue(MetroComboBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroComboBoxWeight FontWeight
        {
            get { return metroComboBoxWeight; }
            set { metroComboBoxWeight = value; UpdateMetroComboBox(); }
        }

        public ComboBox.ObjectCollection Items
        {
            get { return metroComboBox.Items; }
        }

        public string SelectedText
        {
            get { return metroComboBox.SelectedText; }
            set { metroComboBox.SelectedText = value; }
        }

        public MetroComboBox Control
        {
            get { return metroComboBox; }
        }

        public new string Text
        {
            get { return metroComboBox.Text; }
            set { metroComboBox.Text = value; }
        }
        #endregion

        #region Events
        [Category(ExtenderDefaults.PropertyCategory.Event)]
        public event EventHandler SelectedIndexChanged;
        #endregion

        private MetroComboBox metroComboBox;

        public MetroToolbarComboBox() : base()
        {
            metroComboBox = new MetroComboBox();
            Size = new Size(90, 34);
            Dock = DockStyle.Left;
            Text = string.Empty;
            BackColor = Color.White;
            
            metroComboBox.UseStyleColors = true;
            metroComboBox.FontSize = MetroComboBoxSize.Medium;
            metroComboBox.FontWeight = MetroComboBoxWeight.Regular;
            Controls.Add(metroComboBox);

            AddEventHandler();
        }

        private void AddEventHandler()
        {
            metroComboBox.SelectedIndexChanged += MetroComboBox_SelectedIndexChanged;
        }

        private void MetroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            metroComboBox.Size = new Size(Width-6, 29);
            metroComboBox.Location = new Point(3, (Height - 29) / 2);
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


        private void UpdateMetroComboBox()
        {
            metroComboBox.FontSize = metroComboBoxSize;
            metroComboBox.FontWeight = metroComboBoxWeight;
            metroComboBox.Enabled = Enabled;
        }
    }
}
