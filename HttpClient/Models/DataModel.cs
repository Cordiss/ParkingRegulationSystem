using System.Collections.Generic;

namespace HttpClient.Models
{
    /// <summary>
    /// Defines model according to second JSON response layer.
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// Gets decree payment status.
        /// </summary>
        public bool PaymentStatus { get; set; }

        /// <summary>
        /// Gets decree ruling number.
        /// </summary>
        public string RulingNumber { get; set; }

        /// <summary>
        /// Gets array of image path strings.
        /// </summary>
        public List<string> Photos { get; set; }

        /// <summary>
        /// Gets URL of pdf file.
        /// </summary>
        public string Pdf { get; set; }

        /// <summary>
        /// Gets evacuation address.
        /// </summary>
        public string EvacuationAddress { get; set; }

        /// <summary>
        /// Gets evacuation date and time.
        /// </summary>
        public string EvacuationDatetime { get; set; }

        /// <summary>
        /// Gets act date and time.
        /// </summary>
        public string ActDate { get; set; }

        /// <summary>
        /// Gets act place address.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets car number.
        /// </summary>
        public string CarNumber { get; set; }
    }
}