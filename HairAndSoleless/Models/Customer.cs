using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HairAndSoleless.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [Remote("ContactEmailExists", "RemoteValidation", ErrorMessage = "You're not a customer if you have an @avega.se address")]
        public string Contact { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
