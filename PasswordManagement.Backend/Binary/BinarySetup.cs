using System;
using System.IO;

namespace PasswordManagement.Backend.Binary
{
    public static class BinarySetup
    {
        public static string fileRoot =
            @"C:\Users\{user}\AppData\Roaming\PWManagement".Replace("{user}", Environment.UserName);

        public static AppCore SetupBinaries(this AppCore app)
        {
            if (!Directory.Exists(fileRoot))
            {
                Directory.CreateDirectory(fileRoot);
            }

            return app;
        }
    }
}