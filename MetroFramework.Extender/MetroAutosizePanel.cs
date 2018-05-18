using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public class MetroAutosizePanel : Controls.MetroPanel
    {
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (Parent != null)
            {
                Parent.Layout += Parent_Layout;
                Parent.Resize += Parent_Resize;
            }
        }
        private void Parent_Resize(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (Parent != null)
            {
                Width = Parent.ClientSize.Width - 40;
                Height = Parent.ClientSize.Height - Top - 20;
            }
        }

        private void Parent_Layout(object sender, LayoutEventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (Parent != null)
            {
                Width = Parent.ClientSize.Width - 40;
                Height = Parent.ClientSize.Height - Top - 20;
            }
        }
    }
}
