using System;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Models
{
    /// <summary>
    /// Implements <see cref="IDecreeModel"/> interface.
    /// </summary>
    public class DecreeModel : IDecreeModel
    {
        #region Properties

        /// <summary>
        /// Gets Decree ruling number.
        /// </summary>
        public string RulingNumber { get; set; }

        /// <summary>
        /// Gets Decree pdf file URL.
        /// </summary>
        public string PdfPath { get; set; }

        /// <summary>
        /// Gets act place address.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets evacuation address.
        /// </summary>
        public string EvacuationAddress { get; set; }

        /// <summary>
        /// Gets payment status.
        /// </summary>
        public bool PaymentStatus { get; set; }

        /// <summary>
        /// Gets act date and time.
        /// </summary>
        public DateTime? ActData { get; set; }

        /// <summary>
        /// Gets evacuation date and time.
        /// </summary>
        public DateTime? EvacuationDateTime { get; set; }

        /// <summary>
        /// Gets <see cref="CarModel"/>.
        /// </summary>
        public CarModel Car { get; set; }

        /// <summary>
        /// Gets count of photos.
        /// </summary>
        public int PhotoCount => PhotoModels?.Count() ?? 0;

        /// <summary>
        /// Gets collection of <see cref="PhotoModel"/> models.
        /// </summary>
        public IEnumerable<PhotoModel> PhotoModels { get; set; }

        /// <summary>
        /// Gets view string of <see cref="ActData"/> property.
        /// </summary>
        public string ActDateTimeString => 
            ActData.HasValue ? ActData.Value.ToString("g") : "-";

        /// <summary>
        /// Gets view string of <see cref="EvacuationDateTime"/> property.
        /// </summary>
        public string EvacuationDateTimeString =>
            EvacuationDateTime.HasValue ? EvacuationDateTime.Value.ToString("g") : "-";

        #endregion
    }
}