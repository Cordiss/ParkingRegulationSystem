using DataAccess.Models;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Defines repository for <see cref="Car"/> entity.
    /// </summary>
    public interface ICarRepository
    {
        /// <summary>
        /// Gets <see cref="Car"/> entity by number.
        /// </summary>
        /// <param name="number"><see cref="Car"/> number.</param>
        /// <returns><see cref="Car"/> entity.</returns>
        Car GetByNumber(string number);

        /// <summary>
        /// Creates one more row in database in <see cref="Car"/> table.
        /// </summary>
        /// <param name="number"><see cref="Car"/> number.</param>
        /// <param name="model"><see cref="Car"/> model.</param>
        /// <returns>Status of operation.</returns>
        bool Create(string number, string model);
    }
}