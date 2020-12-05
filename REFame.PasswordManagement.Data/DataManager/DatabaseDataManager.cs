using System.Collections.Generic;
using System.Threading.Tasks;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Data.DataManager
{
    /// <summary>
    ///     A Data-Manager for the connection to the Database
    /// </summary>
    public class DatabaseDataManager : IDataManager<PasswordData>
    {
        private readonly DataSet<PASSWORDDATA> passwordData;

        public DatabaseDataManager(DataSet<PASSWORDDATA> dataSet = null)
        {
            passwordData = dataSet ?? new DataSet<PASSWORDDATA>();
        }

        /// <summary>
        ///     Adds a data set to the Database and saves it
        /// </summary>
        /// <param name="value"></param>
        public void AddData(PasswordData value)
        {
            var data = new PASSWORDDATA
            {
                PWID = value.Identifier,
                PWDATA = value.Password,
                PWCOMMENT = value.Comments,
                PWDESCRIPTION = value.Description,
                USERUSID = Globals.CurrentUserId
            };

            passwordData.Entities.Add(data);
            passwordData.SaveChanges();
        }

        /// <summary>
        ///     Loads all data sets from the Database
        /// </summary>
        /// <returns></returns>
        public List<PasswordData> LoadData()
        {
            var dataDisplay = new List<PasswordData>();

            foreach (PASSWORDDATA x in passwordData.Entities)
            {
                if (x.USERUSID == Globals.CurrentUserId)
                {
                    dataDisplay.Add(new PasswordData
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
        ///     Removes a Data Set from the Database and saves it
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

        public async Task AddDataAsync(PasswordData value)
        {
            var data = new PASSWORDDATA
            {
                PWID = value.Identifier,
                PWDATA = value.Password,
                PWCOMMENT = value.Comments,
                PWDESCRIPTION = value.Description,
                USERUSID = Globals.CurrentUserId
            };

            await passwordData.Entities.AddAsync(data);
            await passwordData.SaveChangesAsync();
        }

        public async Task<List<PasswordData>> LoadDataAsync()
        {
            var dataDisplay = new List<PasswordData>();

            await foreach (var x in passwordData.Entities.AsAsyncEnumerable())
            {
                if (x.USERUSID == Globals.CurrentUserId)
                {
                    dataDisplay.Add(new PasswordData
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

        public async Task<bool> RemoveAsync(PasswordData item)
        {
            PASSWORDDATA itemToDelete = await passwordData.Entities.FindAsync(item.Identifier);
            passwordData.Remove(itemToDelete);
            await passwordData.SaveChangesAsync();

            return await passwordData.Entities.FindAsync(item.Identifier) == null;
        }
    }
}