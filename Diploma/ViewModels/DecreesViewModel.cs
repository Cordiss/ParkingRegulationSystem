using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

using Diploma.Models;
using Diploma.Services;
using HttpClient;

namespace Diploma.ViewModels
{
    /// <summary>
    /// Defines Decrees view model.
    /// </summary>
    public class DecreesViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Reference to <see cref="IDecreeService"/> interface.
        /// </summary>
        private readonly IDecreeService _decreeService;

        /// <summary>
        /// Reference to <see cref="IClient"/> interface.
        /// </summary>
        private readonly IClient _client;

        /// <summary>
        /// Backend filed for <see cref="DetailsCaseViewModel"/>.
        /// </summary>
        private DetailsCaseViewModel _detailsCaseViewModel;

        /// <summary>
        /// Backend field for <see cref="DecreeModels"/>.
        /// </summary>
        private ObservableCollection<DecreeModel> _decreeModels;

        /// <summary>
        /// Stores original collection of <see cref="DecreeModel"/>.
        /// </summary>
        private DecreeModel[] _decreeModelsOriginal;

        /// <summary>
        /// Backend field for <see cref="SelectedItem"/>.
        /// </summary>
        private DecreeModel _selectedDecreeModel;

        /// <summary>
        /// Backend field for <see cref="SearchCriteriaText"/>.
        /// </summary>
        private string _searchCriteriaText;

        /// <summary>
        /// Backend field for <see cref="IsDetailsMode"/>.
        /// </summary>
        private bool _isDetailsModel;

        #endregion

        #region _ctors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="decreeService">Reference of <see cref="IDecreeService"/> interface.</param>
        /// <param name="client">Reference of <see cref="IClient"/> interface.</param>
        public DecreesViewModel(IDecreeService decreeService, IClient client)
        {
            _decreeService = decreeService ?? throw new ArgumentNullException(nameof(decreeService));
            _client = client ?? throw new ArgumentNullException(nameof(client));

            OpenDetailsCommand = new DelegateCommand(OpenDetailsExecute);
            SearchCommand = new DelegateCommand(ApplySearch);
            ClearSearchCommand = new DelegateCommand(() =>
                {
                    DecreeModels = _decreeModelsOriginal.ToObservableCollection();
                });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets view collection of <see cref="DecreeModel"/>.
        /// </summary>
        public ObservableCollection<DecreeModel> DecreeModels
        {
            get
            {
                if (_decreeModels == null)
                {
                    _decreeModels = new ObservableCollection<DecreeModel>();
                }

                return _decreeModels;
            }

            private set => SetProperty(ref _decreeModels, value);
        }

        /// <summary>
        /// Gets or sets selected <see cref="DecreeModel"/>.
        /// </summary>
        public DecreeModel SelectedItem
        {
            get => _selectedDecreeModel;
            set
            {
                SetProperty(ref _selectedDecreeModel, value);
                OpenDetailsExecute();
            }
        }

        /// <summary>
        /// Gets Details Case view model.
        /// </summary>
        public DetailsCaseViewModel DetailsCaseViewModel
        {
            get => _detailsCaseViewModel;
            private set => SetProperty(ref _detailsCaseViewModel, value);
        }

        /// <summary>
        /// Gets or sets text for searching.
        /// </summary>
        public string SearchCriteriaText
        {
            get => _searchCriteriaText;
            set
            {
                SetProperty(ref _searchCriteriaText, value);

                if (string.IsNullOrEmpty(value))
                {
                    ClearSearchCommand.Execute(null);
                }
            }
        }

        /// <summary>
        /// Gets or sets flag that responsible for showing/hiding Details screen.
        /// </summary>
        public bool IsDetailsMode
        {
            get => _isDetailsModel;
            set => SetProperty(ref _isDetailsModel, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets command that opens details screen.
        /// </summary>
        public ICommand OpenDetailsCommand { get; }

        /// <summary>
        /// Gets command that performs searching through the <see cref="DecreeModels"/>. 
        /// </summary>
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Gets command that clears search field.
        /// </summary>
        public ICommand ClearSearchCommand { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes DecreeViewModel.
        /// </summary>
        public void Initialize()
        {
            DecreeModels = _decreeService.GetDecrees().ToObservableCollection();

            _decreeModelsOriginal = DecreeModels.ToArray();
        }

        /// <summary>
        /// Executes switching to DetailsScreen.
        /// </summary>
        private void OpenDetailsExecute()
        {
            DetailsCaseViewModel = new DetailsCaseViewModel(SelectedItem, _client);
            IsDetailsMode = true;
        }

        /// <summary>
        /// Performs searching through the <see cref="DecreeModels"/>.
        /// </summary>
        private void ApplySearch()
        {
            if (IsDetailsMode) return;

            DecreeModels = _decreeModelsOriginal.Where(IsModelContainsInfo).ToObservableCollection();
        }

        /// <summary>
        /// Checks if concrete <see cref="DecreeModel"/> from <see cref="DecreeModels"/> contains search criteria.
        /// </summary>
        /// <param name="model"><see cref="DecreeModel"/> from <see cref="DecreeModels"/>.</param>
        /// <returns>True if yes; False if not.</returns>
        private bool IsModelContainsInfo(DecreeModel model)
        {
            return model.RulingNumber.Contains(SearchCriteriaText) ||
                   model.Place.Contains(SearchCriteriaText) ||
                   model.Car.Number.Contains(SearchCriteriaText);

        }

        #endregion
    }
}