using System.Globalization;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;

namespace REFame.PasswordManagement.AppCore.Tests.Mock
{
    internal class MockCultureBuilder : ICultureBuilder
    {
        public CultureInfo Get(ICore core)
        {
            return new CultureInfo("de-de");
        }
    }
}