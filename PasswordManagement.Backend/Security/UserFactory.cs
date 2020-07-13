using PasswordManagement.Backend.Security;
using PasswordManagement.Database.Model;
using System.Threading;

namespace PasswordManagement.Backend.Security
{
    public class UserFactory
    {
        public static USERDATA CreateUser(string userName, string password)
        {
            USERDATA newUser = new USERDATA();
            newUser.USUSERNAME = userName;
            string pwSalt = Password.GetSalt();
            newUser.USPASSWORD = Password.GetHash(pwSalt);

            // Thread sleeps for few seconds for preventing brute-force attacks
            Thread.Sleep(500);

            return newUser;
        }
    }
}