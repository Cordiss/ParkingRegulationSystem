using System.Collections.Generic;

using Diploma.Models;

namespace Diploma.Services
{
    /// <summary>
    /// Defines login for Decrees service.
    /// </summary>
    public interface IDecreeService
    {
        /// <summary>
        /// Gets Decree models.
        /// </summary>
        /// <returns>Collection of <see cref="DecreeModel"/>.</returns>
        IEnumerable<DecreeModel> GetDecrees();
    }
}