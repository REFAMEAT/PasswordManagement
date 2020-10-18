using System;
using System.IO;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace PasswordManagement.Logging
{
    public class LoggerFactory
    {
        /// <summary>
        ///     Path to the JSON file
        /// </summary>
        private readonly string logPath = @$"C:\Users\{Environment.UserName}\AppData\Roaming\PWManagement";

        private readonly ILogger currentLogger;


        public LoggerFactory()
        {
            string targetDirectory = Path.Combine(logPath, "Logs");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithExceptionDetails()
                .Enrich.WithThreadName()
                .Enrich.WithMemoryUsage()
                .WriteTo.Debug()
                .WriteTo.File(new JsonFormatter(), Path.Combine(targetDirectory, "Log.json"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 1024 * 1024 * 1024,
                    shared: true
                )
                .WriteTo.File(Path.Combine(targetDirectory, "Log.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 1024 * 1024 * 1024,
                    shared: true,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties}{NewLine}{Exception}")
                .CreateLogger();

            currentLogger = new FileLogger(Log.Logger);
            Log.Logger.Information("Initialized IbfLogger");
        }

        public ILogger Create()
        {
            return currentLogger;
        }
    }
}