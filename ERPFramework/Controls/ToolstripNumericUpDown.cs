using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ERPFramework.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolstripNumericUpDown : ToolStripControlHost
    {
        public ToolstripNumericUpDown()
            : base(new NumericUpDown())
        {
        }

        public int Value
        {
            get
            {
                int result = 1;
                if (int.TryParse(Text, out result))
                    return result;
                else
                    return 1;
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged += new EventHandler(OnValueChanged);
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged -= new EventHandler(OnValueChanged);
        }

        public event EventHandler ValueChanged;

        public Control NumericUpDownControl
        {
            get { return Control as NumericUpDown; }
        }

        public void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolstripComboBox : ToolStripControlHost
    {
        public event EventHandler SelectedIndexChanged;

        public ToolstripComboBox()
            : base(new ComboBox())
        {
        }

        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return ((ComboBox)Control).DropDownStyle;
            }

            set
            {
                ((ComboBox)Control).DropDownStyle = value;
            }
        }

        public ComboBox.ObjectCollection Items
        {
            get
            {
                return ((ComboBox)Control).Items;
            }
        }

        public object DataSource
        {
            get
            {
                return ((ComboBox)Control).DataSource;
            }

            set
            {
                ((ComboBox)Control).DataSource = value;
            }
        }

        public string DisplayMember
        {
            get
            {
                return ((ComboBox)Control).DisplayMember;
            }

            set
            {
                ((ComboBox)Control).DisplayMember = value;
            }
        }

        public string ValueMember
        {
            get
            {
                return ((ComboBox)Control).ValueMember;
            }

            set
            {
                ((ComboBox)Control).ValueMember = value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return ((ComboBox)Control).SelectedIndex;
            }

            set
            {
                ((ComboBox)Control).SelectedIndex = value;
            }
        }

        public BindingContext BindingContext
        {
            get
            {
                return ((ComboBox)Control).BindingContext;
            }

            set
            {
                ((ComboBox)Control).BindingContext = value;
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((ComboBox)control).SelectedIndexChanged += new EventHandler(SelectedIndexChangedEvent);
        }

        private void SelectedIndexChangedEvent(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            ((ComboBox)control).SelectedIndexChanged -= new EventHandler(SelectedIndexChangedEvent);
        }
    }
}