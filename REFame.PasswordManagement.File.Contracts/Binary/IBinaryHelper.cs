using System.Threading.Tasks;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.File.Contracts.Binary
{
    public interface IBinaryHelper
    {
        /// <summary>
        ///     Read a <see cref="!:BinaryData" /> from the .bin file
        /// </summary>
        /// <returns></returns>
        public BinaryData GetData();

        /// <summary>
        ///     Write a <see cref="!:BinaryData" /> to the .bin File
        /// </summary>
        /// <param name="content"></param>
        public void Write(BinaryData content);

        /// <summary>
        ///     Read a <see cref="!:BinaryData" /> from the .bin file
        /// </summary>
        /// <returns></returns>
        public Task<BinaryData> GetDataAsync();

        /// <summary>
        ///    Write a <see cref="!:BinaryData" /> to the .bin file
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task WriteAsync(BinaryData content);

        public void OverwriteDefaultPath(string newPath);
    }
}