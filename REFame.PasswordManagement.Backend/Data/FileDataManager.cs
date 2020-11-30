﻿using System.Collections.Generic;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.Backend.Data
{
    /// <summary>
    ///     A Data-Manager for managing entities in local binary Files
    /// </summary>
    public class FileDataManager : IDataManager<PasswordData>
    {
        private readonly BinaryHelper binaryHelper;
        private BinaryData binaryData;

        public FileDataManager(string path = null)
        {
            binaryHelper = new BinaryHelper(path);
            binaryData = binaryHelper.GetData();
        }

        /// <summary>
        ///     Adds a entity to the binary file
        /// </summary>
        /// <param name="item"></param>
        public void AddData(PasswordData item)
        {
            binaryData.Passwords.Add(item);
            binaryHelper.Write(binaryData);
        }

        /// <summary>
        ///     Removes a entity from the binary file
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
        ///     Loads the data from the file
        /// </summary>
        /// <returns></returns>
        public List<PasswordData> LoadData()
        {
            binaryData = binaryHelper.GetData();
            return binaryData.Passwords;
        }
    }
}