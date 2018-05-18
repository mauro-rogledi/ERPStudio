using System;
using System.ComponentModel;
using System.Drawing;

namespace MetroFramework.Extender
{
    public class MetroLinkChecked : MetroFramework.Controls.MetroLink
    {
        private Image originalImage = null;
        private Image originalNoFocusImage = null;

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public Image NoFocusCheckedImage { get; set; }

        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public Image CheckedImage { get; set; }

        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        public bool CheckOnClick { get; set; } = true;

        private bool _checked = false;
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _checked; }
            set { if (value != _checked) { _checked = value; ChangeImages(); } }
        }
        [Category(ExtenderDefaults.PropertyCategory.Event)]
        public EventHandler CheckedChanged = null;

        protected override void OnClick(EventArgs e)
        {
            if (CheckOnClick)
                Checked = !Checked;

            base.OnClick(e);
        }

        private void ChangeImages()
        {
            if (originalImage == null)
            {
                originalImage = Image;
                originalNoFocusImage = NoFocusImage;
            }

            Image = _checked ? CheckedImage : originalImage;
            NoFocusImage = _checked ? NoFocusCheckedImage : originalNoFocusImage;
            Invalidate();

            if (CheckedChanged != null)
                CheckedChanged(this, EventArgs.Empty);
        }
    }
}
