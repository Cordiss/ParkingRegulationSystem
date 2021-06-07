using System;
using System.Windows.Input;

using DevExpress.Mvvm;
using Diploma.Factory;

namespace Diploma.ViewModels.MenuItems
{
    /// <summary>
    /// Defines BaseMenuItem ViewModel.
    /// </summary>
    public class BaseMenuItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Reference to <see cref="IViewModelFactory"/> interface.
        /// </summary>
        protected readonly IViewModelFactory DefaultViewModelFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModelFactory">Reference to <see cref="IViewModelFactory"/> interface.</param>
        public BaseMenuItemViewModel(IViewModelFactory viewModelFactory)
        {
            DefaultViewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            Command = new DelegateCommand(ExecuteCommand);
        }

        /// <summary>
        /// Stores reference to current shown ViewModel.
        /// </summary>
        public ViewModelBase CurrentViewModel { get; protected set; }

        /// <summary>
        /// Gets MenuItem name.
        /// </summary>
        public virtual string MenuItemName { get; protected set; } 

        /// <summary>
        /// Gets MenuItem command.
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// Executes MenuItem command.
        /// </summary>
        protected virtual void ExecuteCommand()
        {
            if (!CanExecuteCommand()) return;

            if (CurrentViewModel != null)
            {
                CurrentViewModel.Dispose();
                CurrentViewModel = null;
            }
        }

        /// <summary>
        /// Verifies if command can be executed.
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanExecuteCommand()
        {
            return true;
        }
    }
}