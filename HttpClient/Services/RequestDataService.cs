using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repositories;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using HttpClient.Enums;
using HttpClient.Events;
using HttpClient.Helpers;
using HttpClient.Messaging;
using HttpClient.Models;
using HttpClient.Requester;

using Newtonsoft.Json;

namespace HttpClient.Services
{
    /// <summary>
    /// Contains implementation of <see cref="IRequestDateService"/> interface.
    /// </summary>
    public class RequestDataService : IRequestDateService
    {
        /// <summary>
        /// Reference to <see cref="IHttpRequester"/> interface.
        /// </summary>
        private readonly IHttpRequester _httpRequester;

        /// <summary>
        /// Reference to <see cref="IDecreeRepository"/> interface.
        /// </summary>
        private readonly IDecreeRepository _decreeRepository;

        /// <summary>
        /// Reference to <see cref="IPhotoRepository"/> interface.
        /// </summary>
        private readonly IPhotoRepository _photoRepository;

        /// <summary>
        /// Reference to <see cref="IMessenger"/> interface.
        /// </summary>
        private readonly IMessenger _messenger;

        /// <summary>
        /// Stores custom queue of <see cref="UnitedEntity{T,E}"/>.
        /// </summary>
        private CustomQueue<UnitedEntity<Decree, Photo>> _savedEntities;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpRequester">Reference to <see cref="IHttpRequester"/> interface.</param>
        /// <param name="decreeRepository">Reference to <see cref="IDecreeRepository"/> interface.</param>
        /// <param name="photoRepository">Reference to <see cref="IPhotoRepository"/> interface.</param>
        /// <param name="messenger">Reference to <see cref="IMessenger"/> interface.</param>
        public RequestDataService(
            IHttpRequester httpRequester, 
            IDecreeRepository decreeRepository, 
            IPhotoRepository photoRepository,
            IMessenger messenger)
        {
            _httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
            _decreeRepository = decreeRepository ?? throw new ArgumentNullException(nameof(decreeRepository));
            _photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        /// <summary>
        /// Checks if there is some data in Data Base.
        /// </summary>
        /// <returns>True - Exists; False - Don't.</returns>
        public bool IsSomeDataExists()
        {
            return _decreeRepository.IsSomeDecreeExists();
        }

        #region DecreesRequesting

        /// <summary>
        /// Performs requesting data.
        /// </summary>
        /// <param name="parameters">Array of parameters for request.</param>
        public void RequestData(string[] parameters)
        {
            List<Task> tasks = new List<Task>();

            _savedEntities = new CustomQueue<UnitedEntity<Decree, Photo>>(100);

            foreach (var parameter in parameters)
            {
                var task = new Task(() => ProcessDataRequesting(parameter));
                tasks.Add(task);

                task.Start();
            }

            Task.WaitAll(tasks.ToArray());
            Task.Run(() => SavedEntitiesOnCollectionModified()).GetAwaiter().GetResult();
            _messenger.Send(new IntraMessageRequestData(RequestDataMessageStatus.Completed));
        }

        /// <summary>
        /// Performs processing of data request.
        /// </summary>
        /// <param name="parameter">Data request parameter.</param>
        private void ProcessDataRequesting(string parameter)
        {
            var result = _httpRequester.Execute(WebUtility.UrlEncode(parameter));

            RequestModel requestModel = GetRequestModel(result);
            if (requestModel.Data == null) return;

            Decree entity = requestModel.ToEntity();

            try
            {
                entity.Car.Model = PdfParser.GetCarModel(entity.PdfPath);
                entity.ActData = PdfParser.GetActDateTime(entity.PdfPath);
            }
            catch (Exception)
            {
                return;
            }

            Photo[] photos = requestModel.Data.ToEntity(entity);

            UnitedEntity<Decree, Photo> unitedEntity = new UnitedEntity<Decree, Photo>(entity, photos);

            _savedEntities.Enqueue(unitedEntity);
            _messenger.Send(new IntraMessageOnDataOperationEnded(DataReceivedType.Loaded));
        }

        /// <summary>
        /// Handler of <see cref="CustomQueue{T}"/> collection modifications.
        /// </summary>
        private void SavedEntitiesOnCollectionModified()
        {
            while (!_savedEntities.IsEmpty)
            {
                var unitedEntity = _savedEntities.Dequeue();

                var decreeEntity = unitedEntity.EntityOne;
                var photoEntities = unitedEntity.EntityTwo;

                _decreeRepository.Create(decreeEntity);

                foreach (var photo in photoEntities)
                {
                    _photoRepository.Create(photo.FilePath, decreeEntity);
                }

                _messenger.Send(new IntraMessageOnDataOperationEnded(DataReceivedType.Saved));
            }
        }

        /// <summary>
        /// Converts JSON string to <see cref="RequestModel"/>.
        /// </summary>
        /// <param name="json">JSON string.</param>
        /// <returns><see cref="RequestModel"/> instance.</returns>
        private RequestModel GetRequestModel(string json)
        {
            return JsonConvert.DeserializeObject<RequestModel>(json,
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        #endregion

        #region ImageDownloading

        /// <summary>
        /// Downloads image.
        /// </summary>
        /// <param name="imagePathString">Image URL.</param>
        /// <returns>Image byte array.</returns>
        public object DownloadImage(string imagePathString)
        {
            return _httpRequester.Download(imagePathString);
        }

        #endregion
    }
}