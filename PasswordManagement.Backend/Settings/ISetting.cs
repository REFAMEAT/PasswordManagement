namespace PasswordManagement.Backend.Settings
{
    internal interface ISetting
    {
        void Load();
        void Save();
    }
}