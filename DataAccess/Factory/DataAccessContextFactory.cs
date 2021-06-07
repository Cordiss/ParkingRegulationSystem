using DataAccess.Infrastructure;

namespace DataAccess.Factory
{
    /// <summary>
    /// Defines factory for <see cref="DataAccessContext"/> object.
    /// </summary>
    public static class DataAccessContextFactory
    {
        /// <summary>
        /// Local instance.
        /// </summary>
        private static volatile DataAccessContext _dataAccessContext;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object DataContextSyncRoot = new object();

        /// <summary>
        /// Creates new instance of <see cref="DataAccessContext"/>.
        /// </summary>
        /// <returns><see cref="DataAccessContext"/> object.</returns>
        public static DataAccessContext GetDataContext()
        {
            if (_dataAccessContext == null)
            {
                lock (DataContextSyncRoot)
                {
                    if (_dataAccessContext == null)
                    {
                        string connectionString = ConnectionHelper.GetConnectionString();
                        _dataAccessContext = new DataAccessContext(connectionString);
                    }
                }
            }

            return _dataAccessContext;
        }
    }
}