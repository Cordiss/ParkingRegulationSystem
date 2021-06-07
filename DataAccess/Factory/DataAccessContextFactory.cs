using DataAccess.Infrastructure;

namespace DataAccess.Factory
{
    public static class DataAccessContextFactory
    {
        private static volatile DataAccessContext _dataAccessContext;

        private static readonly object DataContextSyncRoot = new object();

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