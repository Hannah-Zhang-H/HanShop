using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The address cannot be blank")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter an address between 1 and 100 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9''-'.','\s]*$", ErrorMessage = "Please enter an address made up of letters, numbers, and dots only")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The receivingName cannot be blank")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter a receivingName between 1 and 50 characters in length")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a receivingName made up of letters only")]
        [Display(Name = "ReceivingName")]
        public string ReceivingName { get; set; }

        [Required(ErrorMessage = "The Phone cannot be blank")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Please enter a Phone between 1 and 20 characters in length")]
        [RegularExpression(@"^[0-9''-'\s]*$", ErrorMessage = "Please enter a Phone made up of numbers only")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        public string Notes { get; set; }
        public DateTime CreatingTime { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}