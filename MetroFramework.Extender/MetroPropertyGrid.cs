using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Extender
{
    public class MetroPropertyGrid : Control, IMetroControl
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
            set { metroStyleManager = value;  UpdatePropertyGrid(); }
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
        private PropertyGrid propertyGrid = null;
        private MetroLinkChecked btnAlfabetical = null;
        private MetroLinkChecked btnGrouped = null;

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; UpdatePropertyGrid(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; UpdatePropertyGrid(); }
        }

        private bool toolbarVisible = true;
        [DefaultValue(true)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool ToolbarVisible
        {
            get { return toolbarVisible; }
            set { toolbarVisible = value; UpdatePropertyGrid(); }
        }

        private bool helpVisible = true;
        [DefaultValue(true)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool HelpVisible
        {
            get { return helpVisible; }
            set { helpVisible = value; UpdatePropertyGrid(); }
        }
        #endregion

        #region Constructor
        public MetroPropertyGrid()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            base.TabStop = false;

            CreatePropertyGrid();
            UpdatePropertyGrid();
        }

        #endregion

        #region Private Methods
        private void CreatePropertyGrid()
        {
            if (propertyGrid != null) return;

            propertyGrid = new PropertyGrid();
            propertyGrid.ToolbarVisible = false;
            Controls.Add(propertyGrid);

            btnGrouped = new MetroLinkChecked();
            btnGrouped.Visible = toolbarVisible;
            btnGrouped.Location = new Point(3, 3);
            btnGrouped.Size = new Size(25, 25);
            btnGrouped.Image = Properties.Resources.GroupofQuestions24;
            btnGrouped.NoFocusCheckedImage = Properties.Resources.GroupofQuestions24g;
            btnGrouped.CheckedImage = Properties.Resources.GroupofQuestions24ck;
            btnGrouped.NoFocusCheckedImage = Properties.Resources.GroupofQuestions24ckg;
            btnGrouped.ImageSize = 24;
            btnGrouped.Checked = true;
            btnGrouped.CheckOnClick = false;
            btnGrouped.Click += btnGrouped_CheckedChanged;
            Controls.Add(btnGrouped);

            btnAlfabetical = new MetroLinkChecked();
            btnAlfabetical.Visible = toolbarVisible;
            btnAlfabetical.Location = new Point(31, 3);
            btnAlfabetical.Size = new Size(25, 25);
            btnAlfabetical.Image = Properties.Resources.AlphabeticalSorting24;
            btnAlfabetical.NoFocusCheckedImage = Properties.Resources.AlphabeticalSorting24g;
            btnAlfabetical.CheckedImage = Properties.Resources.AlphabeticalSorting24ck;
            btnAlfabetical.NoFocusCheckedImage = Properties.Resources.AlphabeticalSorting24ckg;
            btnAlfabetical.ImageSize = 24;
            btnAlfabetical.Checked = false;
            btnAlfabetical.CheckOnClick = false;
            btnAlfabetical.Click += BtnAlfabetical_CheckedChanged;

            Controls.Add(btnAlfabetical);
            UpdatePropertyGrid();
        }

        private void btnGrouped_CheckedChanged(object sender, EventArgs e)
        {
            btnGrouped.Checked = true;
            btnAlfabetical.Checked = !btnGrouped.Checked;
            propertyGrid.PropertySort = PropertySort.CategorizedAlphabetical;
        }

        private void BtnAlfabetical_CheckedChanged(object sender, EventArgs e)
        {
            btnAlfabetical.Checked = true;
            btnGrouped.Checked = !btnAlfabetical.Checked;
            propertyGrid.PropertySort = PropertySort.Alphabetical;
        }

        private void UpdatePropertyGrid()
        {
            propertyGrid.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
            int toolbarHeight = toolbarVisible ? 25 : 0;
            propertyGrid.Size = new Size(Width - 6, Height - 6 - toolbarHeight);
            propertyGrid.Location = new Point(3, 3 + toolbarHeight);
            propertyGrid.HelpVisible = helpVisible;
            propertyGrid.HelpBackColor = MetroPaint.BackColor.Button.Normal(Theme);
            propertyGrid.HelpForeColor = MetroPaint.GetStyleColor(Style);
            propertyGrid.HelpBorderColor = MetroPaint.BorderColor.Button.Normal(Theme);
            propertyGrid.CategorySplitterColor = MetroPaint.GetStyleColor(Style);
            propertyGrid.LineColor = MetroPaint.GetStyleColor(Style);
            propertyGrid.SelectedItemWithFocusBackColor = MetroPaint.GetStyleColor(Style);
            propertyGrid.SelectedItemWithFocusForeColor = (Theme == MetroThemeStyle.Light) ? Color.FromArgb(17, 17, 17) : Color.FromArgb(255, 255, 255);
            propertyGrid.ViewBackColor = MetroPaint.BackColor.Form(Theme);
            propertyGrid.ViewForeColor = (Theme == MetroThemeStyle.Light) ? Color.FromArgb(17, 17, 17) : Color.FromArgb(255, 255, 255);
            propertyGrid.ViewBorderColor = MetroPaint.BorderColor.Button.Normal(Theme);

            btnGrouped.Visible = toolbarVisible;
            btnAlfabetical.Visible = toolbarVisible;
        }
        #endregion

        #region Routing Fields
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public object SelectedObject
        {
            get { return propertyGrid == null ? null : propertyGrid.SelectedObject; }
            set { if (propertyGrid != null) propertyGrid.SelectedObject = value; }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (propertyGrid != null)
                UpdatePropertyGrid();
        }
        #endregion
    }
}
