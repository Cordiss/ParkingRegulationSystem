using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using DataAccess.Factory;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation
{
    public class DecreeRepository : IDecreeRepository
    {
        private readonly DataAccessContext _dataAccessContext;

        public DecreeRepository()
        {
            _dataAccessContext = DataAccessContextFactory.GetDataContext();
        }

        public Decree GetByNumber(string number)
        {
            return _dataAccessContext.Decrees.Include(x => x.Car).FirstOrDefault(d => d.RulingNumber.Equals(number));
        }

        public IEnumerable<Decree> Get()
        {
            return _dataAccessContext.Decrees.OrderBy(d => d.RulingNumber).Include(x => x.Car).ToList();
        }

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

        public bool IsSomeDecreeExists()
        {
            return _dataAccessContext.Decrees.FirstOrDefault() != null;
        }

        private bool IsEntityExists(string rulingNumber)
        {
            return _dataAccessContext.Decrees.FirstOrDefault(x => x.RulingNumber.Equals(rulingNumber)) != null;
        }

        private Car IsCarExists(string carNumber)
        {
            return _dataAccessContext.Car.FirstOrDefault(x => x.Number == carNumber);
        }
    }
}