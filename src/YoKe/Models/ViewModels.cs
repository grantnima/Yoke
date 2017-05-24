using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YoKe.Models;

namespace YoKe.Models
{
  
    public class HomeIndexViewModel
    {
        public List<ProductList> Products { get; set; }
    }

    public class ProductList
    {
        public Product p { get; set; }
    }

    public class CartItem
    {
        public string productName { get; set; }
        public double price { get; set; }
        public int qty { get; set; }
    }
}
