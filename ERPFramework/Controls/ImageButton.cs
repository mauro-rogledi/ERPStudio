using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    public enum Alignment { Left, Right, Top, Bottom, Center };

    [Designer(typeof(ButtonDesigner)),
    ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    public class ImageButton : Button
    {
        #region costruttori

        public ImageButton()
        {
            FlatStyle = FlatStyle.System;
        }

        #endregion

        #region membri privati

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private const int BCM_SETIMAGELIST = 0x1600 + 2;

        [StructLayout(LayoutKind.Sequential)]
        private struct BUTTON_IMAGELIST
        {
            public IntPtr himl;
            public RECT margin;
            public int uAlign;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref BUTTON_IMAGELIST lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct DLLVERSIONINFO
        {
            public int cbSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformID;
        }

        [DllImport("comctl32.dll", EntryPoint = "DllGetVersion")]
        private static extern int GetCommonControlDLLVersion(ref DLLVERSIONINFO dvi);

        private int ComCtlMajorVersion = -1;

        #endregion

        private Alignment alignment = Alignment.Left;

        [
        Category("Appearance"),
        Description("Allineala!"),
        DefaultValue(Alignment.Left)
        ]
        public Alignment AlignImage
        {
            get
            {
                return alignment;
            }

            set
            {
                switch (value)
                {
                    case Alignment.Left:
                        this.ImageAlign = ContentAlignment.MiddleLeft;
                        break;

                    case Alignment.Right:
                        this.ImageAlign = ContentAlignment.MiddleRight;
                        break;

                    case Alignment.Center:
                        this.ImageAlign = ContentAlignment.MiddleCenter;
                        break;

                    case Alignment.Top:
                        this.ImageAlign = ContentAlignment.TopCenter;
                        break;

                    case Alignment.Bottom:
                        this.ImageAlign = ContentAlignment.BottomCenter;
                        break;
                }

                alignment = value;
            }
        }

        //---------------------------------------------------------------------------
        protected override void OnCreateControl()
        {
            if (Image != null)
                SetImage((Bitmap)Image, alignment);
        }

        #region overload di setimage

        //---------------------------------------------------------------------------
        public void SetImage(Bitmap image)
        {
            SetImage(new Bitmap[] { image }, Alignment.Center, 0, 0, 0, 0);
        }

        //---------------------------------------------------------------------------
        public void SetImage(Bitmap image, Alignment align)
        {
            SetImage(new Bitmap[] { image }, align, 0, 0, 0, 0);
        }

        //---------------------------------------------------------------------------
        public void SetImage(Bitmap image, Alignment align, int leftMargin, int topMargin, int rightMargin,
            int bottomMargin)
        {
            SetImage(new Bitmap[] { image }, align, leftMargin, topMargin, rightMargin, bottomMargin);
        }

        //---------------------------------------------------------------------------
        public void SetImage(Bitmap normalImage, Bitmap hoverImage, Bitmap pressedImage,
            Bitmap disabledImage, Bitmap focusedImage)
        {
            SetImage(new Bitmap[] { normalImage, hoverImage, pressedImage,
									  disabledImage, focusedImage },
                Alignment.Center, 0, 0, 0, 0);
        }

        //---------------------------------------------------------------------------
        public void SetImage(Bitmap normalImage, Bitmap hoverImage, Bitmap pressedImage,
            Bitmap disabledImage, Bitmap focusedImage,
            Alignment align)
        {
            SetImage(new Bitmap[] { normalImage, hoverImage, pressedImage,
									  disabledImage, focusedImage },
                align, 0, 0, 0, 0);
        }

        //---------------------------------------------------------------------------
        public void SetImage(Bitmap normalImage, Bitmap hoverImage, Bitmap pressedImage,
            Bitmap disabledImage, Bitmap focusedImage,
            Alignment align, int leftMargin, int topMargin, int rightMargin,
            int bottomMargin)
        {
            SetImage(new Bitmap[] { normalImage, hoverImage, pressedImage,
									  disabledImage, focusedImage },
                align, leftMargin, topMargin, rightMargin, bottomMargin);
        }

        #endregion

        //---------------------------------------------------------------------------
        public void SetImage
            (
            Bitmap[] images,
            Alignment align,
            int leftMargin, int topMargin, int rightMargin, int bottomMargin
            )
        {
            if (ComCtlMajorVersion < 0)
            {
                DLLVERSIONINFO dllVersion = new DLLVERSIONINFO();
                dllVersion.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));
                GetCommonControlDLLVersion(ref dllVersion);
                ComCtlMajorVersion = dllVersion.dwMajorVersion;
            }

            if (ComCtlMajorVersion >= 6 && FlatStyle == FlatStyle.System)
            {
                RECT rect = new RECT();
                rect.left = leftMargin;
                rect.top = topMargin;
                rect.right = rightMargin;
                rect.bottom = bottomMargin;

                BUTTON_IMAGELIST buttonImageList = new BUTTON_IMAGELIST();
                buttonImageList.margin = rect;
                buttonImageList.uAlign = (int)align;

                ImageList = GenerateImageList(images);
                buttonImageList.himl = ImageList.Handle;

                SendMessage(this.Handle, BCM_SETIMAGELIST, 0, ref buttonImageList);
            }
            else
            {
                FlatStyle = FlatStyle.Standard;

                if (images.Length > 0)
                {
                    Image = images[0];
                }
            }
        }

        //---------------------------------------------------------------------------
        private ImageList GenerateImageList(Bitmap[] images)
        {
            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;

            if (images.Length > 0)
            {
                il.ImageSize = new Size(images[0].Width, images[0].Height);

                foreach (Bitmap image in images)
                {
                    il.Images.Add(image);
                    Bitmap bm = (Bitmap)il.Images[il.Images.Count - 1];

                    // copy pixel data from original Bitmap into ImageList
                    // to work around a bug in ImageList:
                    // adding an image to an ImageList destroys the alpha channel
                    for (int x = 0; x < bm.Width; x++)
                    {
                        for (int y = 0; y < bm.Height; y++)
                        {
                            bm.SetPixel(x, y, image.GetPixel(x, y));
                        }
                    }
                }
            }

            return il;
        }
    }

    public class ButtonDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        public ButtonDesigner()
        {
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("AllowDrop");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ForeColor");

            //Properties.Remove( "ImageAlign" );
            //Properties.Remove( "ImageIndex" );
            //Properties.Remove( "ImageList" );
            Properties.Remove("FlatStyle");
        }
    }
}