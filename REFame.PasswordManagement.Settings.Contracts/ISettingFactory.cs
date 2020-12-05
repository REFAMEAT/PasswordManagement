using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.Settings.Contracts
{
    public interface ISettingFactory : IBindableFactory
    {
        SettingMediator GetMediator();
        public string GetHeader();
    }
}