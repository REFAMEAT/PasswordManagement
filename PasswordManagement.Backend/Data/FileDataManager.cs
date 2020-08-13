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
        private BinaryData binaryData;
        private readonly BinaryHelper binaryHelper;

        internal FileDataManager(string path = null)
        {
            binaryHelper = new BinaryHelper(path);
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
            PasswordData itemToDelete = binaryData.Passwords.Find(x => x.Identifier == item.Identifier);
            bool success = binaryData.Passwords.Remove(itemToDelete);
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