using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.Contracts
{
    public interface ISettingFactory : IBindableFactory
    {
        SettingMediator GetMediator();
        public string GetHeader();
    }
}