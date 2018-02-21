using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;

namespace ERPFramework.Controls
{
    public class MetroGridHeaderPanel : MetroFramework.Controls.MetroUserControl
    {
        private ExtendedDataGridView metroGrid = null;
        private bool eventInAction = false;

        private bool showSelectUnselect = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public bool ShowSelectUnselect
        {
            get { return showSelectUnselect; }
            set
            {
                showSelectUnselect = value;
                if (btnSelect != null)
                    btnSelect.Visible = value;
            }
        }

        private bool showFilter = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public bool ShowFilter
        {
            get { return showFilter; }
            set { showFilter = value;
                if (lblFilter!=null)
                {
                    txtFilter.Visible = value;
                    lblFilter.Visible = value;
                }
            }
        }

        private MetroCheckBox btnSelect;
        private MetroTextBox txtFilter;
        private MetroLabel lblFilter;

        public MetroGridHeaderPanel()
        { }

        public MetroGridHeaderPanel(ExtendedDataGridView metroGrid)
        {
            this.metroGrid = metroGrid;
            Dock = System.Windows.Forms.DockStyle.Top;
            Height = 32;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);

            metroGrid.RowsAdded += MetroGrid_RowsAdded;
            metroGrid.RowsRemoved += MetroGrid_RowsRemoved;
            metroGrid.CellValueChanged += MetroGrid_CellValueChanged;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            txtFilter = new MetroTextBox
            {
                Visible = showFilter,
                Dock = System.Windows.Forms.DockStyle.Left,
                FontWeight = MetroTextBoxWeight.Regular,
                FontSize = MetroTextBoxSize.Medium,
                UseStyleColors = true,
                Width = 160,
                Height = 25
            };
            Controls.Add(txtFilter);

            lblFilter = new MetroLabel
            {
                Text = Properties.Resources.Search,
                Visible = showFilter,
                Dock = System.Windows.Forms.DockStyle.Left,
                FontWeight = MetroLabelWeight.Regular,
                Height = 32,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                UseStyleColors = true
            };
            Controls.Add(lblFilter);
            var size = TextRenderer.MeasureText(lblFilter.Text, lblFilter.Font);
            lblFilter.Width = size.Width+10;

            btnSelect = new MetroCheckBox
            {
                Text = Properties.Resources.SelectAll,
                Visible = showSelectUnselect,
                Checked = false,
                Height = 32,
                Dock = System.Windows.Forms.DockStyle.Left,
                FontWeight = MetroCheckBoxWeight.Regular,
                FontSize = MetroCheckBoxSize.Medium,
                UseStyleColors = true
            };

            Controls.Add(btnSelect);

            Controls.SetChildIndex(lblFilter, 5);
            Controls.SetChildIndex(txtFilter, 1);
            Controls.SetChildIndex(btnSelect, 2);

            btnSelect.CheckStateChanged += BtnSelect_CheckStateChanged;
            txtFilter.KeyUp += TxtFilter_KeyUp;

        }

        private void TxtFilter_KeyUp(object sender, KeyEventArgs e)
        {
             if (txtFilter.Text.IsEmpty())
            {
                metroGrid.DataView.RowFilter = "";
            }
            else
                if (e.KeyCode == Keys.Space)
                metroGrid.DataView.RowFilter = CreateFilter(txtFilter.Text.Split(' '));
        }

        private string CreateFilter(string[] filters)
        {
            var result = string.Empty;
            var concat = "AND ";
            foreach (DataGridViewColumn col in metroGrid.Columns)
            {
                foreach (string filter in filters)
                {
                    if (col.Visible && filter.Length > 0)
                    {
                        result = result.SeparConcat($"Convert({col.Name}, 'System.String') LIKE '%{filter}%'", concat);
                        concat = "AND ";
                    }
                }
                concat = "OR ";
            }
            return result;
        }

        private void BtnSelect_CheckStateChanged(object sender, System.EventArgs e)
        {
            if (metroGrid.SelectColumnName == string.Empty || eventInAction)
                return;

            eventInAction = true;
            foreach (DataGridViewRow dgw in metroGrid.Rows)
                if (!dgw.IsNewRow)
                    dgw.SetValue<bool>(metroGrid.SelectColumnName, btnSelect.Checked);

            eventInAction = false;
        }

        private void MetroGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ChangeGlobalCheckState(e.ColumnIndex);
        }

        private void MetroGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ChangeGlobalCheckState(-1);
        }

        private void MetroGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ChangeGlobalCheckState(-1);
        }

        private void ChangeGlobalCheckState(int columnIndex)
        {
            if (eventInAction ||
                btnSelect == null ||
                metroGrid.SelectColumnName.IsEmpty() ||
                (metroGrid.Columns[metroGrid.SelectColumnName].Index != columnIndex && columnIndex >=0))
                return;

            eventInAction = true;
            var bFirstRow = true;

            foreach (DataGridViewRow dgw in metroGrid.Rows)
            {
                if (dgw.IsNewRow)
                    continue;
                if (bFirstRow)
                {
                    btnSelect.CheckState = CheckState.Checked;
                    btnSelect.Checked = dgw.GetValue<bool>(metroGrid.SelectColumnName);
                    bFirstRow = false;
                }
                else
                {
                    if (btnSelect.Checked != dgw.GetValue<bool>(metroGrid.SelectColumnName))
                        btnSelect.CheckState = CheckState.Indeterminate;
                }
            }
            eventInAction = false;
        }
    }
}
