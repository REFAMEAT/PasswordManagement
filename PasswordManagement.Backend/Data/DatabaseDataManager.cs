using System.Collections.Generic;
using PasswordManagement.Backend.Binary;
using PasswordManagement.Database.Model;

namespace PasswordManagement.Backend.Data
{
    public class DatabaseDataManager : IDataManager<PasswordData>
    {
        public void AddData(PasswordData value)
        {
            PASSWORDDATA data = new PASSWORDDATA()
            {
                PWDATA = value.Password,
                PWCOMMENT = value.Comments,
                PWDESCRIPTION = value.Description,
                PWUSID = Globals.CurrentUserId,
            };
        }

        public List<PasswordData> LoadData()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(PasswordData item)
        {
            throw new System.NotImplementedException();
        }
    }
}