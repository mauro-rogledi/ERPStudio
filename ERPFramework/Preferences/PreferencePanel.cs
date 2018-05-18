using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERPFramework.Data;
using ERPFramework.Forms;
using MetroFramework.Extender;

namespace ERPFramework.Preferences
{
    public partial class PreferencePanel : MetroFramework.Controls.MetroUserControl
    {
        private string ApplicationName = "";
        private BindingSource masterBind;
        internal ControlBinder controlBinder;

        [Browsable(false)]
        public event EventHandler<PreferencePanel> DocumentClose;

        [Browsable(false)]
        protected DBMode DocumentMode { get; private set; }
        [Browsable(false)]
        public bool CanRemove { get; set; }


        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [Localizable(true)]
        public string ButtonText { set; get; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public Image ButtonImage { set; get; }

        public MetroLinkChecked ButtonComputer { get { return btnComputer; } }
        public MetroLinkChecked ButtonUser { get { return btnUser; } }

        public MetroLinkChecked ButtonApplication { get { return btnApplication; } }

        public MetroToolbarDropDownButton ButtonList { get { return btnList; } }

        public bool IsNew { private set; get; }
        public PreferencePanel()
        {
            InitializeComponent();
            InitializeAuxComponent();
        }

        public PreferencePanel(string appName)
        {
            ApplicationName = appName;
            InitializeComponent();
            InitializeAuxComponent();
        }
        private void InitializeAuxComponent()
        {
            metroPropertyGrid1.StyleManager = GlobalInfo.StyleManager;
            controlBinder = new ControlBinder();
            masterBind = new BindingSource();
            IsNew = false;
            CanRemove = true;
            DocumentMode = DBMode.Browse;
        }
        public void AttachData(object obj)
        {
            masterBind.DataSource = obj;
            masterBind.CurrencyManager.Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            OnInitializeData();
            ManageOperationButton();

            OnBindData();
            if (controlBinder != null)
                controlBinder.Enable(false);

            base.OnLoad(e);
        }

        private void ManageOperationButton()
        {
            try
            {
                btnNew.Visible = DocumentMode == DBMode.Browse;
                btnEdit.Visible = btnList.Items.Count > 0 && DocumentMode == DBMode.Browse;
                btnDelete.Visible = btnList.Items.Count > 0 && DocumentMode == DBMode.Browse;
                btnUndo.Visible = DocumentMode == DBMode.Edit;
                btnSave.Visible = DocumentMode == DBMode.Edit;
                btnList.Enabled = DocumentMode == DBMode.Browse;
                btnComputer.Enabled = DocumentMode != DBMode.Browse;
                btnUser.Enabled = DocumentMode != DBMode.Browse;
                btnApplication.Enabled = DocumentMode != DBMode.Browse && !ApplicationName.IsEmpty();
                btnApplication.Visible = !ApplicationName.IsEmpty();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        #region Virtual Function

        protected virtual void OnInitializeData()
        {
        }

        protected virtual void OnBindData()
        {
            BindControl(metroPropertyGrid1, "SelectedObject");
        }

        protected virtual void OnDisableControlsForNew()
        {
        }

        protected virtual void OnDisableControlsForEdit()
        {
        }

        protected virtual void OnFindData(MyToolStripItem tsi)
        {
        }

        protected virtual void OnNewData()
        {
            throw new Exception("OnNewData");
        }

        protected virtual bool OnLoadData()
        {
            throw new Exception("OnLoadData");
        }

        protected virtual bool OnSaveData()
        {
            throw new Exception("OnSaveData");
        }

        protected virtual void OnBeforeEdit()
        {
        }

        protected virtual bool OnUndoData()
        {
            throw new Exception("OnUndoData");
        }

        protected virtual bool OnDeleteData()
        {
            throw new Exception("OnDeleteData");
        }

        protected virtual bool OnToolStripButtonClick(ButtonClicked button)
        {
            throw new Exception("OnToolStripButtonClick");
        }

        protected virtual void OnValidateControl()
        {
        }

        protected virtual void OnNewEvent()
        {
            if (OnNewButton())
            {
                masterBind.CancelEdit();
                masterBind.ResetCurrentItem();
                IsNew = true;
                OnNewData();
                OnAfterAddNew();
                DocumentMode = DBMode.Edit;
            }
            ManageToolbarEvents();
            FocusOnNew();
        }

        protected virtual bool OnEditEvent()
        {
            IsNew = false;
            DocumentMode = DBMode.Edit;
            OnEditButton();
            CanRemove = false;

            ManageToolbarEvents();
            return true;
        }

        protected virtual void OnSaveEvent()
        {
            if (!this.Validate())
                return;

            OnValidateControl();
            if (!OnBeforeSave())
                return;
            if (!OnSaveButton())
                return;
            OnAfterSave();

            DocumentMode = DBMode.Browse;
            ManageToolbarEvents();
            IsNew = false;
        }

        protected virtual void OnUndoEvent()
        {
            OnValidateControl();
            if (!OnUndoButton()) return;

            IsNew = false;
            DocumentMode = DBMode.Browse;
            ManageToolbarEvents();
        }

        protected virtual void OnDeleteEvent()
        {
            if (!OnDeleteButton()) return;
            OnAfterDelete();

            ManageToolbarEvents();
        }

        protected virtual void OnExitEvent()
        {
            if (DocumentClose != null)
                DocumentClose(this, this);
        }

        #endregion

        #region virtual override function

        protected virtual bool OnNewButton()
        {
            if (OnBeforeAddNew())
            {
                IsNew = true;
                OnNewData();
                OnPrepareAuxData();
                return true;
            }

            return false;
        }

        protected virtual void OnEditButton()
        {
            CanRemove = false;
            IsNew = false;
            OnBeforeEdit();
            OnPrepareAuxData();
        }

        protected virtual bool OnSaveButton()
        {
            return OnSaveData();
        }

        protected virtual bool OnDeleteButton()
        {
            if (OnBeforeDelete())
            {
                IsNew = false;
                return OnDeleteData();
            }

            return false;
        }

        protected virtual bool OnUndoButton()
        {
            if (OnBeforeUndo())
            {
                masterBind.EndEdit();
                masterBind.CancelEdit();
                CanRemove = true;
                return true;
            }
            return false;
        }

        protected virtual bool OnBeforeClosing()
        {
            if (DocumentMode == DBMode.Edit)
            {
                if (MessageBox.Show
                    (
                        Properties.Resources.Exit_Close,
                        Properties.Resources.Warning,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    ) == DialogResult.Yes)
                {
                    OnUndoData();
                    return false;
                }
                else return true;
            }
            else return false;
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
            OnPrepareAuxData();
            return true;
        }

        protected virtual bool OnBeforeDelete()
        {
            return MessageBox.Show(
                    Properties.Resources.Msg_Delete,
                    Properties.Resources.Warning,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        protected virtual bool OnAfterDelete()
        {
            masterBind.CurrencyManager.Refresh();
            return true;
        }

        protected virtual bool OnPrintDocument(string sender, PrinterForm pf)
        {
            return true;
        }

        protected virtual bool OnBeforeUndo()
        {
            return MessageBox.Show
                (
                Properties.Resources.Exit_Undo,
                Properties.Resources.Warning,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                ) == DialogResult.Yes;
        }

        #endregion

        private void ManageToolbarEvents()
        {
            ManageOperationButton();
            if (controlBinder != null)
                controlBinder.Enable(DocumentMode == DBMode.Edit);

            if (DocumentMode == DBMode.Edit)
            {
                if (IsNew)
                    OnDisableControlsForNew();
                else
                    OnDisableControlsForEdit();

                if (controlBinder != null)
                    controlBinder.SetFocus();
            }
        }

        public void BindControl(Control control)
        {
            controlBinder.Bind(control);
        }

        public Binding BindControl(Control control, string property)
        {
            Binding objBind = new Binding(property, masterBind, null);
            control.DataBindings.Add(objBind);
            controlBinder.Bind(control);

            return objBind;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            OnNewEvent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            (sender as MetroToolbarDropDownButton).ShowMenu();
        }

        private void btnList_DropDownItemClicked(object sender, EventArgs e)
        {
            OnFindData(sender as MyToolStripItem);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditEvent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSaveEvent();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            OnUndoEvent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteEvent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            OnExitEvent();
        }
    }
}
