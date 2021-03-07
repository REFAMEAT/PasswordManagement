using Microsoft.Data.SqlClient;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.DB
{
    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        private readonly IConfigurationFactory<DatabaseData> configurationFactory;

        public ConnectionStringBuilder(IConfigurationFactory<DatabaseData> configurationFactory)
        {
            this.configurationFactory = configurationFactory;
        }

        public string Create()
        {
            var configuration = configurationFactory
                .SetPath()
                .Create()
                .Load();

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();

            if (!configuration.IntegratedSecurity)
            {
                connectionStringBuilder.InitialCatalog = configuration.DatabaseName;
                connectionStringBuilder.DataSource = configuration.ServerName;
                connectionStringBuilder.UserID = configuration.Username;
                connectionStringBuilder.Password = configuration.Password;
            }
            else
            {
                connectionStringBuilder.InitialCatalog = configuration.DatabaseName;
                connectionStringBuilder.DataSource = configuration.ServerName;
                connectionStringBuilder.IntegratedSecurity = true;
            }

            return connectionStringBuilder.ToString();
        }
    }
}