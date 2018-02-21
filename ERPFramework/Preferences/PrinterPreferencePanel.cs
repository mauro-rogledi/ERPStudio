using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms.Design;
using ERPFramework.Libraries;
using MetroFramework.Extender;

namespace ERPFramework.Preferences
{
    public partial class PrinterPreferencePanel : PrinterPreferencePanelNoVis
    {
        public PrinterPreferencePanel(string appName)
            : base(appName,  null)
        {
            InitializeComponent();
        }

        public PrinterPreferencePanel()
        {
            InitializeComponent();
        }

        protected override void OnPrepareAuxData()
        {
        }

        protected override bool OnBeforeAddNew()
        {

            Preferences.L_Copies = 1;
            Preferences.E_Copies = 1;
            Preferences.B_Copies = 1;
            Preferences.E_Orientation = Orientation.Landscape;
            Preferences.L_Orientation = Orientation.Portrait;
            Preferences.B_Orientation = Orientation.Portrait;
            return true;
        }

        protected override bool OnAfterAddNew()
        {
            var oPS = new PrinterSettings();
            Preferences.L_PrinterName = oPS.PrinterName;
            Preferences.L_Copies = oPS.Copies;
            Preferences.L_Orientation = oPS.DefaultPageSettings.Landscape
                                    ? Orientation.Landscape
                                    : Orientation.Portrait;

            //Envelope = new PrinterData();
            Preferences.E_PrinterName = oPS.PrinterName;
            Preferences.E_Copies = oPS.Copies;
            Preferences.E_Orientation = oPS.DefaultPageSettings.Landscape
                                    ? Orientation.Landscape
                                    : Orientation.Portrait;

            Preferences.B_PrinterName = oPS.PrinterName;
            Preferences.B_Copies = oPS.Copies;
            Preferences.B_Orientation = oPS.DefaultPageSettings.Landscape
                                    ? Orientation.Landscape
                                    : Orientation.Portrait;
            return base.OnAfterAddNew();
        }

    }

    #region PrinterPreferencePanelNoVis

    public partial class PrinterPreferencePanelNoVis : ERPFramework.Preferences.GenericPreferencePanel<PrinterPref>
    {
        public PrinterPreferencePanelNoVis(string appName, Type[] types = null)
            : base(appName, types)
        {
        }

        public PrinterPreferencePanelNoVis()
        {
        }
    }

    #endregion


    public class PrinterListEditor : UITypeEditor
    {
        private MetroListBox lsbPrinters;
        private IWindowsFormsEditorService _editorService;

        public PrinterListEditor()
        {
            lsbPrinters = new MetroListBox
            {
                UseStyleColors = true,
                FontWeight = MetroFramework.MetroTextBoxWeight.Regular,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                Size = new System.Drawing.Size(180, 280)
            };

            lsbPrinters.SelectedIndexChanged += lsbPrinters_SelectedIndexChanged;

            foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                lsbPrinters.Items.Add(printer.ToString());
        }

        private void lsbPrinters_SelectedIndexChanged(object sender, EventArgs e)
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
                    _editorService.DropDownControl(lsbPrinters);
                    return lsbPrinters.Text;

                }
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }

    #region Source
    class PrinterSourceEditor : UITypeEditor
    {
        private MetroListBox lsbSource;
        private IWindowsFormsEditorService _editorService;
        readonly PrinterSettings ps = new PrinterSettings();

        public PrinterSourceEditor()
        {
            lsbSource = new MetroListBox
            {
                UseStyleColors = true,
                FontWeight = MetroFramework.MetroTextBoxWeight.Regular,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                Size = new System.Drawing.Size(180, 280)
            };
            lsbSource.SelectedIndexChanged += lsbSource_SelectedIndexChanged;
        }

        private void lsbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            _editorService.CloseDropDown();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {
                var printerPref = context.Instance as PrinterPref;

                var name = context.PropertyDescriptor.Name;

                var sourceType = new MetroListBoxManager(lsbSource);
                sourceType.CreateList<int>();

                ps.PrinterName = name.StartsWith("L")
                                    ? printerPref.L_PrinterName
                                    : printerPref.E_PrinterName;

                foreach (PaperSource pps in ps.PaperSources)
                    sourceType.AddValue(pps.RawKind, pps.SourceName);

                _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (_editorService != null)
                {
                    _editorService.DropDownControl(lsbSource);
                    return sourceType.GetValue<int>();
                }
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

    }
    class PrinterSourceConverter : TypeConverter
    {
        readonly PrinterSettings ps = new PrinterSettings();

        public PrinterSourceConverter()
        {
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var name = context.PropertyDescriptor.Name;
            var printerPref = context.Instance as PrinterPref;

            ps.PrinterName = name.StartsWith("L")
                                ? printerPref.L_PrinterName
                                : printerPref.E_PrinterName;

            foreach (PaperSource pps in ps.PaperSources)
                if (pps.SourceName == (string)value)
                    return pps.RawKind;

            return 0;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //if (value != null && value.GetType() == typeof(int))
            //    return languageManager.GetText((int)value);
            var name = context.PropertyDescriptor.Name;
            var printerPref = context.Instance as PrinterPref;

            ps.PrinterName = name.StartsWith("L")
                                ? printerPref.L_PrinterName
                                : printerPref.E_PrinterName;

            foreach (PaperSource pps in ps.PaperSources)
                if (pps.RawKind == (int)value)
                    return pps.SourceName;

            return "";
        }
    }
    #endregion

    #region Size
    class PrinterSizeEditor : UITypeEditor
    {
        private MetroListBox lsbSize;
        private IWindowsFormsEditorService _editorService;
        readonly PrinterSettings ps = new PrinterSettings();

        public PrinterSizeEditor()
        {
            lsbSize = new MetroListBox
            {
                UseStyleColors = true,
                FontWeight = MetroFramework.MetroTextBoxWeight.Regular,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                Size = new System.Drawing.Size(180, 280)
            };
            lsbSize.SelectedIndexChanged += lsbSize_SelectedIndexChanged;
        }

        private void lsbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            _editorService.CloseDropDown();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {
                var printerPref = context.Instance as PrinterPref;

                var name = context.PropertyDescriptor.Name;

                var sizeType = new MetroListBoxManager(lsbSize);
                sizeType.CreateList<int>();

                ps.PrinterName = name.StartsWith("L")
                                    ? printerPref.L_PrinterName
                                    : printerPref.E_PrinterName;

                foreach (PaperSize pps in ps.PaperSizes)
                    sizeType.AddValue(pps.RawKind, pps.PaperName);

                _editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (_editorService != null)
                {
                    _editorService.DropDownControl(lsbSize);
                    return sizeType.GetValue<int>();
                }
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

    }
    class PrinterSizeConverter : TypeConverter
    {
        readonly PrinterSettings ps = new PrinterSettings();

        public PrinterSizeConverter()
        {
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var name = context.PropertyDescriptor.Name;
            var printerPref = context.Instance as PrinterPref;

            ps.PrinterName = name.StartsWith("L")
                                ? printerPref.L_PrinterName
                                : printerPref.E_PrinterName;

            foreach (PaperSize pps in ps.PaperSizes)
                if (pps.PaperName == (string)value)
                    return pps.RawKind;

            return 0;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //if (value != null && value.GetType() == typeof(int))
            //    return languageManager.GetText((int)value);
            var name = context.PropertyDescriptor.Name;
            var printerPref = context.Instance as PrinterPref;

            ps.PrinterName = name.StartsWith("L")
                                ? printerPref.L_PrinterName
                                : printerPref.E_PrinterName;

            foreach (PaperSize pps in ps.PaperSizes)
                if (pps.RawKind == (int)value)
                    return pps.PaperName;

            return "";
        }
    }
    #endregion

    [DataContract]
    public class PrinterPref
    {
        [Editor(typeof(PrinterListEditor),typeof(System.Drawing.Design.UITypeEditor))]
        [DataMember]
        [LocalizedCategory("Letter"), LocalizedDisplayName("PrinterName")]
        public string L_PrinterName { get; set; }

        [DataMember]
        [LocalizedCategory("Letter"), LocalizedDisplayName("Copies")]
        public int L_Copies { get; set; }

        [DataMember]
        [LocalizedCategory("Letter"), LocalizedDisplayName("Orientation")]
        public Orientation L_Orientation { get; set; }

        [Editor(typeof(PrinterSourceEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(PrinterSourceConverter))]
        [DataMember]
        [LocalizedCategory("Letter"), LocalizedDisplayName("Source")]
        public int L_Source { get; set; }

        [Editor(typeof(PrinterSizeEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(PrinterSizeConverter))]
        [DataMember]
        [LocalizedCategory("Letter"), LocalizedDisplayName("PageSize")]
        public int L_Size { get; set; }

        [DataMember]
        [LocalizedCategory("Letter"), LocalizedDisplayName("Collate")]
        public bool L_Collate { get; set; }

        [Editor(typeof(PrinterListEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DataMember]
        [LocalizedCategory("Evelope"), LocalizedDisplayName("PrinterName")]
        public string E_PrinterName { get; set; }

        [DataMember]
        [LocalizedCategory("Evelope"), LocalizedDisplayName("Copies")]
        public int E_Copies { get; set; }

        [DataMember]
        [LocalizedCategory("Evelope"), LocalizedDisplayName("Orientation")]
        public Orientation E_Orientation { get; set; }

        [Editor(typeof(PrinterSourceEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(PrinterSourceConverter))]
        [DataMember]
        [LocalizedCategory("Evelope"), LocalizedDisplayName("Source")]
        public int E_Source { get; set; }

        [DataMember]
        [Editor(typeof(PrinterSizeEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(PrinterSizeConverter))]
        [LocalizedCategory("Evelope"), LocalizedDisplayName("PageSize")]
        public int E_Size { get; set; }

        [DataMember]
        [LocalizedCategory("Evelope"), LocalizedDisplayName("Collate")]
        public bool E_Collate { get; set; }


        [Editor(typeof(PrinterListEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DataMember]
        [LocalizedCategory("Label"), LocalizedDisplayName("PrinterName")]
        public string B_PrinterName { get; set; }

        [DataMember]
        [LocalizedCategory("Label"), LocalizedDisplayName("Copies")]
        public int B_Copies { get; set; }

        [DataMember]
        [LocalizedCategory("Label"), LocalizedDisplayName("Orientation")]
        public Orientation B_Orientation { get; set; }

        [Editor(typeof(PrinterSourceEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(PrinterSourceConverter))]
        [DataMember]
        [LocalizedCategory("Label"), LocalizedDisplayName("Source")]
        public int B_Source { get; set; }

        [Editor(typeof(PrinterSizeEditor), typeof(System.Drawing.Design.UITypeEditor)), TypeConverter(typeof(PrinterSizeConverter))]
        [DataMember]
        [LocalizedCategory("Label"), LocalizedDisplayName("PageSize")]
        public int B_Size { get; set; }

        [DataMember]
        [LocalizedCategory("Label"), LocalizedDisplayName("Collate")]
        public bool B_Collate { get; set; }

    }

    public class PrinterPreferencesManager : PreferencesManager<PrinterPref>
    {
        public PrinterPreferencesManager(string application)
            : base(application, null)
        {
        }
    }
}