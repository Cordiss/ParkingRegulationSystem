using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Car
    {
        [Key]
        public string Number { get; set; }

        public string Model { get; set; }
    }
}