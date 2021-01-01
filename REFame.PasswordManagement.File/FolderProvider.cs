using System;
using System.IO;

namespace REFame.PasswordManagement.File
{
    public interface IFolderProvider
    {
        string AppDataFolder { get; set; }
    }

    public class FolderProvider : IFolderProvider
    {
        public FolderProvider()
        {
            AppDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "REFame",
                "PasswordManagement");

            var directoryInfo = new DirectoryInfo(AppDataFolder);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.CreateSubdirectory("Log");
            }
        }
        
        public string AppDataFolder { get; set; }
    }
}