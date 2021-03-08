using System.Threading.Tasks;

namespace REFame.PasswordManagement.Configuration.Contracts
{
    public interface IConfiguration<T> where T : new()
    {
        string Path { get; }
        Task<T> LoadAsync();
        Task WriteAsync(T value);
        T Load();
        void Write(T value);
    }
}