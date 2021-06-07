using System.Collections.Generic;

using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IPhotoRepository
    {
        Photo GetById(long id);
        IEnumerable<Photo> GetByDecreeNumber(string decreeNumber);
        bool Create(string filePath, Decree decree);
    }
}