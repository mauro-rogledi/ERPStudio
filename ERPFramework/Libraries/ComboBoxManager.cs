using ERPFramework.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPFramework.Libraries
{
    public class ComboBoxManager
    {
        protected Type enumType;
        private ComboBox cbbBox;
        private DataGridViewComboBoxColumn dgwCbbBox;
        private object list;

        public ComboBoxManager()
        {
        }

        public ComboBoxManager(ComboBox cbbBox)
        {
            AttachTo(cbbBox);
        }

        public ComboBoxManager(DataGridViewComboBoxColumn cbbBox)
        {
            AttachTo(cbbBox);
        }

        public void AttachTo(ComboBox cbbBox)
        {
            this.cbbBox = cbbBox;
            if (list != null)
            {
                cbbBox.DataSource = list;
                cbbBox.DisplayMember = "Display";
                cbbBox.ValueMember = "Archive";
            }
        }

        public void AttachTo(DataGridViewComboBoxColumn cbbBox)
        {
            this.dgwCbbBox = cbbBox;
            if (list != null)
            {
                dgwCbbBox.DataSource = list;
                dgwCbbBox.DisplayMember = "Display";
                dgwCbbBox.ValueMember = "Archive";
            }
        }

        public void CreateList<T>()
        {
            list = new List<GenericList<T>>();
            if (cbbBox != null)
            {
                cbbBox.DataSource = list;
                cbbBox.DisplayMember = "Display";
                cbbBox.ValueMember = "Archive";
            }
            else if (dgwCbbBox != null)
            {
                dgwCbbBox.DataSource = list;
                dgwCbbBox.DisplayMember = "Display";
                dgwCbbBox.ValueMember = "Archive";
            }
        }

        public int Count<T>()
        {
                return (list != null)
                    ? ((List<GenericList<T>>)list).Count
                    : 0;
        }


        public void Clear<T>()
        {
            if (list == null)
                return;
            ((List<GenericList<T>>)list).Clear();
        }

        virtual public void AttachDataReader<T>(IDataReaderUpdater dr, IColumn code, IColumn description, bool alsoNull)
        {
            CreateList<T>();
            if (GlobalInfo.DBaseInfo.dbManager != null)
            {
                if (alsoNull)
                    AddValue<T>(default(T), string.Empty);
                dr.Find();
                for (int t = 0; t < dr.Count; t++)
                    AddValue<T>(dr.GetValue<T>(code, t), dr.GetValue<string>(description, t));
            }
        }

        public void AddValue<T>(T key, string text)
        {
            if (list == null)
                CreateList<T>();
            ((List<GenericList<T>>)list).Add(new GenericList<T>(key, text));
        }

        public T GetValue<T>()
        {
            if (cbbBox != null)
            {
                if (cbbBox.SelectedIndex == -1)
                {
                    //object obj = default(T);
                    return default(T);
                }
                return ((List<GenericList<T>>)list)[cbbBox.SelectedIndex].Archive;
            }
            else
            {
                //object obj = default(T);
                return default(T);
            }
        }

        public void ChangeText<T>(int val, string Text)
        {
            ((List<GenericList<T>>)list)[val].Display = Text;
        }

        public void Refresh()
        {
            if (cbbBox != null && list != null && cbbBox.BindingContext != null && cbbBox.BindingContext[list] != null)
                ((CurrencyManager)cbbBox.BindingContext[list]).Refresh();
        }
    }

    public class GenericList<T>
    {
        private T myArchive;
        private string mydisplay;
        private bool displayDesc;

        public GenericList(T archive, string display)
            : this(archive, display, false)
        { }

        public GenericList(T archive, string display, bool displayDesc)
        {
            this.myArchive = archive;
            this.mydisplay = display;
            this.displayDesc = displayDesc;
        }

        public T Archive { get { return myArchive; } }

        public string Display
        {
            get { return mydisplay; }
            set { mydisplay = value; }
        }

        public override string ToString()
        {
            return displayDesc
                    ? this.mydisplay
                    : (string)Convert.ChangeType(this.myArchive, typeof(string));

            //return value;
        }
    }
}