using System.Collections.Generic;

namespace PasswordManagement.Backend.Data
{
    internal interface IDataManager<T> where T : class
    {
        void AddData(T value);
        List<T> LoadData();
        bool Remove(T item);
    }
}