using System;
using System.IO;
using System.Management.Automation;
using IWshRuntimeLibrary;
using File = IWshRuntimeLibrary.File;

namespace PasswordManagement.SetupUI
{
    public class Shortcut
    {
        internal static void Create(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            string targetFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\{fi.Name}.lnk";

            CreateShortcut(filePath, targetFile, "", "", "", "");
        }


        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="Description">Shortcut description</param>
        /// <param name="Arguments">Command line arguments</param>
        /// <param name="HotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="WorkingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string TargetPath, string ShortcutFile, string Description,
            string Arguments, string HotKey, string WorkingDirectory)
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(TargetPath))
                throw new ArgumentNullException("TargetPath");
            if (String.IsNullOrEmpty(ShortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create WshShellClass instance:
            var wshShell = new WshShellClass();

            // Create shortcut object:
            IWshRuntimeLibrary.IWshShortcut shorcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutFile);

            // Assign shortcut properties:
            shorcut.TargetPath = TargetPath;
            shorcut.Description = Description;
            if (!String.IsNullOrEmpty(Arguments))
                shorcut.Arguments = Arguments;
            if (!String.IsNullOrEmpty(HotKey))
                shorcut.Hotkey = HotKey;
            if (!String.IsNullOrEmpty(WorkingDirectory))
                shorcut.WorkingDirectory = WorkingDirectory;

            // Save the shortcut:
            shorcut.Save();
        }
    }
}