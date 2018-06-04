using ERPFramework;
using ERPFramework.Forms;
using MetroFramework.Extender;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    public partial class MenuControl : MetroFramework.Controls.MetroUserControl
    {
        [Category(ErpManagerDefaults.PropertyCategory.Event)]

        private MetroFlowLayoutPanel panelModule = null;
        private PanelModules panelMenu = null;
        private PanelPreferences panelPreferences = null;
        private FavoritesMenu favoritesMenu = null;

        public bool CanClose()
        {
            return tbcForms.CanClose();
        }

        public MenuControl()
        {
            favoritesMenu = FavoritesMenu.Load();

            InitializeComponent();
            tbcForms.UseStyleColors = true;
        }

        public void Show(object obj)
        {
            System.Diagnostics.Debug.Assert(obj is IDocumentBase);
            tbcForms.Open(obj as IDocumentBase);
        }

        internal void AddModule(List<ApplicationMenuModule> moduleList)
        {
            var count = 0;
            CreatePanelModule();
            foreach (ApplicationMenuModule module in moduleList)
                if (DisplayMenu(module.MenuFolders))
                {
                    count++;
                    var rows = count / 2 + count % 2;
                    var mb = new ModuleTile(module);
                    mb.ButtonClick += Mb_ButtonClick1;
                    if (count % 2 == 0)
                        this.panelModule.SetFlowBreak(mb, true);

                    panelModule.Controls.Add(mb);
                    panelModule.Height = rows * 86 + 6;
                }
        }

        private void Mb_ButtonClick1(object sender, ApplicationMenuModule e)
        {
            panelMenu.SelectModule(e);
            //panelMenu.StyleManager.Update();
        }

        private void CreatePanelModule()
        {
            panelPreferences = new PanelPreferences
            {
                Visible = false,
                Dock = DockStyle.Bottom,
                Height = 26,
                UseStyleColors = true,
            };
            splitContainer.Panel1.Controls.Add(panelPreferences);
            panelPreferences.FavoritesMenu = favoritesMenu;

            panelMenu = new PanelModules
            {
                UseStyleColors = true,
                Dock = DockStyle.Fill,
                VerticalScrollbar = true,
                AutoScroll = true,
                FavoritesMenu = favoritesMenu
            };
            splitContainer.Panel1.Controls.Add(panelMenu);
            panelMenu.FavoriteClick += PanelMenu_FavoriteClick;

            panelModule = new MetroFlowLayoutPanel
            {
                Dock = DockStyle.Top,
                UseStyleColors = true
            };
            splitContainer.Panel1.Controls.Add(panelModule);
        }

        internal static bool DisplayMenu(List<ApplicationMenuFolder> amf)
        {
            foreach (ApplicationMenuFolder folder in amf)
                if (!DisplayFolder(folder))
                    return false;

            return true;
        }

        internal static bool DisplayFolder(ApplicationMenuFolder amf)
        {
            foreach (ApplicationMenuItem menu in amf.MenuItems)
                if (menu.UserGrant <= GlobalInfo.UserInfo.userType)
                    return true;

            return false;
        }

        private void PanelMenu_FavoriteClick(object sender, bool bIsFavorite)
        {
            var item = (sender as ApplicationMenuItem);

            panelPreferences.AddRemoveButton(item, bIsFavorite);
            panelPreferences.StyleManager.Update();
        }
    }
}
