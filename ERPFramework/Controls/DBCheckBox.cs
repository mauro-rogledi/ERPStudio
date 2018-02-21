using ERPFramework.Data;
using ERPFramework.Libraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    /// <summary>
    /// Summary description for DBCheckBox.
    /// </summary>
    [System.ComponentModel.DefaultEvent("Click"),
    System.ComponentModel.DefaultProperty("Checked"),
    ToolboxBitmap(typeof(System.Windows.Forms.CheckBox))]
    public class DBCheckBox : MetroFramework.Controls.MetroCheckBox
    {
        protected const int WM_KEYDOWN = 0x0100;

        public event EventHandler PageUp;

        public event EventHandler PageDown;

        public DBCheckBox()
            : base()
        {
            //this.FlatStyle = FlatStyle.System;
        }

        public byte DBChecked
        {
            get
            {
                try
                {
                    return (Checked) ? (byte)1 : (byte)0;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    return 0;
                }
            }
            set
            {
                try
                {
                    Checked = (value == 1) ? true : false;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
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

                if (keyCode == Keys.PageUp)
                    if (this.PageUp != null) this.PageUp(this, new EventArgs());

                if (keyCode == Keys.PageDown)
                    if (this.PageDown != null) this.PageDown(this, new EventArgs());
            }
            return base.PreProcessMessage(ref msg);
        }
    }

    /// <summary>
    /// Summary description for DBCheckBox.
    /// </summary>
    [System.ComponentModel.DefaultEvent("Click"),
    System.ComponentModel.DefaultProperty("Checked"),
    ToolboxBitmap(typeof(System.Windows.Forms.CheckBox))]
    public class DBRadioButton : MetroFramework.Controls.MetroRadioButton
    {
        protected const int WM_KEYDOWN = 0x0100;

        public byte DBChecked
        {
            get { return (Checked) ? (byte)1 : (byte)0; }
            set { Checked = (value == 1) ? true : false; }
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
    /// Summary description for TagMenuItem.
    /// </summary>
    [System.ComponentModel.DefaultEvent("Click"),
    System.ComponentModel.DefaultProperty("Text"),
    ToolboxBitmap(typeof(System.Windows.Forms.MenuItem))]
    public class FormMenuItem : System.Windows.Forms.MenuItem
    {
        [Description("The form associated with the item menu"), Category("User")]
        private Form form;

        public Form Form
        {
            set { form = value; }
            get { return form; }
        }
    }

    /// <summary>
    /// Summary description for FormToolStripMenuItem.
    /// </summary>
    [System.ComponentModel.DefaultEvent("Click"),
    System.ComponentModel.DefaultProperty("Text"),
   ToolboxBitmap(typeof(System.Windows.Forms.ToolStripMenuItem))]
    public class FormToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        [Description("The form associated with the item menu"), Category("User")]
        private Form form;

        public Form Form
        {
            set { form = value; }
            get { return form; }
        }
    }

    /// <summary>
    /// Summary description for DBCheckBox.
    /// </summary>
    [System.ComponentModel.DefaultEvent("Click"),
    System.ComponentModel.DefaultProperty("Text"),
    ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
    public class DBComboBox : MetroFramework.Controls.MetroComboBox
    {
        protected ComboBoxManager cbbManager;

        virtual public void AttachDataReader<T>(IDataReaderUpdater dr, IColumn code, IColumn description)
        {
            AttachDataReader<T>(dr, code, description, false);
        }

        virtual public void AttachDataReader<T>(IDataReaderUpdater dr, IColumn code, IColumn description, bool alsoNULL)
        {
            cbbManager = new ComboBoxManager(this);
            if (GlobalInfo.DBaseInfo.dbManager != null)
            {
                if (alsoNULL)
                    cbbManager.AddValue<T>(default(T) == null ? (T)Convert.ChangeType(string.Empty, typeof(T)) : default(T), string.Empty);
                FillComboBox<T>(dr, code, description);
                cbbManager.Refresh();
            }
        }

        virtual protected void FillComboBox<T>(IDataReaderUpdater dr, IColumn code, IColumn description)
        {
            dr.Find();
            for (int t = 0; t < dr.Count; t++)
                cbbManager.AddValue(dr.GetValue<T>(code, t), dr.GetValue<string>(description, t));
        }

        virtual public T GetValue<T>()
        {
            return cbbManager != null
                        ? cbbManager.GetValue<T>()
                        : default(T);
        }
    }
}