using System;

namespace HttpClient.Messaging
{
    /// <summary>
    /// Defines internal message that notifies about switching view models.
    /// </summary>
    public class IntraMessageViewModelSwitch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewModelType">Type of new ViewModel.</param>
        public IntraMessageViewModelSwitch(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        /// <summary>
        /// Gets <see cref="Type"/> of new ViewModel.
        /// </summary>
        public Type ViewModelType { get; }
    }
}