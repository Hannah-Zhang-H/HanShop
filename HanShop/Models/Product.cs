using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "The product name cannot be blank")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter a product name between 3 and 50 characters in length")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The price cannot be blank")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The SalePrice cannot be blank")]
        public decimal? SalePrice { get; set; }
        [Required(ErrorMessage = "The storage cannot be blank")]
        public int storage { get; set; }
        [Required(ErrorMessage = "The Description cannot be blank")]
        public string Description { get; set; }
        public int? CategoryID { get; set; }

        [Required(ErrorMessage = "The ProductCode cannot be blank")]
        public string ProductCode { get; set; }
        public string Img { get; set; }
        public virtual Category Category
        {
            get; set;
        }
        public virtual ICollection<Review>Revies { get; set; }




    }
}