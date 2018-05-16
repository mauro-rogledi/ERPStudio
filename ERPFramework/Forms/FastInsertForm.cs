#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using ERPFramework.Controls;
using ERPFramework.CounterManager;
using ERPFramework.Data;
using ERPFramework.ModulesHelper;
using ERPFramework.Preferences;
using MetroFramework.Controls;
using MetroFramework.Extender;

#endregion

namespace ERPFramework.Forms
{
    public partial class FastInsertForm : MetroFramework.Forms.MetroForm, IDocument, IPropagate
    {
        #region Private

        protected string ConnectionString { get; private set; }

        protected Control keyControl;
        protected Data.DBManager dbManager;
        internal ControlBinder controlBinder;

        private List<MessageData> messageList;

        [Browsable(false)]
        public bool IsModal { get; set; }

        [Browsable(false)]
        public List<Addon> AddonList { get; } = new List<Addon>();


        #endregion

        #region Properties

        [Localizable(true)]
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public string Title { get; set; }

        public event EventHandler Exit;


        [Browsable(false)]
        public ProviderType providerType { get; private set; }

        public List<MessageData> ErrorMessageList { get { return messageList; } }
        public enum NullValue { SetNull, NotSet }
        public bool IsNew { get; private set; }
        public Control KeyControl { get { return keyControl; } }
        public DBMode DocumentMode { get { return (dbManager != null) ? dbManager.Status : DBMode.Browse; } }

        [Browsable(false)]
        public SqlProxyTransaction Transaction { get; set; } = null;
        [Browsable(false)]
        public SqlProxyConnection Connection { get; } = null;

        public bool SilentMode
        {
            set
            {
                if (dbManager != null)
                    dbManager.SilentMode = value;
            }
            get
            {
                return (dbManager != null)
                    ? dbManager.SilentMode
                    : false;
            }
        }

        #endregion

        #region ToolbarEvent

        public enum ToolbarEventKind
        {
            New, Save, Edit, Undo, Exit
        }

        public bool AddNew()
        {
            return ToolbarEvent(FastInsertForm.ToolbarEventKind.New);
        }

        public bool Save()
        {
            return ToolbarEvent(FastInsertForm.ToolbarEventKind.Save);
        }

        public bool Edit()
        {
            return ToolbarEvent(FastInsertForm.ToolbarEventKind.Edit);
        }


        #endregion


        public void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (dbManager != null)
                dbManager.Dispose();
        }

        public FastInsertForm()
        {
            InitializeComponent();
        }

        public FastInsertForm(string formname)
        {
            Name = formname;
            InitializeComponent();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            ConnectionString = GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString;
            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            controlBinder = new ControlBinder();
            messageList = new List<MessageData>();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            OnInitializeData();
            OnCustomizeToolbar(metroToolbar);

            ManageOperationButton();

            OnBindData();

            if (controlBinder != null)
                controlBinder.Enable(false);

            AddOnInitializeData();
            OnAddSplitMenuButton();
            AddonAddSplitMenuButton();

        }

        protected virtual void OnEnableToolbarButtons(ExMetroToolbar toolstrip)
        {
        }

        protected virtual void OnCustomizeToolbar(ExMetroToolbar toolstrip)
        {
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OnReady();
        }

        public void PropagateMouseDoubleClick(Control c)
        {
            if (c is IClickable)
                OnLinkClick(c as IClickable);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            var c = GetChildAtPoint(e.Location);
            if (c is IClickable)
                OnLinkClick(c as IClickable);
        }

        public virtual void OnLinkClick(IClickable c)
        {
        }

        public bool FindRecord(IRadarParameters key)
        {
            bool Found = dbManager.FindRecord(key);
            OnPrepareAuxData();
            ManageToolbarEvents();

            return Found;
        }

        public virtual void OnReady()
        {
        }

        protected virtual void AddOnInitializeData()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnInitializeData(dbManager);
        }

        protected virtual void OnAddSplitMenuButton()
        {
        }

        protected virtual void OnToolStripMenuClick(string tag)
        {
        }

        protected virtual void AddonAddSplitMenuButton()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnAddSplitMenuButton();
        }

        public void AddSplitToolbarButton(MetroToolbarButtonType btnType, string text, string tag, System.Drawing.Image image)
        {
            metroToolbar.AddSplitToolbarButton(btnType, text, tag, image);

            switch (btnType)
            {
                case MetroToolbarButtonType.Preference:
                    metroToolbar.ButtonPrefVisible = true;
                    break;
                case MetroToolbarButtonType.Preview:
                    metroToolbar.ButtonPreviewVisible = true;
                    break;
                case MetroToolbarButtonType.Print:
                    metroToolbar.ButtonPrintVisible = true;
                    break;
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.New))
                        OnNewEvent();
                    break;

                case Keys.F10:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Save))
                        OnSaveEvent();
                    break;

                case Keys.Escape:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Undo))
                        OnUndoEvent();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void formDB_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !OnBeforeClosing();
            if (dbManager != null && !e.Cancel)
                dbManager.Dispose();
        }

        public Binding BindControl(Control control, IColumn column, NullValue nullValue)
        {
            return BindControl(control, column, GetProperty(control), nullValue);
        }

        public Binding BindControl(Control control, IColumn column)
        {
            return BindControl(control, column, GetProperty(control), NullValue.SetNull);
        }

        public Binding BindControl(Control control, IColumn column, string property)
        {
            return BindControl(control, column, property, NullValue.SetNull);
        }

        public Binding BindControl(Control control, IColumn column, string property, NullValue nullValue)
        {
            return BindControl(control, column, property, nullValue, Findable.NO);
        }

        public Binding BindControl(Control control, IColumn column, NullValue nullValue, Findable findable)
        {
            return BindControl(control, column, GetProperty(control), nullValue, findable);
        }

        public Binding BindControl(Control control, IColumn column, Findable findable)
        {
            return BindControl(control, column, GetProperty(control), NullValue.SetNull, findable);
        }

        public Binding BindControl(Control control, IColumn column, string property, Findable findable)
        {
            return BindControl(control, column, property, NullValue.SetNull, findable);
        }

        public Binding BindControl(Control control, IColumn column, string property, NullValue nullValue, Findable findable)
        {
            if (column.Len > 0)
            {
                if (control is MetroTextBox)
                    ((MetroTextBox)control).MaxLength = column.Len;
                else if (control is TextBox)
                    ((TextBox)control).MaxLength = column.Len;
            }

            controlBinder.Bind(control, column, property, findable);
            if (dbManager.GetDataColumn(column).DefaultValue is System.DBNull && nullValue == NullValue.SetNull)
                dbManager.GetDataColumn(column).DefaultValue = column.DefaultValue;
            return control.DataBindings.Add(property, dbManager.MasterBinding, column.Name);
        }

        public void BindCounter(ICounterManager bindable)
        {
            Debug.Assert(dbManager != null);
            if (dbManager != null)
                dbManager.BindCounter(ref bindable);
        }

        public void BindCounter<T>(ref ICounterManager bindable)
        {
            Debug.Assert(dbManager != null);
            if (dbManager != null)
                dbManager.BindCounter(ref bindable);
        }

        public Binding BindObject(IBindableComponent bindableObj, IColumn column)
        {
            return BindObject(bindableObj, column, NullValue.SetNull);
        }

        public Binding BindObject(IBindableComponent bindableObj, IColumn column, NullValue nullValue)
        {
            if (dbManager.GetDataColumn(column).DefaultValue is System.DBNull && nullValue == NullValue.SetNull)
                dbManager.GetDataColumn(column).DefaultValue = column.DefaultValue;
            return bindableObj.DataBindings.Add("Value", dbManager.MasterBinding, column.Name);
        }

        public Binding BindLocal(Control control, string property, string datamember)
        {
            controlBinder.Bind(control);
            return control.DataBindings.Add(property, this, datamember);
        }

        /// <summary>
        /// Only For Enable/Disable Controls
        /// </summary>
        public Binding BindControl(Control control)
        {
            controlBinder.Bind(control);

            if (control.GetType() == typeof(ExtendedDataGridView))
            {
                ((ExtendedDataGridView)control).DocumentForm = this;
                ((ExtendedDataGridView)control).LoadSetting();
            }
            return null;
        }

        public void BindColumn(DataGridViewColumn gridcol, IColumn column)
        {
            gridcol.Name = column.Name;
            gridcol.DataPropertyName = column.Name;
            if (gridcol.CellType == typeof(DataGridViewTextBoxCell) && column.Len > 0)
                ((DataGridViewTextBoxColumn)gridcol).MaxInputLength = column.Len;

            if (dbManager.GetDataColumn(column) != null && dbManager.GetDataColumn(column).DefaultValue is System.DBNull)
                dbManager.GetDataColumn(column).DefaultValue = column.DefaultValue;
        }

        private string GetProperty(Control control)
        {
            if (control is DateTextBox)
                return "Today";
            else if (control is MetroTextBoxNumeric)
                return "Double";
            else if (control is TextBox || control is MetroTextBox || control is MetroCounterTextBox || control is MetroIntelliTextBox || control is MetroMaskedTextbox)
                return "Text";
            else if (control is CheckBox || control is RadioButton || control is MetroCheckBox || control is MetroRadioButton || control is MetroToggle)
                return "Checked";
            else if (control is ComboBox || control is MetroComboBox)
                return "SelectedValue";
            else if (control is NumericUpDown || control is MetroNumericUpDown)
                return "Value";
            else
                throw new Exception("Unknow type");
        }


        private string PrepareFindQuery()
        {
            return controlBinder.GetFindableString();
        }

        #region Virtual Function

        protected virtual void OnInitializeData()
        {
        }

        protected virtual void OnBindData()
        {
        }

        protected virtual void OnExtraBind()
        {
        }

        protected virtual void OnDisableControlsForNew()
        {
        }

        protected virtual void OnDisableControlsForEdit()
        {
        }

        protected virtual void OnValidateControl()
        {
        }

        protected virtual IRadarParameters GetKeyFromDocument()
        {
            return null;
        }

        public virtual bool OnDuringSave()
        {
            return true;
        }

        public virtual bool OnDuringDelete()
        {
            return true;
        }

        protected virtual void OnAddPreferenceButton(PreferenceForm prefForm)
        {
        }

        #endregion

        #region virtual override function

        protected virtual bool OnNewButton()
        {
            if (OnBeforeAddNew())
            {
                IsNew = true;
                bool oK = dbManager.AddNew();
                if (oK)
                    OnPrepareAuxData();

                return oK;
            }

            return false;
        }

        protected virtual bool OnSaveButton()
        {
            //IsNew = false;
            return dbManager.Save();
        }

        protected virtual bool OnUndoButton()
        {
            if (OnBeforeUndo())
            {
                IsNew = false;
                dbManager.Undo();
                OnPrepareAuxData();
                return true;
            }
            return false;
        }

        protected virtual void OnEditButton()
        {
            IsNew = false;
            dbManager.Edit();
            OnPrepareAuxData();
        }

        protected virtual bool OnBeforeClosing()
        {
            if (dbManager != null && dbManager.Status == DBMode.Edit)
            {
                if (MessageBox.Show
                    (
                        Properties.Resources.Exit_Close,
                        Properties.Resources.Warning,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    ) == DialogResult.Yes)
                {
                    dbManager.Undo();
                    return true;
                }
                else return false;
            }
            else return true;
        }

        protected virtual void OnPrepareAuxData()
        {
        }

        protected virtual bool OnBeforeAddNew()
        {
            return true;
        }

        protected virtual bool OnAfterAddNew()
        {
            return true;
        }

        protected virtual void FocusOnNew()
        {
        }

        protected virtual bool OnBeforeSave()
        {
            return true;
        }

        protected virtual bool OnAfterSave()
        {
            return true;
        }

        protected virtual bool OnBeforeDelete()
        {
            return dbManager != null && dbManager.SilentMode
                    ? true
                    : MessageBox.Show(
                                    Properties.Resources.Msg_Delete,
                                    Properties.Resources.Warning,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        protected virtual bool OnAfterDelete()
        {
            return true;
        }

        protected virtual bool OnBeforeUndo()
        {
            if (dbManager.isChanged)
                return MessageBox.Show
                    (
                    Properties.Resources.Exit_Undo,
                    Properties.Resources.Warning,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    ) == DialogResult.Yes;
            else return true;
        }

        #endregion

        #region Move between Records & Update

        public bool ToolbarEvent(ToolbarEventKind eventKind)
        {
            bool isOK = true;
            switch (eventKind)
            {
                case ToolbarEventKind.New:
                    OnNewEvent();
                    break;

                case ToolbarEventKind.Save:
                    OnSaveEvent();
                    break;

                case ToolbarEventKind.Edit:
                    OnEditEvent();
                    break;


                case ToolbarEventKind.Undo:
                    OnUndoEvent();
                    break;

                case ToolbarEventKind.Exit:
                    OnExitEvent();
                    break;
            }
            return isOK;
        }

        private void OnNewEvent()
        {
            if (OnNewButton())
            {
                IsNew = true;
                OnAfterAddNew();
            }
            ManageToolbarEvents();
            FocusOnNew();
        }

        private bool OnEditEvent()
        {
            IsNew = false;
            if (DocumentMode == DBMode.Find)
            {
                dbManager.LastKey = GetKeyFromDocument();
            }
            OnEditButton();

            ManageToolbarEvents();
            return true;
        }

        private void OnSaveEvent()
        {
            if (!this.Validate())
                return;

            OnValidateControl();

            if (!OnBeforeSave())
                return;
            OnValidateControl();

            dbManager.StartTransaction();

            dbManager.ValidateControl();
            if (!OnDuringSave() || !OnSaveButton())
            {
                dbManager.Rollback();
                return;
            }
            dbManager.Commit();
            dbManager.UnlockRecordAndFind();

            OnAfterSave();
            dbManager.Refresh();

            OnPrepareAuxData();

            ManageToolbarEvents();
            IsNew = false;
            if (this.Modal)
                this.Close();
        }

        private void OnUndoEvent()
        {
            OnValidateControl();

            //dbManager.ValidateControl();
            if (!OnUndoButton()) return;
            IsNew = false;
            ManageToolbarEvents();
            if (this.Modal)
                this.Close();
        }

        private void OnExitEvent()
        {
            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);

            this.Close();
        }

        private void OnPreferenceEvent()
        {
            Preferences.PreferenceForm pf = new Preferences.PreferenceForm();
            OnAddPreferenceButton(pf);
            AddonAddPreferenceButton(pf);
            OpenDocument.Show(pf);
        }

        private void toolbar_Click(object sender, EventArgs e)
        {
            //if (sender == btnPrint)
            //    OnNewEvent();
            //else if (sender == btnSave)
            //    OnSaveEvent();
            //else if (sender == btnUndo)
            //    OnUndoEvent();
            //else if (sender == btnExit)
            //    OnExitEvent();
        }

        private void ManageToolbarEvents()
        {
            ManageOperationButton();
            if (controlBinder != null)
            {
                controlBinder.Enable(dbManager.Status == DBMode.Edit);
                controlBinder.SetFindable(dbManager.Status == DBMode.Find);
            }

            if (dbManager.Status == DBMode.Edit)
            {
                if (IsNew)
                {
                    OnDisableControlsForNew();
                    AddonDisableControlsForNew();
                }
                else
                {
                    OnDisableControlsForEdit();
                    AddonDisableControlsForEdit();
                }

                if (controlBinder != null)
                    controlBinder.SetFocus();
            }
        }
        #endregion

        #region Enable Disable OperationButton

        public void ManageOperationButton()
        {
            try
            {
                if (dbManager != null)
                {
                    OnEnableToolbarButtons(metroToolbar);
                    metroToolbar.Status = ToolbarStateConvert(dbManager);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private MetroToolbarState ToolbarStateConvert(Data.DBManager dbManager)
        {
            switch (dbManager.Status)
            {
                case DBMode.Browse:
                    return dbManager.Count > 0
                        ? MetroToolbarState.Found
                        : MetroToolbarState.Browse;
                case DBMode.Edit:
                    return MetroToolbarState.Edit;
                case DBMode.Find:
                    return MetroToolbarState.Find;
            }
            return MetroToolbarState.Browse;
        }

        #endregion

        #region MessageBox Overload

        public DialogResult MyMessageBox(string text)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, ""));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MessageBox.Show(text);
        }

        public DialogResult MyMessageBox(string text, string caption)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, caption));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MessageBox.Show(text, caption);
        }

        public DialogResult MyMessageBox(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, caption));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public DialogResult MyMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, caption));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MessageBox.Show(text, caption, buttons, icon);
        }

        #endregion

        //private void metroToolbar_ButtonClick(object sender, MetroToolbarButtonType e)
        //{
        //    switch(e)
        //    {
        //        case MetroToolbarButtonType.New:
        //            OnNewEvent();
        //            break;
        //        case MetroToolbarButtonType.Save:
        //            OnSaveEvent();
        //            break;
        //        case MetroToolbarButtonType.Edit:
        //            OnEditEvent();
        //            break;
        //        case MetroToolbarButtonType.Undo:
        //            OnUndoEvent();
        //            break;
        //        case MetroToolbarButtonType.Exit:
        //            OnExitEvent();
        //            break;
        //        case MetroToolbarButtonType.Preference:
        //            OnPreferenceEvent();
        //            break;
        //        case MetroToolbarButtonType.Print:
        //            //OnPrintEvent();
        //            break;
        //        case MetroToolbarButtonType.Preview:
        //            //OnPreviewEvent();
        //            break;
        //    }
        //}


        private void metroToolbar_ToolStripMenuClick(object sender, string tag)
        {
            OnToolStripMenuClick(tag);
            OnAddOnToolStripMenuClick(tag);
        }

        protected virtual void OnAddOnToolStripMenuClick(string tag)
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnToolStripMenuClick(tag);
        }

        private void OnToolStripMenuClick(object tag)
        {
            throw new NotImplementedException();
        }

        protected virtual void AddonDisableControlsForEdit()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnDisableControlsForEdit();
        }

        protected virtual void AddonDisableControlsForNew()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnDisableControlsForNew();
        }

        protected virtual void AddonAddPreferenceButton(PreferenceForm prefForm)
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnAddPreferenceButton(prefForm);
        }

        protected PrinterForm OnPrintEvent(PrintType printType)
        {
            PrinterForm pf = new PrinterForm(Name, PrinterForm.PrintMode.Print, printType);
            return pf;
        }

        private void metroToolbar_ItemClicked(object sender, MetroToolbarButtonType e)
        {
            switch (e)
            {
                case MetroToolbarButtonType.New:
                    OnNewEvent();
                    break;
                case MetroToolbarButtonType.Edit:
                    OnEditEvent();
                    break;
                case MetroToolbarButtonType.Save:
                    OnSaveEvent();
                    break;
                case MetroToolbarButtonType.Undo:
                    OnUndoEvent();
                    break;
                case MetroToolbarButtonType.Exit:
                    OnExitEvent();
                    break;
                case MetroToolbarButtonType.Preference:
                    OnPreferenceEvent();
                    break;
                case MetroToolbarButtonType.Print:
                    throw new NotImplementedException("Print Error: Missing something");
                case MetroToolbarButtonType.Preview:
                    throw new NotImplementedException("Preview Error: Missing something");
                case MetroToolbarButtonType.Custom:
                    OnCustomToolbarButtonClick(sender as IMetroToolBarButton);
                    break;
            }
        }

        protected virtual void OnCustomToolbarButtonClick(IMetroToolBarButton btn)
        {
        }

        private void metroToolbar_ToolStripMenuClick_1(object sender, string tag)
        {
            OnToolStripMenuClick(tag);
            OnAddOnToolStripMenuClick(tag);
        }
    }

}