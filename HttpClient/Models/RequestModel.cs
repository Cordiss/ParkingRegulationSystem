namespace HttpClient.Models
{
    /// <summary>
    /// Defines model according to first JSON response layer.
    /// </summary>
    public class RequestModel
    {
        /// <summary>
        /// Gets status of request.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets error string.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets data model.
        /// </summary>
        public DataModel Data { get; set; }
    }
}