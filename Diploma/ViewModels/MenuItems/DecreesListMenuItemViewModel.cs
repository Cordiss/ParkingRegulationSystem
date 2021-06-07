using Diploma.Factory;
using HttpClient.Messaging;

namespace Diploma.ViewModels.MenuItems
{
    /// <summary>
    /// Defines DecreesList MenuItem ViewModel.
    /// </summary>
    public class DecreesListMenuItemViewModel : BaseMenuItemViewModel
    {
        /// <summary>
        /// Stores MenuItem name.
        /// </summary>
        private const string MenuItemNameConst = "REGULATION LIST";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModelFactory">Reference to <see cref="IViewModelFactory"/> interface.</param>
        public DecreesListMenuItemViewModel(IViewModelFactory viewModelFactory)
            : base(viewModelFactory) { }

        /// <summary>
        /// Gets MenuItem name.
        /// </summary>
        public override string MenuItemName => MenuItemNameConst;

        /// <summary>
        /// Executes MenuItem command.
        /// </summary>
        protected override void ExecuteCommand()
        {
            base.ExecuteCommand();
            DefaultMessenger.Send(new IntraMessageViewModelSwitch(typeof(DecreesViewModel)),
                typeof(MainWindowViewModel), null);
        }
    }
}