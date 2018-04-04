namespace SqlProxyProvider
{
    public interface ISqlProviderCommand : System.Data.IDbCommand
    {
        System.Data.IDbCommand Command { get; }
        new ISqlProviderParameterCollection Parameters { get; }
    }
}
