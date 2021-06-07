using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Photo
    {
        [Key]
        public long Id { get; set; }

        public string FilePath { get; set; }

        public Decree Decree { get; set; }
    }
}