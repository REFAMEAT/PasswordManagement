using System;

namespace PasswordManagement.Model
{
    public class AppCore
    {
        private static bool created;

        private AppCore()
        {
        }

        public static AppCore StartCore()
        {
            // Core can be created once
            if (!created)
            {
                created = true;
                var app = new AppCore();
                return app;
            }

            throw new ApplicationException();
        }
    }
}