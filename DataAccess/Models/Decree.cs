using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    /// <summary>
    /// Defines Decree entity.
    /// </summary>
    public class Decree
    {
        /// <summary>
        /// Gets or sets ruling number.
        /// </summary>
        [Key]
        public string RulingNumber { get; set; }

        /// <summary>
        /// Gets or sets path to pdf file.
        /// </summary>
        public string PdfPath { get; set; }

        /// <summary>
        /// Gets or sets address of incident.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets evacuation address.
        /// </summary>
        public string EvacuationAddress { get; set; }

        /// <summary>
        /// Gets or sets decree payment status.
        /// </summary>
        public bool PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets datetime of incident.
        /// </summary>
        public DateTime? ActData { get; set; }

        /// <summary>
        /// Gets or sets datetime of evacuation.
        /// </summary>
        public DateTime? EvacuationDateTime { get; set; }

        /// <summary>
        /// Gets or sets car instance.
        /// </summary>
        public Car Car { get; set; }        
    }
}