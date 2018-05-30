using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ERPFramework.CounterManager;
using ERPFramework.Data;

namespace ERPFramework.Forms
{
    public class BindingCollection : CollectionBase
    {
        public BindingSource this[string nameBinding]
        {
            get
            {
                foreach (BindingSource bindingSource in List)
                    if (bindingSource != null && bindingSource.DataMember == nameBinding)
                        return bindingSource;

                return null;
            }
        }

        public BindingSource Add(BindingSource bindingSource)
        {
            List.Add(bindingSource);
            return bindingSource;
        }

        public void EndEdit()
        {
            foreach (BindingSource bindingSource in List)
                if (bindingSource != null)
                    bindingSource.EndEdit();
        }

        public bool AllowNew
        {
            set
            {
                foreach (BindingSource bindingSource in List)
                    if (bindingSource != null)
                        bindingSource.AllowNew = value;
            }
        }
    }

    #region BinderCollectionItem

    public class BinderCollectorItem
    {
        #region Public Properties

        public object Control { get; private set; }

        public string Table { get; private set; }

        public string Column { get; private set; }

        public string Property { get; private set; }

        public Findable Findable { get; set; }

        #endregion

        public BinderCollectorItem(object control, string table, string column, string property, Findable findable)
        {
            this.Control = control;
            this.Table = table;
            this.Column = column;
            this.Property = property;
            this.Findable = findable;
        }
    }

    #endregion

    #region BinderCollection

    public class BinderCollector : List<BinderCollectorItem>
    {
        public BinderCollectorItem Add(object control, string table, string column, string property, Findable findable)
        {
            BinderCollectorItem bind = new BinderCollectorItem(control, table, column, property, findable);
            this.Add(bind);

            return bind;
        }

        //public BinderCollectorItem this[string control]
        //{
        //    get
        //    {
        //        List.
        //    }
        //}
    }

    #endregion

    internal class ControlBinder
    {
        private BinderCollector bCollection;

        public ControlBinder()
        {
            bCollection = new BinderCollector();
        }

        /// <summary>
        /// Only for Enable/Disable Controls
        /// </summary>
        public BinderCollectorItem Bind(object control)
        {
            return bCollection.Add(control, string.Empty, string.Empty, string.Empty, Findable.NO);
        }

        public BinderCollectorItem Bind(object control, IColumn column)
        {
            return bCollection.Add(control, column.Tablename, column.Name, "Text", Findable.NO);
        }

        public BinderCollectorItem Bind(object control, IColumn column, string property)
        {
            return bCollection.Add(control, column.Tablename, column.Name, property, Findable.NO);
        }

        public BinderCollectorItem Bind(object control, IColumn column, Findable findable)
        {
            return bCollection.Add(control, column.Tablename, column.Name, "Text", findable);
        }

        public BinderCollectorItem Bind(object control, IColumn column, string property, Findable findable)
        {
            return bCollection.Add(control, column.Tablename, column.Name, property, findable);
        }

        public BinderCollectorItem Bind(object control, string table, string column, string property)
        {
            return bCollection.Add(control, table, column, property, Findable.NO);
        }

        public BinderCollectorItem Bind(object control, string table)
        {
            return bCollection.Add(control, table, string.Empty, "Text", Findable.NO);
        }

        public BinderCollectorItem Bind(BinderCollector binderCollection, object control, IColumn column)
        {
            return binderCollection.Add(control, column.Tablename, column.Name, "Text", Findable.NO);
        }

        public BinderCollectorItem Bind(BinderCollector binderCollection, object control, IColumn column, string property)
        {
            return binderCollection.Add(control, column.Tablename, column.Name, property, Findable.NO);
        }

        public BinderCollectorItem Bind(BinderCollector binderCollection, object control, string table, string column, string property, Findable findable)
        {
            return binderCollection.Add(control, table, column, property, findable);
        }

        public void SetFindable(object control, Findable findable)
        {
            BinderCollectorItem item = bCollection.Find(p => p.Control == control);
            if (item != null)
                item.Findable = findable;
        }

        public bool Enable(bool status)
        {
            System.Reflection.PropertyInfo pInfo = null;
            foreach (BinderCollectorItem de in bCollection)
            {
                object obj = de.Control;
                pInfo = obj.GetType().GetProperty("Enabled");
                if (pInfo != null &&
                    !(obj.GetType().BaseType == typeof(ERPFramework.Controls.ExtendedDataGridView) ||
                     obj.GetType() == typeof(ERPFramework.Controls.ExtendedDataGridView)))
                    pInfo.SetValue(obj, status, null);
                else
                {
                    pInfo = obj.GetType().GetProperty("ReadOnly");
                    if (pInfo != null)
                        pInfo.SetValue(obj, !status, null);
                }

                if (obj.GetType().BaseType == typeof(ERPFramework.Controls.ExtendedDataGridView))
                {
                    ERPFramework.Controls.ExtendedDataGridView dgw = (ERPFramework.Controls.ExtendedDataGridView)obj;
                    dgw.AllowUserToDeleteRows = status;
                }
            }
            return true;
        }

        public bool SetFindable(bool finding)
        {
            if (!finding)
                return true;

            System.Reflection.PropertyInfo pInfo = null;
            System.Reflection.MethodInfo mInfo = null;
            foreach (BinderCollectorItem de in bCollection)
            {
                if (de.Findable == Findable.NO)
                    continue;

                object obj = de.Control;
                pInfo = obj.GetType().GetProperty("Enabled");
                if (pInfo != null)
                    pInfo.SetValue(obj, true, null);
                else
                {
                    pInfo = obj.GetType().GetProperty("ReadOnly");
                    if (pInfo != null)
                        pInfo.SetValue(obj, false, null);
                }

                mInfo = obj.GetType().GetMethod("Clean");
                if (mInfo != null)
                    mInfo.Invoke(obj, new object[] { });
            }
            return true;
        }

        public string GetFindableString()
        {
            string findableQuery = string.Empty;

            foreach (BinderCollectorItem de in bCollection)
            {
                if (de.Findable == Findable.NO)
                    continue;

                string compareString = CompareString(de);
                if (findableQuery != string.Empty && compareString != string.Empty)
                    findableQuery += " AND ";

                findableQuery += compareString;
            }

            return findableQuery;
        }

        private string CompareString(BinderCollectorItem item)
        {
            string sVal = string.Empty;

            // Controllo se Empty
            if (item.Control.GetType().BaseType == typeof(CodesControl) ||
                item.Control.GetType() == typeof(CodesControl))
            {
                CodesControl codes = (CodesControl)item.Control;
                sVal = codes.IsEmpty
                            ? ""
                            : codes.Findable;
            }
            //else if (item.Control.GetType() == typeof(CounterControl))
            //{
            //    CounterControl counter = (CounterControl)item.Control;
            //    sVal = counter.IsEmpty
            //                ? ""
            //                : counter.ToString();
            //}
            else
            {
                object obj = item.Control;
                System.Reflection.PropertyInfo pInfo = obj.GetType().GetProperty(item.Property);
                sVal = (pInfo == null)
                    ? ""
                    : pInfo.GetValue(obj, null).ToString();
            }

            if (item.Property == "Text")
            {
                if (sVal.Length > 0)
                    return string.Format("[{0}].[{1}] LIKE '{2}%'", item.Table, item.Column, sVal);
            }
            else
            {
                if (sVal.Length > 0)
                    return string.Format("[{0}].[{1}] = '{2}'", item.Table, item.Column, sVal);
            }
            return string.Empty;
        }

        public void SetFocus()
        {
            System.Reflection.MethodInfo mInfo = null;
            System.Reflection.PropertyInfo pInfo = null;

            foreach (BinderCollectorItem de in bCollection)
            {
                object obj = de.Control;
                bool enabled = false;
                pInfo = obj.GetType().GetProperty("Enabled");
                if (pInfo != null)
                    enabled = (bool)pInfo.GetValue(obj, null);
                else
                {
                    pInfo = obj.GetType().GetProperty("ReadOnly");
                    if (pInfo != null)
                        enabled = !(bool)pInfo.GetValue(obj, null);
                }

                mInfo = obj.GetType().GetMethod("Focus");
                if (mInfo != null && enabled)
                {
                    mInfo.Invoke(obj, null);
                    break;
                }
            }
        }
    }

    public class BindableObject<T> : IBindableComponent, ICounterManager
    {
        private BindingContext bindingContext;
        private ControlBindingsCollection dataBindings;
        private ERPFramework.CounterManager.CounterManager counterUpdater;
        private RRCounter rRCounter;

        public T Value { get; set; }
        public bool IsUpdatable { get; set; } = true;

        private bool statesButton = true;
        [DefaultValue(true)]
        [Category(MetroFramework.Extender.ExtenderDefaults.PropertyCategory.Behaviour)]
        public bool StatesButton
        {
            get { return statesButton; }
            set { statesButton = value; }
        }

        public event EventHandler Disposed;

        public void EndCurentEdit()
        {
            ((PropertyManager)this.BindingContext[0]).EndCurrentEdit();
        }

        public void ResumeBinding()
        {
            ((PropertyManager)this.BindingContext[0]).ResumeBinding();
        }

        public BindingContext BindingContext
        {
            get
            {
                if (bindingContext == null)
                {
                    bindingContext = new BindingContext();
                }
                return bindingContext;
            }
            set
            {
                bindingContext = value;
            }
        }

        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (dataBindings == null)
                {
                    dataBindings = new ControlBindingsCollection
                    (this);
                }
                return dataBindings;
            }
        }

        public void Dispose()
        {
            if (Disposed != null)
                Disposed(this, EventArgs.Empty);
        }

        public ISite Site { get { return null; } set { } }

        public void UpdateValue(DBOperation operation)
        {
            int val = (int)Convert.ChangeType(Value, typeof(int));
            switch (operation)
            {
                case DBOperation.Get:
                    GetNewValue();
                    break;

                case DBOperation.New:
                        if (StatesButton)
                            GetNewValue();

                        counterUpdater.SetValue(val);
                    break;

                case DBOperation.Delete:
                    counterUpdater.DeleteValue(val);
                    break;

            }
        }

        public void GetNewValue()
        {
            if (counterUpdater == null)
                return;

            Value = (T)Convert.ChangeType(counterUpdater.GetNewIntValue(), typeof(T));

        }

        public void AttachCounterType(int key, DateTime curDate, ERPFramework.Forms.IDocumentBase documentBase)
        {
            System.Diagnostics.Debug.Assert(counterUpdater == null);
            this.rRCounter = new RRCounter(null);
            if (!rRCounter.Find(curDate.Year, key))
            {
                MessageBox.Show(Properties.Resources.Msg_MissingCounter,
                Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            counterUpdater = new ERPFramework.CounterManager.CounterManager(key, curDate.Year, rRCounter.GetValue<string>(EF_Counter.Description), documentBase);
        }
        public bool IsAutomatic { get; set; }
    }
}