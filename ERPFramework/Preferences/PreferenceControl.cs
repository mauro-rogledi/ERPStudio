using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERPFramework.Preferences
{
    #region PreferencePanelButton

    [Browsable(false)]
    internal class PreferencePanelButton : MetroFramework.Controls.MetroTextBox.MetroTextButton
    {
        public delegate void ButtonClickEventHandler(PreferencePanelButton btn, PreferencePanel Panel);

        public event ButtonClickEventHandler ButtonClick;

        private bool selected;

        public PreferencePanel PrefPanel { get; private set; }

        #region Properties

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
            }
        }

        #endregion

        public PreferencePanelButton(PreferencePanel prefPanel)
        {
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            Text = prefPanel.ButtonText;
            Dock = DockStyle.Top;
            UseStyleColors = true;
            Image = prefPanel.ButtonImage;
            ImageAlign = ContentAlignment.MiddleRight;
            Size = new System.Drawing.Size(127, 24);
            MinimumSize = new System.Drawing.Size(10, 24);
            PrefPanel = prefPanel;
        }

        public new Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, this.PrefPanel);

            if (Selected)
                Deselect();
            base.OnClick(e);
        }

        private void Deselect()
        {
            foreach (Control ctrl in this.Parent.Controls)
            {
                if (ctrl.GetType() == typeof(PreferencePanelButton) && ctrl != this)
                    ((PreferencePanelButton)ctrl).Selected = false;
            }
        }
    }

    #endregion

    public class PreferenceContainer : SplitContainer
    {
        public PreferenceForm pf;

        public PreferenceContainer()
            : base()
        {
            if (Panel1 != null)
                Panel1.BackColor = Color.White;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent.GetType() == typeof(PreferenceForm))
                pf = (PreferenceForm)Parent;
            base.OnParentChanged(e);
        }

        public void AddPanel(PreferencePanel panel)
        {
            if (!ExistButton(panel))
                AddButton(panel);
        }

        private void AddButton(PreferencePanel panel)
        {
            var btn = new PreferencePanelButton(panel);
            Panel1.Controls.Add(btn);
            btn.ButtonClick += new PreferencePanelButton.ButtonClickEventHandler(btn_ButtonClick);
        }

        private void btn_ButtonClick(PreferencePanelButton btn, PreferencePanel Panel)
        {
            if (CanAddPanel(Panel))
            {
                if (Panel2.Controls.Count > 0)
                    Panel2.Controls.RemoveAt(0);
                Panel2.Controls.Add(Panel);
                btn.Selected = true;
            }
            Panel.Dock = DockStyle.Fill;
            Panel.DocumentClose += Panel_DocumentClose;
        }

        private void Panel_DocumentClose(object sender, PreferencePanel prefPanel)
        {
            Panel2.Controls.Remove(prefPanel);
        }

        private bool CanAddPanel(UserControl Panel)
        {
            if (Panel2.Controls.Count == 0)
                return true;

            if (Panel2.Controls.Contains(Panel))
                return false;

            var pnl = (PreferencePanel)Panel2.Controls[0];
            if (pnl.CanRemove)
                return true;

            return false;
        }

        private bool ExistButton(PreferencePanel panel)
        {
            foreach (Control ctrl in Panel1.Controls)
                if (ctrl.GetType().Equals(typeof(PreferencePanelButton)) &&
                    ctrl.Text == panel.ButtonText)
                    return true;
            return false;
        }
    }
}