using DataAccess.Factory;
using DataAccess.Repositories;
using DevExpress.Mvvm;
using HttpClient.Requester;
using HttpClient.Services;

namespace HttpClient.Factory
{
    /// <summary>
    /// Defines factory for <see cref="IRequestDateService"/> interface.
    /// </summary>
    public static class RequestDataServiceFactory
    {
        /// <summary>
        /// Local instance of <see cref="IRequestDateService"/> interface.
        /// </summary>
        private static volatile IRequestDateService _requestDateService;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object RequestDataServiceSyncRoot = new object();

        /// <summary>
        /// Gets <see cref="IRequestDateService"/> instance.
        /// </summary>
        /// <param name="messenger"><see cref="IMessenger"/> instance.</param>
        /// <returns><see cref="IRequestDateService"/> instance.</returns>
        public static IRequestDateService GetRequestDateService(IMessenger messenger)
        {
            if (_requestDateService == null)
            {
                lock (RequestDataServiceSyncRoot)
                {
                    if (_requestDateService == null)
                    {
                        IHttpRequester httpRequester = HttpRequesterFactory.GetHttpRequester();
                        IDecreeRepository decreeRepository = DecreeRepositoryFactory.GetRepository();
                        IPhotoRepository photoRepository = PhotoRepositoryFactory.GetRepository();

                        _requestDateService = new RequestDataService(httpRequester, decreeRepository, photoRepository, messenger);
                    }
                }
            }

            return _requestDateService;
        }
    }
}