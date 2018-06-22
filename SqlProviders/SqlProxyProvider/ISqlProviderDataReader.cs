namespace SqlProxyProvider
{
    public interface ISqlProviderDataReader
    {
        System.Data.IDataReader DataReader { get; }
    }
}
