using System.Collections.Generic;
using PasswordManagement.File.Binary;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;

namespace PasswordManagement.Backend.Data
{
    internal class FileDataManager : IDataManager<PasswordData>
    {
        private BinaryData binaryData;
        private readonly BinaryHelper binaryHelper = new BinaryHelper();

        internal FileDataManager()
        {
            binaryData = binaryHelper.GetData();
        }

        public void AddData(PasswordData item)
        {
            binaryData.Passwords.Add(item);
            binaryHelper.Write(binaryData);
        }

        public bool Remove(PasswordData item)
        {
            bool success = binaryData.Passwords.Remove(item);
            binaryHelper.Write(binaryData);

            return success;
        }

        public List<PasswordData> LoadData()
        {
            binaryData = binaryHelper.GetData();
            return binaryData.Passwords;
        }
    }
}