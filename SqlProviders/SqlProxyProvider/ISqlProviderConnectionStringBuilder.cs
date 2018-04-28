namespace SqlProxyProvider
{
    public interface ISqlProviderConnectionStringBuilder
    {
        string ConnectionString { get; }
        string DataSource { get; set; }
        string UserID { get; set; }
        string InitialCatalog { get; set; }
        string Password { get; set; }
        bool IntegratedSecurity { get; set; }
    }
}
