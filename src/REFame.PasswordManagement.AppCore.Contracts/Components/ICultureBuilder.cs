using System.Globalization;

namespace REFame.PasswordManagement.AppCore.Contracts.Components
{
    public interface ICultureBuilder
    {
        CultureInfo Get(ICore core);
    }
}