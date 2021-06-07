using System.Collections.Generic;
using System.Linq;

using DataAccess.Factory;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation
{
    /// <summary>
    /// Implements <see cref="IPhotoRepository"/> interface.
    /// </summary>
    public class PhotoRepository : IPhotoRepository
    {
        /// <summary>
        /// Reference to <see cref="DataAccessContext"/> instance.
        /// </summary>
        private readonly DataAccessContext _dataAccessContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PhotoRepository()
        {
            _dataAccessContext = DataAccessContextFactory.GetDataContext();
        }

        /// <summary>
        /// Gets <see cref="Photo"/> entity by id.
        /// </summary>
        /// <param name="id"><see cref="Photo"/> ID.</param>
        /// <returns><see cref="Photo"/> entity.</returns>
        public Photo GetById(long id)
        {
            return _dataAccessContext.Photo.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Gets collection of <see cref="Photo"/> entities.
        /// </summary>
        /// <param name="decreeNumber"><see cref="Decree"/> ruling number.</param>
        /// <returns><see cref="IEnumerable{Photo}"/>.</returns>
        public IEnumerable<Photo> GetByDecreeNumber(string decreeNumber)
        {
            return _dataAccessContext.Photo.Where(p => p.Decree.RulingNumber.Equals(decreeNumber)).ToList();
        }

        /// <summary>
        /// Creates new row in database.
        /// </summary>
        /// <param name="filePath"><see cref="Photo"/> image path.</param>
        /// <param name="decree">Reference to <see cref="Decree"/> entity.</param>
        /// <returns>Status of operation.</returns>
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

        /// <summary>
        /// Checks if there is row in database with same file path.
        /// </summary>
        /// <param name="filePath"><see cref="Photo"/> file path.</param>
        /// <returns>True - Exists; False - Not;</returns>
        private bool IsEntityExists(string filePath)
        {
            return _dataAccessContext.Photo.FirstOrDefault(x => x.FilePath.Equals(filePath)) != null;
        }
    }
}