using System;

namespace PasswordManagement.SetupUI
{
    public class Globals
    {
        internal const string ServerName = "178.115.236.84";

        internal const string NetCoreVersion = "3.1.302";

        internal const string ProductVersion = "1.0.17";

        internal const string PasswordManagementDownloadPath = @"\\" + ServerName + @"\DataTransfer\builds\PasswordManager Release\";

        internal const string PasswordManagementDownloadPathFtp = @"ftp://"+ ServerName +"/array1/DataTransfer/builds/PasswordManager Release/";

        internal const string NetCoreRuntimeDownloadPath = @"https://download.visualstudio.microsoft.com/download/pr/3eb7efa1-96c6-4e97-bb9f-563ecf595f8a/7efd9c1cdd74df8fb0a34c288138a84f/windowsdesktop-runtime-3.1.6-win-x64.exe";

        internal const string TotalDownloadPath = PasswordManagementDownloadPath + ProductVersion;

        internal const string TotalDownloadPathFtp = PasswordManagementDownloadPathFtp + ProductVersion;

        internal static string ManagerPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\PasswordManager";

        internal static string ExePath = ManagerPath + "\\PasswordManagement.exe";

    }
}