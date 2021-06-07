using Diploma.Factory;
using HttpClient.Messaging;

namespace Diploma.ViewModels.MenuItems
{
    /// <summary>
    /// Defines MainPage MenuItem ViewModel.
    /// </summary>
    public class MainPageMenuItemViewModel : BaseMenuItemViewModel
    {
        /// <summary>
        /// Stores MenuItem name.
        /// </summary>
        private const string MenuItemNameConst = "MAIN PAGE";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModelFactory">Reference to <see cref="IViewModelFactory"/> interface.</param>
        public MainPageMenuItemViewModel(IViewModelFactory viewModelFactory) 
            : base(viewModelFactory)
        {
            ExecuteCommand();
        }

        /// <summary>
        /// Gets MenuItem name.
        /// </summary>
        public override string MenuItemName => MenuItemNameConst;

        /// <summary>
        /// Executes MenuItem command.
        /// </summary>
        protected sealed override void ExecuteCommand()
        {
            base.ExecuteCommand();

            DefaultMessenger.Send(new IntraMessageViewModelSwitch(typeof(MainPageViewModel)),
                typeof(MainWindowViewModel), null);
        }
    }
}