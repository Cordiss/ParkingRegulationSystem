using System;
using HttpClient.Enums;

namespace HttpClient.Messaging
{
    /// <summary>
    /// Defines internal message that notifies about current requesting data status.
    /// </summary>
    public class IntraMessageRequestData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status">Data requesting status.</param>
        public IntraMessageRequestData(RequestDataMessageStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status">Data requesting status.</param>
        /// <param name="startNumber">Data start number.</param>
        /// <param name="endNumber">Data end number.</param>
        public IntraMessageRequestData(RequestDataMessageStatus status, string startNumber, string endNumber)
        {
            Status = status;

            var startNumberInt = Int32.Parse(startNumber);
            var endNumberInt = Int32.Parse(endNumber);

            Count = endNumberInt - startNumberInt;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status">Data requesting status.</param>
        /// <param name="exception">Exception that was thrown during data requesting.</param>
        public IntraMessageRequestData(RequestDataMessageStatus status, Exception exception)
        {
            Status = status;
            Exception = exception;
        }

        /// <summary>
        /// Gets data requesting status.
        /// </summary>
        public RequestDataMessageStatus Status { get; }

        /// <summary>
        /// Gets count of data entities that was requested.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets exception that was throws during data requesting.
        /// </summary>
        public Exception Exception { get; }
    }
}