using System;
using System.Collections.Generic;
using HttpClient.Messaging;
using HttpClient.Services;

namespace HttpClient
{
    /// <summary>
    /// Contains implementation of <see cref="IClient"/> interface.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        /// Reference to <see cref="IRequestDateService"/> interface.
        /// </summary>
        private readonly IRequestDateService _requestDateService;

        /// <summary>
        /// Reference to <see cref="IErrorSink"/> interface.
        /// </summary>
        private readonly IErrorSink _errorSink;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="requestDateService">Reference to <see cref="IRequestDateService"/> interface.</param>
        /// <param name="errorSink">Reference to <see cref="IErrorSink"/> interface.</param>
        public Client(IRequestDateService requestDateService, IErrorSink errorSink)
        {
            _requestDateService = requestDateService ?? throw new ArgumentNullException(nameof(requestDateService));
            _errorSink = errorSink ?? throw new ArgumentNullException(nameof(errorSink));
        }

        /// <summary>
        /// Performs data request and saving in Data Base.
        /// </summary>
        /// <param name="startNumberString">Data id start number.</param>
        /// <param name="endNumberString">Data id stop number.</param>
        public void RequestData(string startNumberString, string endNumberString)
        {
            var startNumber = Int32.Parse(startNumberString.Remove(0, 2));
            var endNumber = Int32.Parse(endNumberString.Remove(0, 2));

            List<string> parameters = new List<string>();

            for (int i = startNumber; i <= endNumber; i++)
            {
                var letters = startNumberString.Remove(3);
                parameters.Add($"{letters}{i}");
            }

            try
            {
                _requestDateService.RequestData(parameters.ToArray());
            }
            catch (Exception exception)
            {
                _errorSink.Receive(exception);
            }
        }

        /// <summary>
        /// Performs downloading image.
        /// </summary>
        /// <param name="imagePathString">Image URL.</param>
        /// <returns>Image byte array.</returns>
        public object DownloadImagesByteArray(string imagePathString)
        {
            try
            {
                return _requestDateService.DownloadImage(imagePathString);
            }
            catch (Exception exception)
            {
                _errorSink.Receive(exception);
                return null;
            }
        }

        /// <summary>
        /// Checks if there is some data in Data Base.
        /// </summary>
        /// <returns>True - Exists; False - Don't.</returns>
        public bool IsSomeDataExists()
        {
            return _requestDateService.IsSomeDataExists();
        }
    }
}