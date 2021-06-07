using System.Collections.Generic;

using DataAccess.Models;

using Diploma.Models;

namespace Diploma.Helpers
{
    /// <summary>
    /// Helper class that converts entities to models.
    /// </summary>
    public static class EntityMapper
    {
        /// <summary>
        /// Converts collection of <see cref="Decree"/> to <see cref="DecreeModel"/> collection.
        /// </summary>
        /// <param name="entities">Collection of <see cref="Decree"/> entities.</param>
        /// <returns>Collection of <see cref="DecreeModel"/> models.</returns>
        public static IEnumerable<DecreeModel> ToModels(this IEnumerable<Decree> entities)
        {
            List<DecreeModel> models = new List<DecreeModel>();

            foreach (var entity in entities)
            {
                DecreeModel model = new DecreeModel
                {
                    RulingNumber = entity.RulingNumber,
                    PdfPath = entity.PdfPath,
                    Place = entity.Place,
                    EvacuationAddress = entity.EvacuationAddress,
                    PaymentStatus = entity.PaymentStatus,
                    ActData = entity.ActData,
                    EvacuationDateTime = entity.EvacuationDateTime,
                    Car = entity.Car.ToCarModel()
                };

                models.Add(model);
            }

            return models;
        }

        /// <summary>
        /// Converts collection of <see cref="Photo"/> to <see cref="PhotoModel"/> collection.
        /// </summary>
        /// <param name="entities">Collection of <see cref="Photo"/> entities.</param>
        /// <returns>Collection of <see cref="PhotoModel"/> models.</returns>
        public static IEnumerable<PhotoModel> ToPhotoModel(this IEnumerable<Photo> entities)
        {
            List<PhotoModel> models = new List<PhotoModel>();

            foreach (var entity in entities)
            {
                PhotoModel model = new PhotoModel
                {
                    Id = entity.Id,
                    FilePath = entity.FilePath
                };

                models.Add(model);
            }

            return models;
        }

        /// <summary>
        /// Converts <see cref="Car"/> entity to <see cref="CarModel"/> model.
        /// </summary>
        /// <param name="entity"><see cref="Car"/> entity.</param>
        /// <returns><see cref="DecreeModel"/> model.</returns>
        public static CarModel ToCarModel(this Car entity)
        {
            return new CarModel
            {
                Number = entity.Number,
                Model = entity.Model
            };
        }
    }
}