using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Drawing;

namespace ERPFramework.Controls
{
    public partial class MetroGridFooterPanel : MetroUserControl
    {
        protected ExtendedDataGridView metroGrid { get; private set; } = null;

        public MetroGridFooterPanel()
        {
            InitializeComponent();
        }

        public MetroGridFooterPanel(ExtendedDataGridView metroGrid)
        {
            this.metroGrid = metroGrid;

            InitializeComponent();

            metroGrid.RowsAdded += MetroGrid_RowsAdded;
            metroGrid.RowsRemoved += MetroGrid_RowsRemoved;
            metroGrid.RowEnter += MetroGrid_RowEnter;
            metroGrid.RowLeave += MetroGrid_RowLeave;
            metroGrid.DataBindingComplete += MetroGrid_DataBindingComplete;
            metroGrid.CellValueChanged += MetroGrid_CellValueChanged;
            metroGrid.ChangeDataSource += MetroGrid_ChangeDataSource;

            metroGrid.Scroll += MetroGrid_Scroll;
            metroGrid.Resize += MetroGrid_Resize;

            metroGrid.ColumnWidthChanged += MetroGrid_ColumnWidthChanged;
            metroGrid.RowHeadersWidthChanged += MetroGrid_RowHeadersWidthChanged;

            metroGrid.ColumnAdded += MetroGrid_ColumnAdded;
            metroGrid.ColumnRemoved += MetroGrid_ColumnRemoved;
            metroGrid.ColumnStateChanged += MetroGrid_ColumnStateChanged;
            metroGrid.ColumnDisplayIndexChanged += MetroGrid_ColumnDisplayIndexChanged;

            Padding = new Padding(0, 2, 0, 0);
        }

        private void MetroGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            OnDataBindingComplete(e);
        }

        private void MetroGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            OnRowLeave(e);
        }

        private void MetroGrid_ChangeDataSource(object sender, EventArgs e)
        {
            OnChangeDataSource(e);
        }

        private void MetroGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            OnRowEnter(e);
        }

        private void MetroGrid_Resize(object sender, EventArgs e)
        {
            OnMetroGridResize(e);
        }

        private void MetroGrid_ColumnDisplayIndexChanged(object sender, System.Windows.Forms.DataGridViewColumnEventArgs e)
        {
            OnColumnDisplayIndexChanged(e);
        }

        private void MetroGrid_ColumnStateChanged(object sender, System.Windows.Forms.DataGridViewColumnStateChangedEventArgs e)
        {
            OnColumnStateChanged(e);
        }

        private void MetroGrid_ColumnRemoved(object sender, System.Windows.Forms.DataGridViewColumnEventArgs e)
        {
            OnColumnRemoved(e);
        }

        private void MetroGrid_ColumnAdded(object sender, System.Windows.Forms.DataGridViewColumnEventArgs e)
        {
            OnColumnAdded(e);
        }

        private void MetroGrid_RowHeadersWidthChanged(object sender, System.EventArgs e)
        {
            OnRowHeadersWidthChanged(e);
        }

        private void MetroGrid_ColumnWidthChanged(object sender, System.Windows.Forms.DataGridViewColumnEventArgs e)
        {
            OnColumnWidthChanged(e);
        }

        private void MetroGrid_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
           OnMetroGridScroll(e);
        }

        private void MetroGrid_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            OnCellValueChanged(e);
        }

        private void MetroGrid_RowsRemoved(object sender, System.Windows.Forms.DataGridViewRowsRemovedEventArgs e)
        {
            OnRowsRemoved(e);
        }

        private void MetroGrid_RowsAdded(object sender, System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            OnRowsAdded(e);
        }


        protected void OnRowLeave(DataGridViewCellEventArgs e)
        {
        }

        protected virtual void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
        }

        protected virtual void OnMetroGridResize(EventArgs e)
        {
        }
        
        protected virtual void OnColumnDisplayIndexChanged(DataGridViewColumnEventArgs e)
        {
        }

        protected virtual void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
        }

        protected virtual void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
        }

        protected virtual void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
        }

        protected virtual void OnRowHeadersWidthChanged(EventArgs e)
        {
        }

        protected virtual void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
        }

        protected virtual void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
        }

        protected virtual void OnMetroGridScroll(ScrollEventArgs e)
        {
        }

        protected virtual void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
        }

        protected virtual void OnRowEnter(DataGridViewCellEventArgs e)
        {
        }

        protected virtual void OnChangeDataSource(EventArgs e)
        {
        }

        protected virtual void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (MetroPaint.GetStylePen(Style) != null)
                e.Graphics.DrawLine(MetroPaint.GetStylePen(Style), new Point(0,0), new Point(Width,0));
        }
    }
}
