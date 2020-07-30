using System.Collections.Generic;
using PasswordManagement.File.Binary;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;

namespace PasswordManagement.Backend.Data
{
    /// <summary>
    /// A Data-Manager for managing entities in local binary Files
    /// </summary>
    internal class FileDataManager : IDataManager<PasswordData>
    {
        private readonly BinaryHelper binaryHelper = new BinaryHelper();
        private BinaryData binaryData;

        internal FileDataManager()
        {
            binaryData = binaryHelper.GetData();
        }

        /// <summary>
        /// Adds a entity to the binary file
        /// </summary>
        /// <param name="item"></param>
        public void AddData(PasswordData item)
        {
            binaryData.Passwords.Add(item);
            binaryHelper.Write(binaryData);
        }

        /// <summary>
        /// Removes a entity from the binary file
        /// </summary>
        /// <param name="item">The entity to delete</param>
        /// <returns></returns>
        public bool Remove(PasswordData item)
        {
            bool success = binaryData.Passwords.Remove(item);
            binaryHelper.Write(binaryData);

            return success;
        }

        /// <summary>
        /// Loads the data from the file
        /// </summary>
        /// <returns></returns>
        public List<PasswordData> LoadData()
        {
            binaryData = binaryHelper.GetData();
            return binaryData.Passwords;
        }
    }
}