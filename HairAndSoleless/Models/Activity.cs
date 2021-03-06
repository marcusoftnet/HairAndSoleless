﻿using System;
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

        [Display(Name = "Coach"), Required]
        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        [Display(Name = "Customer"), Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}