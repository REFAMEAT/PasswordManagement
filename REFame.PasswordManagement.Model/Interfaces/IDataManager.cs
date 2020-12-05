using System.Collections.Generic;
using System.Threading.Tasks;

namespace REFame.PasswordManagement.Model.Interfaces
{
    public interface IDataManager<T> where T : class
    {
        void AddData(T value);
        List<T> LoadData();
        bool Remove(T item);

        Task AddDataAsync(T value);
        Task<List<T>> LoadDataAsync();
        Task<bool> RemoveAsync(T item);
    }
}