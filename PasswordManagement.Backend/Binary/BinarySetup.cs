using System;
using System.IO;

namespace PasswordManagement.Backend.Binary
{
    internal static class BinarySetup
    {
        internal static string fileRoot =
            @"C:\Users\{user}\AppData\Roaming\PWManagement".Replace("{user}", Environment.UserName);

        internal static AppCore SetupBinaries(this AppCore app)
        {
            if (!Directory.Exists(fileRoot))
            {
                Directory.CreateDirectory(fileRoot);
            }

            return app;
        }
    }
}