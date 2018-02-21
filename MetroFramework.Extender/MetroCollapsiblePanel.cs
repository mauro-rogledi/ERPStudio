using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Animation;
using MetroFramework.Drawing;

namespace MetroFramework.Extender
{
    public enum CollapseDirectionStyle
    {
        None,
        Height,
        Width
    }
    public partial class MetroCollapsiblePanel : MetroFramework.Controls.MetroPanel, IPropagate
    {
        #region Fields

        private MetroFramework.Controls.MetroLabel metroLabel = null;
        private MetroFramework.Controls.MetroLink metroLink = null;
        Point linePositionStart;
        Point linePositionEnd;
        Pen linePen;
        bool labelAdded;

        bool horizzontalSeparator = true;
        [Category("Metro Appearance")]
        [Description("Show Horizzontal separator")]
        [DisplayName("Horizzontal Separator")]
        [DefaultValue(true)]
        public bool HorizzontalSeparator
        {
            get { return horizzontalSeparator; }
            set { horizzontalSeparator = value; UpdateSeparator(); }
        }

        private bool showLabel = false;
        [Category("Metro Appearance")]
        [Description("Show label text")]
        [DisplayName("Show Label")]
        [DefaultValue(false)]
        public bool ShowLabel
        {
            get { return showLabel; }
            set { showLabel = value; UpdateLabelTextBox(); UpdateSeparator(); }
        }
        private string label = string.Empty;
        [Category("Metro Appearance")]
        [Description("Label to display before separator")]
        [Localizable(true)]
        public string Label
        {
            get { return label; }
            set { label = value; UpdateLabelTextBox(); }
        }

        private CollapseDirectionStyle collapseDirection = CollapseDirectionStyle.None;
        [Category("Metro Appearance")]
        [Description("Set direction to collapse")]
        [DefaultValue(CollapseDirectionStyle.None)]
        public CollapseDirectionStyle CollapseDirection
        {
            get { return collapseDirection; }
            set{collapseDirection = value; UpdateButton(); }
        }

        private MetroLabelSize metroLabelSize = MetroLabelSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category("Metro Appearance")]
        public MetroLabelSize FontSize
        {
            get { return metroLabelSize; }
            set { metroLabelSize = value; UpdateLabelTextBox(); }
        }

        private MetroLabelWeight metroLabelWeight = MetroLabelWeight.Regular;
        [DefaultValue(MetroLabelWeight.Regular)]
        [Category("Metro Appearance")]
        public MetroLabelWeight FontWeight
        {
            get { return metroLabelWeight; }
            set { metroLabelWeight = value; UpdateLabelTextBox(); }
        }
        #endregion

        public MetroCollapsiblePanel()
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            metroLink = new MetroFramework.Controls.MetroLink();
            metroLink.Image = Properties.Resources.TriangleDown16;
            metroLink.NoFocusImage = Properties.Resources.TriangleDown16g;
            metroLink.UseStyleColors = true;
            metroLink.Size = new Size(16, 16);
            metroLink.Location = new Point(Width-16,0);
            metroLink.Click += MetroLink_Click;
            metroLink.Visible = collapseDirection != CollapseDirectionStyle.None;
            metroLink.TabStop = false;
            Controls.Add(metroLink);
        }

        private void MetroLink_Click(object sender, EventArgs e)
        {
            ExpandAnimation animation = new ExpandAnimation();
            animation.AnimationCompleted += Animation_AnimationCompleted;
            switch(collapseDirection)
            {
                case CollapseDirectionStyle.Width:
                    if (MaximumSize.Width > Width)
                        animation.Start(this, MaximumSize, TransitionType.Linear, 20);
                    else
                        animation.Start(this, MinimumSize, TransitionType.Linear, 20);
                    break;
                case CollapseDirectionStyle.Height:
                    if (Height > MinimumSize.Height)
                        animation.Start(this, MinimumSize, TransitionType.Linear, 20);
                    else
                        animation.Start(this, MaximumSize, TransitionType.Linear, 20);
                    break;
            }
        }

        private void Animation_AnimationCompleted(object sender, EventArgs e)
        {
            UpdateButton();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (horizzontalSeparator)
                PaintSeparator(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SuspendLayout();
            base.OnSizeChanged(e);
            UpdateSeparator();
            UpdateButtonPosition();
            ResumeLayout();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            var c = GetChildAtPoint(e.Location);
            PropagateMouseDoubleClick(c);
        }

        public void PropagateMouseDoubleClick(Control c)
        {
            if (Parent is IPropagate)
                (Parent as IPropagate).PropagateMouseDoubleClick(c);
        }

        private void PaintSeparator(PaintEventArgs e)
        {
            if (MetroPaint.GetStylePen(Style)!=null)
                e.Graphics.DrawLine(MetroPaint.GetStylePen(Style), linePositionStart, linePositionEnd);
        }

        private void UpdateLabelTextBox()
        {
            if (showLabel)
            {
                if (metroLabel == null)
                {
                    metroLabel = new MetroFramework.Controls.MetroLabel();
                    metroLabel.AutoSize = true;
                    metroLabel.Location = new Point(0, 0);
                }
                metroLabel.FontSize = FontSize;
                metroLabel.FontWeight = FontWeight;
                metroLabel.Text = label;
                metroLabel.UseStyleColors = true;

                if (!labelAdded)
                {
                    Controls.Add(metroLabel);
                    labelAdded = true;
                }
            }
            else
            {
                if (labelAdded)
                {
                    Controls.Remove(metroLabel);
                    labelAdded = false;
                }
            }
            Refresh();
        }

        private void UpdateSeparator()
        {
            linePen = MetroFramework.Drawing.MetroPaint.GetStylePen(Style);
            linePen.Width = 3;
            if (showLabel)
            {
                int height = MetroFonts.Label(metroLabelSize, metroLabelWeight).Height;
                linePositionStart = new Point(0, height);
                linePositionEnd = new Point(Width, height);
            }
            else
            {
                linePositionStart = new Point(0, 0);
                linePositionEnd = new Point(Width, 0);
            }
            Refresh();
        }

        private void UpdateButton()
        {
            metroLink.Visible = collapseDirection != CollapseDirectionStyle.None;
            switch (collapseDirection)
            {
                case CollapseDirectionStyle.Width:
                    if (Size == MaximumSize)
                    {
                        metroLink.Image = Properties.Resources.TriangleLeft16;
                        metroLink.NoFocusImage = Properties.Resources.TriangleLeft16g;
                    }
                    else
                    {
                        metroLink.Image = Properties.Resources.TriangleRight16;
                        metroLink.NoFocusImage = Properties.Resources.TriangleRight16g;
                    }
                    break;
                case CollapseDirectionStyle.Height:
                    if (Size == MaximumSize)
                    {
                        metroLink.Image = Properties.Resources.TriangleUp16;
                        metroLink.NoFocusImage = Properties.Resources.TriangleUp16g;
                    }
                    else
                    {
                        metroLink.Image = Properties.Resources.TriangleDown16;
                        metroLink.NoFocusImage = Properties.Resources.TriangleDown16g;
                    }
                    break;
            }
            Refresh();
        }

        private void UpdateButtonPosition()
        {
            metroLink.Location = new Point(Width - 16, 0);
        }
    }
}
