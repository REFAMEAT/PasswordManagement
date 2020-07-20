using System.Collections.Generic;
using PasswordManagement.Database.Model;

namespace PasswordManagement.Backend.Data
{
    public interface IDataManager<T> where T : class
    {
        void AddData(T value);
        List<T> LoadData();
        bool Remove(T item);
    }
}