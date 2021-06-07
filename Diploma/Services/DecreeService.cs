using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess.Repositories;

using Diploma.Helpers;
using Diploma.Models;

namespace Diploma.Services
{
    /// <summary>
    /// Implementation of <see cref="IDecreeService"/> interface.
    /// </summary>
    public class DecreeService : IDecreeService
    {
        /// <summary>
        /// Reference to <see cref="IDecreeRepository"/> interface.
        /// </summary>
        private readonly IDecreeRepository _decreeRepository;

        /// <summary>
        /// Reference to <see cref="IPhotoRepository"/> interface.
        /// </summary>
        private readonly IPhotoRepository _photoRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="decreeRepository">Reference to <see cref="IDecreeRepository"/> interface.</param>
        /// <param name="photoRepository">Reference to <see cref="IPhotoRepository"/> interface.</param>
        public DecreeService(IDecreeRepository decreeRepository,
            IPhotoRepository photoRepository)
        {
            _decreeRepository = decreeRepository ?? throw new ArgumentNullException(nameof(decreeRepository));
            _photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
        }

        /// <summary>
        /// Gets Decree models.
        /// </summary>
        /// <returns>Collection of <see cref="DecreeModel"/>.</returns>
        public IEnumerable<DecreeModel> GetDecrees()
        {
            var result = _decreeRepository.Get().ToModels().ToList();

            foreach (var decree in result)
            {
                var photos = _photoRepository.GetByDecreeNumber(decree.RulingNumber);
                decree.PhotoModels = photos.ToPhotoModel();
            }

            return result;
        }
    }
}