using System;

using Diploma.ViewModels;

namespace Diploma.Factory
{
    /// <summary>
    /// Defines logic that allow create ViewModels.
    /// </summary>
    public interface IViewModelFactory
    {
        /// <summary>
        /// Creates new ViewModel instance.
        /// </summary>
        /// <param name="pageViewModelType">Type of ViewModel.</param>
        /// <returns>New ViewModel instance.</returns>
        ViewModelBase CreateViewModelByType(Type pageViewModelType);
    }
}