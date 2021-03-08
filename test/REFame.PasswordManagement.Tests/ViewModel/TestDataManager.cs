using System.Collections.Generic;
using System.Threading.Tasks;
using REFame.PasswordManagement.Data.Contracts;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Tests.ViewModel
{
    internal class TestDataManager : IDataManager<PasswordData>
    {
        public readonly List<PasswordData> PasswordData = new List<PasswordData>();

        public void AddData(PasswordData value)
        {
            PasswordData.Add(value);
        }

        public List<PasswordData> LoadData()
        {
            return PasswordData;
        }

        public bool Remove(PasswordData item)
        {
            return PasswordData.Remove(item);
        }

        public Task AddDataAsync(PasswordData value)
        {
            PasswordData.Add(value);
            return Task.CompletedTask;
        }

        public Task<List<PasswordData>> LoadDataAsync()
        {
            return Task.FromResult(PasswordData);
        }

        public Task<bool> RemoveAsync(PasswordData item)
        {
            return Task.FromResult(PasswordData.Remove(item));
        }
    }
}