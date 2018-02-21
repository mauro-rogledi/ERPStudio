using System.ComponentModel;
using MetroFramework.Extender;

namespace ERPFramework.Controls
{
    public enum MetroToolbarState { New, Edit, Browse, Found, Find, Run }

    public class ExMetroToolbar : MetroToolbar
    {
        #region Public Properties

        private MetroToolbarState _metroToolbarState;
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(MetroToolbarState.Browse)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroToolbarState Status
        {
            get { return _metroToolbarState; }
            set
            {
                _metroToolbarState = value;
                ChangeToolbar(value);
            }
        }

        private bool buttonPrintVisible = true;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ButtonPrintVisible { get { return buttonPrintVisible; } set { buttonPrintVisible = value; ChangeToolbar(MetroToolbarState.Browse); } }

        private bool buttonPreviewVisible = true;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ButtonPreviewVisible { get { return buttonPreviewVisible; } set { buttonPreviewVisible = value; ChangeToolbar(MetroToolbarState.Browse); } }

        private bool buttonAddOnVisible = true;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ButtonAddonVisible { get { return buttonAddOnVisible; } set { buttonAddOnVisible = value; ChangeToolbar(MetroToolbarState.Browse); } }

        private bool buttonPrefVisible = true;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ButtonPrefVisible { get { return buttonPrefVisible; } set { buttonPrefVisible = value; ChangeToolbar(MetroToolbarState.Browse); } }

        private bool buttonDeleteVisible = true;
        [Category(ExtenderDefaults.PropertyCategory.Appearance)]
        [DefaultValue(false)]
        public bool ButtonDeleteVisible { get { return buttonDeleteVisible; } set { buttonDeleteVisible = value; ChangeToolbar(MetroToolbarState.Browse); } }
        #endregion

        private void ChangeToolbar(MetroToolbarState status = MetroToolbarState.Browse)
        {
            IMetroToolBarButton btn = null;
            
            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.New);
            if (btn!=null)
                btn.IsVisible = status == MetroToolbarState.Browse || status == MetroToolbarState.Found;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Edit);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Found;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Delete);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Found && buttonDeleteVisible;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Undo);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.New || status == MetroToolbarState.Edit || status == MetroToolbarState.Find;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Save);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.New || status == MetroToolbarState.Edit;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Search);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Browse || status == MetroToolbarState.Found || status == MetroToolbarState.Find;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Filter);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Browse || status == MetroToolbarState.Found;

            btn = GetButton<MetroToolbarDropDownButton>(MetroToolbarButtonType.Preview);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Found && buttonPreviewVisible;

            btn = GetButton<MetroToolbarDropDownButton>(MetroToolbarButtonType.Print);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Found && buttonPrintVisible;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Preference);
            if (btn != null)
                btn.IsVisible = status == MetroToolbarState.Browse || status == MetroToolbarState.Found && buttonPrefVisible;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.AddOn);
            if (btn != null)
                btn.IsVisible = buttonAddOnVisible;

            btn = GetButton<MetroToolbarPushButton>(MetroToolbarButtonType.Execute);
            if (btn != null)
            {
                var pushButton = btn as MetroToolbarPushButton;
                pushButton.Image = status == MetroToolbarState.Browse
                                  ? Properties.Resources.Play32
                                  : Properties.Resources.Stop32;

                pushButton.NoFocusImage = status == MetroToolbarState.Browse
                                    ? Properties.Resources.Play32g
                                    : Properties.Resources.Stop32g;
            }
        }

    }
}
