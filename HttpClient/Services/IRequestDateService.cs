namespace HttpClient.Services
{
    /// <summary>
    /// Defines service that communicates with API.
    /// </summary>
    public interface IRequestDateService
    {
        /// <summary>
        /// Performs requesting data.
        /// </summary>
        /// <param name="parameters">Array of parameters for request.</param>
        void RequestData(string[] parameters);

        /// <summary>
        /// Downloads image.
        /// </summary>
        /// <param name="imagePathString">Image URL.</param>
        /// <returns>Image byte array.</returns>
        object DownloadImage(string imagePathString);

        /// <summary>
        /// Checks if there is some data in Data Base.
        /// </summary>
        /// <returns>True - Exists; False - Don't.</returns>
        bool IsSomeDataExists();
    }
}