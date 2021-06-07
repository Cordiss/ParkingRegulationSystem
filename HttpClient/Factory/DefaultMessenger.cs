using DevExpress.Mvvm;

namespace HttpClient.Factory
{
    /// <summary>
    /// Defines <see cref="IMessenger"/> factory.
    /// </summary>
    public static class DefaultMessenger
    {
        /// <summary>
        /// Local instance of <see cref="IMessenger"/>.
        /// </summary>
        private static volatile IMessenger _defaultMessenger;

        /// <summary>
        /// Synchronization object for safely access to local instance.
        /// </summary>
        private static readonly object DefaultMessengerSyncRoot = new object();

        /// <summary>
        /// Gets instance of <see cref="IMessenger"/> interface.
        /// </summary>
        public static IMessenger Instance
        {
            get
            {
                if (_defaultMessenger == null)
                {
                    lock (DefaultMessengerSyncRoot)
                    {
                        if (_defaultMessenger == null)
                        {
                            _defaultMessenger = new Messenger(true);
                        }
                    }
                }

                return _defaultMessenger;
            }
        }
    }
}