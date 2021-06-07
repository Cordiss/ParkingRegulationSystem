namespace HttpClient
{
    /// <summary>
    /// Defines HttpClient interface.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Performs data request and saving in Data Base.
        /// </summary>
        /// <param name="startNumberString">Data id start number.</param>
        /// <param name="endNumberString">Data id stop number.</param>
        void RequestData(string startNumberString, string endNumberString);

        /// <summary>
        /// Performs downloading image.
        /// </summary>
        /// <param name="imagePathString">Image URL.</param>
        /// <returns>Image byte array.</returns>
        object DownloadImagesByteArray(string imagePathString);

        /// <summary>
        /// Checks if there is some data in Data Base.
        /// </summary>
        /// <returns>True - Exists; False - Don't.</returns>
        bool IsSomeDataExists();
    }
}