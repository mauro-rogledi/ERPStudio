using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{
    public class MetroToolbarLabel : MetroLabel, IMetroToolBarButton
    {
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

        public MetroToolbarLabel() : base()
        {
            Size = new Size(8, 34);
            Dock = DockStyle.Left;
            TextAlign = ContentAlignment.MiddleCenter;
        }

        protected override void OnTextChanged(EventArgs e)
        {
           base.OnTextChanged(e);
           var size = TextRenderer.MeasureText(Text, Font);
           Size = new Size(size.Width+6, 34);
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
