using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{
    public class MetroToolbarSeparator : MetroLink, IMetroToolBarButton
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

        public string nome;

        protected override void OnPaintForeground(PaintEventArgs e)
        {
            base.OnPaintForeground(e);

            DrawVerticalLine(e.Graphics);
        }

        public MetroToolbarSeparator() : base()
        {
            Size = new Size(4, 46);
            Dock = DockStyle.Left;
            Text = string.Empty;
            ImageSize = 32;
            Enabled = false;
        }

        private void DrawVerticalLine(Graphics g)
        {
            if (Visible)
                using (var pen = new Pen(Color.FromArgb(192, 192, 192), 1))
                {
                    g.DrawLine(pen, new Point(2, 8), new Point(2, 40));
                }
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
