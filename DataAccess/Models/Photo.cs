using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    /// <summary>
    /// Defines Photo entity.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets primary key for Photo entity in db table.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets path to image file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Decree"/> instance.
        /// </summary>
        public Decree Decree { get; set; }
    }
}