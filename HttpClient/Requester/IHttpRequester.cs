namespace HttpClient.Requester
{
    /// <summary>
    /// Defines logic that allows to communicate with API via POST requests.
    /// </summary>
    public interface IHttpRequester
    {
        /// <summary>
        /// Executes POST request to API.
        /// </summary>
        /// <param name="parameter">Specific parameter for POST request.</param>
        /// <returns>Request result.</returns>
        string Execute(string parameter);

        /// <summary>
        /// Downloads file from URL.
        /// </summary>
        /// <param name="url">File URL.</param>
        /// <returns>File byte array.</returns>
        object Download(string url);
    }
}