using System;
using System.Collections.Generic;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;

namespace PasswordManagement.Backend.Data
{
    /// <summary>
    /// A Data-Manager for the connection to the Database
    /// </summary>
    internal class DatabaseDataManager : IDataManager<PasswordData>
    {
        readonly DataSet<PASSWORDDATA> passwordData = new DataSet<PASSWORDDATA>();

        /// <summary>
        /// Adds a data set to the Database and saves it
        /// </summary>
        /// <param name="value"></param>
        public void AddData(PasswordData value)
        {
            PASSWORDDATA data = new PASSWORDDATA()
            {
                PWID = Guid.NewGuid().ToString(),
                PWDATA = value.Password,
                PWCOMMENT = value.Comments,
                PWDESCRIPTION = value.Description,
                USERUSID = Globals.CurrentUserId
            };

            passwordData.Entities.Add(data);
            passwordData.SaveChanges();
        }

        /// <summary>
        /// Loads all data sets from the Database
        /// </summary>
        /// <returns></returns>
        public List<PasswordData> LoadData()
        {
            List<PasswordData> dataDisplay = new List<PasswordData>();

            foreach (PASSWORDDATA x in passwordData.Entities)
            {
                if (x.USERUSID == Globals.CurrentUserId)
                {
                    dataDisplay.Add(new PasswordData()
                    {
                        Identifier = x.PWID,
                        Password = x.PWDATA,
                        Comments = x.PWCOMMENT,
                        Description = x.PWDESCRIPTION
                    }); 
                }
            }

            return dataDisplay;
        }

        /// <summary>
        /// Removes a Data Set from the Database and saves it
        /// </summary>
        /// <param name="item">The item to delete</param>
        /// <returns></returns>
        public bool Remove(PasswordData item)
        {
            PASSWORDDATA itemToDelete = passwordData.Entities.Find(item.Identifier);
            passwordData.Remove(itemToDelete);
            passwordData.SaveChanges();

            return passwordData.Entities.Find(item.Identifier) == null;
        }
    }
}