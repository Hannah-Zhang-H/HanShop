using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="The user name cannot be blank")]
        [StringLength(50, MinimumLength = 1, ErrorMessage ="Please enter a user name between 1 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage ="Please enter a user name made up of letters only")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Password cannot be blank")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The nickname cannot be blank")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter a nickname name between 1 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]*$", ErrorMessage = "Please enter a nickname made up of letters and numbers only")]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }
        public string Gender { get; set; }
        public string Introduction { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}