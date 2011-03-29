using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HairAndSoleless.Models
{
    public class Coach
    {
        public int CoachId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Team { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}