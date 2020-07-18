using System.Collections.Generic;

namespace PasswordManagement.Logging
{
    public class Logs
    {
        public Logs()
        {
            SavedLogs = new List<Log>();
        }

        public string MachineName { get; set; }

        public string WindowsVersion { get; set; }

        public string UserName { get; set; }

        public List<Log> SavedLogs { get; set; }
    }
}