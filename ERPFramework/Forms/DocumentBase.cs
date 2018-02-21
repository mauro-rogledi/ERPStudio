#region Using directives

using ERPFramework.Controls;
using ERPFramework.CounterManager;
using ERPFramework.Data;
using ERPFramework.Preferences;
using MetroFramework.Controls;
using MetroFramework.Extender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ERPFramework.Forms
{
    public partial class DocumentBase : MetroUserControl, IAddon, IDocument
    {
        #region Private

        protected string ConnectionString { get; private set; }

        protected Control keyControl = null;
        protected Data.DBManager dbManager = null;
        internal ControlBinder controlBinder;
        internal ControlFinder controlFinder = null;
        private List<MessageData> messageList = null;
        private bool isShown = true;

        #endregion

        #region Properties
        public List<Addon> AddonList { get; } = new List<Addon>();

        [Localizable(true)]
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public string Title { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [Description("The width of AddOn panel when opened")]
        [DefaultValue(0)]
        public int AddonWidth { get; set; } = 0;

        [Browsable(false)]
        public ProviderType providerType { get; private set; }
        public List<MessageData> ErrorMessageList { get { return messageList; } }
        public enum NullValue { SetNull, NotSet }
        public bool IsNew { get; private set; }
        public Control KeyControl { get { return keyControl; } }
        public DBMode DocumentMode { get { return (dbManager != null) ? dbManager.Status : DBMode.Browse; } }
        public bool IsModal { get; set; }

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

        #region Events & Delegate

        public delegate void PostFormLoadEventHandler(object sender, Data.DBManager dbManager);

        public event PostFormLoadEventHandler PostLoaded;

        public delegate bool ToolbarButtonEventHandler(ToolStripButton sender);

        public delegate bool ToolbarSplitButtonEventHandler(ToolStripSplitButton sender);

        //public event ToolbarSplitButtonEventHandler OnEnableToolbarSplitButtons;

        public delegate void OpenFormEventHandler(MetroFramework.Forms.MetroForm frm, bool modal);

        public event OpenFormEventHandler OpenForm;

        public event EventHandler Exit;

        #endregion

        #region ToolbarEvent

        public enum ToolbarEventKind
        {
            New, Edit, Delete, Save
        }

        public bool AddNew()
        {
            return ToolbarEvent(DocumentForm.ToolbarEventKind.New);
        }

        public bool Edit()
        {
            return ToolbarEvent(DocumentForm.ToolbarEventKind.Edit);
        }

        public bool Save()
        {
            return ToolbarEvent(DocumentForm.ToolbarEventKind.Save);
        }

        public bool Delete()
        {
            return ToolbarEvent(DocumentForm.ToolbarEventKind.Delete);
        }

        #endregion


        public void OnClosing(object sender, FormClosingEventArgs e)
        { }

        public void Close()
        { }

        public DocumentBase()
        {
            InitializeComponent();
        }

        public DocumentBase(string formname)
        {
            Name = formname;
            ConnectionString = GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString;
            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            controlBinder = new ControlBinder();
            controlFinder = new ControlFinder(this);
            messageList = new List<MessageData>();
            InitializeComponent();
            aopAddons.Visible = false;
            aopAddons.StyleManager = GlobalInfo.StyleManager;
        }

        public T FindControl<T>(string ctrlName)
        {
            return controlFinder.Find<T>(ctrlName);
        }

        private void DoEnableAddonButton(DBManager dbManager)
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                {
                    foreach (MetroTile mt in aopAddons.Buttons)
                    {
                        bool? enabled = scr.OnEnableAddonButton(mt, dbManager);
                        if (enabled != null)
                            mt.Enabled = (bool)enabled;
                    }
                }
        }

        protected virtual bool OnEnableToolbarButtons(MetroLink button)
        {
            return true;
        }

        protected virtual void OnCustomizeToolbar(MetroToolbar toolstrip)
        {
        }

        public void AddOnButton(string name, string text, Image img, MetroAddonPanelButton.MetroAddonPanelButtonSize buttonSize)
        {
            aopAddons.AddButton(name, text, img, buttonSize);
            aopAddons.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            AddonInitializeComponent();

            AddonLoad();
            OnAttachData();
            AddonAttachData();
            OnCustomizeToolbar(metroToolbar);
            AddonCustomizeToolbar(metroToolbar);

            ManageOperationButton();

            OnBindData();
            AddonBindData();

            if (controlBinder != null)
                controlBinder.Enable(false);

            OnAddSplitMenuButton();
            AddonAddSplitMenuButton();

            if (PostLoaded != null)
            {
                PostLoaded(this, dbManager);
                ManageOperationButton();
            }


            base.OnLoad(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (isShown)
            {
                AddonAddButton();
                DoEnableAddonButton(dbManager);
                bntAddon.Visible = AddonList.Count > 0 && AddonWidth > 0;
            }
            isShown = false;
        }

        private void formDB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }

        private void formDB_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F4:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.New))
                        OnNewEvent();
                    break;

                case Keys.F3:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.Edit))
                        OnEditEvent();
                    break;

                case Keys.F5:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.Delete))
                        OnDeleteEvent();
                    break;

                case Keys.F8:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.Find))
                        OnFindEvent();
                    break;

                case Keys.F9:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.Search))
                        OnSearchEvent();
                    break;

                case Keys.F10:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.Save))
                        OnSaveEvent();
                    break;

                case Keys.Escape:
                    if (metroToolbar.GetButtonState(MetroToolbarButton.Undo))
                        OnUndoEvent();
                    break;
            }
        }

        private void formDB_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !(OnBeforeClosing() && AddonOnBeforeClosing());
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

        public void FindableControl(Control control)
        {
            controlBinder.SetFindable(control, Findable.YES);
        }

        public void FindableControl(Control control, Findable findable)
        {
            controlBinder.SetFindable(control, findable);
        }

        /// <summary>
        /// Only For Enable/Disable Controls
        /// </summary>
        public Binding BindControl(Control control)
        {
            controlBinder.Bind(control);
            if (control is ExtendedDataGridView)
            {
                ((ExtendedDataGridView)control).DocumentForm = this;
                ((ExtendedDataGridView)control).LoadSetting();
            }

            return null;
        }

        //public void ShowDialog(bool visible)
        //{
        //    if (!visible)
        //        this.WindowState = FormWindowState.Minimized;
        //    this.ShowInTaskbar = visible;
        //    this.Visible = visible;
        //    base.Show();
        //    Application.DoEvents();
        //}

        public void BindColumn(DataGridViewColumn gridcol, IColumn column)
        {
            gridcol.Name = column.Name;
            gridcol.DataPropertyName = column.Name;
            gridcol.Tag = column.DefaultValue;
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
            else
                throw new Exception("Unknow type");
        }


        private string PrepareFindQuery()
        {
            return controlBinder.GetFindableString();
        }

        #region Virtual Function

        protected virtual void OnAttachData()
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

        protected virtual void OnCustomButtonClick(MetroTile button)
        {
        }

        protected virtual IRadarParameters GetKeyFromDocument()
        {
            return null;
        }

        protected virtual void OnAddPreferenceButton(PreferenceForm prefForm)
        {
        }

        public virtual bool OnDuringSave()
        {
            return true;
        }

        public virtual bool OnDuringDelete()
        {
            return true;
        }

        #endregion

        #region Addon Method

        protected virtual void AddonInitializeComponent()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.InitializerComponent();
        }

        protected virtual void AddonLoad()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnLoad();
        }

        protected virtual void AddonAttachData()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnAttachData(dbManager);
        }

        protected virtual void AddonAddButton()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnAddonButton();
        }

        protected virtual void AddonCustomizeToolbar(MetroToolbar toolstrip)
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnCustomizeToolbar(toolstrip);
        }

        protected virtual void AddonBindData()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnBindData();
        }

        protected virtual void AddonPrepareAuxData()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnPrepareAuxData();
        }

        protected virtual void AddonValidateControl()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnValidateControl();
        }

        protected virtual void AddonAddSplitMenuButton()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnAddSplitMenuButton();
        }

        protected virtual void AddonDisableControlsForNew()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnDisableControlsForNew();
        }

        protected virtual void AddonAddPreferenceButton(PreferenceForm prefForm)
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnAddPreferenceButton(prefForm);
        }

        protected virtual void AddonDisableControlsForEdit()
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnDisableControlsForEdit();
        }

        protected virtual void AddonCustomButtonClick(MetroTile button)
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnCustomButtonClick(button);
        }

        protected virtual void AddonPrintDocument(PrintInfo sender, printerForm pf)
        {
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    scr.OnPrintDocument(sender, pf);
        }

        protected virtual bool AddonOnBeforeAddNew()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnBeforeAddNew();

            return bOK;
        }

        protected virtual bool AddonOnBeforeUndo()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnBeforeUndo();

            return bOK;
        }

        protected virtual bool AddonOnBeforeSave()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnBeforeSave();

            return bOK;
        }

        protected virtual bool AddonOnSaveButton()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnSaveButton();

            return bOK;
        }

        protected virtual bool AddonOnDeleteButton()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnDeleteButton();

            return bOK;
        }

        

        protected virtual bool AddOnOnAfterSave()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnAfterSave();

            return bOK;
        }

        protected virtual bool AddonOnAfterDelete()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnAfterDelete();

            return bOK;
        }

        protected virtual bool AddonOnBeforeDelete()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnBeforeDelete();

            return bOK;
        }

        protected virtual bool AddonOnBeforeClosing()
        {
            bool bOK = true;
            if (AddonList != null)
                foreach (Addon scr in AddonList)
                    bOK &= scr.OnBeforeClosing();

            return bOK;
        }

        #endregion

        #region virtual override function

        protected virtual bool OnNewButton()
        {
            if (OnBeforeAddNew() && AddonOnBeforeAddNew())
            {
                IsNew = true;
                bool oK = dbManager.AddNew();
                if (oK)
                {
                    OnPrepareAuxData();
                    AddonPrepareAuxData();
                }

                return oK;
            }

            return false;
        }

        protected virtual void OnEditButton()
        {
            IsNew = false;
            dbManager.Edit();
            OnPrepareAuxData();
            AddonPrepareAuxData();
        }

        protected virtual bool OnSaveButton()
        {
            //IsNew = false;
            return dbManager.Save();
        }

        protected virtual bool OnDeleteButton()
        {
            if (OnBeforeDelete() && AddonOnBeforeDelete())
            {
                IsNew = false;
                return dbManager.Delete();
            }

            return false;
        }

        protected virtual bool OnUndoButton()
        {
            if (OnBeforeUndo() && AddonOnBeforeUndo())
            {
                IsNew = false;
                dbManager.Undo();
                OnPrepareAuxData();
                AddonPrepareAuxData();
                return true;
            }
            return false;
        }

        protected virtual void OnSearchButton()
        {
            var rdr = Activator.CreateInstance(dbManager.RadarDocument, dbManager.RadarParams);
            Debug.Assert(rdr is IRadarForm);
            RadarForm myRadar = rdr as RadarForm;
            myRadar.FindQuery = string.Empty;
            ERPFramework.GlobalInfo.StyleManager.Clone(myRadar);

            if (dbManager.Status == DBMode.Find)
                myRadar.FindQuery = PrepareFindQuery();

            myRadar.RadarFormRowSelected += new RadarForm.RadarFormRowSelectedEventHandler(myRadar_RadarFormRowSelected);
            myRadar.ShowDialog(GlobalInfo.MainForm);
            myRadar.RadarFormRowSelected -= new RadarForm.RadarFormRowSelectedEventHandler(myRadar_RadarFormRowSelected);
        }

        protected virtual void OnFindButton()
        {
            dbManager.Find();
            ManageToolbarEvents();
        }

        private void myRadar_RadarFormRowSelected(object sender, RadarFormRowSelectedArgs pe)
        {
            FindRecord(pe.parameters);
            if (dbManager.Status == DBMode.Find)
                OnUndoEvent();
        }

        public bool FindRecord(IRadarParameters key)
        {
            bool Found = dbManager.FindRecord(key);
            OnPrepareAuxData();
            AddonPrepareAuxData();
            ManageToolbarEvents();

            return Found;
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

        protected virtual bool OnPrintDocument(PrintInfo sender, printerForm pf)
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

        private void tbsMove_Click(object sender, ToolBarButtonClickEventArgs e)
        {
            if (dbManager == null) return;

            ToolBarButton btn = e.Button;

            ManageToolbarEvents();
        }

        public bool ToolbarEvent(ToolbarEventKind eventKind)
        {
            bool isOK = true;
            switch (eventKind)
            {
                case ToolbarEventKind.New:
                    OnNewEvent();
                    break;

                case ToolbarEventKind.Edit:
                    isOK = OnEditEvent();
                    break;

                case ToolbarEventKind.Delete:
                    OnDeleteEvent();
                    break;

                case ToolbarEventKind.Save:
                    OnSaveEvent();
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
            if (!dbManager.isLocked)
                OnEditButton();
            else
                return false;

            ManageToolbarEvents();
            return true;
        }

        private void OnSaveEvent()
        {
            if (!this.Validate())
                return;

            OnValidateControl();
            AddonValidateControl();

            if (!OnBeforeSave() || !AddonOnBeforeSave())
                return;
            OnValidateControl();
            AddonValidateControl();

            dbManager.StartTransaction();

            dbManager.ValidateControl();
            if (!AddonOnSaveButton() || !OnDuringSave() || !OnSaveButton())
            {
                dbManager.Rollback();
                return;
            }
            dbManager.Commit();
            dbManager.UnlockRecordAndFind();

            OnAfterSave();
            AddOnOnAfterSave();
            dbManager.Refresh();

            OnPrepareAuxData();
            AddonPrepareAuxData();

            ManageToolbarEvents();
            IsNew = false;
            if (this.IsModal)
                this.Close();
        }

        private void OnDeleteEvent()
        {
            dbManager.StartTransaction();
            if (!AddonOnDeleteButton() || !OnDuringSave() || !OnDeleteButton())
            {
                dbManager.Rollback();
                return;
            }
            dbManager.Commit();
            OnAfterDelete();
            AddonOnAfterDelete();
            ManageToolbarEvents();
        }

        private void OnUndoEvent()
        {
            OnValidateControl();

            //dbManager.ValidateControl();
            if (!OnUndoButton()) return;
            IsNew = false;
            ManageToolbarEvents();
            if (this.IsModal)
                this.Close();
        }

        private void OnFindEvent()
        {
            OnFindButton();
        }

        private void OnSearchEvent()
        {
            OnSearchButton();

            //ManageToolbarEvents();
        }

        private void OnPreviewEvent(PrintInfo sender)
        {
            printerForm pf = new printerForm(Name, printerForm.PrintMode.Preview, sender.PrintType);
            if (OnPrintDocument(sender, pf))
                OpenForm(pf, GlobalInfo.globalPref.ModalWindow);
        }

        private void OnPrintEvent(PrintInfo sender)
        {
            printerForm pf = new printerForm(Name, printerForm.PrintMode.Print, sender.PrintType);
            OnPrintDocument(sender, pf);
        }

        private void OnPreferenceEvent()
        {
            Preferences.PreferenceForm pf = new Preferences.PreferenceForm();
            OnAddPreferenceButton(pf);
            AddonAddPreferenceButton(pf);
            OpenForm(pf, true);
        }

        private void OnExitEvent()
        {
            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);
        }

        public void OpenNewForm(MetroFramework.Forms.MetroForm df, bool modal)
        {
            if (OpenForm != null)
                OpenForm(df, modal);
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

        private void tbnPrint_ButtonClick(object sender, EventArgs e)
        {
            //OnPrintEvent(tbnPrint.Tag != null ? (PrintInfo)tbnPrint.Tag : null);
        }

        private void tbnPreview_ButtonClick(object sender, EventArgs e)
        {
            //OnPreviewEvent(tbnPreview.Tag != null ? (PrintInfo)tbnPreview.Tag : null);
        }

        private void tbnPref_Click(object sender, EventArgs e)
        {
            OnPreferenceEvent();
        }

        #endregion

        #region Enable Disable OperationButton

        public void ManageOperationButton()
        {
            try
            {
                if (dbManager != null)
                {
                    metroToolbar.Status = ToolbarStateConvert(dbManager);
                    DoEnableAddonButton(dbManager);
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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        #endregion

        #region Print

        protected virtual void OnAddSplitMenuButton()
        {
        }

        protected void AddSplitPreviewButton(string text, string reportName, PrintType printType, Image image)
        {
            //if (tbnPreview.Tag == null)
            //    tbnPreview.Tag = new PrintInfo(reportName, printType);

            //ToolStripMenuItem mnuPrev = new ToolStripMenuItem(text);
            //mnuPrev.Click += new EventHandler(mnu_PreviewClick);
            //mnuPrev.Tag = new PrintInfo(reportName, printType);
            //mnuPrev.Image = image;
            //tbnPreview.DropDownItems.Add(mnuPrev);
        }

        protected void AddSplitPrintButton(string text, string reportName, PrintType printType, Image image)
        {
            //if (tbnPrint.Tag == null)
            //    tbnPrint.Tag = new PrintInfo(reportName, printType);

            //ToolStripMenuItem mnuPrint = new ToolStripMenuItem(text);
            //mnuPrint.Click += new EventHandler(mnu_PrintClick);
            //mnuPrint.Tag = new PrintInfo(reportName, printType);
            //mnuPrint.Image = image;
            //tbnPrint.DropDownItems.Add(mnuPrint);
        }

        private void mnu_PreviewClick(object sender, EventArgs e)
        {
            ToolStripMenuItem mnu = (ToolStripMenuItem)sender;
            OnPreviewEvent(mnu.Tag != null ? (PrintInfo)mnu.Tag : null);
        }

        private void mnu_PrintClick(object sender, EventArgs e)
        {
            ToolStripMenuItem mnu = (ToolStripMenuItem)sender;
            OnPrintEvent(mnu.Tag != null ? (PrintInfo)mnu.Tag : null);
        }

        #endregion

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void metroToolbar_ButtonClick(object sender, MetroToolbarButton e)
        {
            switch(e)
            {
                case MetroToolbarButton.New:
                    OnNewEvent();
                    break;
                case MetroToolbarButton.Edit:
                    OnEditEvent();
                    break;
                case MetroToolbarButton.Save:
                    OnSaveEvent();
                    break;
                case MetroToolbarButton.Delete:
                    OnDeleteEvent();
                    break;
                case MetroToolbarButton.Search:
                    OnSearchEvent();
                    break;
                case MetroToolbarButton.Find:
                    OnFindEvent();
                    break;
                case MetroToolbarButton.Undo:
                    OnUndoEvent();
                    break;
                case MetroToolbarButton.Exit:
                    OnExitEvent();
                    break;
                case MetroToolbarButton.Preference:
                    OnPreferenceEvent();
                    break;
                case MetroToolbarButton.Print:
                    //OnPrintEvent();
                    break;
                case MetroToolbarButton.Preview:
                    //OnPreviewEvent();
                    break;
            }
        }

        private void bntAddon_Click(object sender, EventArgs e)
        {
            aopAddons.ChangeStatus(AddonWidth);
        }

        protected override void OnResize(EventArgs e)
        {
            bntAddon.Location = new Point(Size.Width - 32, bntAddon.Top);
        }

        private void aopAddons_ButtonClick(object sender, EventArgs e)
        {
            OnCustomButtonClick((MetroTile)sender);
            AddonCustomButtonClick((MetroTile)sender);
        }
    }

    internal class RadarDocumentParam : RadarParameters
    {
        public RadarDocumentParam(Table rdrTable, string code)
        {
            Params = new List<object>();
            for (int t = 0; t < rdrTable.PrimaryKey.Length; t++)
                Params.Add(rdrTable.PrimaryKey[t].Name);
        }
    }

    internal static class FindableCleaner
    {
        private static void Clean(this TextBox txtbox)
        {
            txtbox.Text = string.Empty;
        }
    }

    public class MessageData
    {
        public string document { get; private set; }

        public string text { get; private set; }

        public string caption { get; private set; }

        public string key { get; private set; }

        public MessageData(string document, string key, string text, string caption)
        {
            this.document = document;
            this.key = key;
            this.text = text;
            this.caption = caption;
        }
    }
}