using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DataAccess.Factory;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation
{
    /// <summary>
    /// Implements <see cref="IDecreeRepository"/> interface.
    /// </summary>
    public class DecreeRepository : IDecreeRepository
    {
        /// <summary>
        /// Reference to <see cref="DataAccessContext"/> instance.
        /// </summary>
        private readonly DataAccessContext _dataAccessContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DecreeRepository()
        {
            _dataAccessContext = DataAccessContextFactory.GetDataContext();
        }

        /// <summary>
        /// Gets <see cref="Decree"/> by ruling number.
        /// </summary>
        /// <param name="number"><see cref="Decree"/> ruling number.</param>
        /// <returns><see cref="Decree"/> entity.</returns>
        public Decree GetByNumber(string number)
        {
            return _dataAccessContext.Decrees.Include(x => x.Car).FirstOrDefault(d => d.RulingNumber.Equals(number));
        }

        /// <summary>
        /// Gets collection of <see cref="Decree"/> entities.
        /// </summary>
        /// <returns><see cref="IEnumerable{Decree}"/>.</returns>
        public IEnumerable<Decree> Get()
        {
            return _dataAccessContext.Decrees.OrderBy(d => d.RulingNumber).Include(x => x.Car).ToList();
        }

        /// <summary>
        /// Creates new row in <see cref="Decree"/> table.
        /// </summary>
        /// <param name="entity"><see cref="Decree"/> object.</param>
        /// <returns>Status of operation.</returns>
        public bool Create(Decree entity)
        {
            if (IsEntityExists(entity.RulingNumber))
            {
                return true;
            }

            var car = IsCarExists(entity.Car.Number);
            if (car != null)
            {
                entity.Car = car;
            }

            _dataAccessContext.Decrees.Add(entity);
            return _dataAccessContext.SaveChanges() != 0;
        }

        /// <summary>
        /// Checks if there are some rows in database.
        /// </summary>
        /// <returns>True - database is not empty; False - db is empty.</returns>
        public bool IsSomeDecreeExists()
        {
            return _dataAccessContext.Decrees.FirstOrDefault() != null;
        }

        /// <summary>
        /// Checks if there is <see cref="Decree"/> in db with same ruling number.
        /// </summary>
        /// <param name="rulingNumber"><see cref="Decree"/> ruling number.</param>
        /// <returns>True - Exists; False - No;</returns>
        private bool IsEntityExists(string rulingNumber)
        {
            return _dataAccessContext.Decrees.FirstOrDefault(x => x.RulingNumber.Equals(rulingNumber)) != null;
        }

        /// <summary>
        /// Checks if there is <see cref="Car"/> entity in db with same car number.
        /// </summary>
        /// <param name="carNumber"><see cref="Car"/> number.</param>
        /// <returns><see cref="Car"/> instance.</returns>
        private Car IsCarExists(string carNumber)
        {
            return _dataAccessContext.Car.FirstOrDefault(x => x.Number == carNumber);
        }
    }
}