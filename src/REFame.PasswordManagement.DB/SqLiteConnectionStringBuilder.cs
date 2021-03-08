using System.IO;
using Microsoft.Data.Sqlite;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.File;

namespace REFame.PasswordManagement.DB
{
    public class SqLiteConnectionStringBuilder : ISqLiteConnectionStringBuilder
    {
        private readonly IFolderProvider folderProvider;

        public SqLiteConnectionStringBuilder(IFolderProvider folderProvider)
        {
            this.folderProvider = folderProvider;
        }

        public string Create()
        {
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();

            connectionStringBuilder.DataSource = Path.Combine(folderProvider.AppDataFolder, "Data.sqlite");
            //connectionStringBuilder.Password = "{6303147B-2315-4C7D-B6D4-4DA5F8ADECCF}";
            connectionStringBuilder.Mode = SqliteOpenMode.ReadWriteCreate;

            return connectionStringBuilder.ToString();
        }
    }
}