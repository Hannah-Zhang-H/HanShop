using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatingTime { get; set; }

        [Required(ErrorMessage = "The ReviewContent cannot be blank")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Please enter a ReviewContent between 1 and 100 characters in length")]
        [Display(Name = "ReviewContent")]
        public string ReviewContent { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}