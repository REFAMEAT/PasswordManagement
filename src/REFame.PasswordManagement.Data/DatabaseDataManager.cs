using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Data.Contracts;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Data
{
    /// <summary>
    ///     A Data-Manager for the connection to the Database
    /// </summary>
    public class DatabaseDataManager : IDataManager<PasswordData>
    {
        private readonly IPwmDbContext db;

        public DatabaseDataManager(IPwmDbContextFactory dbDbContext)
        {
            db = dbDbContext.Create();
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

            db.PASSWORDDATA.Add(data);
            db.SaveChanges();
        }

        /// <summary>
        ///     Loads all data sets from the Database
        /// </summary>
        /// <returns></returns>
        public List<PasswordData> LoadData()
        {
            var dataDisplay = new List<PasswordData>();

            foreach (PASSWORDDATA x in db.PASSWORDDATA)
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
            PASSWORDDATA itemToDelete = db.PASSWORDDATA
                .FirstOrDefault(x => x.PWID == item.Identifier);
            db.Remove(itemToDelete);
            db.SaveChanges();

            return db.PASSWORDDATA.Where(x => x.PWID == item.Identifier) == null;
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

            db.PASSWORDDATA.Add(data);
            await db.SaveChangesAsync();
        }

        public async Task<List<PasswordData>> LoadDataAsync()
        {
            var dataDisplay = new List<PasswordData>();

            await foreach (var x in db.PASSWORDDATA.AsAsyncEnumerable())
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
            PASSWORDDATA itemToDelete = await db.PASSWORDDATA
                .Where(x => x.PWID == item.Identifier)
                .FirstOrDefaultAsync();
            db.Remove(itemToDelete);
            await db.SaveChangesAsync();

            return db.PASSWORDDATA
                .Where(x => x.PWID == item.Identifier)
                .FirstOrDefaultAsync() == null;
        }
    }
}