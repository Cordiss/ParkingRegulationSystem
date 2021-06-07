namespace HttpClient.Enums
{
    /// <summary>
    /// Enumerates request data status events.
    /// </summary>
    public enum RequestDataMessageStatus
    {
        /// <summary>
        /// Started requesting data.
        /// </summary>
        Started = 0,

        /// <summary>
        /// Data requesting completed.
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Data requesting failed.
        /// </summary>
        Failed = 2
    }
}