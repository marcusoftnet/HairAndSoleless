using System;
using System.ComponentModel.DataAnnotations;

namespace HairAndSoleless.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        [Required]
        public string Heading { get; set; }

        [Display(Name = "Number of hours"), Range(1, 40), Required]
        public int NumberOfHours { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        public int CoachId { get; set; }
        [Required]
        public Coach Coach { get; set; }

        public int CustomerId { get; set; }
        [Required]
        public Customer Customer { get; set; }

        //public int I { get; set; }
    }
}