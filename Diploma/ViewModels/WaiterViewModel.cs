using Diploma.Helpers;

namespace Diploma.ViewModels
{
    public class WaiterViewModel : ViewModelBase
    {
        private int _loadedProgress;
        private int _savedProgress;
        private int _limit;

        public WaiterViewModel(int limit)
        {
            Limit = limit;
        }

        public int LoadedProgress
        {
            get => _loadedProgress;
            set => SetProperty(ref _loadedProgress, value);
        }

        public int SavedProgress
        {
            get => _savedProgress;
            set => SetProperty(ref _savedProgress, value);
        }

        [DependsOnProperty(nameof(LoadedProgress))]
        [DependsOnProperty(nameof(SavedProgress))]
        public int ActualResult => (LoadedProgress + SavedProgress) / 2;

        public int Limit
        {
            get => _limit;
            private set => SetProperty(ref _limit, value);
        }
    }
}