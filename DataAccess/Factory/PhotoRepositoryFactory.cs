using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;

namespace DataAccess.Factory
{
    /// <summary>
    /// Defines factory for <see cref="IPhotoRepository"/> object.
    /// </summary>
    public static class PhotoRepositoryFactory
    {
        /// <summary>
        /// Local instance.
        /// </summary>
        private static volatile IPhotoRepository _photoRepository;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object PhotoRepositorySyncRoot = new object();

        /// <summary>
        /// Creates a new instance of <see cref="IPhotoRepository"/> object.
        /// </summary>
        /// <returns></returns>
        public static IPhotoRepository GetRepository()
        {
            if (_photoRepository == null)
            {
                lock (PhotoRepositorySyncRoot)
                {
                    if (_photoRepository == null)
                    {
                        _photoRepository = new PhotoRepository();
                    }
                }
            }

            return _photoRepository;
        }
    }
}