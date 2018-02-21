using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Animation;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{
    public class MetroAddOnPanel : MetroFramework.Controls.MetroPanel
    {
        public enum MetroAddonPanelStatus { Open, Close}

        #region Fields

        private MetroLink btnPinned;
        private MetroFlowLayoutPanel panelbutton;
        private int width = 203;

        private bool pinned = false;
        [Category(MetroFramework.Extender.ExtenderDefaults.PropertyCategory.Appearance)]
        [Description("Panel is pinned")]
        [DefaultValue(false)]
        public bool Pinned { get { return pinned; } set { pinned = value; ChangePinned(); } }

        [Category(MetroFramework.Extender.ExtenderDefaults.PropertyCategory.Appearance)]
        [Description("Panel Status")]
        [DefaultValue(MetroAddonPanelStatus.Close)]
        public MetroAddonPanelStatus Status { get; set; } = MetroAddonPanelStatus.Close;

        [Category(MetroFramework.Extender.ExtenderDefaults.PropertyCategory.Event)]
        [Description("Occurs when a button is clicked")]
        public event EventHandler ButtonClick;

        public MetroProvaTile[] Buttons
        {
            get
            {
                List<MetroProvaTile> buttonList = new List<MetroProvaTile>();
                buttonList.AddRange(panelbutton.Controls.OfType<MetroProvaTile>());
                return buttonList.ToArray();
            }
        }

        #endregion

        public MetroAddOnPanel()
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            btnPinned = new MetroLink();
            btnPinned.Location = new Point(5, 5);
            btnPinned.Size = new Size(32,32);
            btnPinned.Image = Properties.Resources.Pin24;
            btnPinned.NoFocusImage = Properties.Resources.Pin24g;
            btnPinned.ImageSize = 32;
            btnPinned.UseSelectable = true;
            btnPinned.TextImageRelation = TextImageRelation.ImageAboveText;
            btnPinned.Click += BtnPinned_Click;
            btnPinned.UseStyleColors = true;
            btnPinned.BackColor = BackColor;
            btnPinned.UseCustomBackColor = true;
            btnPinned.Name = "btnPinned";
            Controls.Add(btnPinned);

            panelbutton = new MetroFlowLayoutPanel();
            panelbutton.Location = new Point(0, 40);
            panelbutton.Size = new Size(0,Height - 40);
            panelbutton.UseStyleColors = true;
            panelbutton.BackColor = BackColor;
            panelbutton.UseCustomBackColor = true;
            panelbutton.Name = "panelbutton";
            Controls.Add(panelbutton);
        }

        private void BtnPinned_Click(object sender, EventArgs e)
        {
            pinned = !pinned;
            ChangePinned();
        }

        [Browsable(false)]
        public void ChangeStatus(int width)
        {
            ExpandAnimation animation = new ExpandAnimation();
            animation.AnimationCompleted += Animation_AnimationCompleted;
            switch(Status)
            {
                case MetroAddonPanelStatus.Open:
                    if (Pinned)
                        break;
                    animation.Start(this, new Size(0, Height), TransitionType.Linear, 20);
                    break;
                case MetroAddonPanelStatus.Close:
                    BringToFront();
                    Visible = true;
                    animation.Start(this, new Size(width, Height), TransitionType.Linear, 20);
                    break;
            }
        }

        public void AddButton(string name, string text, Image img, MetroAddonPanelButton.MetroAddonPanelButtonSize buttonSize)
        {
            MetroAddonPanelButton mab = new MetroAddonPanelButton();
            mab.Name = name;
            mab.Text = text;
            mab.TileImage = img;
            mab.ImageAlign = ContentAlignment.TopCenter;
            mab.TextAlign = ContentAlignment.BottomCenter;
            mab.UseTileImage = true;
            mab.UseStyleColors = true;

            mab.ButtonSize = buttonSize;
            mab.Click += Mab_Click;
            panelbutton.Controls.Add(mab);
        }

        private void Mab_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(sender, e);
        }

        private void Animation_AnimationCompleted(object sender, EventArgs e)
        {
            switch (Status)
            {
                case MetroAddonPanelStatus.Open:
                    Status = MetroAddonPanelStatus.Close;
                    Visible = false;
                    SendToBack();
                    break;
                case MetroAddonPanelStatus.Close:
                    Status = MetroAddonPanelStatus.Open;
                    Focus();
                    break;
            }
        }

        private void ChangePinned()
        {
            BorderStyle = pinned ? BorderStyle.FixedSingle : BorderStyle.None;
            btnPinned.Image = pinned ? Properties.Resources.Unpin24 : Properties.Resources.Pin24;
            btnPinned.NoFocusImage = pinned ? Properties.Resources.Unpin24g : Properties.Resources.Pin24g;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (StyleManager != null)
            {
                if (StyleManager.Theme == MetroThemeStyle.Dark)
                    BackColor = Color.FromArgb(11, 11, 11);
                else
                    BackColor = Color.FromArgb(244, 244, 244);
            }

            if (panelbutton.Size.Height != Size.Height)
                panelbutton.Size = new Size(width, Size.Height);

            panelbutton.BackColor = BackColor;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
        }

    }

    public class MetroAddonPanelButton : MetroProvaTile
    {
        public enum MetroAddonPanelButtonSize { Normal, Double }

        #region Fields
        private MetroAddonPanelButtonSize _buttonSize = MetroAddonPanelButtonSize.Normal;
        [DefaultValue(MetroAddonPanelButtonSize.Normal)]
        public MetroAddonPanelButtonSize ButtonSize
        {
            get
            {
                return _buttonSize;
            }
            set
            {
                _buttonSize = value;
                switch (_buttonSize)
                {
                    case MetroAddonPanelButtonSize.Normal:
                        Size = new Size(95, 97);
                        TileTextFontSize = MetroTileTextSize.Small;
                        break;

                    case MetroAddonPanelButtonSize.Double:
                        Size = new Size(194, 97);
                        TileTextFontSize = MetroTileTextSize.Medium;
                        break;

                }
            }
        }
        #endregion

        public MetroAddonPanelButton()
        {
            TextAlign = ContentAlignment.BottomCenter;
            TileImageAlign = ContentAlignment.MiddleCenter;
            UseStyleColors = true;
            UseTileImage = true;
            TileTextFontSize = MetroTileTextSize.Medium;
            TileTextFontWeight = MetroTileTextWeight.Light;
        }
    }
}
