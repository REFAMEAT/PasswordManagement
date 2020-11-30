namespace REFame.PasswordManagement.Logging
{
    public class Logger
    {
        private static Logger current;
        private readonly ILogger currentLogger;

        private Logger()
        {
            var loggerFactory = new LoggerFactory();
            currentLogger = loggerFactory.Create();
        }

        public static Logger Current => current ??= new Logger();

        public ILogger Get()
        {
            return currentLogger;
        }
    }
}