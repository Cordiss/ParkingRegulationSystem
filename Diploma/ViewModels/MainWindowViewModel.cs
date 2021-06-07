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
    public class MainWindowViewModel : ViewModelBase
    { 
        #region Fields

        private ViewModelBase _currentViewModel;

        private MainPageViewModel _mainPageViewModel;
        private DecreesViewModel _decreesViewModel;
        private WaiterViewModel _waiterViewModel;

        private readonly IViewModelFactory _viewModelFactory;

        private bool _isWaiter;

        #endregion

        #region _ctors

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

        public DecreesViewModel DecreesViewModel
        {
            get => _decreesViewModel;
            private set => SetProperty(ref _decreesViewModel, value);
        }

        public MainPageViewModel MainPageViewModel
        {
            get => _mainPageViewModel;
            private set => SetProperty(ref _mainPageViewModel, value);
        }

        public WaiterViewModel WaiterViewModel
        {
            get => _waiterViewModel;
            private set => SetProperty(ref _waiterViewModel, value);
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            private set => SetProperty(ref _currentViewModel, value);
        }

        public bool IsWaiter
        {
            get => _isWaiter;
            private set => SetProperty(ref _isWaiter, value);
        }

        public List<BaseMenuItemViewModel> MenuItems { get; }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; }

        #endregion

        #region Methods

        private void OnExceptionReceived(IntraMessageErrorException message)
        {
            try
            {
                
            }
            catch (Exception e)
            {

            }
        }

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

        private void ShutdownExecute()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}