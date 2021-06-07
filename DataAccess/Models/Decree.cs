using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Decree
    {
        [Key]
        public string RulingNumber { get; set; }

        public string PdfPath { get; set; }

        public string Place { get; set; }

        public string EvacuationAddress { get; set; }

        public bool PaymentStatus { get; set; }

        public DateTime? ActData { get; set; }

        public DateTime? EvacuationDateTime { get; set; }

        public Car Car { get; set; }        
    }
}