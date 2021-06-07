using System.Data.SqlClient;
using System.Reflection;

namespace DataAccess.Infrastructure
{
    public static class ConnectionHelper
    {
        private const string DefaultDatabaseServer = @"(localdb)\MSSQLLocalDB";

        private const string DefaultDatabaseName = "DiplomaDB";

        private const string DefaultApplicationName = "DataAccessApp";

        public static string GetConnectionString()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var applicationName = entryAssembly != null
                ? entryAssembly.GetName().Name
                : DefaultApplicationName;

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = DefaultDatabaseServer,
                InitialCatalog = DefaultDatabaseName,
                IntegratedSecurity = true,
                MultipleActiveResultSets = true,
                ApplicationName = applicationName
            };

            return builder.ConnectionString;
        }
    }
}