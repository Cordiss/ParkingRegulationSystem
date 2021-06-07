using System.Collections.Generic;

using DataAccess.Models;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines repository for <see cref="Decree"/> entity.
    /// </summary>
    public interface IDecreeRepository
    {
        /// <summary>
        /// Gets <see cref="Decree"/> by ruling number.
        /// </summary>
        /// <param name="number"><see cref="Decree"/> ruling number.</param>
        /// <returns><see cref="Decree"/> entity.</returns>
        Decree GetByNumber(string number);

        /// <summary>
        /// Checks if there are some rows in database.
        /// </summary>
        /// <returns>True - database is not empty; False - db is empty.</returns>
        bool IsSomeDecreeExists();

        /// <summary>
        /// Gets collection of <see cref="Decree"/> entities.
        /// </summary>
        /// <returns><see cref="IEnumerable{Decree}"/>.</returns>
        IEnumerable<Decree> Get();

        /// <summary>
        /// Creates new row in <see cref="Decree"/> table.
        /// </summary>
        /// <param name="entity"><see cref="Decree"/> object.</param>
        /// <returns>Status of operation.</returns>
        bool Create(Decree entity);
    }
}