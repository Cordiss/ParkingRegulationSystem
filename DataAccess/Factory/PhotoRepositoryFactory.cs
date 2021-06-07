using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;

namespace DataAccess.Factory
{
    public static class PhotoRepositoryFactory
    {
        private static volatile IPhotoRepository _photoRepository;

        private static readonly object PhotoRepositorySyncRoot = new object();

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