using System;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    public class ExTabPage : MetroFramework.Controls.MetroTabPage
    {
        public ExTabPage()
            : base()
        {
        }

        public ExTabPage(string text)
            : base()
        {
            Text = text;
        }

        protected const int WM_KEYDOWN = 0x0100;

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                Keys keyData = ((Keys)(int)msg.WParam) | ModifierKeys;
                Keys keyCode = ((Keys)(int)msg.WParam);

                if (keyCode == Keys.Return || keyCode == Keys.Down)
                    msg.WParam = (IntPtr)Keys.Tab;

                if (keyCode == Keys.Up)
                    msg.WParam = (IntPtr)((int)Keys.Tab | (int)Keys.Shift);
            }
            return base.PreProcessMessage(ref msg);
        }

        public void PageDown(ExGroupBox group)
        {
            Control ctrl = this;
            if (this.Controls[0].GetType() == typeof(ExFlowLayoutPanel))
                ctrl = this.Controls[0];

            for (int t = 0; t < ctrl.Controls.Count; t++)
            {
                if (ctrl.Controls[t].GetType() == typeof(ExGroupBox) &&
                    ctrl.Controls[t] == group)
                {
                    for (int i = t + 1; i < ctrl.Controls.Count; i++)
                    {
                        if (ctrl.Controls[i].GetType() == typeof(ExGroupBox))
                        {
                            SearchFirstCtrl((ExGroupBox)ctrl.Controls[i]);
                            return;
                        }
                    }
                }
            }
        }

        public void PageUp(ExGroupBox group)
        {
            Control ctrl = this;
            if (this.Controls[0].GetType() == typeof(ExFlowLayoutPanel))
                ctrl = this.Controls[0];

            for (int t = 0; t < ctrl.Controls.Count; t++)
            {
                if (ctrl.Controls[t].GetType() == typeof(ExGroupBox) &&
                    ctrl.Controls[t] == group)
                {
                    for (int i = t - 1; i >= 0; i--)
                    {
                        if (ctrl.Controls[i].GetType() == typeof(ExGroupBox))
                        {
                            SearchFirstCtrl((ExGroupBox)ctrl.Controls[i]);
                            return;
                        }
                    }
                }
            }
        }

        public void SearchFirstCtrl(ExGroupBox grp)
        {
            for (int t = 0; t < grp.Controls.Count; t++)
            {
                if (grp.Controls[t].GetType() != typeof(Label))
                {
                    grp.Controls[t].Focus();
                    return;
                }
            }
        }
    }

    public class ExTabControl : TabControl
    {
        public ExTabControl()
            : base()
        {
        }

        protected const int WM_KEYDOWN = 0x0100;

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                Keys keyData = ((Keys)(int)msg.WParam) | ModifierKeys;
                Keys keyCode = ((Keys)(int)msg.WParam);

                if (keyCode == Keys.Return || keyCode == Keys.Down)
                    msg.WParam = (IntPtr)Keys.Tab;

                if (keyCode == Keys.Up)
                    msg.WParam = (IntPtr)((int)Keys.Tab | (int)Keys.Shift);
            }
            return base.PreProcessMessage(ref msg);
        }
    }

    public class ExGroupBox : GroupBox
    {
        public ExGroupBox()
        {
        }

        public void PageUp()
        {
            if (this.Parent.GetType() == typeof(ExTabPage))
            {
                ((ExTabPage)this.Parent).PageUp(this);
                return;
            }

            if (this.Parent.GetType() == typeof(ExFlowLayoutPanel))
            {
                ((ExFlowLayoutPanel)this.Parent).PageUp(this);
                return;
            }
        }

        public void PageDown()
        {
            if (this.Parent.GetType() == typeof(ExTabPage))
            {
                ((ExTabPage)this.Parent).PageDown(this);
                return;
            }

            if (this.Parent.GetType() == typeof(ExFlowLayoutPanel))
            {
                ((ExFlowLayoutPanel)this.Parent).PageDown(this);
                return;
            }
        }
    }

    public class ExFlowLayoutPanel : FlowLayoutPanel
    {
        public ExFlowLayoutPanel()
        {
        }

        public void PageUp(ExGroupBox grp)
        {
            if (this.Parent.GetType() == typeof(ExTabPage))
            {
                ((ExTabPage)this.Parent).PageUp(grp);
                return;
            }
        }

        public void PageDown(ExGroupBox grp)
        {
            if (this.Parent.GetType() == typeof(ExTabPage))
            {
                ((ExTabPage)this.Parent).PageDown(grp);
                return;
            }
        }
    }
}