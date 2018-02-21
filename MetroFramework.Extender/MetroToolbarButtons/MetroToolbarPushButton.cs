using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{
    public class MetroToolbarPushButton : MetroLink, IMetroToolBarButton
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

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ToolBarButtonStyle.PushButton)]
        public bool IsVisible
        {
            get { return Visible; }
            set { ChangeVisible(value); }
        }

        #region Focus Methods

        public MetroToolbarPushButton() : base()
        {
            Size = new Size(40, 34);
            Dock = DockStyle.Left;
            Text = string.Empty;
            ImageSize = 32;
            Margin = new Padding(6);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Parent is MetroToolbar)
                (Parent as MetroToolbar).ToolbarPushButtonClicked(this);
        }

        #endregion

        private void ChangeVisible(bool value)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || Parent == null)
                return;

            if (value == Visible)
                return;

            Visible = value;

            if (Parent is MetroToolbar)
                (Parent as MetroToolbar).ButtonVisibleChanged();
        }
    }
}
