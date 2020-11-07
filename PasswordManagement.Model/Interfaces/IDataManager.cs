using System.Collections.Generic;

namespace REFame.PasswordManagement.Model.Interfaces
{
    public interface IDataManager<T> where T : class
    {
        void AddData(T value);
        List<T> LoadData();
        bool Remove(T item);
    }
}