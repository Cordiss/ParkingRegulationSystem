using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    /// <summary>
    /// Defines Car entity.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Gets or sets car number.
        /// </summary>
        [Key]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets car model.
        /// </summary>
        public string Model { get; set; }
    }
}