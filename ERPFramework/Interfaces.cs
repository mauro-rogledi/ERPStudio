namespace ERPFramework
{
    public interface IFindable
    {
        void Clean();
    }

    public interface IClickable
    {
        void AttachRadar<T>();
        void AttachRadar<T>(params object[] args);

        bool HasRadar { get; }
        bool OpenDocument(string val = "");
    }
}