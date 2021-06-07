using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using DevExpress.Mvvm;

using Diploma.Factory;
using Diploma.ViewModels.MenuItems;

using HttpClient.Enums;
using HttpClient.Messaging;

namespace Diploma.ViewModels
{
    /// <summary>
    /// Defines main ViewModel of UI.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    { 
        #region Fields

        /// <summary>
        /// Backend filed for <see cref="CurrentViewModel"/> property.
        /// </summary>
        private ViewModelBase _currentViewModel;

        /// <summary>
        /// Backend field for <see cref="MainPageViewModel"/> property.
        /// </summary>
        private MainPageViewModel _mainPageViewModel;

        /// <summary>
        /// Backend field for <see cref="DecreesViewModel"/> property.
        /// </summary>
        private DecreesViewModel _decreesViewModel;

        /// <summary>
        /// Backend field for <see cref="WaiterViewModel"/> property.
        /// </summary>
        private WaiterViewModel _waiterViewModel;

        /// <summary>
        /// Reference to <see cref="IViewModelFactory"/> interface.
        /// </summary>
        private readonly IViewModelFactory _viewModelFactory;

        /// <summary>
        /// Backend field for <see cref="IsWaiter"/>.
        /// </summary>
        private bool _isWaiter;

        #endregion

        #region _ctors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModelFactory">Reference to <see cref="IViewModelFactory"/> interface.</param>
        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            DefaultMessenger.Register<IntraMessageErrorException>(this, null, OnExceptionReceived);
            DefaultMessenger.Register<IntraMessageViewModelSwitch>(this, null, OnViewModelSwitch);
            DefaultMessenger.Register<IntraMessageRequestData>(this, null, OnRequestDataMessageReceived);
            DefaultMessenger.Register<IntraMessageOnDataOperationEnded>(this, null, OnNewDataWasLoaded);

            MenuItems = new List<BaseMenuItemViewModel>
            {
                new MainPageMenuItemViewModel(_viewModelFactory),
                new DecreesListMenuItemViewModel(_viewModelFactory)
            };

            if (DecreesViewModel == null)
            {
                DecreesViewModel = (DecreesViewModel)_viewModelFactory.CreateViewModelByType(typeof(DecreesViewModel));
                DecreesViewModel.Initialize();
            }

            CloseCommand = new DelegateCommand(ShutdownExecute);
        }
    
        #endregion

        #region Properties

        /// <summary>
        /// Gets DecreesViewModel instance.
        /// </summary>
        public DecreesViewModel DecreesViewModel
        {
            get => _decreesViewModel;
            private set => SetProperty(ref _decreesViewModel, value);
        }

        /// <summary>
        /// Gets MainPageViewModel instance.
        /// </summary>
        public MainPageViewModel MainPageViewModel
        {
            get => _mainPageViewModel;
            private set => SetProperty(ref _mainPageViewModel, value);
        }

        /// <summary>
        /// Gets WaiterViewModel instance.
        /// </summary>
        public WaiterViewModel WaiterViewModel
        {
            get => _waiterViewModel;
            private set => SetProperty(ref _waiterViewModel, value);
        }

        /// <summary>
        /// Gets current showing view model instance.
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        /// <summary>
        /// Gets flag that indicates if waiter screen is currently shown.
        /// </summary>
        public bool IsWaiter
        {
            get => _isWaiter;
            private set => SetProperty(ref _isWaiter, value);
        }

        /// <summary>
        /// Gets collection of menu item view models.
        /// </summary>
        public List<BaseMenuItemViewModel> MenuItems { get; }

        #endregion

        #region Commands

        /// <summary>
        /// Gets command that closes application.
        /// </summary>
        public ICommand CloseCommand { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Handler for <see cref="IntraMessageErrorException"/> messages.
        /// </summary>
        /// <param name="message">Message.</param>
        private void OnExceptionReceived(IntraMessageErrorException message)
        {
            try
            {
                
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Handler for <see cref="IntraMessageViewModelSwitch"/> messages.
        /// </summary>
        /// <param name="message">Message.</param>
        private void OnViewModelSwitch(IntraMessageViewModelSwitch message)
        {
            if (message.ViewModelType == typeof(DecreesViewModel))
            {
                if (DecreesViewModel == null)
                {
                    DecreesViewModel = (DecreesViewModel)_viewModelFactory.CreateViewModelByType(typeof(DecreesViewModel));
                    DecreesViewModel.Initialize();
                }

                DecreesViewModel.IsDetailsMode = false;
                CurrentViewModel = DecreesViewModel;
            }

            if (message.ViewModelType == typeof(MainPageViewModel))
            {
                MainPageViewModel =
                    (MainPageViewModel) _viewModelFactory.CreateViewModelByType(typeof(MainPageViewModel));

                CurrentViewModel = MainPageViewModel;
            }
        }

        /// <summary>
        /// Handler for <see cref="IntraMessageRequestData"/> messages.
        /// </summary>
        /// <param name="message">Message.</param>
        private void OnRequestDataMessageReceived(IntraMessageRequestData message)
        {
            switch (message.Status)
            {
                case RequestDataMessageStatus.Started:
                {
                    WaiterViewModel = new WaiterViewModel(message.Count);
                    CurrentViewModel = WaiterViewModel;
                    IsWaiter = true;
                    break;
                }

                case RequestDataMessageStatus.Completed:
                {
                    Task.Run(() =>
                    {
                        DecreesViewModel = (DecreesViewModel)_viewModelFactory.CreateViewModelByType(typeof(DecreesViewModel));
                        DecreesViewModel.Initialize();
                    }).GetAwaiter().GetResult();

                    IsWaiter = false;
                    CurrentViewModel = MainPageViewModel;
                    break;
                }
            }
        }

        /// <summary>
        /// Handler for <see cref="IntraMessageOnDataOperationEnded"/> messages.
        /// </summary>
        /// <param name="message">Message.</param>
        private void OnNewDataWasLoaded(IntraMessageOnDataOperationEnded message)
        {
            if (!IsWaiter) return;

            if (message.Type == DataReceivedType.Loaded)
            {
                WaiterViewModel.LoadedProgress++;
            }

            if (message.Type == DataReceivedType.Saved)
            {
                WaiterViewModel.SavedProgress++;
            }
        }

        /// <summary>
        /// Executes <see cref="CloseCommand"/>.
        /// </summary>
        private void ShutdownExecute()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}