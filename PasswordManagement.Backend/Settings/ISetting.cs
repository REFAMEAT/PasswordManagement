using System;

namespace PasswordManagement.Backend.Settings
{
    public interface ISetting
    {
        void Load();
        void Save();
    }
}