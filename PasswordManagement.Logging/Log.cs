namespace PasswordManagement.Logging
{
    public class Log
    {
        public string TimeStamp { get; set; }

        public string ErrorMessage { get; set; }

        public LogLevel Level { get; set; }

        public string Caller { get; set; }
    }
}