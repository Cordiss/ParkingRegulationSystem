using Diploma.Helpers;

namespace Diploma.ViewModels
{
    /// <summary>
    /// Defines view model for waiter screen.
    /// </summary>
    public class WaiterViewModel : ViewModelBase
    {
        /// <summary>
        /// Backend field for <see cref="LoadedProgress"/> property.
        /// </summary>
        private int _loadedProgress;

        /// <summary>
        /// Backend field for <see cref="SavedProgress"/> property.
        /// </summary>
        private int _savedProgress;

        /// <summary>
        /// Backend field for <see cref="Limit"/> property.
        /// </summary>
        private int _limit;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="limit">Among data to load.</param>
        public WaiterViewModel(int limit)
        {
            Limit = limit;
        }

        /// <summary>
        /// Gets or sets among count of loaded data.
        /// </summary>
        public int LoadedProgress
        {
            get => _loadedProgress;
            set => SetProperty(ref _loadedProgress, value);
        }

        /// <summary>
        /// Gets or sets among count of saved data.
        /// </summary>
        public int SavedProgress
        {
            get => _savedProgress;
            set => SetProperty(ref _savedProgress, value);
        }

        /// <summary>
        /// Gets current result of operation.
        /// </summary>
        [DependsOnProperty(nameof(LoadedProgress))]
        [DependsOnProperty(nameof(SavedProgress))]
        public int ActualResult => (LoadedProgress + SavedProgress) / 2;

        /// <summary>
        /// Gets among data count.
        /// </summary>
        public int Limit
        {
            get => _limit;
            private set => SetProperty(ref _limit, value);
        }
    }
}