using System.Collections.Generic;

using DataAccess.Models;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines repository for <see cref="Photo"/> instance.
    /// </summary>
    public interface IPhotoRepository
    {
        /// <summary>
        /// Gets <see cref="Photo"/> entity by id.
        /// </summary>
        /// <param name="id"><see cref="Photo"/> ID.</param>
        /// <returns><see cref="Photo"/> entity.</returns>
        Photo GetById(long id);

        /// <summary>
        /// Gets collection of <see cref="Photo"/> entities.
        /// </summary>
        /// <param name="decreeNumber"><see cref="Decree"/> ruling number.</param>
        /// <returns><see cref="IEnumerable{Photo}"/>.</returns>
        IEnumerable<Photo> GetByDecreeNumber(string decreeNumber);

        /// <summary>
        /// Creates new row in database.
        /// </summary>
        /// <param name="filePath"><see cref="Photo"/> image path.</param>
        /// <param name="decree">Reference to <see cref="Decree"/> entity.</param>
        /// <returns>Status of operation.</returns>
        bool Create(string filePath, Decree decree);
    }
}