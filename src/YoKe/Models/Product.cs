using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace YoKe.Models
{
    public partial class Product
    {
        public Product()
        {
            Evaluate = new HashSet<Evaluate>();
            Orders = new HashSet<Orders>();
        }

        public int ObjId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Feature { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public string SmallImg { get; set; }
        public string BigImg { get; set; }
        public int? ProductState { get; set; }
        public int? TheCustomer { get; set; }
        public string ProductType { get; set; }

        public virtual ICollection<Evaluate> Evaluate { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
