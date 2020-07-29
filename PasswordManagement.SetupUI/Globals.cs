using System;

namespace PasswordManagement.SetupUI
{
    public class Globals
    {
        internal const string NetCoreVersion = "3.1.302";

        internal const string ProductVersion = "1.0.17";

        internal const string PasswordManagementDownloadPath = @"\\poseidon\DataTransfer\builds\PasswordManager Release\";
        
        internal const string PasswordManagementDownloadPathFtp = @"ftp://poseidon/array1/DataTransfer/builds/PasswordManager Release/";

        internal const string NetCoreRuntimeDownloadPath = @"\\poseidon\DataTransfer\windowsdesktop-runtime-3.1.6-win-x64.exe";

        internal const string TotalDownloadPath = PasswordManagementDownloadPath + ProductVersion;

        internal const string TotalDownloadPathFtp = PasswordManagementDownloadPathFtp + ProductVersion;

        internal static string ManagerPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\PasswordManager";

        internal static string ExePath = ManagerPath + "\\PasswordManagement.exe";

    }
}