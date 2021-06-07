using System;

using DevExpress.Mvvm;

namespace HttpClient.Messaging
{
    /// <summary>
    /// Contains implementation of <see cref="IErrorSink"/> interface.
    /// </summary>
    public class ErrorSink : IErrorSink
    {
        /// <summary>
        /// Reference to <see cref="IMessenger"/> interface.
        /// </summary>
        private readonly IMessenger _messenger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messenger">Reference to <see cref="IMessenger"/> interface.</param>
        public ErrorSink(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        /// <summary>
        /// Sends <see cref="IntraMessageErrorException"/> message when exception was thrown.
        /// </summary>
        /// <param name="exception">Thrown exception.</param>
        public void Receive(Exception exception)
        {
            _messenger.Send(new IntraMessageErrorException(exception));
        }
    }
}