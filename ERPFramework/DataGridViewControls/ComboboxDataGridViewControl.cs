using ERPFramework.Data;
using ERPFramework.Libraries;
using System.Windows.Forms;

namespace ERPFramework.DataGridViewControls
{
    public class ComboboxDataGridViewControlColumn : DataGridViewComboBoxColumn
    {
        protected ComboBoxManager cbbManager;

        public ComboboxDataGridViewControlColumn()
        {
        }

        virtual public void AttachDataReader(IDataReaderUpdater dr, IColumn code, IColumn description)
        {
            AttachDataReader(dr, code, description, false);
        }

        virtual public void AttachDataReader(IDataReaderUpdater dr, IColumn code, IColumn description, bool alsoNULL)
        {
            cbbManager = new ComboBoxManager(this);
            cbbManager.CreateList<string>();
            if (GlobalInfo.DBaseInfo.dbManager != null)
            {
                if (alsoNULL)
                    cbbManager.AddValue(string.Empty, string.Empty);
                FillComboBox(dr, code, description);
                cbbManager.AttachTo(this);
                cbbManager.Refresh();
            }
        }

        virtual public void RefreshDataReader(IDataReaderUpdater dr, IColumn code, IColumn description)
        {
            cbbManager.Clear<string>();
            dr.Find();
            for (int t = 0; t < dr.Count; t++)
                cbbManager.AddValue(dr.GetValue<string>(code, t), dr.GetValue<string>(description, t));

            cbbManager.Refresh();
        }

        virtual protected void FillComboBox(IDataReaderUpdater dr, IColumn code, IColumn description)
        {
            dr.Find();
            for (int t = 0; t < dr.Count; t++)
                cbbManager.AddValue(dr.GetValue<string>(code, t), dr.GetValue<string>(description, t));
        }

        virtual public T GetValue<T>()
        {
            return cbbManager != null
                        ? cbbManager.GetValue<T>()
                        : default(T);
        }
    }
}