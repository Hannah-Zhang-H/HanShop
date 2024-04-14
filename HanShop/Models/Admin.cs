using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The admin name cannot be blank")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter an admin name between 1 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter an admin name made up of letters only")]
        [Display(Name = "admin name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Password cannot be blank")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Power { get; set; }

        public DateTime AddingTime { get; set; }

    }
}