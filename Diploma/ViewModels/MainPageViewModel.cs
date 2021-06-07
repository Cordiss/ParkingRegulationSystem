using System;
using System.Threading.Tasks;
using System.Windows.Input;

using DevExpress.Mvvm;

using Diploma.Behaviors;
using Diploma.Helpers;

using HttpClient;
using HttpClient.Enums;
using HttpClient.Messaging;

namespace Diploma.ViewModels
{
    /// <summary>
    /// Defines main page view model.
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Constants

        /// <summary>
        /// Stores default decree start ruling number.
        /// </summary>
        private const int DefaultStartNumber = 13000;

        /// <summary>
        /// Stores default decree end ruling number.
        /// </summary>
        private const int DefaultStopNumber = 13100;

        #endregion

        #region Fields

        /// <summary>
        /// Backend filed for <see cref="StartDecreeNumber"/> property.
        /// </summary>
        private string _startDecreeNumber = DefaultStartNumber.ToString();

        /// <summary>
        /// Backend filed for <see cref="StopDecreeNumber"/> property.
        /// </summary>
        private string _stopDecreeNumber = DefaultStopNumber.ToString();

        /// <summary>
        /// Reference to <see cref="IClient"/> interface.
        /// </summary>
        private readonly IClient _client;

        #endregion

        #region _ctors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">Reference to <see cref="IClient"/> interface.</param>
        public MainPageViewModel(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));

            RequestDataCommand = new DelegateCommand(ExecuteRequestDataCommand);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets starting ruling number.
        /// </summary>
        public string StartDecreeNumber
        {
            get => _startDecreeNumber;
            set => SetProperty(ref _startDecreeNumber, value);
        }

        /// <summary>
        /// Gets or sets ending ruling number.
        /// </summary>
        public string StopDecreeNumber
        {
            get => _stopDecreeNumber;
            set => SetProperty(ref _stopDecreeNumber, value);
        }

        /// <summary>
        /// Gets status of data validation.
        /// </summary>
        [DependsOnProperty(nameof(StartDecreeNumber))]
        [DependsOnProperty(nameof(StopDecreeNumber))]
        public bool IsDataValid => 
            StartDecreeNumber.Validate(out var _) && StopDecreeNumber.Validate(out var _);


        #endregion

        #region Commands

        /// <summary>
        /// Gets request data command.
        /// </summary>
        public ICommand RequestDataCommand { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes request of data.
        /// </summary>
        private void ExecuteRequestDataCommand()
        {
            DefaultMessenger.Send(new IntraMessageRequestData(RequestDataMessageStatus.Started, StartDecreeNumber, StopDecreeNumber));

            try
            {
                Task.Run(() => _client.RequestData($"АБ0{StartDecreeNumber}", $"АБ0{StopDecreeNumber}"));
            }
            catch (Exception e)
            {
                DefaultMessenger.Send(new IntraMessageRequestData(RequestDataMessageStatus.Failed, e));
            }
        }

        #endregion
    }
}