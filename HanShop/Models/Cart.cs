using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int ProductNum { get; set; }
        public DateTime CreatingTime { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}