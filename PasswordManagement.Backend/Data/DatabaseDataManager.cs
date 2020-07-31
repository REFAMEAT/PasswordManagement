using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;
using PasswordManagement.Model.Setting;

namespace PasswordManagement.Backend.Data
{
    internal class DatabaseDataManager : IDataManager<PasswordData>
    {
        private readonly DataSet<PASSWORDDATA> passwordData;

        public DatabaseDataManager(DatabaseData config = null, DataSet<PASSWORDDATA> dataSet = null)
        {
            if (dataSet is null)
            {
                passwordData = new DataSet<PASSWORDDATA>(config); 
            }
            else
            {
                passwordData = dataSet;
            }
        }

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

        public bool Remove(PasswordData item)
        {
            PASSWORDDATA itemToDelete = passwordData.Entities.Find(item.Identifier);
            passwordData.Remove(itemToDelete);
            passwordData.SaveChanges();

            return passwordData.Entities.Find(item.Identifier) == null;
        }
    }
}