using System.Globalization;
using System.Runtime.CompilerServices;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;

namespace REFame.PasswordManagement.AppCore
{
    public static class CoreBuilderExtensions
    {
        public static CoreBuilder CreateCoreBuilder(this ICore core)
        {
            return new CoreBuilder(core);
        }
    }

    public class CoreBuilder
    {
        private readonly ICore core;
        public ICoreInformation Info => coreInformation;

        public CoreBuilder(ICore core)
        {
            this.core = core;
        }

        private readonly CoreInformation coreInformation = new CoreInformation();

        public CoreBuilder UseLogin<T>() where T : ILoginHandler
        {
            coreInformation.LoginHandler = core.GetRegisteredType<T>();
            return this;
        }

        public CoreBuilder UseCulture<T>() where T : ICultureBuilder
        {
            var cultureBuilder = core.GetRegisteredType<T>();

            CultureInfo culture = cultureBuilder.Get(core);

            coreInformation.AppCultureInfo = culture;

            return this;
        }

        public CoreBuilder UseUI<T>() where T : IUIBuilder
        {
            var uiBuilder = core.GetRegisteredType<T>();

            coreInformation.ThemeData = uiBuilder.BuildTheme(core);

            return this;
        }

        public CoreBuilder MainWindow<T>()
        {
            coreInformation.MainWindow = typeof(T);
            return this;
        }

        public CoreBuilder MainViewModel<T>()
        {
            coreInformation.MainViewModel = typeof(T);
            return this;
        }

        public ICore BuildCore()
        {
            core.CoreInformation = coreInformation;
            return core;
        }
    }
}