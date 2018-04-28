namespace SqlProxyProvider
{
    public interface ISqlProviderDataReader : System.Data.IDataReader
    {
        System.Data.IDataReader DataReader { get; }
    }
}
