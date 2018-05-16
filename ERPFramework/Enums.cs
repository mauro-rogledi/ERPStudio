namespace ERPFramework
{
    public enum InputType { E_Numeric = 0, E_Text = 1 }

    public enum ButtonClicked { Computer, User, Application }

    public enum Orientation { Portrait = 0, Landscape = 1 }

    public enum Languages
    {
        it_IT,
        en_US
    }

    public enum PrintType { Letter, Envelope, Label, Pdf, Email }

    public enum Findable { YES, NO }

    public enum ZoomLevel
    {
        E_Page_Width = 1,
        E_Whole_Page = 2,
        E_10Perc = 10,
        E_20Perc = 20,
        E_40Perc = 40,
        E_50Perc = 50,
        E_70Perc = 70,
        E_100Perc = 100,
        E_200Perc = 200,
        E_400Perc = 400
    }

    public enum PageRange
    {
        E_Page_All = 0,
        E_Page_Current = 1,
        E_Page_Range = 2
    }

    #region Enums

    public enum UserType
    {
        Guest, User, SuperUser, Administrator
    }

    public enum UserStatus
    {
        Found, NotFound, Expired, Locked
    }

    public enum DocumentType
    {
        Document, FastDocument, Batch, Report, Preview, Preferences
    }


    public enum AuthenticationMode
    {
        Windows, Sql
    }

    #endregion
}