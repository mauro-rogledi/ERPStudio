using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using ERPFramework.Data;
using ERPFramework.DataGridViewControls;
using ERPFramework.Forms;

namespace ERPFramework.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.DataGrid))]
    public class ExtendedDataGridView : MetroFramework.Controls.MetroGrid
    {
        #region Public Properties

        [Browsable(false)]
        public IColumn RowIndex { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public bool DoubleInsert { set; private get; }

        public IDocumentBase DocumentForm{ get; set; }

        public bool NoMessage { get; set; }

        private MetroGridContainerPanel containerPanel = null;

        private bool showHeaderPanel = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowHeaderPanel
        {
            get { return showHeaderPanel; }
            set { showHeaderPanel = value; if (containerPanel != null) containerPanel.ShowHeaderPanel = value; }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue("")]
        public string SelectColumnName { get; set; }

        private bool showSelectUnselect = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowSelectUnselect
        {
            get { return showSelectUnselect; }
            set { showSelectUnselect = value; if (containerPanel != null) containerPanel.ShowSelectUnselect = value; }
        }

        private bool showFilter = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowFilter
        {
            get { return showFilter; }
            set { showFilter = value; if (containerPanel != null) containerPanel.ShowFilter = value; }
        }

        private bool showFooterPanel = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowFooterPanel
        {
            get { return showFooterPanel; }
            set { showFooterPanel = value; if (containerPanel != null) containerPanel.ShowFooterPanel = value; }
        }

        private bool columnsChecked = false;
        [Browsable(false)]
        public bool ColumnsChecked
        {
            get { return columnsChecked; }
            set { columnsChecked = value; }
        }

        private List<MetroGridTotalColumn> totalColumns = new List<MetroGridTotalColumn>();
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsableAttribute(EditorBrowsableState.Advanced)]
        public List<MetroGridTotalColumn> TotalColumns { get { return totalColumns; } }

        public new object DataSource
        {
            set
            {
                if (value is DataSet)
                {
                    var ds = value as DataSet;
                    base.DataSource = new DataView(ds.Tables[0]);
                }
                else if (value is DataTable)
                {
                    var tb = value as DataTable;
                    base.DataSource = new DataView(tb);
                }
                else if (value is BindingSource)
                    base.DataSource = value;

                ChangeDataSource?.Invoke(this, EventArgs.Empty);
            }

            get { return base.DataSource; }
        }

        public DataView DataView
        {
            get
            {
                return DataSource as DataView;
            }
        }

        #endregion

        public void LoadSetting() { DataGridViewProperties.Load(this); }

        #region MenuStrip

        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsert;
        private System.Windows.Forms.ToolStripMenuItem tsmiProp;
        private System.Windows.Forms.ToolStripMenuItem tsmiPropSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiPropDel;

        public void AddMenuStrip(System.Drawing.Image img, string name, string text, EventHandler handler)
        {
            var customStripMenu = new ToolStripMenuItem
            {
                Image = img,
                ImageTransparentColor = System.Drawing.Color.Fuchsia,
                Name = name,
                Size = new System.Drawing.Size(152, 22),
                Text = text
            };
            customStripMenu.Click += handler;

            this.cmsMenu.Items.Add(customStripMenu);
        }

        private void CreateMenuStrip()
        {
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsert = new ToolStripMenuItem();
            this.tsmiProp = new ToolStripMenuItem();
            this.tsmiPropSave = new ToolStripMenuItem();
            this.tsmiPropDel = new ToolStripMenuItem();

            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInsert,
            this.tsmiDelete,
            this.tsmiProp});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(153, 48);

            //
            // tsmiDelete
            //
            this.tsmiDelete.Image = Properties.Resources.Delete24g;
            this.tsmiDelete.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmiDelete.Text = Properties.Resources.DeleteRow;
            this.tsmiDelete.Click += new EventHandler(tsmiDelete_Click);

            //
            // tsmiInsert
            //
            this.tsmiInsert.Image = Properties.Resources.New24g;
            this.tsmiInsert.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsmiInsert.Name = "tsmiInsert";
            this.tsmiInsert.Size = new System.Drawing.Size(138, 22);
            this.tsmiInsert.Text = Properties.Resources.InsertRow;
            this.tsmiInsert.Click += new System.EventHandler(tsmiInsert_Click);

            //
            // tsmiProp
            //
            this.tsmiProp.Image = Properties.Resources.Edit24g;
            this.tsmiProp.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsmiProp.Name = "tsmiPropSave";
            this.tsmiProp.Size = new System.Drawing.Size(138, 22);
            this.tsmiProp.Text = Properties.Resources.Setting;
            this.tsmiProp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPropSave,
            this.tsmiPropDel});
            //
            // tsmiPropSave
            //
            this.tsmiPropSave.Image = Properties.Resources.Save24g;
            this.tsmiPropSave.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsmiPropSave.Name = "tsmiPropSave";
            this.tsmiPropSave.Size = new System.Drawing.Size(138, 22);
            this.tsmiPropSave.Text = Properties.Resources.SaveSetting;
            this.tsmiPropSave.Click += new System.EventHandler(tsmiPropSave_Click);
            //
            // tsmiPropDel
            //
            this.tsmiPropDel.Image = Properties.Resources.Delete24g;
            this.tsmiPropDel.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsmiPropDel.Name = "tsmiPropDel";
            this.tsmiPropDel.Size = new System.Drawing.Size(138, 22);
            this.tsmiPropDel.Text = Properties.Resources.DeleteSetting;
            this.tsmiPropDel.Click += new System.EventHandler(tsmiPropDel_Click);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            deletingRow = true;
            var curRow = this.CurrentRow;

            if (this.CurrentRow != null && !this.CurrentRow.IsNewRow)
                this.Rows.Remove(curRow);

            deletingRow = false;
        }

        private void tsmiInsert_Click(object sender, EventArgs e)
        {
            this.CommitEdit(DataGridViewDataErrorContexts.Commit);
            try
            {
                var currow = this.CurrentRow.Index;
                if (DataSource is DataView)
                {
                    var bSource = this.DataSource as DataView;

                    // TODO Poi capiro perche .... doppio insert necessario
                    bSource.AddNew();
                    if (DoubleInsert)
                        bSource.AddNew();
                }
                else
                    if (DataSource is BindingSource)
                {
                    var bSource = this.DataSource as BindingSource;

                    // TODO Poi capiro perche .... doppio insert necessario
                    bSource.AddNew();
                    if (DoubleInsert)
                        bSource.AddNew();
                }

                // Move rows to insert new Row
                for (int row = this.Rows.Count - 2 + (this.Rows.Count == 2 ? 1 : 0); row > currow; row--)
                {
                    for (int col = 0; col < this.Columns.Count; col++)
                        this.Rows[row].Cells[col].Value = this.Rows[row - 1].Cells[col].Value;
                }

                // Clean current row
                ClearRow(this.Rows[currow]);

                // Recreate the column order
                if (RowIndex != null)
                    for (int row = 0; row < this.RowCount; row++)
                        SetValue<int>(row, RowIndex, row);

                this.InvalidateRow(this.Rows.Count - 1);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void tsmiPropSave_Click(object sender, EventArgs e)
        {
            DataGridViewProperties.Save(this);
        }

        private void tsmiPropDel_Click(object sender, EventArgs e)
        {
            DataGridViewProperties.Delete(this);
        }

        private void ClearRow(DataGridViewRow row)
        {
            for (int t = 0; t < Columns.Count; t++)
            {
                if (row.Cells[t].ValueType == typeof(Double))
                    row.Cells[t].Value = 0;
                else if (row.Cells[t].ValueType == typeof(bool))
                    row.Cells[t].Value = false;
                else
                    row.Cells[t].Value = "";
            }
        }

        #endregion

        #region Event & delegate

        public delegate string DataGridViewColumnValidatingEventHandler(object sender, DataGridViewCellValidatingEventArgs e);

        public delegate void DataGridViewCellValidatedEventHandler(object sender, DataGridViewCellValidatedArgs pe);
        public delegate void DataGridViewDoubleClickEventHanlder(object sender, DataGridViewDoubleClickArgs e);
        public delegate void DataGridViewValueChangeEventHandler(object sender, DataGridViewCellEventArgs pe);


        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewCellValidatedEventHandler DataGridViewCellValidated;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewValueChangeEventHandler DataGridViewValueChanged;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewDoubleClickEventHanlder DataGridViewDoubleClickCell;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewCellEventHandler DataGridViewRowEnter;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewRowsRemovedEventHandler DataGridViewRowRemoved;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewRowsAddedEventHandler DataGridViewRowAdded;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewCellCancelEventHandler DataGridViewRowValidating;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewRowEventHandler DataGridViewPrepareRowEventHandler;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event DataGridViewCellCancelEventHandler DataGridViewRowIsEmpty;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event EventHandler ChangeDataSource;
        #endregion

        protected DataTable gridTable;

        private Hashtable CheckingColumns = new Hashtable();
        private Hashtable readonlyColumns = new Hashtable();
        private Hashtable ChangeValueColumns = new Hashtable();
        private Hashtable ColumnsChanged = new Hashtable();
        private bool isReadOnly;
        private bool deletingEmptyRow;
        private bool deletingRow;

        public ExtendedDataGridView()
            : base()
        {
            CreateMenuStrip();
            this.ContextMenuStrip = cmsMenu;
            NoMessage = false;
        }

        #region Events
        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            if (DataGridViewRowRemoved == null)
            {
                base.OnRowsRemoved(e);
                return;
            }

            if (DocumentForm != null && (DocumentForm.DocumentMode == DBMode.Browse || DocumentForm.DocumentMode == DBMode.Validating))
                return;

            if (DataGridViewRowRemoved != null && (DocumentForm == null || DocumentForm.DocumentMode != DBMode.Browse))
                DataGridViewRowRemoved(this, e);

            base.OnRowsRemoved(e);
        }

        protected override void OnCellContentDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellContentDoubleClick(e);

            if (e.RowIndex < 0)
                return;
            var cellname = this.Columns[e.ColumnIndex].DataPropertyName;
            var cellvalue = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (DataGridViewDoubleClickCell != null)
                DataGridViewDoubleClickCell(this, new DataGridViewDoubleClickArgs(this, cellname, cellvalue, e.RowIndex));

            var c = this.Columns[e.ColumnIndex];
            if (c is IClickable)
                (c as IClickable).OpenDocument(cellvalue);
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            displayErrorDialogIfNoHandler = false;
            base.OnDataError(displayErrorDialogIfNoHandler, e);
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            if (!this.Rows[e.RowIndex].IsNewRow)
                PrepareAuxColumns(e.RowIndex);

            if (DocumentForm == null || (DocumentForm.DocumentMode == DBMode.Browse || DocumentForm.DocumentMode == DBMode.Validating))
                return;

            var curRow = e.RowIndex - 1;

            // Tolto rowisblank altrimenti non crea numero riga a riga nuova
            if (RowIndex != null && curRow >= 0/* && RowIsBlank(curRow)*/)
            {
                SetValue<int>(curRow, RowIndex, GetMaxRowIndex(RowIndex));
            }

            if (DataGridViewRowAdded != null)
                DataGridViewRowAdded(this, e);

            base.OnRowsAdded(e);
        }

        protected override void OnRowEnter(DataGridViewCellEventArgs e)
        {
            base.OnRowEnter(e);

            tsmiInsert.Enabled = e.RowIndex != this.RowCount - 1;
            DataGridViewRowEnter?.Invoke(this, e);
        }

        protected override void OnAllowUserToDeleteRowsChanged(EventArgs e)
        {
            cmsMenu.Items[tsmiInsert.Name].Visible = this.AllowUserToAddRows;
            cmsMenu.Items[tsmiDelete.Name].Visible = this.AllowUserToDeleteRows;

            base.OnAllowUserToDeleteRowsChanged(e);
        }

        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            CurrentCell.Tag = CurrentCell.Value;
            base.OnCellEnter(e);
        }

        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            if (ReadOnly)
                return;

            var columnName = this.Columns[e.ColumnIndex].DataPropertyName;
            if (CheckingColumns.ContainsKey(columnName))
            {
                var eventHandler = (DataGridViewColumnValidatingEventHandler)CheckingColumns[columnName];
                this.Rows[e.RowIndex].ErrorText = eventHandler(this.Columns[e.ColumnIndex], e);
                Invalidate();
            }

            if (this[e.ColumnIndex, e.RowIndex].Value!=null && CurrentCell.Tag.ToString() != e.FormattedValue.ToString())
            {
                if (ColumnsChanged.ContainsKey(columnName))
                {
                    var eventHandler = (DataGridViewValueChangeEventHandler)ColumnsChanged[columnName];
                    var args = new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex);
                    eventHandler(this.Columns[e.ColumnIndex], args);
                    Invalidate();
                }
            }
            base.OnCellValidating(e);
        }

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || this.Rows[e.RowIndex].IsNewRow || (DocumentForm != null && DocumentForm.DocumentMode == DBMode.Browse) || NoMessage)
                return;

            var columnName = this.Columns[e.ColumnIndex].DataPropertyName;
            if (ChangeValueColumns.ContainsKey(columnName))
            {
                var eventHandler = (DataGridViewValueChangeEventHandler)ChangeValueColumns[columnName];
                eventHandler(this.Columns[e.ColumnIndex], e);
                Invalidate();
            }
            base.OnCellValueChanged(e);
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            base.OnCellContentClick(e);
        }

        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            base.OnCurrentCellDirtyStateChanged(e);
            if (IsCurrentCellDirty)
            {
                CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        protected override void OnRowValidating(DataGridViewCellCancelEventArgs e)
        {
            base.OnRowValidating(e);
            if (deletingEmptyRow || deletingRow)
            {
                return;
            }

            if (DataGridViewRowValidating != null)
                DataGridViewRowValidating(this, e);
        }

        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            if (deletingEmptyRow || deletingRow)
                return;
            if (DocumentForm == null || DocumentForm.DocumentMode == DBMode.Browse)
                return;
            DeleteEmptyRows();
        }

        #endregion

        #region ColumValidator ReadOnly

        public void AddColumnValidator(IColumn columName, DataGridViewColumnValidatingEventHandler columnvalidatingeventhandler)
        {
            var bEnable = Enabled;
            var bReadOnly = ReadOnly;

            AddColumnValidator(columName.Name, columnvalidatingeventhandler);
        }

        public void AddColumnValidator(string columName, DataGridViewColumnValidatingEventHandler columnvalidatingeventhandler)
        {
            if (!CheckingColumns.ContainsKey(columName))
                CheckingColumns.Add(columName, columnvalidatingeventhandler);
            else
                Debug.Assert(false, "Column alredy presents in ColumnValidator");
        }

        public void AddColumnChangeValue(IColumn columName, DataGridViewValueChangeEventHandler columnvaluechangeeventhandler)
        {
            AddColumnChangeValue(columName.Name, columnvaluechangeeventhandler);
        }

        public void AddColumnsChangedValue(IColumn columName, DataGridViewValueChangeEventHandler columnvaluechangeeventhandler)
        {
            if (!ColumnsChanged.ContainsKey(columName.Name))
                ColumnsChanged.Add(columName.Name, columnvaluechangeeventhandler);
            else
                Debug.Assert(false, "Column alredy presents in ColumnsChangedValue");
        }

        public void AddColumnChangeValue(string columName, DataGridViewValueChangeEventHandler columnvaluechangeeventhandler)
        {
            if (!ChangeValueColumns.ContainsKey(columName))
                ChangeValueColumns.Add(columName, columnvaluechangeeventhandler);
            else
                Debug.Assert(false, "Column alredy presents in ColumnChangeValue");
        }

        public void AddReadOnlyColumn(IColumn columName, bool state)
        {
            AddReadOnlyColumn(columName.Name, state);
        }

        public void AddReadOnlyColumn(string columnName, bool state)
        {
            switch (state)
            {
                case true:
                    if (!readonlyColumns.ContainsKey(columnName))
                        readonlyColumns.Add(columnName, "");
                    //else
                    //    Debug.Assert(false, "Column alredy defined as readonly");
                    break;

                case false:
                    if (readonlyColumns.ContainsKey(columnName))
                        readonlyColumns.Remove(columnName);
                    //else
                    //    Debug.Assert(false, "Column not defined as readonly");
                    break;
            }
        }

        #endregion

        public void ControlValidate(string sender, string description)
        {
            if (DocumentForm == null || DocumentForm.DocumentMode == DBMode.Browse)
                return;

            if (DataGridViewCellValidated != null)
                DataGridViewCellValidated(this, new DataGridViewCellValidatedArgs(sender, description));
        }

        public void CellChanged(string sender, string description)
        {
            if (DocumentForm == null || DocumentForm.DocumentMode == DBMode.Browse)
                return;

            if (DataGridViewValueChanged != null)
                DataGridViewValueChanged(this, new DataGridViewCellEventArgs(0, 0));
        }

        public new bool ReadOnly
        {
            get { return isReadOnly; }
            set
            {
                try
                {
                    foreach (DataGridViewBand band in this.Columns)
                    {
                        if (!readonlyColumns.ContainsKey(this.Columns[band.Index].DataPropertyName))
                            band.ReadOnly = value;
                    }
                }
                catch (Exception exc)
                {
                    Debug.Write(exc.Message);
                }
                isReadOnly = value;
                cmsMenu.Items[tsmiInsert.Name].Enabled = !value;
                cmsMenu.Items[tsmiDelete.Name].Enabled = !value;
            }
        }

        public void DeleteEmptyRows()
        {
            deletingEmptyRow = true;

            for (int t = this.Rows.Count - 1; t >= 0; t--)
            {
                var dr = this.Rows[t].DataBoundItem != null
                    ? (this.Rows[t].DataBoundItem as DataRowView).Row
                    : null;

                if (RowIsBlank(t))
                {
                    if (this.Rows[t].IsNewRow)
                    {
                         if(dr != null)
                            dr.RejectChanges();
                    }
                    else
                        this.Rows.Remove(this.Rows[t]);
                }
            }
            deletingEmptyRow = false;
        }

        protected virtual bool RowIsBlank(int row)
        {
            if (DataGridViewRowIsEmpty != null && !Rows[row].IsNewRow)
            {
                var e = new DataGridViewCellCancelEventArgs(0, row);
                DataGridViewRowIsEmpty(this, e);
                return e.Cancel;
            }

            for (int t = 0; t < this.Columns.Count; t++)
            {
                if (this.Rows[row].Cells[t].Value == null)
                    continue;

                // Se la riga è cancellata, la considero vuota.
                var cell = this.Rows[row].Cells[t].Value.ToString();
                cell = cell.TrimStart();
                cell = cell.TrimEnd();
                if (this.Rows[row].Cells[t].EditType == typeof(NumericTextBoxDataGridViewControl))
                {
                    if (cell != "0" && cell != "")
                        return false;
                }
                else
                    if (cell != string.Empty)
                    return false;
            }
            return true;
        }

        public DataRowView AddNewRow()
        {
            var ok = this.DataSource is BindingSource;
            var bSource = (BindingSource)this.DataSource;
            return (DataRowView)bSource.AddNew();
        }

        public DataRowView AddNewRow(IColumn col)
        {
            var drw = AddNewRow();
            drw.BeginEdit();
            drw.SetValue<int>(col, GetMaxRowIndex(col));
            drw.EndEdit();
            return drw;
        }

        public int GetMaxRowIndex(IColumn col)
        {
            var maxval = int.MinValue;
            for (int t = 0; t < Rows.Count; t++)
            {
                if (GetValue<int>(t, col) > maxval)
                    maxval = GetValue<int>(t, col);
            }

            return maxval + 1;
        }

        protected virtual void PrepareAuxColumns(int row)
        {
            if (DataGridViewPrepareRowEventHandler != null)
                DataGridViewPrepareRowEventHandler(this, new DataGridViewRowEventArgs(this.Rows[row]));
        }

        public void Clear()
        {
            var nRow = this.Rows.Count - 1;
            for (int t = nRow; t >= 0; t--)
                if (!this.Rows[t].IsNewRow)
                    this.Rows.RemoveAt(t);
        }

        public virtual void SortRows()
        {
            Debug.Assert(gridTable != null, "Missing table value in ExtendedDataGridView");
            Debug.Assert(RowIndex != null, "Missing sort column value in ExtendedDataGridView");
            var mySL = new SortedList();
            var nRow = gridTable.Rows.Count;

            for (int i = 0; i < nRow; i++)
            {
                if (gridTable.Rows[i].RowState != DataRowState.Deleted)
                {
                    var pos = int.Parse(gridTable.Rows[i][RowIndex.Name].ToString());
                    mySL.Add(pos.ToString("0000"), i.ToString());
                }
            }

            var row = 1;
            foreach (DictionaryEntry de in mySL)
            {
                var pos = int.Parse(de.Value.ToString());
                gridTable.Rows[pos][RowIndex.Name] = row;
            }
        }

        #region Get/Set Cell Value

        public T CurrentValue<T>()
        {
            return (T)Convert.ChangeType(CurrentCell.EditedFormattedValue, typeof(T));
        }

        public DataGridViewRow this[int row]
        {
            get { return this.Rows[row]; }
        }

        public DataGridViewBand this[IColumn column]
        {
            get { return this.Columns[column.Name]; }
        }

        public DataGridViewCell this[IColumn column, int row]
        {
            get { return this.Rows[row].Cells[column.Name]; }
        }

        public T GetValue<T>(int row, IColumn column)
        {
            try
            {
                if (typeof(T).BaseType == typeof(Enum))
                {
                    return Rows[row].Cells[column.Name].Value == System.DBNull.Value
                        ? (T)Enum.ToObject(typeof(T), default(T))
                        : (T)Enum.ToObject(typeof(T), Rows[row].Cells[column.Name].Value);
                }
                if (typeof(T) == typeof(int))
                {
                    var val = 0;
                    if (int.TryParse(Rows[row].Cells[column.Name].Value.ToString(), out val))
                        return (T)Convert.ChangeType(Rows[row].Cells[column.Name].Value, typeof(T));
                    else
                        return (T)Convert.ChangeType(0, typeof(T));
                }
                else
                {
                    return Rows[row].Cells[column.Name].Value == System.DBNull.Value
                            ? (
                                typeof(T) == typeof(string)
                                    ? (T)Convert.ChangeType("", typeof(T))
                                    : (T)Convert.ChangeType(default(T), typeof(T))
                              )
                        : (T)Convert.ChangeType(Rows[row].Cells[column.Name].Value, typeof(T));
                }
            }
            catch
            {
                return default(T);
            }
        }

        public void SetValue<T>(int row, IColumn column, T value)
        {
            this.Rows[row].Cells[column.Name].Value = value;
            this.InvalidateRow(row);
        }

        public void SetTotalColumn(string name, object value)
        {
            if (containerPanel != null)
                containerPanel.SetTotalColumn(name, value);
        }

        #endregion

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            ResizeColumns();

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResizeColumns();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            ResizeColumns();
        }

        public void ResizeColumns()
        {
            var fixedspace = 0;

            foreach (DataGridViewColumn colf in Columns)
            {
                // Prima imposto e calcolo lo spazio occupato dalle colonne fisse
                if (colf.Tag != null && colf.Tag is ExtenderColumn)
                {
                    var size = colf.Tag as ExtenderColumn;
                    if (size.FixedSize > 0)
                    {
                        colf.Width = size.FixedSize;
                        fixedspace += colf.Width;
                    }
                }
                var spacetofill = this.ClientSize.Width - fixedspace;
                if (VerticalScrollBar.Visible)
                    spacetofill -= VerticalScrollBar.Width;
                foreach (DataGridViewColumn colv in Columns)
                {
                    // Prima imposto e calcolo lo spazio occupato dalle colonne fisse
                    if (colv.Tag != null && colv.Tag is ExtenderColumn)
                    {
                        var size = colv.Tag as ExtenderColumn;
                        if (size.VariableSize > 0)
                            colv.Width = (int)Math.Truncate(spacetofill * size.VariableSize / 100);
                    }
                }
            }

        }

        private System.Type footerType = null;
        public void AddFooterPanel<T>()
        {
            footerType = typeof(T);
            if (containerPanel != null) containerPanel.AddFooterPanel<T>();
        }

        #region Change Parent Management
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (DesignMode || Parent == null)
                return;

            AddContainerPanel();
        }

        private void AddContainerPanel()
        {
            if (this.Parent is MetroGridContainerPanel)
            {
                containerPanel = this.Parent as MetroGridContainerPanel;
                containerPanel.UseStyleColors = this.UseStyleColors;
                containerPanel.ShowHeaderPanel = showHeaderPanel;
                containerPanel.ShowSelectUnselect = showSelectUnselect;
                containerPanel.FooterPanelType = footerType;
                containerPanel.DataGrid = this;
            }
        }
        #endregion
    }

    public class DataGridViewCellValidatedArgs : EventArgs
    {
        private string desc = string.Empty;
        private string send = string.Empty;

        public string Description { get { return desc; } }

        public string Sender { get { return send; } }

        public DataGridViewCellValidatedArgs(string sender, string description)
        {
            desc = description;
            send = sender;
        }
    }

    public class DataGridViewDoubleClickArgs : EventArgs
    {
        public string ColumnName { get; private set; }

        public string CellValue { get; private set; }

        public object Sender { get; private set; }

        public int Row { get; private set; }

        public DataGridViewDoubleClickArgs(object sender, string columnname, string cellvalue, int row)
        {
            ColumnName = columnname;
            CellValue = cellvalue;
            Sender = sender;
            Row = row;
        }
    }

    [DataContract]
    internal class DataGridColumnProperties
    {
        [DataMember(Name = "N")]
        public string ColumnName { get; set; }
        [DataMember(Name="D")]
        public int DisplayIndex { get; set; }
        [DataMember(Name="W")]
        public int Width { get; set; }
        [DataMember(Name="V")]
        public bool Visible { get; set; }
    }

    public static class DataGridViewProperties
    {
        private static Dictionary<string, List<DataGridColumnProperties>> gridProperties { get; set; }

        public static void Load(ExtendedDataGridView dgw)
        {
            dgw.AllowUserToOrderColumns = true;
            var gridnamespace = string.Format("{0}.{1}", dgw.DocumentForm.Name, dgw.Name);
            if (string.IsNullOrEmpty(Properties.Settings.Default.DataGridViewSetting))
                return;

            if (gridProperties == null)
            {
                using (System.IO.MemoryStream sw = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Properties.Settings.Default.DataGridViewSetting)))
                {
                    var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, List<DataGridColumnProperties>>));
                    gridProperties = (Dictionary<string, List<DataGridColumnProperties>>)serializer.ReadObject(sw);
                    sw.Close();
                }
            }

            if (!gridProperties.ContainsKey(gridnamespace))
                return;

            foreach (DataGridColumnProperties column in gridProperties[gridnamespace])
            {
                dgw.Columns[column.ColumnName].DisplayIndex = column.DisplayIndex;
                dgw.Columns[column.ColumnName].Width = column.Width;
                dgw.Columns[column.ColumnName].Visible = column.Visible;
            }
        }

        public static void Save(ExtendedDataGridView dgw)
        {
            Debug.Assert(!dgw.DocumentForm.Name.IsEmpty(), "ExtendedDataGridView don't binded");
            var gridnamespace = string.Format("{0}.{1}", dgw.DocumentForm.Name, dgw.Name);

            if (gridProperties == null)
                gridProperties = new Dictionary<string, List<DataGridColumnProperties>>();

            if (gridProperties.ContainsKey(gridnamespace))
                gridProperties[gridnamespace].Clear();
            else
                gridProperties.Add(gridnamespace, new List<DataGridColumnProperties>());

            foreach (DataGridViewColumn column in dgw.Columns)
            {
                var dgcp = new DataGridColumnProperties
                {
                    ColumnName = column.Name,
                    DisplayIndex = column.DisplayIndex,
                    Width = column.Width,
                    Visible = column.Visible
                };
                gridProperties[gridnamespace].Add(dgcp);
            }

            using (System.IO.MemoryStream sw = new System.IO.MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, List<DataGridColumnProperties>>));
                serializer.WriteObject(sw, gridProperties);
                Properties.Settings.Default.DataGridViewSetting = Encoding.UTF8.GetString(sw.ToArray());
                sw.Close();
            }
            Properties.Settings.Default.Save();
        }

        public static void Delete(ExtendedDataGridView dgw)
        {
            var gridnamespace = string.Format("{0}.{1}", dgw.DocumentForm.Name, dgw.Name);

            if (gridProperties == null)
                gridProperties = new Dictionary<string, List<DataGridColumnProperties>>();

            if (gridProperties.ContainsKey(gridnamespace))
                gridProperties[gridnamespace].Clear();

            using (System.IO.MemoryStream sw = new System.IO.MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, List<DataGridColumnProperties>>));
                serializer.WriteObject(sw, gridProperties);
                Properties.Settings.Default.DataGridViewSetting = Encoding.UTF8.GetString(sw.ToArray());
                sw.Close();
            }
            Properties.Settings.Default.Save();
        }
    }

    public class ExtenderColumn
    {
        public int FixedSize { get; set; } = 0;
        public float VariableSize { get; set; } = 0;
        public object DefaultValue { get; set; } = "";
    }

    public static class ExtendDataGridViewColumn
    {
        public static void FixedSize(this DataGridViewColumn col, int val)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (col.Tag == null)
                col.Tag = new ExtenderColumn { FixedSize = val };
            else
                (col.Tag as ExtenderColumn).FixedSize = val;
        }

        public static void VariableSize(this DataGridViewColumn col, float val)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (col.Tag == null)
                col.Tag = new ExtenderColumn { VariableSize = val };
            else
                (col.Tag as ExtenderColumn).VariableSize = val;
        }

        public static void DefaultValue(this DataGridViewColumn col, object val)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (col.Tag == null)
                col.Tag = new ExtenderColumn { DefaultValue = val };
            else
                (col.Tag as ExtenderColumn).DefaultValue = val;
        }
    }

    public static class ExtendDataGridViewRow
    {
        public static void SetValue<T>(this DataGridViewRow row, IColumn col, T value)
        {
            SetValue<T>(row, col.Name, value);
        }

        public static void SetValue<T>(this DataGridViewRow row, string col, T value)
        {
            row.Cells[col].Value = value;
        }

        public static T GetValue<T>(this DataGridViewRow row, IColumn col)
        {
            return GetValue<T>(row, col.Name);
        }

        public static T GetValue<T>(this DataGridViewRow row, string col)
        {
            if (row.Cells[col].Value != System.DBNull.Value && row.Cells[col].Value != null && row.Cells[col].Value.ToString() != string.Empty)
                return (T)Convert.ChangeType(row.Cells[col].Value, typeof(T));
            else
                return default(T);
        }
    }

    public static class ExtendDataRowView
    {
        public static void SetValue<T>(this DataRowView row, IColumn col, T value)
        {
            row[col.Name] = value;
        }

        public static T GetValue<T>(this DataRowView row, IColumn col)
        {
            return (T)Convert.ChangeType(row[col.Name], typeof(T));
        }
    }
}
