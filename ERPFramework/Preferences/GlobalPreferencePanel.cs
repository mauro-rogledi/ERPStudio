using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms.Design;
using ERPFramework.Controls;
using MetroFramework.Extender;

namespace ERPFramework.Preferences
{
    public partial class GlobalPreferencePanel : GlobalPreferencePanelNoVis
    {
        public GlobalPreferencePanel(string appName)
            : base(appName)
        {
            InitializeComponent();
        }

        public GlobalPreferencePanel()
        {
            InitializeComponent();
        }
    }

    public class LanguageListEditor : UITypeEditor
    {
        private EnumsManager<Languages> languageManager;
        private MetroListBox lsbLanguages;
        private IWindowsFormsEditorService _editorService;

        public LanguageListEditor()
        {
            lsbLanguages = new MetroListBox();
            languageManager = new EnumsManager<Languages>(lsbLanguages, "");
            lsbLanguages.Size = new System.Drawing.Size(180, 80);
            lsbLanguages.SelectedIndexChanged += LsbLanguages_SelectedIndexChanged;
        }

        private void LsbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            _editorService.CloseDropDown();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {

                _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (_editorService != null)
                {
                    int e = (int)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);

                    //lsbLanguages.Text = languageManager.GetText(e);
                    _editorService.DropDownControl(lsbLanguages);
                    return lsbLanguages.SelectedIndex;

                }
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

    }

    class LanguageConverter : TypeConverter
    {
        EnumsManager<Languages> languageManager;
        public LanguageConverter()
        {
            languageManager = new EnumsManager<Languages>("");
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(int))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value != null && value.GetType() == typeof(int))
                return languageManager.GetText((int)value);

            return null; // base.ConvertFrom(context, culture, value);
        }
    }


    #region GlobalPreferencePanelNoVis

    public partial class GlobalPreferencePanelNoVis : ERPFramework.Preferences.GenericPreferencePanel<GlobalPreferences>
    {
        public GlobalPreferencePanelNoVis(string appName)
            : base(appName)
        {
        }

        public GlobalPreferencePanelNoVis()
        {
        }
    }

    #endregion

    [DataContract]
    public class GlobalPreferences
    {
        [DataMember]
        [DefaultValue(false)]
        public bool ShowControlBox { get; set; } = false;

        [DataMember]
        [DefaultValue(false)]
        public bool ExpandSearchWindow { get; set; } = false;
        

        [DataMember]
        [LocalizedCategory("Localization"), LocalizedDisplayName("ForceLanguage")]
        public bool ForceLanguage { get; set; }

        [   Editor(typeof(LanguageListEditor), 
            typeof(System.Drawing.Design.UITypeEditor)),
            TypeConverter(typeof(LanguageConverter)),
            LocalizedCategory("Localization")]
        [DataMember]
        [LocalizedDisplayName("Language")]
        public int Language { get; set; }

        [DataMember]
        [LocalizedDisplayName("Color")]
        public MetroFramework.MetroColorStyle CustmColor { get; set; }

        [DataMember]
        public bool UseTransaction { get; set; }

        public GlobalPreferences()
        {
        }
    }
}