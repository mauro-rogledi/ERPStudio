using System;
using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Extender
{
    public class MetroToolbarContainer: Control, IMetroToolBarButton, IMetroControl
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
            set { metroComboBoxSize = value; UpdateContainer(); }
        }

        private MetroComboBoxWeight metroComboBoxWeight = MetroComboBoxWeight.Regular;
        [DefaultValue(MetroComboBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroComboBoxWeight FontWeight
        {
            get { return metroComboBoxWeight; }
            set { metroComboBoxWeight = value; UpdateContainer(); }
        }
        [EditorBrowsableAttribute(EditorBrowsableState.Always)]
        [EditorAttribute(typeof(MetroToolbarContainerEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public ControlCollection Items { get { return base.Controls; } }
        #endregion

        public MetroToolbarContainer() : base()
        {
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

        private void UpdateContainer()
        {
            
        }

        internal class MetroToolbarContainerEditor : System.ComponentModel.Design.CollectionEditor
        {
            private Type[] types;

            public MetroToolbarContainerEditor(Type type) : base(type)
            {
                types = new Type[]
                {
                typeof(MetroLink),
                typeof(MetroLabel)
                };

            }


            protected override Type[] CreateNewItemTypes()
            {
                return types;
            }
        }
    }
}
