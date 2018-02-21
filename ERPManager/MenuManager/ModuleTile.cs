using System;
using System.Drawing;
using ERPFramework;

namespace ERPManager.MenuManager
{
    internal class ModuleTile : MetroFramework.Controls.MetroTile
    {
        public event EventHandler<ApplicationMenuModule> ButtonClick;
        public ApplicationMenuModule MenuModule { get; private set; }

        public ModuleTile()
        {
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            UseStyleColors = true;
        }

        public ModuleTile(ApplicationMenuModule menuModule)
        {
            TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            UseStyleColors = true;
            MenuModule = menuModule;
            Width = 80;
            Height = 80;
            Text = menuModule.Menu;
            TileTextFontSize = MetroFramework.MetroTileTextSize.Small;
            TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;


            if (menuModule.Image != "")
            {
                var ass = System.Reflection.Assembly.Load(menuModule.Namespace.Library);
                var myManager = new System.Resources.ResourceManager(menuModule.Namespace.Library + ".Properties.Resources", ass);
                try
                {
                    TileImage = (Image)myManager.GetObject(menuModule.Image);
                    UseTileImage = true;
                    TileImageAlign = ContentAlignment.MiddleCenter;

                    // btn.Image = Image.FromFile(imagePath);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.Assert(false);
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, MenuModule);

            base.OnClick(e);
        }
    }
}
