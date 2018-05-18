using System.Windows.Forms;
using MetroFramework.Drawing;

namespace ERPFramework.Controls
{
    public class HorizontalSeparator : MetroFramework.Controls.MetroPanel
    {
        // Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            System.Drawing.Pen pen = MetroPaint.GetStylePen(Style);
            pen.Width = 3;
            e.Graphics.DrawLine(pen, 0, 0, Width - 1, 0);
        }
    }
}
