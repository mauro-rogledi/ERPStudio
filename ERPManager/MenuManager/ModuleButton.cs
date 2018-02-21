using ERPFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPManager.MenuManager
{
    internal class ModuleButton : MetroFramework.Controls.MetroLink
    {
        public event EventHandler ButtonClick;
        public ApplicationMenuModule MenuModule { get; private set; }

        public ModuleButton()
        {
            FontWeight = MetroFramework.MetroLinkWeight.Light;
            FontSize = MetroFramework.MetroLinkSize.Tall;
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            UseStyleColors = true;
        }

        public ModuleButton(ApplicationMenuModule MenuModule)
        {
            FontWeight = MetroFramework.MetroLinkWeight.Light;
            FontSize = MetroFramework.MetroLinkSize.Tall;
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UseStyleColors = true;
            this.MenuModule = MenuModule;
        }

        protected override void OnClick(EventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, e);

            base.OnClick(e);
        }
    }
}
