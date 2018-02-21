using ERPFramework.Libraries;
using System;
using System.Collections;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    public partial class Multiselect : UserControl
    {
        private ListBoxManager<string> rightManager;
        private ListBoxManager<string> leftManager;

        private enum clickSource { left, right } ;

        private clickSource source;

        public Multiselect()
        {
            InitializeComponent();

            rightManager = new ListBoxManager<string>();
            leftManager = new ListBoxManager<string>();
        }

        #region Public Methods

        public void AddLeftValue(string key, string text)
        {
            leftManager.AddValue(key, text);
            if (!leftManager.IsAttached)
                leftManager.AttachTo(lsbLeft);
            else
                leftManager.Refresh();
        }

        public void AddRightValue(string key, string text)
        {
            rightManager.AddValue(key, text);
            if (!rightManager.IsAttached)
                rightManager.AttachTo(lsbRight);
            else
                rightManager.Refresh();
        }

        public void AllLeft()
        {
            btnNone_Click(this, new EventArgs());
        }

        public void AllRight()
        {
            btnAll_Click(this, new EventArgs());
        }

        public void MoveLeft(string key)
        {
            GenericList<string> el = rightManager.FindKey(key);
            if (el != null)
            {
                AddLeftValue(el.Archive, el.Display);
                rightManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        public void MoveRight(string key)
        {
            GenericList<string> el = leftManager.FindKey(key);
            if (el != null)
            {
                AddRightValue(el.Archive, el.Display);
                leftManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        public int LeftCount { get { return leftManager.Count; } }

        public int RightCount { get { return rightManager.Count; } }

        public string GetKeyLeftAt(int idx)
        {
            return (string)leftManager.GetKeyAt(idx);
        }

        public string GetKeyLeftValue(int idx)
        {
            return leftManager.GetValueAt(idx);
        }

        public string GetKeyRighttAt(int idx)
        {
            return (string)rightManager.GetKeyAt(idx);
        }

        public string GetKeyRightValue(int idx)
        {
            return rightManager.GetValueAt(idx);
        }

        public string LeftText
        {
            get { return lblLeft.Text; }
            set { lblLeft.Text = value; }
        }

        public string RightText
        {
            get { return lblRight.Text; }
            set { lblRight.Text = value; }
        }

        #endregion

        #region Paint

        protected override void OnResize(EventArgs e)
        {
            AdjustControl();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            AdjustControl();
            base.OnPaint(e);
        }

        private void AdjustControl()
        {
            int interspace = 14 + btnAll.Width;
            int lsbwidth = (Width - interspace) / 2;
            int buttonHeight = btnAll.Height * 4 + 21;
            int buttonLeft = (Width - btnAll.Width) / 2;
            int buttonTop = (lsbLeft.Height - buttonHeight) / 2;

            lblLeft.Width = lblRight.Width = lsbwidth;
            lsbLeft.Width = lsbRight.Width = lsbwidth;

            btnNone.Left = btnMinus.Left = btnPlus.Left = btnAll.Left = buttonLeft;
            btnNone.Top = buttonTop + pnlTop.Height;
            btnMinus.Top = btnNone.Top + btnNone.Height + 7;
            btnPlus.Top = btnMinus.Top + btnMinus.Height + 7;
            btnAll.Top = btnPlus.Top + btnPlus.Height + 7;
        }

        #endregion

        #region Events

        private void lsbRight_MouseDown(object sender, MouseEventArgs e)
        {
            source = clickSource.right;
            if (lsbRight.SelectedItems != null)
                lsbRight.DoDragDrop(lsbRight.SelectedItems, DragDropEffects.All);
        }

        private void lsbRight_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListBox.SelectedObjectCollection)) && e.AllowedEffect == DragDropEffects.All)
                e.Effect = DragDropEffects.All;
        }

        private void lsbRight_DragDrop(object sender, DragEventArgs e)
        {
            if (source == clickSource.right)
                return;

            source = clickSource.right;

            ListBox.SelectedObjectCollection coll = (ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection));
            foreach (GenericList<string> el in coll)
            {
                AddRightValue(el.Archive, el.Display);
                leftManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        private void lsbLeft_MouseDown(object sender, MouseEventArgs e)
        {
            source = clickSource.left;
            if (lsbLeft.SelectedItems != null)
                lsbLeft.DoDragDrop(lsbLeft.SelectedItems, DragDropEffects.All);
        }

        private void lsbLeft_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListBox.SelectedObjectCollection)) && e.AllowedEffect == DragDropEffects.All)
                e.Effect = DragDropEffects.All;
        }

        private void lsbLeft_DragDrop(object sender, DragEventArgs e)
        {
            if (source == clickSource.left)
                return;

            source = clickSource.left;

            ListBox.SelectedObjectCollection coll = (ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection));
            foreach (GenericList<string> el in coll)
            {
                AddLeftValue(el.Archive, el.Display);
                rightManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (GenericList<string> el in lsbRight.Items)
            {
                AddLeftValue(el.Archive, el.Display);
                rightManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            foreach (GenericList<string> el in lsbRight.SelectedItems)
            {
                AddLeftValue(el.Archive, el.Display);
                rightManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            foreach (GenericList<string> el in lsbLeft.SelectedItems)
            {
                AddRightValue(el.Archive, el.Display);
                leftManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (GenericList<string> el in lsbLeft.Items)
            {
                AddRightValue(el.Archive, el.Display);
                leftManager.Remove(el);
            }
            rightManager.Refresh();
            leftManager.Refresh();
        }

        #endregion
    }

    #region ListBoxManager

    public class ListBoxManager<T> : ArrayList
    {
        private ListBox lstview;

        public ListBoxManager()
        {
        }

        public ListBoxManager(ListBox lstview)
        {
            AttachTo(lstview);
        }

        public bool IsAttached { get { return lstview != null; } }

        public void AttachTo(ListBox lstview)
        {
            this.lstview = lstview;

            lstview.DataSource = this;
            lstview.DisplayMember = "Display";
            lstview.ValueMember = "Archive";
        }

        public void AddValue(T key, string text)
        {
            this.Add(new GenericList<T>(key, text));
        }

        internal GenericList<T> FindKey(T mykey)
        {
            foreach (GenericList<T> el in this)
            {
                if (el.Archive.Equals(mykey))
                    return el;
            }

            return null;
        }

        public void Refresh()
        {
            if (lstview != null && lstview.BindingContext[this] != null)
                ((CurrencyManager)lstview.BindingContext[this]).Refresh();
        }

        public T GetValue()
        {
            if (lstview != null)
            {
                if (lstview.SelectedIndex == -1)
                    return default(T);
                return ((GenericList<T>)this[lstview.SelectedIndex]).Archive;
            }
            else
            {
                return default(T);
            }
        }

        public T GetKeyAt(int idx)
        {
            return ((GenericList<T>)this[idx]).Archive;
        }

        public string GetValueAt(int idx)
        {
            return ((GenericList<T>)this[idx]).Display;
        }

        public void ChangeText(int val, string Text)
        {
            ((GenericList<T>)this[val]).Display = Text;
        }
    }

    #endregion
}