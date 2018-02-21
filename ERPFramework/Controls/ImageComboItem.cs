using System.Drawing;

namespace ERPFramework.Controls
{
    public class ImageComboItem : object
    {
        // forecolor: transparent = inherit
        private Color forecolor = Color.FromKnownColor(KnownColor.Transparent);

        private bool mark = false;
        private int imageindex = -1;
        private object tag = null;
        private string text = null;

        // constructors
        public ImageComboItem()
        {
        }

        public ImageComboItem(string Text)
        {
            text = Text;
        }

        public ImageComboItem(string Text, int ImageIndex)
        {
            text = Text;
            imageindex = ImageIndex;
        }

        public ImageComboItem(string Text, int ImageIndex, bool Mark)
        {
            text = Text;
            imageindex = ImageIndex;
            mark = Mark;
        }

        public ImageComboItem(string Text, int ImageIndex, bool Mark, Color ForeColor)
        {
            text = Text;
            imageindex = ImageIndex;
            mark = Mark;
            forecolor = ForeColor;
        }

        public ImageComboItem(string Text, int ImageIndex, bool Mark, Color ForeColor, object Tag)
        {
            text = Text;
            imageindex = ImageIndex;
            mark = Mark;
            forecolor = ForeColor;
            tag = Tag;
        }

        // forecolor
        public Color ForeColor
        {
            get
            {
                return forecolor;
            }
            set
            {
                forecolor = value;
            }
        }

        // image index
        public int ImageIndex
        {
            get
            {
                return imageindex;
            }
            set
            {
                imageindex = value;
            }
        }

        // mark (bold)
        public bool Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }

        // tag
        public object Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        // item text
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        // ToString() should return item text
        public override string ToString()
        {
            return text;
        }
    }
}