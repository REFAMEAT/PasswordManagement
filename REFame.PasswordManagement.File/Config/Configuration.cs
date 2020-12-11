using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REFame.PasswordManagement.File.Contracts.Config;

namespace REFame.PasswordManagement.File.Config
{
    public class Configuration<T> : IConfiguration<T> where T : new()
    {
        public string Path { get; internal set; }

        public async Task<T> LoadAsync()
        {
            string content;

            try
            {
                content = await System.IO.File.ReadAllTextAsync(Path);
            }
            catch (FileNotFoundException)
            {
                return new T();
            }

            content = content.Replace("\r\n", null);
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        public async Task WriteAsync(T value)
        {
            var serializer = new JsonSerializer();

            await using var sw = new StreamWriter(Path);
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            JsonConvert.SerializeObject(value);

            serializer.Serialize(jsonWriter, value);
        }

        public T Load()
        {
            string content;

            try
            {
                content = System.IO.File.ReadAllText(Path);
            }
            catch (FileNotFoundException ex)
            {
                return new T();
            }

            content = content.Replace("\r\n", null);

            var data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        public void Write(T value)
        {
            var serializer = new JsonSerializer();

            using var sw = new StreamWriter(Path);
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            JsonConvert.SerializeObject(value);

            serializer.Serialize(jsonWriter, value);
        }
    }
}