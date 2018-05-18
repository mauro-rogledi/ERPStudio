using System;
using System.ComponentModel;

namespace ERPFramework.Controls
{
    public class MetroGridContainerPanel : MetroFramework.Controls.MetroPanel
    {
        #region Fields
        private bool showSelectUnselect = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowSelectUnselect
        {
            get { return showSelectUnselect; }
            set
            {
                showSelectUnselect = value;
                if (headerPanel!=null) headerPanel.ShowSelectUnselect= value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue("")]

        private bool showFilter = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public bool ShowFilter
        {
            get { return showFilter; }
            set
            {
                showFilter = value;
                if (headerPanel != null) headerPanel.ShowFilter = showFilter; 
            }
        }

        private bool showHeaderPanel = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public bool ShowHeaderPanel
        {
            get { return showHeaderPanel; }
            set
            {
                showHeaderPanel = value;
                ChangeHeaderPanelVisible(value);
            }
        }

        private bool showFooterPanel = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ShowFooterPanel
        {
            get { return showFooterPanel; }
            set { showFooterPanel = value; ChangeFooterPanelVisible(value); }
        }

        [Browsable(false)]
        [DefaultValue(null)]
        public Type FooterPanelType { get; set; }

        public void AddFooterPanel<T>()
        {
            System.Diagnostics.Debug.Assert(footerPanel == null);
            footerPanel = Activator.CreateInstance(typeof(T), new object[] { metroGrid }) as MetroGridFooterPanel;
            footerPanel.Visible = showFooterPanel;
            Controls.Add(footerPanel);
            footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        }

        public ExtendedDataGridView DataGrid
        {
            set
            {
                metroGrid = value;
                SetDefaults(metroGrid);
                CreateHeaderPanel(metroGrid);
            }
        }

        private MetroGridHeaderPanel headerPanel = null;
        private ExtendedDataGridView metroGrid = null;
        private MetroGridFooterPanel footerPanel = null;
        #endregion

        public MetroGridContainerPanel()
        {
            Disposed += MetroGridContainerPanel_Disposed;
        }

        private void MetroGridContainerPanel_Disposed(object sender, EventArgs e)
        {
            if (footerPanel != null)
                footerPanel.Dispose();
            if (headerPanel != null)
                headerPanel.Dispose();
        }


        public MetroGridContainerPanel(ExtendedDataGridView datagrid)
        {
            metroGrid = datagrid;
            SetDefaults(datagrid);
            CreateHeaderPanel(datagrid);
        }

        private void CreateHeaderPanel(ExtendedDataGridView datagrid)
        {
            headerPanel = new MetroGridHeaderPanel(datagrid)
            {
                Visible = showHeaderPanel,
                ShowFilter = showFilter
            };

            Controls.Add(headerPanel);
        }

        private void ChangeHeaderPanelVisible(bool value)
        {
            if (headerPanel != null)
            {
                headerPanel.Visible = value;
                metroGrid.BringToFront();
            }
        }

        private void ChangeFooterPanelVisible(bool value)
        {
            if (footerPanel == null && FooterPanelType != null)
            {
                footerPanel = Activator.CreateInstance(FooterPanelType, new object[] { metroGrid }) as MetroGridFooterPanel;
                Controls.Add(footerPanel);
            }

            if (footerPanel != null)
                footerPanel.Visible = value;
        }

        private void SetDefaults(ExtendedDataGridView datagrid)
        {
            Dock = datagrid.Dock;
            Anchor = datagrid.Anchor;
            Padding = datagrid.Padding;
            Margin = datagrid.Margin;
            Top = datagrid.Top;
            Left = datagrid.Left;
            BorderStyle = this.BorderStyle;
            Width = datagrid.Width;
            Height = datagrid.Height;

            ShowHeaderPanel = datagrid.ShowHeaderPanel;
            showFooterPanel = datagrid.ShowFooterPanel;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
           // metroGrid.BringToFront();
        }

        internal void SetTotalColumn(string name, object value)
        {
            if (footerPanel!=null && footerPanel is MetroGridTotalFooterPanel)
            {
                (footerPanel as MetroGridTotalFooterPanel).SetTotalColumn(name, value);
            }
        }
    }
}
