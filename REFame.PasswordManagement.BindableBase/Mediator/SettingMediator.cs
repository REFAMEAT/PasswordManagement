using System;

namespace REFame.PasswordManagement.WpfBase
{
    public class SettingMediator
    {
        public event EventHandler SaveRequested;

        public void RequestSave(object caller)
        {
            SaveRequested?.Invoke(caller, EventArgs.Empty);
        }
    }
}