using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public interface IPropagate
    {
        void PropagateMouseDoubleClick(System.Windows.Forms.Control c);
    }

    public interface IMetroToolBarButton
    {
        //private EventHandler<MetroToolbarButtonDock> buttonDockChanged;

        bool IsVisible { get; set; }

        MetroToolbarButtonType ButtonType { get; set; }
        bool Visible { get; set; }
        DockStyle Dock { get; set; }
        string Name { get; set; }
    }
}
