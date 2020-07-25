using System;

namespace PasswordManagement.Backend
{
    internal class AppCore
    {
        private static bool created = false;

        private AppCore()
        {
        }

        internal static AppCore StartCore()
        {
            // Core can be created once
            if (!created)
            {
                created = true;
                AppCore app = new AppCore();
                return app; 
            }
            else
            {
                throw new ApplicationException();
            }
        }
    }
}