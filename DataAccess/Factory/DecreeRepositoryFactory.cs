using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;

namespace DataAccess.Factory
{
    /// <summary>
    /// Defines factory for <see cref="IDecreeRepository"/> object.
    /// </summary>
    public static class DecreeRepositoryFactory
    {
        /// <summary>
        /// Local instance.
        /// </summary>
        private static volatile IDecreeRepository _decreeRepository;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object DecreeRepositorySyncRoot = new object();

        /// <summary>
        /// Creates new object of <see cref="IDecreeRepository"/> object.
        /// </summary>
        /// <returns><see cref="IDecreeRepository"/> instance.</returns>
        public static IDecreeRepository GetRepository()
        {
            if (_decreeRepository == null)
            {
                lock (DecreeRepositorySyncRoot)
                {
                    if (_decreeRepository == null)
                    {
                        _decreeRepository = new DecreeRepository();
                    }
                }
            }

            return _decreeRepository;
        }
    }
}