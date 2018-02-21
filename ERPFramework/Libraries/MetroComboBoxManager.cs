using ERPFramework.Data;
using MetroFramework.Controls;
using MetroFramework.Extender;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPFramework.Libraries
{
    public class MetroComboBoxManager
    {
        protected Type enumType;
        private MetroComboBox cbbBox;
        private DataGridViewComboBoxColumn dgwCbbBox = null;
        private object list;

        public MetroComboBoxManager()
        {
        }

        public MetroComboBoxManager(MetroComboBox cbbBox)
        {
            AttachTo(cbbBox);
        }

        public void AttachTo(MetroComboBox cbbBox)
        {
            this.cbbBox = cbbBox;
            if (list != null)
            {
                cbbBox.DataSource = list;
                cbbBox.DisplayMember = "Display";
                cbbBox.ValueMember = "Archive";
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
                    return default(T);

                return ((List<GenericList<T>>)list)[cbbBox.SelectedIndex].Archive;
            }
            else
                return default(T);
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

    public class MetroListBoxManager
    {
        protected Type enumType;
        private MetroListBox cbbBox;
        private object list;

        public MetroListBoxManager()
        {
        }

        public MetroListBoxManager(MetroListBox cbbBox)
        {
            AttachTo(cbbBox);
        }

        public void AttachTo(MetroListBox cbbBox)
        {
            this.cbbBox = cbbBox;
            if (list != null)
            {
                cbbBox.DataSource = list;
                cbbBox.DisplayMember = "Display";
                cbbBox.ValueMember = "Archive";
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
                    return default(T);

                return ((List<GenericList<T>>)list)[cbbBox.SelectedIndex].Archive;
            }
            else
                return default(T);
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
}