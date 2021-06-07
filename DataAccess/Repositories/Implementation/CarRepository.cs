using System.Linq;

using DataAccess.Factory;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation
{
    /// <summary>
    /// Implements <see cref="ICarRepository"/> interface.
    /// </summary>
    public class CarRepository : ICarRepository
    {
        /// <summary>
        /// Reference to <see cref="DataAccessContext"/> instance.
        /// </summary>
        private readonly DataAccessContext _dataAccessContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public CarRepository()
        {
            _dataAccessContext = DataAccessContextFactory.GetDataContext();
        }

        /// <summary>
        /// Gets <see cref="Car"/> entity by number.
        /// </summary>
        /// <param name="number"><see cref="Car"/> number.</param>
        /// <returns><see cref="Car"/> entity.</returns>
        public Car GetByNumber(string number)
        {
            var g = _dataAccessContext.Car.FirstOrDefault(c => c.Number.Equals(number));
            return g;
        }

        /// <summary>
        /// Creates one more row in database in <see cref="Car"/> table.
        /// </summary>
        /// <param name="number"><see cref="Car"/> number.</param>
        /// <param name="model"><see cref="Car"/> model.</param>
        /// <returns>Status of operation.</returns>
        public bool Create(string number, string model)
        {
            var car = new Car
            {
                Number = number,
                Model = model
            };

            _dataAccessContext.Car.Add(car);
            return _dataAccessContext.SaveChanges() != 0;
        }
    }
}