using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.Preferences;
using MetroFramework.Extender;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ERPFramework.ModulesHelper;

namespace ERPFramework.Forms
{
    public interface IAddon
    {
        List<Addon> AddonList { get; }
    }

    public class Addon : IDisposable
    {
        #region Virtual Method

        public virtual void InitializerComponent()
        {
        }

        public virtual void OnLoad()
        {
        }

        public virtual void OnBindData()
        {
        }

        public virtual void OnInitializeData(Data.DBManager dbManager)
        {
        }

        public virtual void OnDisableControlsForNew()
        {
        }

        public virtual void OnDisableControlsForEdit()
        {
        }

        public virtual void OnAddSplitMenuButton()
        {
        }

        public virtual void OnValidateControl()
        {
        }

        public virtual void OnPrepareAuxData()
        {
        }

        public virtual void OnCustomButtonClick(MetroFramework.Controls.MetroProvaTile button)
        {
        }

        public virtual void OnCustomizeToolbar(MetroToolbar toolstrip)
        {
        }

        public virtual void OnAddonButton()
        {

        }

        public virtual bool? OnEnableAddonButton(MetroFramework.Controls.MetroProvaTile sender, Data.DBManager dbManager)
        {
            return null;
        }

        public virtual void OnAddPreferenceButton(PreferenceForm prefForm)
        {
        }

        //public virtual bool OnPrintDocument(PrintInfo sender, PrinterForm pf)
        //{
        //    return true;
        //}

        public virtual void OnToolStripMenuClick(string tag)
        { }

        public virtual bool OnBeforeAddNew()
        {
            return true;
        }

        public virtual bool OnAfterAddNew()
        {
            return true;
        }

        public virtual bool OnBeforeDelete()
        {
            return true;
        }

        public virtual bool OnAfterDelete()
        {
            return true;
        }

        public virtual bool OnBeforeSave()
        {
            return true;
        }

        public virtual bool OnSaveButton()
        {
            return true;
        }

        public virtual bool OnDeleteButton()
        {
            return true;
        }

        public virtual bool OnAfterSave()
        {
            return true;
        }

        public virtual bool OnBeforeUndo()
        {
            return true;
        }

        public virtual bool OnBeforeClosing()
        {
            return true;
        }

        public virtual List<SqlABParameter> CreateSlaveParam(string name)
        {
            return null;
        }

        public virtual void SetParameters(Data.DBManager dbManager, IRadarParameters key, DBCollection collection)
        {
        }

        public virtual string CreateSlaveQuery(string name, List<SqlABParameter> dParam)
        {
            return string.Empty;
        }

        public virtual void MasterRowUpdated(Data.DBManager dbManager, SqlABRowUpdatedEventArgs e)
        {
        }

        public virtual void RowUpdating(Data.DBManager dbManager, SqlABRowUpdatingEventArgs e)
        {
        }

        #endregion

        protected IDocument guestForm;

        public Addon(IDocument form)
        {
            guestForm = form;
        }

        public void Dispose()
        {
        }

        protected PrinterForm OnPreviewEvent(PrintType printType, string title = "")
        {
            PrinterForm pf = new PrinterForm("Preview", PrinterForm.PrintMode.Preview, printType);
            pf.Title = title;
            OpenDocument.Show(pf);
            return pf;
        }

        protected PrinterForm OnPrintEvent(PrintType printType)
        {
            PrinterForm pf = new PrinterForm("Print", PrinterForm.PrintMode.Print, printType);
            return pf;
        }

        #region MessageBox Overload

        public DialogResult MyMessageBox(string text)
        {
            return guestForm.MyMessageBox(text);
        }

        public DialogResult MyMessageBox(string text, string caption)
        {
            return guestForm.MyMessageBox(text, caption);
        }

        public DialogResult MyMessageBox(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return guestForm.MyMessageBox(owner, text, caption, buttons, icon);
        }

        public DialogResult MyMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return guestForm.MyMessageBox(text, caption, buttons, icon);
        }

        #endregion
    }
}