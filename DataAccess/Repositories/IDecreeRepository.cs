using System.Collections.Generic;

using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IDecreeRepository
    {
        Decree GetByNumber(string number);
        bool IsSomeDecreeExists();
        IEnumerable<Decree> Get();
        bool Create(Decree entity);
    }
}