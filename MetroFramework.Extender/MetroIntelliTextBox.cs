using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using MetroFramework.Forms;

namespace MetroFramework.Extender
{
    public class MetroIntelliTextBox : Control, IMetroControl
    {
        #region Interface

       // [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

       // [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

       // [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private MetroColorStyle metroStyle = MetroColorStyle.Default;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroColorStyle.Default)]
        public MetroColorStyle Style
        {
            get
            {
                if (DesignMode || metroStyle != MetroColorStyle.Default)
                {
                    return metroStyle;
                }

                if (StyleManager != null && metroStyle == MetroColorStyle.Default)
                {
                    return StyleManager.Style;
                }
                if (StyleManager == null && metroStyle == MetroColorStyle.Default)
                {
                    return ExtenderDefaults.Style;
                }

                return metroStyle;
            }
            set { metroStyle = value; }
        }

        private MetroThemeStyle metroTheme = MetroThemeStyle.Default;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroThemeStyle.Default)]
        public MetroThemeStyle Theme
        {
            get
            {
                if (DesignMode || metroTheme != MetroThemeStyle.Default)
                {
                    return metroTheme;
                }

                if (StyleManager != null && metroTheme == MetroThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }
                if (StyleManager == null && metroTheme == MetroThemeStyle.Default)
                {
                    return ExtenderDefaults.Theme;
                }

                return metroTheme;
            }
            set { metroTheme = value; }
        }

        private MetroStyleManager metroStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        #endregion

        #region Fields

        private RichTextBox baseRichTextBox = null;
        private WindowsListBox listBox = null;
        private bool listboxOpen = false;

        private Control ancestor = null;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("The Listbox will be attached to this control")]
        public Control Ancestor
        {
            get { return ancestor; }
            set { ancestor = value;  }
        }

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; UpdateBaseTextBox(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; UpdateBaseTextBox(); }
        }
        #endregion

        #region Routing Fields

        public override ContextMenu ContextMenu
        {
            get { return baseRichTextBox.ContextMenu; }
            set
            {
                ContextMenu = value;
                baseRichTextBox.ContextMenu = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get { return baseRichTextBox.ContextMenuStrip; }
            set
            {
                ContextMenuStrip = value;
                baseRichTextBox.ContextMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get { return baseRichTextBox.Multiline; }
            set { baseRichTextBox.Multiline = value; }
        }

        public override string Text
        {
            get { return baseRichTextBox.Text; }
            set { baseRichTextBox.Text = value; }
        }

        public string[] Lines
        {
            get { return baseRichTextBox.Lines; }
            set { baseRichTextBox.Lines = value; }
        }

        [Browsable(false)]
        public string SelectedText
        {
            get { return baseRichTextBox.SelectedText; }
            set { baseRichTextBox.Text = value; }
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return baseRichTextBox.ReadOnly; }
            set
            {
                baseRichTextBox.ReadOnly = value;
            }
        }

        public int SelectionStart
        {
            get { return baseRichTextBox.SelectionStart; }
            set { baseRichTextBox.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return baseRichTextBox.SelectionLength; }
            set { baseRichTextBox.SelectionLength = value; }
        }

        [DefaultValue(true)]
        public new bool TabStop
        {
            get { return baseRichTextBox.TabStop; }
            set { baseRichTextBox.TabStop = value; }
        }

        public int MaxLength
        {
            get { return baseRichTextBox.MaxLength; }
            set { baseRichTextBox.MaxLength = value; }
        }

        public RichTextBoxScrollBars ScrollBars
        {
            get { return baseRichTextBox.ScrollBars; }
            set { baseRichTextBox.ScrollBars = value; }
        }

        public bool ShortcutsEnabled
        {
            get { return baseRichTextBox.ShortcutsEnabled; }
            set { baseRichTextBox.ShortcutsEnabled = value; }
        }
        #endregion

        #region Constructor
        public MetroIntelliTextBox()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            base.TabStop = false;

            CreateBaseRichTextBox();
            UpdateBaseTextBox();
            AddEventHandler();
        }
        #endregion

        public virtual string[] FillListBox(string word) { return null; }

        #region Private Methods

        private void CreateBaseRichTextBox()
        {
            if (baseRichTextBox != null) return;

            baseRichTextBox = new RichTextBox();

            baseRichTextBox.BorderStyle = BorderStyle.None;
            baseRichTextBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
            baseRichTextBox.Location = new Point(3, 3);
            baseRichTextBox.Size = new Size(Width - 6, Height - 6);
            baseRichTextBox.AcceptsTab = true;

            Size = new Size(baseRichTextBox.Width + 6, baseRichTextBox.Height + 6);

            baseRichTextBox.TabStop = true;

            Controls.Add(baseRichTextBox);
        }

        private void InsertText()
        {
            if (listBox.SelectedItems.Count != 1)
                return;
            CloseListBox();

            int wordEndPosition = baseRichTextBox.SelectionStart;

            while (wordEndPosition < baseRichTextBox.Text.Length && baseRichTextBox.Text[wordEndPosition] != ' ')
            {
                wordEndPosition++;
            }

            int currentPosition = wordEndPosition;

            while (currentPosition > 0 && baseRichTextBox.Text[currentPosition - 1] != ' ')
            {
                currentPosition--;
            }

            string prefix = this.baseRichTextBox.Text.Substring(0, currentPosition);
            string fill = this.listBox.SelectedItem.ToString();
            string suffix = this.baseRichTextBox.Text.Substring(wordEndPosition, this.baseRichTextBox.Text.Length - wordEndPosition);

            this.baseRichTextBox.Text = prefix + fill + suffix;
            this.baseRichTextBox.SelectionStart = prefix.Length + fill.Length;
        }

        private void AddEventHandler()
        {
            baseRichTextBox.AcceptsTabChanged += BaseTextBoxAcceptsTabChanged;

            baseRichTextBox.CausesValidationChanged += BaseTextBoxCausesValidationChanged;
            baseRichTextBox.ChangeUICues += BaseTextBoxChangeUiCues;
            baseRichTextBox.Click += BaseTextBoxClick;

            baseRichTextBox.ClientSizeChanged += BaseTextBoxClientSizeChanged;
            baseRichTextBox.ContextMenuChanged += BaseTextBoxContextMenuChanged;
            baseRichTextBox.ContextMenuStripChanged += BaseTextBoxContextMenuStripChanged;
            baseRichTextBox.CursorChanged += BaseTextBoxCursorChanged;

            baseRichTextBox.KeyDown += BaseTextBoxKeyDown;
            baseRichTextBox.KeyPress += BaseTextBoxKeyPress;
            baseRichTextBox.KeyUp += BaseTextBoxKeyUp;

            baseRichTextBox.SizeChanged += BaseTextBoxSizeChanged;

            baseRichTextBox.TextChanged += BaseTextBoxTextChanged;
            baseRichTextBox.GotFocus += baseTextBox_GotFocus;
            baseRichTextBox.LostFocus += baseTextBox_LostFocus;
            baseRichTextBox.MouseDown += BaseTextBox_MouseDown;
            baseRichTextBox.MouseUp += BaseTextBox_MouseUp;
        }

        private void ListBox_DoubleClick(object sender, EventArgs e)
        {
            InsertText();
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return )
                InsertText();
            if (e.KeyCode == Keys.Escape)
            {
                Parent.Controls.Remove(listBox);
                listBox.Dispose();
            }
        }

        private void BaseTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void BaseTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            baseRichTextBox.Focus();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            baseRichTextBox.Focus();
        }

        void _button_MouseLeave(object sender, EventArgs e)
        {
            UseStyleColors = baseRichTextBox.Focused;
            Invalidate();
        }

        void _button_MouseEnter(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
        }

        void _button_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }
        void baseTextBox_LostFocus(object sender, EventArgs e)
        {
            UseStyleColors = false;
            Invalidate();
            this.InvokeLostFocus(this, e);
        }

        void baseTextBox_GotFocus(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
            this.InvokeGotFocus(this, e);
        }
        private void UpdateBaseTextBox()
        {
            if (baseRichTextBox == null) return;

            baseRichTextBox.Location = new Point(3, 3);
            baseRichTextBox.Size = new Size(Width - 6, Height - 6);

            baseRichTextBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
        }

        private void ShowIntellisense()
        {
            string word = GetCurrentWord();
            if (word.Length > 1)
                ShowListBox(word);

        }
        private string GetCurrentWord()
        {
            int wordEndPosition = baseRichTextBox.SelectionStart;
            int currentPosition = wordEndPosition;

            while (currentPosition > 0 && baseRichTextBox.Text[currentPosition - 1] != ' ')
            {
                currentPosition--;
            }

            return baseRichTextBox.Text.Substring(currentPosition, wordEndPosition - currentPosition);
        }

        #endregion

        #region ListBox Management

        private void CreateListBox()
        {
            if (listboxOpen)
                return;

            listBox = new WindowsListBox();
            listBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
            listBox.Size = new System.Drawing.Size(208, 154);
            listBox.BringToFront();
            listBox.TopMost = true;
            listBox.DoubleClick += ListBox_DoubleClick;
            listBox.Click += ListBox_Click;
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            baseRichTextBox.Focus();
        }

        private void ManageListBoxKey(KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:
                    CloseListBox();
                    break;
                case Keys.Up:
                    if (listBox.SelectedIndex > 0)
                        listBox.SelectedIndex--;
                    e.Handled = true;
                    break;
                case Keys.Down:
                    if (listBox.SelectedIndex < listBox.Items.Count - 1)
                        listBox.SelectedIndex++;
                    e.Handled = true;
                    break;
                case Keys.Home:
                    listBox.SelectedIndex = 0;
                    e.Handled = true;
                    break;
                case Keys.End:
                    listBox.SelectedIndex = listBox.Items.Count - 1;
                    e.Handled = true;
                    break;
                case Keys.Left:
                    CloseListBox();
                    break;
                case Keys.Space:
                    InsertText();
                    break;
                case Keys.Enter:
                    InsertText();
                    e.Handled = true;
                    return;
            }
        }

        private void ShowListBox(string word)
        {
            CreateListBox();

            if (PopulateListBox(word))
                ShowListBox();
            else
                CloseListBox();
        }

        private void ShowListBox()
        {
            if (listboxOpen)
                return;

            Point offset = baseRichTextBox.PointToScreen(Point.Empty);
            Point point = baseRichTextBox.GetPositionFromCharIndex(baseRichTextBox.SelectionStart);
            point.Y += (int)Math.Ceiling(baseRichTextBox.Font.GetHeight()) + 2 + offset.Y;
            point.X += offset.X; // for Courier, may need a better method

            listBox.Location = point;
            listboxOpen = true;
            listBox.Show();
            baseRichTextBox.Focus();
            baseRichTextBox.Focus();
        }
        private bool PopulateListBox(string word)
        {
            listBox.Items.Clear();
            listBox.Items.AddRange(Dictionary.FindWords(word));

            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;

            return listBox.Items.Count > 0;
        }

        private void CloseListBox()
        {
            listBox.Close();
            listboxOpen = false;
        }
        #endregion

        #region Routing Methods

        public event EventHandler AcceptsTabChanged;
        private void BaseTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            if (AcceptsTabChanged != null)
                AcceptsTabChanged(this, e);
        }

        private void BaseTextBoxSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void BaseTextBoxCursorChanged(object sender, EventArgs e)
        {
            //base.OnCursorChanged(e);
        }

        private void BaseTextBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void BaseTextBoxContextMenuChanged(object sender, EventArgs e)
        {
            base.OnContextMenuChanged(e);
        }

        private void BaseTextBoxClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void BaseTextBoxClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void BaseTextBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void BaseTextBoxCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void BaseTextBoxKeyUp(object sender, KeyEventArgs e)
        {
           base.OnKeyUp(e);
            if (e.KeyCode == Keys.Tab)
            {
                ShowIntellisense();
                e.Handled = true;
            }
            bool buttonMover = e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Home || e.KeyCode == Keys.End;
            if (listboxOpen && !buttonMover)
            {
                string word = GetCurrentWord();
                if (word.Length > 1)
                    ShowListBox(word);
            }
        }

        private void BaseTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
                e.Handled = true;
            base.OnKeyPress(e);
        }

        private void BaseTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (listboxOpen)
                ManageListBoxKey(e);
            base.OnKeyDown(e);
        }

        bool _cleared = false;
        bool _withtext = false;

        private void BaseTextBoxTextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);
            OnTextChanged(e);

            if (baseRichTextBox.Text != "" && !_withtext)
            {
                _withtext = true;
                _cleared = false;
                Invalidate();
            }

            if (baseRichTextBox.Text == "" && !_cleared)
            {
                _withtext = false;
                _cleared = true;
                Invalidate();
            }
        }

        public void Select(int start, int length)
        {
            baseRichTextBox.Select(start, length);
        }

        public void SelectAll()
        {
            baseRichTextBox.SelectAll();
        }

        public void Clear()
        {
            baseRichTextBox.Clear();
        }

        void MetroTextBox_GotFocus(object sender, EventArgs e)
        {
            baseRichTextBox.Focus();
        }

        public void AppendText(string text)
        {
            baseRichTextBox.AppendText(text);
        }

        #endregion

        #region Paint Methods
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;
                baseRichTextBox.BackColor = backColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                    baseRichTextBox.BackColor = backColor;
                }

                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new MetroPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            if (useCustomForeColor)
            {
                baseRichTextBox.ForeColor = ForeColor;
            }
            else
            {
                baseRichTextBox.ForeColor = MetroPaint.ForeColor.Button.Normal(Theme);
            }

            Color borderColor = MetroPaint.BorderColor.ComboBox.Normal(Theme);

            if (useStyleColors)
                borderColor = MetroPaint.GetStyleColor(Style);

            using (Pen p = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 2, Height - 1));
            }
        }
        #endregion

        #region Overridden Methods

        public override void Refresh()
        {
            base.Refresh();
            UpdateBaseTextBox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateBaseTextBox();
        }

        #endregion
    }


    [Browsable(false)]
    internal class WindowsListBox : MetroForm
    {
        private MetroListBox listBox = null;
        public WindowsListBox()
        {
            InitializeComponent();
        }

        public ListBox.SelectedObjectCollection SelectedItems
        {
            get { return listBox.SelectedItems; }
        }

        public int SelectedIndex
        {
            get { return listBox.SelectedIndex; }
            set { listBox.SelectedIndex = value; }
        }

        public ListBox.ObjectCollection Items
        {
            get { return listBox.Items; }
        }

        public object SelectedItem
        {
            get { return listBox.SelectedItem; }
            set { listBox.SelectedItem = value; }
        }

        private void InitializeComponent()
        {
            listBox = new MetroListBox();
            listBox.Location = new System.Drawing.Point(0, 0);
            listBox.Name = "listBox1";
            listBox.Size = new System.Drawing.Size(121, 82);
            listBox.TabIndex = 0;
            listBox.DoubleClick += ListBox_DoubleClick;
            listBox.Click += ListBox_Click;
            listBox.UseStyleColors = true;
            listBox.FontWeight = MetroTextBoxWeight.Regular;
            listBox.FontSize = MetroTextBoxSize.Medium;

            ApplyImageInvert = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackLocation = MetroFramework.Forms.BackLocation.TopRight;
            ClientSize = new System.Drawing.Size(123, 88);
            ControlBox = false;
            Controls.Add(listBox);
            DisplayHeader = false;
            IsMdiContainer = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Movable = false;
            Name = "Form2";
            Resizable = false;
            ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Style = MetroFramework.MetroColorStyle.Default;
            Text = "Form2";
            TransparencyKey = System.Drawing.Color.Empty;
            ResumeLayout(false);
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void ListBox_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            listBox.Size = Size;
            listBox.Theme = Theme;
        }
    }
}
