using Microsoft.Data.SqlClient;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.DB
{
    public class SqlConnectionStringBuilder : ISqlConnectionStringBuilder
    {
        private readonly IConfigurationFactory<DatabaseData> configurationFactory;

        public SqlConnectionStringBuilder(IConfigurationFactory<DatabaseData> configurationFactory)
        {
            this.configurationFactory = configurationFactory;
        }

        public string Create()
        {
            var configuration = configurationFactory
                .SetPath()
                .Create()
                .Load();

            Microsoft.Data.SqlClient.SqlConnectionStringBuilder connectionStringBuilder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder();

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