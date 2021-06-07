using System.IO;
using System.Net;

namespace HttpClient.Requester
{
    /// <summary>
    /// Contains implementation of <see cref="IHttpRequester"/> interface.
    /// </summary>
    public class HttpRequester : IHttpRequester
    {
        /// <summary>
        /// Stores base API url.
        /// </summary>
        private const string ApiUrl = "https://parkingkh.org/api/ruling/";

        /// <summary>
        /// Executes POST request to API.
        /// </summary>
        /// <param name="parameter">Specific parameter for POST request.</param>
        /// <returns>Request result.</returns>
        public string Execute(string parameter)
        {
            string url = $"{ApiUrl}{parameter}/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


        /// <summary>
        /// Downloads file from URL.
        /// </summary>
        /// <param name="url">File URL.</param>
        /// <returns>File byte array.</returns>
        public object Download(string url)
        {
            using (WebClient webClient = new WebClient())
            {
               return webClient.DownloadData(url);
            }
        }
    }
}