using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ERPFramework.CounterManager;
using ERPFramework.Data;

namespace ERPFramework.Forms
{
    public class BindingCollection : List<BindingSource>
    {
        public BindingSource this[string nameBinding]
        {
            get
            {
                return Find(bs => bs.DataMember == nameBinding);
                //foreach (BindingSource bindingSource in List)
                //    if (bindingSource != null && bindingSource.DataMember == nameBinding)
                //        return bindingSource;

                //return null;
            }
        }

        public new BindingSource Add(BindingSource bindingSource)
        {
            Add(bindingSource);
            return bindingSource;
        }

        public void EndEdit()
        {
            ForEach(bs => bs.EndEdit());
            //foreach (BindingSource bindingSource in List)
            //    if (bindingSource != null)
            //        bindingSource.EndEdit();
        }

        public bool AllowNew
        {
            set
            {
                ForEach(bs => bs.AllowNew = value);
                //foreach (BindingSource bindingSource in List)
                //    if (bindingSource != null)
                //        bindingSource.AllowNew = value;
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

        #endregion

        public BinderCollectorItem(object control, string table, string column, string property)
        {
            this.Control = control;
            this.Table = table;
            this.Column = column;
            this.Property = property;
        }
    }

    #endregion

    #region BinderCollection

    public class BinderCollector : List<BinderCollectorItem>
    {
        public BinderCollectorItem Add(object control, string table, string column, string property)
        {
            var bind = new BinderCollectorItem(control, table, column, property);
            this.Add(bind);

            return bind;
        }
    }

    #endregion

    internal class ControlBinder
    {
        private readonly BinderCollector bCollection;

        public ControlBinder()
        {
            bCollection = new BinderCollector();
        }

        public BinderCollectorItem Bind(object control)
        {
            return bCollection.Add(control, string.Empty, string.Empty, string.Empty);
        }

        public BinderCollectorItem Bind(object control, IColumn column, string property = "Text")
        {
            return bCollection.Add(control, column.Tablename, column.Name, property);
        }

        //public BinderCollectorItem Bind(object control, string table, string column, string property)
        //{
        //    return bCollection.Add(control, table, column, property);
        //}

        public BinderCollectorItem Bind(object control, string table)
        {
            return bCollection.Add(control, table, string.Empty, "Text");
        }

        //public BinderCollectorItem Bind(BinderCollector binderCollection, object control, IColumn column)
        //{
        //    return binderCollection.Add(control, column.Tablename, column.Name, "Text");
        //}

        //public BinderCollectorItem Bind(BinderCollector binderCollection, object control, IColumn column, string property)
        //{
        //    return binderCollection.Add(control, column.Tablename, column.Name, property);
        //}

        //public BinderCollectorItem Bind(BinderCollector binderCollection, object control, string table, string column, string property)
        //{
        //    return binderCollection.Add(control, table, column, property);
        //}

        public bool Enable(bool status)
        {
            System.Reflection.PropertyInfo pInfo = null;
            bCollection.ForEach(de =>
            {
                var obj = de.Control;
                pInfo = obj.GetType().GetProperty("Enabled");
                if (pInfo != null &&
                    !(obj.GetType().BaseType == typeof(Controls.ExtendedDataGridView) ||
                     obj.GetType() == typeof(Controls.ExtendedDataGridView)))
                    pInfo.SetValue(obj, status, null);
                else
                {
                    pInfo = obj.GetType().GetProperty("ReadOnly");
                    if (pInfo != null)
                        pInfo.SetValue(obj, !status, null);
                }

                if (obj.GetType().BaseType == typeof(Controls.ExtendedDataGridView))
                {
                    var dgw = obj as Controls.ExtendedDataGridView;
                    dgw.AllowUserToDeleteRows = status;
                }
            });
            return true;
        }

        public void SetFocus()
        {
            System.Reflection.MethodInfo mInfo = null;
            System.Reflection.PropertyInfo pInfo = null;

            bCollection.ForEach(de =>
            {
                var obj = de.Control;
                var enabled = false;
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
                    return;
                }
            });
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