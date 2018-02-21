using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;

namespace ERPFramework.Controls
{
    public enum MetroGridTotalType { Text, Sum, Average, Max, Min, Count }
    public enum MetroGridColumnType { Text, Int, Double, Date }

    public partial class MetroGridTotalFooterPanel : MetroGridFooterPanel
    {
        public MetroGridTotalFooterPanel()
        {
            InitializeComponent();
        }

        public MetroGridTotalFooterPanel(ExtendedDataGridView datagrid)
            : base(datagrid)
        {
            InitializeComponent();
            Height = metroGrid.RowTemplate.Height+4;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CreateTotalsControl();
            RecalculateTotals();
        }

        #region Calculating
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            RecalculateTotals();
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            RecalculateTotals();
        }

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
            RecalculateTotals();
        }

        protected override void OnChangeDataSource(EventArgs e)
        {
            RecalculateTotals();
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
           foreach(MetroGridTotalColumn totalCol in metroGrid.TotalColumns)
            {
                switch(totalCol.TotalType)
                {
                    case MetroGridTotalType.Text:
                        continue;
                    case MetroGridTotalType.Sum:
                        if (totalCol.TotalColumnType == MetroGridColumnType.Double || 
                            totalCol.TotalColumnType == MetroGridColumnType.Int )
                                CalculateSum(totalCol.ColumnName, totalCol.Control);
                        break;
                    case MetroGridTotalType.Average:
                        if (totalCol.TotalColumnType == MetroGridColumnType.Double ||
                            totalCol.TotalColumnType == MetroGridColumnType.Int)
                            CalculateAverage(totalCol.ColumnName, totalCol.Control);
                        break;
                    case MetroGridTotalType.Max:
                        CalculateMax(totalCol.TotalColumnType, totalCol.ColumnName, totalCol.Control);
                        break;
                    case MetroGridTotalType.Count:
                        CalculateCount(totalCol.ColumnName, totalCol.Control);
                        break;
                }
            }
        }

        private void CalculateSum(string columnName, MetroGridTotalControl control)
        {
            var total = 0.0;
            foreach (DataGridViewRow dgw in metroGrid.Rows)
                if (!dgw.IsNewRow)
                    total += dgw.GetValue<double>(columnName);

            if (control != null)
                control.Double = total;
        }

        private void CalculateCount(string columnName, MetroGridTotalControl control)
        {
            if (control != null)
                control.Int = metroGrid.RowCount;
        }

        private void CalculateAverage(string columnName, MetroGridTotalControl control)
        {
            var total = 0.0;
            foreach (DataGridViewRow dgw in metroGrid.Rows)
                if (!dgw.IsNewRow)
                    total += dgw.GetValue<double>(columnName);

            if (control != null)
                control.Double = (total / TotalRow);
        }

        private void CalculateMax(MetroGridColumnType totalColumnType, string columnName, MetroGridTotalControl control)
        {
            var maxInt = int.MinValue;
            var maxDouble = double.MinValue;
            var maxDate = DateTime.MinValue;
            var maxString = string.Empty;

            switch(totalColumnType)
            {
                case MetroGridColumnType.Double:
                    foreach (DataGridViewRow dgw in metroGrid.Rows)
                        if (!dgw.IsNewRow && dgw.GetValue<double>(columnName) > maxDouble)
                            maxDouble = dgw.GetValue<double>(columnName);
                    if (control != null)
                        control.Double = maxDouble;
                    break;

                case MetroGridColumnType.Int:
                    foreach (DataGridViewRow dgw in metroGrid.Rows)
                        if (!dgw.IsNewRow && dgw.GetValue<int>(columnName) > maxInt)
                            maxDouble = dgw.GetValue<int>(columnName);
                    if (control != null)
                        control.Int = maxInt;
                    break;

                case MetroGridColumnType.Date:
                    foreach (DataGridViewRow dgw in metroGrid.Rows)
                        if (!dgw.IsNewRow && dgw.GetValue<DateTime>(columnName) > maxDate)
                            maxDouble = dgw.GetValue<int>(columnName);
                    if (control != null)
                        control.DateTime = maxDate;
                    break;
            }
        }

        internal void SetTotalColumn(string name, object value)
        {
            var col = metroGrid.TotalColumns.FirstOrDefault(p => p.ColumnName == name);
            if (col == null)
                return;

            if (value is int)
            {
                col.Control.Int = (int)value;
            }
            else
                if (value is double)
            {
                col.Control.Double = (double)value;
            }
            else
                col.Text = value.ToString();
        }

        private int TotalRow
        {
            get
            {
                int tot = 0;
                foreach (DataGridViewRow dgw in metroGrid.Rows)
                    if (!dgw.IsNewRow)
                        tot++;
                return tot == 0 ? 1 : tot;
            }
        }
        #endregion

        #region Resizing
        protected override void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            base.OnColumnStateChanged(e);
            ResizeTotalColumns();
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            ResizeTotalColumns();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeTotalColumns();

        }

        protected override void OnMetroGridScroll(ScrollEventArgs e)
        {
            base.OnMetroGridScroll(e);
        }

        protected override void OnMetroGridResize(EventArgs e)
        {
            base.OnMetroGridResize(e);
            ResizeTotalColumns();
        }

        private void ResizeTotalColumns()
        {
            var rowHeaderWidth = metroGrid.RowHeadersVisible ? metroGrid.RowHeadersWidth - 1 : 0;
            var sumLabelWidth = metroGrid.RowHeadersVisible ? metroGrid.RowHeadersWidth - 1 : 0;
            var curPos = rowHeaderWidth;

            foreach (DataGridViewColumn dgvColumn in DisplayedColumns)
            {
                var from = curPos - metroGrid.HorizontalScrollingOffset;
                var width = dgvColumn.Width;

                if (dgvColumn.Visible)
                    curPos += dgvColumn.Width;

                var totalcolumn = metroGrid.TotalColumns.Find(p => { return p.ColumnName == dgvColumn.Name; });
                if (totalcolumn == null || totalcolumn.Column == null || totalcolumn.Control == null)
                    continue;

                if (!totalcolumn.Column.Visible)
                {
                    totalcolumn.Control.Visible = false;
                    continue;
                }
                var oldBounds = totalcolumn.Control.Bounds;

                if (width < 4)
                {
                    if (totalcolumn.Control.Visible)
                        totalcolumn.Control.Visible = false;
                }
                else
                {
                    if (this.RightToLeft == RightToLeft.Yes)
                        from = this.Width - from - width;


                    if (totalcolumn.Control.Left != from || totalcolumn.Control.Width != width)
                        totalcolumn.Control.SetBounds(from, 0, width, 0, BoundsSpecified.X | BoundsSpecified.Width);

                    if (!totalcolumn.Control.Visible)
                        totalcolumn.Control.Visible = true;
                }

 
                if (oldBounds != totalcolumn.Control.Bounds)
                    totalcolumn.Control.Invalidate();

            }
        }

        private void CreateTotalsControl()
        {
            foreach (DataGridViewColumn dgvColumn in DisplayedColumns)
            {
                var totalcolumn = metroGrid.TotalColumns.Find(p => { return p.ColumnName == dgvColumn.Name; });
                if (totalcolumn != null)
                {
                    var ml = new MetroGridTotalControl
                    {
                        Top = 2,
                        Height = metroGrid.RowTemplate.Height,
                        UseStyleColors = totalcolumn.UseStyleColor,
                        Visible = dgvColumn.Visible,
                        Text = totalcolumn.Text,
                        FontSize = totalcolumn.FontSize,
                        FontWeight = totalcolumn.FontWeight,
                        TextAlign = totalcolumn.TextAlign,
                        MaxWholeDigits = totalcolumn.MaxWholeDigits,
                        MaxDecimalPlaces = totalcolumn.MaxDecimalPlaces,
                        AllowNegative = totalcolumn.AllowNegative,
                        ShowPrefix = totalcolumn.ShowPrefix,
                        AllowGroupSeparator = totalcolumn.AllowGroupSeparator,
                        CurrencySimbol = totalcolumn.CurrencySimbol
                    };
                    Controls.Add(ml);
                    totalcolumn.Column = dgvColumn;
                    totalcolumn.Control = ml;
                }
            }
        }

        private List<DataGridViewColumn> DisplayedColumns
        {
            get
            {
                var result = new List<DataGridViewColumn>();
                var column = metroGrid.Columns.GetFirstColumn(DataGridViewElementStates.None);
                if (column == null)
                    return result;

                result.Add(column);
                while ((column = metroGrid.Columns.GetNextColumn(column, DataGridViewElementStates.None, DataGridViewElementStates.None)) != null)
                    result.Add(column);

                return result;
            }
        }
        #endregion
    }

    [Serializable()]
    public class MetroGridTotalColumn : Component
    {
        [Category(ErpFrameworkDefaults.PropertyCategory.Design)]
        public string ColumnName { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public MetroGridTotalType TotalType { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public bool Visible { get; set; }

        [Localizable(true)]
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public string Text { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public MetroLabelWeight FontWeight { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public MetroLabelSize FontSize{ get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColor { get; set; }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public MetroGridColumnType TotalColumnType { get; set; }

        [Category("MetroFramework Extender Numeric")]
        [Description("The maximum number of digits allowed left of the decimal point.")]
        public int MaxWholeDigits { get; set; } = 0;

        [Category("MetroFramework Extender Numeric")]
        [Description("The maximum number of digits allowed right of the decimal point.")]
        public int MaxDecimalPlaces { get; set; } = 0;

        [Category("MetroFramework Extender Numeric")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowNegative { get; set; } = false;

        private bool _showPrefix = false;
        [Category("MetroFramework Extender Numeric")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public bool ShowPrefix
        {
            get { return _showPrefix; }
            set { _showPrefix = value; _prefixLen = _showPrefix ? _currencySimbol.Length : 0; }
        }

        [Category("MetroFramework Extender Numeric")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public string CurrencySimbol
        {
            get { return _currencySimbol; }
            set { _currencySimbol = value; _prefixLen = _currencySimbol.Length; }
        }

        [Category("MetroFramework Extender Numeric")]
        [Description("Allow insert of group separator in number")]
        public bool AllowGroupSeparator { get; set; } = false;

        [Category("MetroFramework Extender Text")]
        public System.Drawing.ContentAlignment TextAlign { get; set; } = System.Drawing.ContentAlignment.MiddleLeft;

        [Browsable(false)]
        public int Int { get; set; }
        [Browsable(false)]
        public double Double { get; set; }
        [Browsable(false)]
        public DataGridViewColumn Column { get; set; }
        [Browsable(false)]
        public MetroGridTotalControl Control { get; set; }

        private string _currencySimbol = NumberFormatInfo.CurrentInfo.CurrencySymbol;
        private int _prefixLen;
    }
}
