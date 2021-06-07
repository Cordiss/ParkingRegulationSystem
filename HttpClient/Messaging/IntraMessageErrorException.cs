using System;

namespace HttpClient.Messaging
{
    /// <summary>
    /// Defines internal message for exceptions.
    /// </summary>
    public class IntraMessageErrorException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Thrown exception.</param>
        public IntraMessageErrorException(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Gets exception instance.
        /// </summary>
        public Exception Exception { get; }
    }
}