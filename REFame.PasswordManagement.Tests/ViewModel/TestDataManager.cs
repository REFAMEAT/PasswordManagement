using System.Collections.Generic;
using System.Threading.Tasks;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.Tests.ViewModel
{
    internal class TestDataManager : IDataManager<PasswordData>
    {
        public List<PasswordData> passwordDatas = new List<PasswordData>();

        public void AddData(PasswordData value)
        {
            passwordDatas.Add(value);
        }

        public List<PasswordData> LoadData()
        {
            return passwordDatas;
        }

        public bool Remove(PasswordData item)
        {
            return passwordDatas.Remove(item);
        }

        public async Task AddDataAsync(PasswordData value)
        {
            passwordDatas.Add(value);
        }

        public async Task<List<PasswordData>> LoadDataAsync()
        {
            return passwordDatas;
        }

        public async Task<bool> RemoveAsync(PasswordData item)
        {
            return passwordDatas.Remove(item);
        }
    }
}