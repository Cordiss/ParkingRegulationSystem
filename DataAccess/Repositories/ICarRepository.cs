
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface ICarRepository
    {
        Car GetByNumber(string number);
        bool Create(string number, string model);
    }
}