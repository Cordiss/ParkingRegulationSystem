using System;

namespace HttpClient.Messaging
{
    /// <summary>
    /// Defines logic that allows handle and notify main view model about exceptions.
    /// </summary>
    public interface IErrorSink
    {
        /// <summary>
        /// Sends <see cref="IntraMessageErrorException"/> message when exception was thrown.
        /// </summary>
        /// <param name="exception">Thrown exception.</param>
        void Receive(Exception exception);
    }
}