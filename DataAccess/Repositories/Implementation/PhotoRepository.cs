using System.Collections.Generic;
using System.Linq;

using DataAccess.Factory;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataAccessContext _dataAccessContext;

        public PhotoRepository()
        {
            _dataAccessContext = DataAccessContextFactory.GetDataContext();
        }

        public Photo GetById(long id)
        {
            return _dataAccessContext.Photo.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Photo> GetByDecreeNumber(string decreeNumber)
        {
            return _dataAccessContext.Photo.Where(p => p.Decree.RulingNumber.Equals(decreeNumber)).ToList();
        }

        public bool Create(string filePath, Decree decree)
        {
            if (IsEntityExists(filePath))
            {
                return true;
            }

            var photo = new Photo
            {
                FilePath = filePath,
                Decree = decree
            };

            _dataAccessContext.Photo.Add(photo);
            return _dataAccessContext.SaveChanges() != 0;
        }

        private bool IsEntityExists(string filePath)
        {
            return _dataAccessContext.Photo.FirstOrDefault(x => x.FilePath.Equals(filePath)) != null;
        }
    }
}