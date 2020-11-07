using System.Collections.Generic;
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
    }
}