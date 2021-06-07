using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using Diploma.Models;
using HttpClient;
using HttpClient.Events;
using HttpClient.Helpers;

namespace Diploma.ViewModels
{
    /// <summary>
    /// Defines details vase view model.
    /// </summary>
    public class DetailsCaseViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Reference to <see cref="IClient"/> interface.
        /// </summary>
        private readonly IClient _client;

        /// <summary>
        /// Backend field for <see cref="CurrentImage"/>.
        /// </summary>
        private BitmapImage _currentImage;

        /// <summary>
        /// Reference to <see cref="IDecreeModel"/> interface.
        /// </summary>
        private IDecreeModel _decreeModel;

        /// <summary>
        /// Backend field for <see cref="ImageSourceList"/> property.
        /// </summary>
        private ObservableCollection<IImageModel> _imageModels;

        /// <summary>
        /// Stores <see cref="CustomQueue{T}"/> for received image byte arrays.
        /// </summary>
        private readonly CustomQueue<Tuple<string, object>> _receivedByteArrays = new CustomQueue<Tuple<string, object>>();

        #endregion

        #region _ctors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="decreeModel">Reference to <see cref="IDecreeModel"/> interface.</param>
        /// <param name="client">Reference to <see cref="IClient"/> interface.</param>
        public DetailsCaseViewModel(IDecreeModel decreeModel, IClient client)
        {
            DecreeModel = decreeModel ?? throw new ArgumentNullException(nameof(decreeModel));
            _client = client ?? throw new ArgumentNullException(nameof(client));

            Initialize();

            ChangeImageCommand = new DelegateCommand<string>(ExecuteChangeImageCommand);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets <see cref="IDecreeModel"/> instance.
        /// </summary>
        public IDecreeModel DecreeModel
        {
            get => _decreeModel;
            private set => SetProperty(ref _decreeModel, value);
        }

        /// <summary>
        /// Gets current viewing image.
        /// </summary>
        public BitmapImage CurrentImage
        {
            get => _currentImage;
            private set => SetProperty(ref _currentImage, value);
        }

        /// <summary>
        /// Gets or sets collection of <see cref="IImageModel"/>.
        /// </summary>
        public ObservableCollection<IImageModel> ImageSourceList
        {
            get => _imageModels;
            set => SetProperty(ref _imageModels, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets command that changes main image.
        /// </summary>
        public ICommand<string> ChangeImageCommand { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes Details view model.
        /// </summary>
        private void Initialize()
        {
            _receivedByteArrays.CollectionModified += ReceivedByteArraysOnCollectionModified;

            ImageSourceList = new ObservableCollection<IImageModel>();

            foreach (var photoModel in DecreeModel.PhotoModels)
            {
                var task = new Task(() => ProcessDownloadingImage(photoModel.FilePath));
                task.Start();
            }
        }

        /// <summary>
        /// Performs downloading image from URL.
        /// </summary>
        /// <param name="imagePathString">Image URL.</param>
        private void ProcessDownloadingImage(string imagePathString)
        {
            var result = _client.DownloadImagesByteArray(imagePathString);
            var entity = new Tuple<string, object>(imagePathString, result);

            _receivedByteArrays.Enqueue(entity);
        }

        /// <summary>
        /// Handler for <see cref="CustomQueue{T}"/> collection changed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void ReceivedByteArraysOnCollectionModified(object sender, CollectionModifiedEventArgs<Tuple<string, object>> e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                var imageData = _receivedByteArrays.Dequeue();
                IImageModel imageModel = new ImageModel(imageData.Item1, imageData.Item2);
                ImageSourceList.Add(imageModel);

                OnPropertyChanged(nameof(ImageSourceList));
                CurrentImage = ImageSourceList.Select(i => i.Image).FirstOrDefault();
            });
        }

        /// <summary>
        /// Executes <see cref="ChangeImageCommand"/>.
        /// </summary>
        /// <param name="selectedImage">Path to selected image.</param>
        private void ExecuteChangeImageCommand(string selectedImage)
        {
            CurrentImage = ImageSourceList
                .Where(i => i.ImagePath.Equals(selectedImage))
                .Select(i => i.Image)
                .FirstOrDefault();
        }

        #endregion
    }
}