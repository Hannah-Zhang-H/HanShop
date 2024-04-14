using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public int? UserId  { get; set; }

        public string OrderNumber { get; set; }

        public decimal SumPrice { get; set; }

        public string Notes { get; set; }

        public DateTime CreatingTime { get; set; }

        public bool IsPaid { get; set; }

        public int OrderState { get; set; }

        public string PaymentMethod { get; set; }

    }
}