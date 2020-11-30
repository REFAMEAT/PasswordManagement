using System;

namespace REFame.PasswordManagement.Logging
{
    public interface ILogger
    {
        void Information(object value);
        void Warning(object value);
        void Debug(object value);
        void Error(object value);
        void Error(Exception exception);
    }
}