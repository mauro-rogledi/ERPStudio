using ERPFramework.Data;
using ERPFramework.Controls;
using ERPFramework.Forms;
using System;
using System.Windows.Forms;

namespace ERPFramework.CounterManager
{
    public class CodesManager : IDocumentDataManager, IDisposable
    {
        private codesForm cF;

        public CodesManager()
        {
            //cF = new codesForm();
            //cF.SilentMode = true;
            //cF.ShowDialog(false);
        }

        public bool AddCode(string code, string description)
        {
            if (!AddNew())
                return false;

            SetHeader(code, description);
            AddRow(description, 1, 15);

            return Save();
        }

        public bool DeleteCode(string code)
        {
            if (Find(new RadarCodesParam(code)))
                return Delete();

            return true;
        }

        public bool AddNew()
        {
            return cF.ToolbarEvent(DocumentForm.ToolbarEventKind.New);
        }

        public bool Edit()
        {
            return cF.ToolbarEvent(DocumentForm.ToolbarEventKind.Edit);
        }

        public bool Save()
        {
            return cF.ToolbarEvent(DocumentForm.ToolbarEventKind.Save);
        }

        public bool Delete()
        {
            return cF.ToolbarEvent(DocumentForm.ToolbarEventKind.Delete);
        }

        public bool Find(IRadarParameters key)
        {
            return cF.FindRecord(key);
        }

        private bool Find(string code)
        {
            RadarCodesParam param = new RadarCodesParam(code);
            return Find(param);
        }

        public void SetHeader(string code, string description)
        {
            cF.GetCbbManager.AddValue(code, description);
            cF.GetCbbManager.Refresh();
            cF.GetCode.Text = description;
            cF.GetDescription.Text = description;
        }

        public void AddRow(string desc, int inputType, int inputLen)
        {
            cF.AddNewRow(desc, inputType, inputLen);
        }

        public DataGridViewRow GetRow(int t)
        {
            return cF.GetDataGrid.Rows[t];
        }

        public int RowCount
        {
            get { return cF.GetDataGrid.Rows.Count; }
        }

        public void Dispose()
        {
            cF.Close();
            cF.Dispose();
            cF = null;
        }
    }
}