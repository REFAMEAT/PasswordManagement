using System.Threading.Tasks;

namespace REFame.PasswordManagement.Services.Interfaces
{
    public interface ISettingService<T>
    {
        Task<T> Load();
        Task Save(T data);
    }
}