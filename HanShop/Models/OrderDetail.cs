﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HanShop.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id  { get; set; }

        public int? OrderId  { get; set; }

        public virtual Order Order { get; set; }

        public int Count  { get; set; }

        public decimal Price { get; set; }

        public decimal SumPrice { get; set; }

        public int? ProductId  { get; set; }

        public virtual Product Product { get; set; }

        public string Title { get; set; }


    }
}