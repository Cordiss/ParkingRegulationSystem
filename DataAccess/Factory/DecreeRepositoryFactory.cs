using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;

namespace DataAccess.Factory
{
    public static class DecreeRepositoryFactory
    {
        private static volatile IDecreeRepository _decreeRepository;

        private static readonly object DecreeRepositorySyncRoot = new object();

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