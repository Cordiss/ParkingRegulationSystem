using System;
using System.Collections.Generic;

namespace Diploma.Models
{
    /// <summary>
    /// Contains description of Decree model.
    /// </summary>
    public interface IDecreeModel
    {
        /// <summary>
        /// Gets Decree ruling number.
        /// </summary>
        string RulingNumber { get; set; }

        /// <summary>
        /// Gets Decree pdf file URL.
        /// </summary>
        string PdfPath { get; set; }

        /// <summary>
        /// Gets act place address.
        /// </summary>
        string Place { get; set; }

        /// <summary>
        /// Gets evacuation address.
        /// </summary>
        string EvacuationAddress { get; set; }

        /// <summary>
        /// Gets payment status.
        /// </summary>
        bool PaymentStatus { get; set; }

        /// <summary>
        /// Gets act date and time.
        /// </summary>
        DateTime? ActData { get; set; }

        /// <summary>
        /// Gets evacuation date and time.
        /// </summary>
        DateTime? EvacuationDateTime { get; set; }

        /// <summary>
        /// Gets <see cref="CarModel"/>.
        /// </summary>
        CarModel Car { get; set; }

        /// <summary>
        /// Gets collection of <see cref="PhotoModel"/> models.
        /// </summary>
        IEnumerable<PhotoModel> PhotoModels { get; set; }
    }
}