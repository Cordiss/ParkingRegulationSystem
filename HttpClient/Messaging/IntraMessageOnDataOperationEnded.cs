using HttpClient.Enums;

namespace HttpClient.Messaging
{
    /// <summary>
    /// Defines internal message that notifies about completion receiving or saving some data.
    /// </summary>
    public class IntraMessageOnDataOperationEnded
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type">Data completion event type.</param>
        public IntraMessageOnDataOperationEnded(DataReceivedType type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets data completion event type.
        /// </summary>
        public DataReceivedType Type { get; }
    }
}