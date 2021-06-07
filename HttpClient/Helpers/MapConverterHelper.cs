using System;
using System.Collections.Generic;
using DataAccess.Models;

using HttpClient.Models;

namespace HttpClient.Helpers
{
    /// <summary>
    /// Defines extension class that allows map entities to models.
    /// </summary>
    public static class MapConverterHelper
    {
        /// <summary>
        /// Converts <see cref="RequestModel"/> to entity.
        /// </summary>
        /// <param name="requestModel">Instance of <see cref="RequestModel"/>.</param>
        /// <returns><see cref="Decree"/> entity.</returns>
        public static Decree ToEntity(this RequestModel requestModel)
        {
            Decree entity = new Decree
            {
                RulingNumber = requestModel.Data.RulingNumber,
                PdfPath = requestModel.Data.Pdf,
                Place = requestModel.Data.Place,
                EvacuationAddress = requestModel.Data.EvacuationAddress,
                PaymentStatus = requestModel.Data.PaymentStatus,
                Car = new Car
                {
                    Number = requestModel.Data.CarNumber
                }
            };

            entity.ActData = string.IsNullOrEmpty(requestModel.Data.ActDate)
                ? (DateTime?) null
                : DateTime.Parse(requestModel.Data.ActDate);

            entity.EvacuationDateTime = string.IsNullOrEmpty(requestModel.Data.EvacuationDatetime)
                ? (DateTime?) null
                : DateTime.Parse(requestModel.Data.EvacuationDatetime);

            return entity;
        }

        /// <summary>
        /// Converts <see cref="DataModel"/> to <see cref="Photo"/> array.
        /// </summary>
        /// <param name="dataModel"><see cref="DataModel"/> instance.</param>
        /// <param name="decree"><see cref="Decree"/> instance.</param>
        /// <returns>Array of <see cref="Photo"/>.</returns>
        public static Photo[] ToEntity(this DataModel dataModel, Decree decree)
        {
            List<Photo> photos = new List<Photo>();

            foreach (var photo in dataModel.Photos)
            {
                Photo entity = new Photo
                {
                    FilePath = photo,
                    Decree = decree
                };

                photos.Add(entity);
            }

            return photos.ToArray();
        }
    }
}