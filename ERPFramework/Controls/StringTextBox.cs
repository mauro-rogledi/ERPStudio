using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    /// <summary>
    /// Summary description for TextBox.
    /// </summary>
    [System.ComponentModel.DefaultEvent("TextChanged"),
    System.ComponentModel.DefaultProperty("Text"),
    ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public class StringTextBox : MetroFramework.Controls.MetroTextBox
    {
        private bool trim;

        [Category("Behavior")]
        [Description("The flags (on/off attributes) associated with the Behavior.")]
        public bool TrimResult
        {
            get { return trim; }
            set { trim = value; }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            this.Text = this.Text.Trim();
            base.OnValidating(e);
        }

        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }

        protected const int WM_KEYDOWN = 0x0100;

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Down || keyData == Keys.Up) return false;

            return base.IsInputKey(keyData);
        }

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

    /// <summary>
    /// Summary description for TextBox.
    /// </summary>
    [System.ComponentModel.DefaultEvent("TextChanged"),
    System.ComponentModel.DefaultProperty("Text"),
    ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    public class TrimMaskedTextBox : MaskedTextBox
    {
        private bool trim;

        [Category("Behavior")]
        [Description("The flags (on/off attributes) associated with the Behavior.")]
        public bool TrimResult
        {
            get { return trim; }
            set { trim = value; }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            this.Text = this.Text.Trim();
            base.OnValidating(e);
        }

        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }

        protected const int WM_KEYDOWN = 0x0100;

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Down || keyData == Keys.Up) return false;

            return base.IsInputKey(keyData);
        }

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
}