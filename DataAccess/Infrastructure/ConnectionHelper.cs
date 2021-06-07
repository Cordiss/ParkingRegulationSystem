using System.Data.SqlClient;
using System.Reflection;

namespace DataAccess.Infrastructure
{
    /// <summary>
    /// Helper class for initializing database connection.
    /// </summary>
    public static class ConnectionHelper
    {
        /// <summary>
        /// Defines default data base server.
        /// </summary>
        private const string DefaultDatabaseServer = @"(localdb)\MSSQLLocalDB";

        /// <summary>
        /// Defines default database name.
        /// </summary>
        private const string DefaultDatabaseName = "DiplomaDB";

        /// <summary>
        /// Defines default application name.
        /// </summary>
        private const string DefaultApplicationName = "DataAccessApp";

        /// <summary>
        /// Creates connection string for database.
        /// </summary>
        /// <returns>Connection string.</returns>
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