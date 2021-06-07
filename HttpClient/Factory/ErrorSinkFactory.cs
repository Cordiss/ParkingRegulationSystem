using HttpClient.Messaging;

namespace HttpClient.Factory
{
    /// <summary>
    /// Defines factory for <see cref="IErrorSink"/> interface.
    /// </summary>
    public static class ErrorSinkFactory
    {
        /// <summary>
        /// Local <see cref="IErrorSink"/> instance.
        /// </summary>
        private static volatile IErrorSink _errorSink;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object ErrorSinkSyncRoot = new object();

        /// <summary>
        /// Gets <see cref="IErrorSink"/> instance.
        /// </summary>
        /// <returns><see cref="IErrorSink"/> instance.</returns>
        public static IErrorSink GetErrorSink()
        {
            if (_errorSink == null)
            {
                lock (ErrorSinkSyncRoot)
                {
                    if (_errorSink == null)
                    {
                        _errorSink = new ErrorSink(DefaultMessenger.Instance);
                    }
                }
            }

            return _errorSink;
        }
    }
}