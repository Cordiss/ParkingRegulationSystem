namespace HttpClient.Enums
{
    /// <summary>
    /// Enumerates types of receiving data events.
    /// </summary>
    public enum DataReceivedType
    {
        /// <summary>
        /// Loaded from API event.
        /// </summary>
        Loaded,

        /// <summary>
        /// Save to database event.
        /// </summary>
        Saved
    }
}