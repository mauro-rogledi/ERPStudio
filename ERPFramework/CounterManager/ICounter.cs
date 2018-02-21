using System;

namespace ERPFramework.CounterManager
{
    //public interface ICounter
    //{
    //    void SetValue(int val);
    //    void DeleteValue(int val);
    //    void SetValue(string val);
    //    int GetNewIntValue();
    //    string GetNewStringValue();
    //}

    public interface ICounterManager
    {
        void AttachCounterType(int key, DateTime curDate, ERPFramework.Forms.IDocumentBase documentBase);
        void UpdateValue(ERPFramework.Data.DBOperation operation);
        bool StatesButton { get;set;}
        bool IsUpdatable { get; set; }
    }
}
