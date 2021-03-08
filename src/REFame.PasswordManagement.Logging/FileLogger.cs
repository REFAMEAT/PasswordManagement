using System;

namespace REFame.PasswordManagement.Logging
{
    public class FileLogger : ILogger
    {
        private readonly Serilog.ILogger logger;

        public FileLogger(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        public void Information(object value)
        {
            logger.Information(Convert.ToString(value));
        }

        public void Warning(object value)
        {
            logger.Warning(Convert.ToString(value));
        }

        public void Debug(object value)
        {
            logger.Debug(Convert.ToString(value));
        }

        public void Error(object value)
        {
            logger.Error(Convert.ToString(value));
        }

        public void Error(Exception exception)
        {
            logger.Error(exception, "Error");
        }
    }
}