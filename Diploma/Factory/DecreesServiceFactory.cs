using DataAccess.Repositories;

using Diploma.Services;

namespace Diploma.Factory
{
    /// <summary>
    /// Defines factory for <see cref="IDecreeService"/> instance.
    /// </summary>
    public static class DecreesServiceFactory
    {
        /// <summary>
        /// Local <see cref="IDecreeService"/> instance.
        /// </summary>
        private static volatile IDecreeService _decreeService;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object DecreeServiceSyncRoot = new object();

        /// <summary>
        /// Gets <see cref="IDecreeService"/> instance.
        /// </summary>
        /// <param name="decreeRepository">Reference to <see cref="IDecreeRepository"/> interface.</param>
        /// <param name="photoRepository">Reference to <see cref="IPhotoRepository"/> interface.</param>
        /// <returns></returns>
        public static IDecreeService GetDecreeService(IDecreeRepository decreeRepository, IPhotoRepository photoRepository)
        {
            if (_decreeService == null)
            {
                lock (DecreeServiceSyncRoot)
                {
                    if (_decreeService == null)
                    {
                        _decreeService = new DecreeService(decreeRepository, photoRepository);
                    }
                }
            }

            return _decreeService;
        }
    }
}