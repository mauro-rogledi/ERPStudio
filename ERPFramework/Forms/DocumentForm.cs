#region Using directives

using ERPFramework.Controls;
using ERPFramework.CounterManager;
using ERPFramework.Data;
using ERPFramework.ModulesHelper;
using ERPFramework.Preferences;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Extender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ERPFramework.Forms
{
    public partial class DocumentForm : MetroUserControl, IAddon, IDocument, IPropagate
    {
        #region Private

        protected const string PRINT = "Print";
        protected const string PREVIEW = "Preview";

        protected Control keyControl;
        protected Data.DBManager dbManager;
        internal ControlBinder controlBinder;
        internal ControlFinder controlFinder;
        private readonly List<MessageData> messageList;
        private bool isShown = true;

        #endregion

        #region Properties
        [Browsable(false)]
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
        [Browsable(false)]
        public List<MessageData> ErrorMessageList { get { return messageList; } }
        public enum NullValue { SetNull, NotSet }
        [Browsable(false)]
        public bool IsNew { get; private set; }
        [Browsable(false)]
        public Control KeyControl { get { return keyControl; } }
        [Browsable(false)]
        public DBMode DocumentMode { get { return (dbManager != null) ? dbManager.Status : DBMode.Browse; } }

        [Browsable(false)]
        public SqlProxyTransaction Transaction
        {
            get
            {
                return dbManager == null ? null : dbManager.SqlProxyTransaction;
            }
        }

        [Browsable(false)]
        public SqlProxyConnection Connection
        {
            get
            {
                return dbManager == null ? null : dbManager.DBConnection;
            }
        }

        [Browsable(false)]
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

        public delegate void OpenFormEventHandler(MetroFramework.Forms.MetroForm frm, bool modal);

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


        #region Layout Methods
        protected bool SetWaitCursor
        {
            set
            {
                if (value)
                {
                    UseWaitCursor = true;
                    Application.DoEvents();
                    SuspendLayout();
                }
                else
                {
                    UseWaitCursor = false;
                    ResumeLayout(true);
                }
            }
        }
        #endregion

        public void Close()
        {
            if (dbManager != null)
                dbManager.Dispose();
        }

        public DocumentForm()
        {
            InitializeComponent();
        }

        public DocumentForm(string formname)
        {
            Name = formname;
            InitializeComponent();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            controlBinder = new ControlBinder();
            controlFinder = new ControlFinder(this);
            messageList = new List<MessageData>();
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
                foreach (Addon addon in AddonList)
                {
                    foreach (MetroProvaTile mt in aopAddons.Buttons)
                    {
                        var enabled = addon.OnEnableAddonButton(mt, dbManager);
                        if (enabled != null)
                            mt.Enabled = (bool)enabled;
                    }
                }
        }

        protected virtual void OnEnableToolbarButtons(ExMetroToolbar toolstrip)
        {
        }

        protected virtual void OnCustomizeToolbar(ExMetroToolbar toolstrip)
        {
        }

        public void AddOnButton(string name, string text, Image img, MetroAddonPanelButton.MetroAddonPanelButtonSize buttonSize)
        {
            aopAddons.AddButton(name, text, img, buttonSize);
            aopAddons.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            AddonInitializeComponent();

            OnInitializeData();
            OnCustomizeToolbar(metroToolbar);

            ManageOperationButton();

            OnBindData();

            if (controlBinder != null)
                controlBinder.Enable(false);

            OnAddSplitMenuButton();

            if (PostLoaded != null)
            {
                PostLoaded(this, dbManager);
                ManageOperationButton();
            }
        }

        protected virtual void OnReady()
        {
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            AddonLoad();
            AddOnInitializeData();
            AddonBindData();
            AddonCustomizeToolbar(metroToolbar);
            AddonAddSplitMenuButton();

            AddonAddButton();
            DoEnableAddonButton(dbManager);
            metroToolbar.ButtonAddonVisible = AddonList.Count > 0 || AddonWidth > 0;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (isShown)
                OnReady();

            isShown = false;
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
            if (c.HasRadar)
                c.OpenDocument();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.New))
                        OnNewEvent();
                    break;

                case Keys.F3:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Edit))
                        OnEditEvent();
                    break;

                case Keys.F4:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Delete))
                        OnDeleteEvent();
                    break;

                case Keys.F8:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Filter))
                        OnFindEvent();
                    break;

                case Keys.F9:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Search))
                        OnSearchEvent();
                    break;

                case Keys.F10:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Save))
                        OnSaveEvent();
                    break;

                case Keys.Escape:
                    if (metroToolbar.GetButtonState(MetroToolbarButtonType.Undo) && DocumentMode == DBMode.Edit)
                        OnUndoEvent();
                    break;

                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
        /// <param name="control">todo: describe control parameter on BindControl</param>
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

        private static string GetProperty(Control control)
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

        protected virtual void OnCustomButtonClick(MetroFramework.Controls.MetroProvaTile button)
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
                foreach (Addon addon in AddonList)
                    addon.InitializerComponent();
        }

        protected virtual void AddonLoad()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnLoad();
        }

        protected virtual void AddOnInitializeData()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnInitializeData(dbManager);
        }

        protected virtual void AddonAddButton()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnAddonButton();
        }

        protected virtual void AddonCustomizeToolbar(MetroToolbar toolstrip)
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnCustomizeToolbar(toolstrip);
        }

        protected virtual void AddonBindData()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnBindData();
        }

        protected virtual void AddonPrepareAuxData()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnPrepareAuxData();
        }

        protected virtual void AddonValidateControl()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnValidateControl();
        }

        protected virtual void AddonAddSplitMenuButton()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnAddSplitMenuButton();
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

        protected virtual void AddonDisableControlsForEdit()
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnDisableControlsForEdit();
        }

        protected virtual void AddonCustomButtonClick(MetroFramework.Controls.MetroProvaTile button)
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnCustomButtonClick(button);
        }

        protected virtual bool AddonOnBeforeAddNew()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnBeforeAddNew();

            return bOK;
        }

        protected virtual bool AddonOnBeforeUndo()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnBeforeUndo();

            return bOK;
        }

        protected virtual bool AddonOnBeforeSave()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnBeforeSave();

            return bOK;
        }

        protected virtual bool AddonOnSaveButton()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnSaveButton();

            return bOK;
        }

        protected virtual bool AddonOnDeleteButton()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnDeleteButton();

            return bOK;
        }

        protected virtual bool AddOnOnAfterSave()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnAfterSave();

            return bOK;
        }

        protected virtual bool AddonOnAfterDelete()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnAfterDelete();

            return bOK;
        }

        protected virtual bool AddonOnBeforeDelete()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnBeforeDelete();

            return bOK;
        }

        protected virtual bool AddonOnBeforeClosing()
        {
            var bOK = true;
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    bOK &= addon.OnBeforeClosing();

            return bOK;
        }

        #endregion

        #region virtual override function

        protected virtual bool OnNewButton()
        {
            if (OnBeforeAddNew() && AddonOnBeforeAddNew())
            {
                IsNew = true;
                var oK = dbManager.AddNew();
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
            var myRadar = rdr as RadarForm;
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
            DrawingControl.SuspendDrawing(this);
            var Found = dbManager.FindRecord(key);
            OnPrepareAuxData();
            AddonPrepareAuxData();
            ManageToolbarEvents();
            DrawingControl.ResumeDrawing(this);

            return Found;
        }

        protected virtual bool OnBeforeClosing()
        {
            if (dbManager != null && dbManager.Status == DBMode.Edit)
            {
                if (MyMessageBox
                    (
                        this,
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

        protected virtual bool OnAfterEdit()
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
                    : MyMessageBox(
                                    this,
                                    Properties.Resources.Msg_Delete,
                                    Properties.Resources.Warning,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        protected virtual bool OnAfterDelete()
        {
            return true;
        }

        protected virtual bool OnPrintDocument(PrintInfo sender, PrinterForm pf)
        {
            return true;
        }

        protected virtual bool OnBeforeUndo()
        {
            return dbManager.isChanged ?
                MyMessageBox
                    (
                    this,
                    Properties.Resources.Exit_Undo,
                    Properties.Resources.Warning,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    ) == DialogResult.Yes
                : true;
        }

        #endregion

        #region Move between Records & Update


        public bool ToolbarEvent(ToolbarEventKind eventKind)
        {
            var isOK = true;
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
            DrawingControl.SuspendDrawing(this);
            IsNew = false;
            if (DocumentMode == DBMode.Find)
                dbManager.LastKey = GetKeyFromDocument();

            OnEditButton();
            OnAfterEdit();

            ManageToolbarEvents();
            DrawingControl.ResumeDrawing(this);
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

            DrawingControl.SuspendDrawing(this);
            dbManager.Commit();
            dbManager.UnlockRecordAndFind();

            OnAfterSave();
            AddOnOnAfterSave();
            dbManager.Refresh();

            OnPrepareAuxData();
            AddonPrepareAuxData();

            ManageToolbarEvents();
            IsNew = false;
            DrawingControl.ResumeDrawing(this);
        }

        private void OnDeleteEvent()
        {
            dbManager.StartTransaction();
            if (!AddonOnDeleteButton() || !OnDeleteButton())
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
            DrawingControl.SuspendDrawing(this);
            OnValidateControl();

            //dbManager.ValidateControl();
            if (!OnUndoButton())
            {
                DrawingControl.ResumeDrawing(this);
                return;
            }
            IsNew = false;
            ManageToolbarEvents();
            DrawingControl.ResumeDrawing(this);
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

        protected PrinterForm OnPreviewEvent(PrintType printType, string title = "")
        {
            var pf = new PrinterForm(Name, PrinterForm.PrintMode.Preview, printType)
            {
                Title = string.Concat(Properties.Resources.Preview, " ", title)
            };
            OpenDocument.Show(pf);
            return pf;
        }

        protected PrinterForm OnPrintEvent(PrintType printType)
        {
            return new PrinterForm(Name, PrinterForm.PrintMode.Print, printType);
        }

        private void OnPreferenceEvent()
        {
            var pf = new Preferences.PreferenceForm();
            OnAddPreferenceButton(pf);
            AddonAddPreferenceButton(pf);
            OpenDocument.Show(pf);
        }

        private void OnExitEvent()
        {
            Close();

            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);
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
                    DoEnableAddonButton(dbManager);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private static MetroToolbarState ToolbarStateConvert(Data.DBManager dbManager)
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


        #region Print

        protected virtual void OnAddSplitMenuButton()
        {
        }

        protected virtual void OnToolStripMenuClick(string tag) { }

        protected virtual void OnAddOnToolStripMenuClick(string tag)
        {
            if (AddonList != null)
                foreach (Addon addon in AddonList)
                    addon.OnToolStripMenuClick(tag);
        }

        public void AddSplitToolbarButton(MetroToolbarButtonType btnType, string text, string tag, Image image)
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
                return MetroMessageBox.Show(this, text);
        }

        public DialogResult MyMessageBox(string text, string caption)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, caption));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MetroMessageBox.Show(this, text, caption);
        }

        public DialogResult MyMessageBox(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, caption));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MetroMessageBox.Show(owner, text, caption, buttons, icon);
        }

        public DialogResult MyMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (SilentMode)
            {
                messageList.Add(new MessageData(this.Name, keyControl.Text, text, caption));
                return System.Windows.Forms.DialogResult.OK;
            }
            else
                return MetroMessageBox.Show(this, text, caption, buttons, icon);
        }

        #endregion

        private void metroToolbar_ToolStripMenuClick(object sender, string tag)
        {
            OnToolStripMenuClick(tag);
            OnAddOnToolStripMenuClick(tag);
        }

        private void aopAddons_ButtonClick(object sender, EventArgs e)
        {
            OnCustomButtonClick((MetroProvaTile)sender);
            AddonCustomButtonClick((MetroFramework.Controls.MetroProvaTile)sender);
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
                case MetroToolbarButtonType.Delete:
                    OnDeleteEvent();
                    break;
                case MetroToolbarButtonType.Search:
                    OnSearchEvent();
                    break;
                case MetroToolbarButtonType.Filter:
                    OnFindEvent();
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
                case MetroToolbarButtonType.AddOn:
                    aopAddons.ChangeStatus(AddonWidth);
                    break;
                case MetroToolbarButtonType.Custom:
                    OnCustomToolbarButtonClick(sender as IMetroToolBarButton);
                    break;
            }
        }

        protected virtual void OnCustomToolbarButtonClick(IMetroToolBarButton btn)
        {
        }
    }

    internal class RadarDocumentParam : RadarParameters
    {
        public RadarDocumentParam(Table rdrTable, string code)
        {
            Params = new Dictionary<string, object>();
            for (int t = 0; t < rdrTable.PrimaryKey.Length; t++)
                Params.Add(nameof(code), rdrTable.PrimaryKey[t].Name);
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

    class DrawingControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
    }
}