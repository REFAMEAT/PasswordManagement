using System;
using System.IO;
using IWshRuntimeLibrary;

namespace PasswordManager.Setup
{
    public class Shortcut
    {
        internal static void Create(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            
            if (!file.Exists)
            {
                throw new FileNotFoundException("Could not find file", filePath);
            }

            string fileName = file.Name.Replace(file.Extension, "");

            string shortcutLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\{fileName}.lnk";

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = fileName;
            shortcut.TargetPath = filePath;
            shortcut.IconLocation = filePath;
            shortcut.Save();
        }
    }
}