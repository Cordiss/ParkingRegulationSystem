using System.Linq;

using DataAccess.Factory;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation
{
    public class CarRepository : ICarRepository
    {
        private readonly DataAccessContext _dataAccessContext;

        public CarRepository()
        {
            _dataAccessContext = DataAccessContextFactory.GetDataContext();
        }

        public Car GetByNumber(string number)
        {
            var g = _dataAccessContext.Car.FirstOrDefault(c => c.Number.Equals(number));
            return g;
        }

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