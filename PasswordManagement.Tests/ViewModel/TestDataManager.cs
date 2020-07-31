using System.Collections.Generic;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;

namespace PasswordManagement.Tests.ViewModel
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