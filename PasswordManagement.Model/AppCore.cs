using System;

namespace PasswordManagement.Model
{
    public class AppCore
    {
        private static bool created = false;

        private AppCore()
        {
        }

        public static AppCore StartCore()
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