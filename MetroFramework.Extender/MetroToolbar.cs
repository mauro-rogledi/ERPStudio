using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace MetroFramework.Extender
{
    public enum MetroToolbarButtonType { New, Edit, Save, Delete, Undo, Search, Filter, Preview, Print, Preference, Exit, Execute, AddOn, Custom }

    public class MetroToolbar : MetroUserControl
    {

        [Category(ExtenderDefaults.PropertyCategory.Event)]
        public event EventHandler<MetroToolbarButtonType> ItemClicked;

        [Category(ExtenderDefaults.PropertyCategory.Event)]
        public event EventHandler<IMetroToolBarButton> ButtonClicked;

        [Category(ExtenderDefaults.PropertyCategory.Event)]
        public event EventHandler<string> ToolStripMenuClick;

        public MetroToolbar()
        {
            Size = new Size(100, 53);
            Dock = DockStyle.Top;
        }

        public bool ShouldSerializeItems() { return Items.Count > 0; }
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorAttribute(typeof(MetroTollbarColectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [EditorBrowsableAttribute(EditorBrowsableState.Advanced)]
        public ControlCollection Items { get { return base.Controls; } }


        public void ToolbarButtonClicked(IMetroToolBarButton e)
        {
            OnbuttonClicked(e);
            ButtonClicked?.Invoke(this, e);
        }

        public void ToolbarPushButtonClicked(IMetroToolBarButton e)
        {
            OnItemClicked(e.ButtonType);
            ItemClicked?.Invoke(e, e.ButtonType);
        }

        public void ToolbarDropDownClicked(IMetroToolBarButton e)
        {
            var btn = e as MetroToolbarDropDownButton;
            switch (btn.Items.Count)
            {
                case 0:
                    OnItemClicked(btn.ButtonType);
                    ItemClicked?.Invoke(this, btn.ButtonType);
                    break;
                case 1:
                    OnToolStripMenuClick(btn.Items[0].Tag as string);
                    break;
                default:
                    btn.ShowMenu();
                    break;
            }
            
        }

        public void AddButton(IMetroToolBarButton btn, int pos)
        {
            Controls.Add(btn as Control);
            Controls.SetChildIndex(btn as Control, pos);
        }

        protected virtual void OnToolStripMenuClick(string tag)
        {
            ToolStripMenuClick?.Invoke(this, tag);

        }

        protected virtual void OnItemClicked(MetroToolbarButtonType buttonType)
        {

        }

        protected virtual void OnbuttonClicked(IMetroToolBarButton button)
        {

        }

        public void ButtonVisibleChanged()
        {
            IMetroToolBarButton lastVisible = null;

            foreach (IMetroToolBarButton btn in Controls)
            {
                var index = Controls.IndexOf(btn as Control);
                if (btn is MetroToolbarSeparator)
                {
                    btn.Visible = lastVisible != null
                            ? !(lastVisible is MetroToolbarSeparator)
                            : true;
                }

                if (btn.Visible && lastVisible == null)
                {
                        lastVisible = btn;
                        continue;
                }

                System.Diagnostics.Debug.WriteLine($"Name '{btn.Name}' type '{btn.GetType()}");

                if (btn.Visible)
                    lastVisible = btn;

            }
        }

        public void AddSplitToolbarButton(MetroToolbarButtonType btnType, string text, string tag, Image image)
        {
            var btn = GetButton<MetroToolbarDropDownButton>(btnType);
            var mnuBtn = new ToolStripMenuItem(text)
            {
                Tag = tag,
                Image = image
            };
            btn.AddDropDownItem(mnuBtn);
            if (btn.Items.Count==1)
                btn.DropDownItemClicked += Btn_DropDownItemClicked;
        }

        private void Btn_DropDownItemClicked(object sender, EventArgs e)
        {
            var tsmi = sender as ToolStripMenuItem;
            OnToolStripMenuClick(tsmi.Tag.ToString());
        }

        public T GetButton<T>(MetroToolbarButtonType btnType)
        {
            foreach (IMetroToolBarButton btn in Controls)
                if (btn.ButtonType == btnType)
                    if (!(btn is MetroToolbarSeparator))
                        return (T)Convert.ChangeType(btn, typeof(T));

            return default(T);
        }

        public T GetButton<T>(string btnName)
        {
            foreach (IMetroToolBarButton btn in Controls)
                if (btn.Name == btnName)
                    if (!(btn is MetroToolbarSeparator))
                        return (T)Convert.ChangeType(btn, typeof(T));

            return default(T);
        }

        public bool GetButtonState(MetroToolbarButtonType btnType)
        {
            foreach (IMetroToolBarButton btn in Controls)
                if (btn.ButtonType == btnType)
                    if (!(btn is MetroToolbarSeparator))
                        return btn.Visible;

            return false;
        }
    }

    internal class MetroTollbarColectionEditor : System.ComponentModel.Design.CollectionEditor
    {
        private Type[] types;

        public MetroTollbarColectionEditor(Type type) : base(type)
        {
            types = new Type[] 
            {
                typeof(MetroToolbarPushButton),
                typeof(MetroToolbarDropDownButton),
                typeof(MetroToolbarSeparator),
                typeof(MetroToolbarLabel),
                typeof(MetroToolbarComboBox),
                typeof(MetroToolbarNumericUpDown),
                typeof(MetroToolbarLinkChecked),
                typeof(MetroToolbarContainer)
            };

        }


        protected override Type[] CreateNewItemTypes()
        {
            return types;
        }
    }
}
