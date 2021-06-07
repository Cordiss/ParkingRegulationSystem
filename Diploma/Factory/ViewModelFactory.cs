using System;

using DataAccess.Factory;
using DataAccess.Repositories;

using Diploma.Services;
using Diploma.ViewModels;

using HttpClient;
using HttpClient.Factory;
using HttpClient.Messaging;
using HttpClient.Services;

using ViewModelBase = Diploma.ViewModels.ViewModelBase;

namespace Diploma.Factory
{
    /// <summary>
    /// Implementation of <see cref="IViewModelFactory"/> interface.
    /// </summary>
    public class ViewModelFactory : IViewModelFactory
    {
        /// <summary>
        /// Creates new ViewModel instance.
        /// </summary>
        /// <param name="viewModelType">Type of ViewModel.</param>
        /// <returns>New ViewModel instance.</returns>
        public ViewModelBase CreateViewModelByType(Type viewModelType)
        {
            if (viewModelType == typeof(DecreesViewModel))
            {
                IDecreeRepository decreeRepository = DecreeRepositoryFactory.GetRepository();
                IPhotoRepository photoRepository = PhotoRepositoryFactory.GetRepository();

                IDecreeService decreeService =  DecreesServiceFactory.GetDecreeService(decreeRepository, photoRepository);

                IRequestDateService requestDateService = RequestDataServiceFactory.GetRequestDateService(DefaultMessenger.Instance);
                IErrorSink errorSink = ErrorSinkFactory.GetErrorSink();
                IClient client = new Client(requestDateService, errorSink);

                return new DecreesViewModel(decreeService, client);
            }

            if (viewModelType == typeof(MainWindowViewModel))
            {
                return new MainWindowViewModel(this);
            }

            if (viewModelType == typeof(MainPageViewModel))
            {
                IRequestDateService requestDateService = RequestDataServiceFactory.GetRequestDateService(DefaultMessenger.Instance);
                IErrorSink errorSink = ErrorSinkFactory.GetErrorSink();
                IClient client = new Client(requestDateService, errorSink);
                return new MainPageViewModel(client);
            }

            return null;
        }
    }
}