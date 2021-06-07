using HttpClient.Requester;

namespace HttpClient.Factory
{
    /// <summary>
    /// Defines factory for <see cref="IHttpRequester"/> interface.
    /// </summary>
    public static class HttpRequesterFactory
    {
        /// <summary>
        /// Local <see cref="IHttpRequester"/> instance.
        /// </summary>
        private static volatile IHttpRequester _httpRequester;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object HttpRequesterSyncRoot = new object();

        /// <summary>
        /// Gets <see cref="IHttpRequester"/> instance.
        /// </summary>
        /// <returns><see cref="IHttpRequester"/> instance.</returns>
        public static IHttpRequester GetHttpRequester()
        {
            if (_httpRequester == null)
            {
                lock (HttpRequesterSyncRoot)
                {
                    if (_httpRequester == null)
                    {
                        _httpRequester = new HttpRequester();
                    }
                }
            }

            return _httpRequester;
        }
    }
}