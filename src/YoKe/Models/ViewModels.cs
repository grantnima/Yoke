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
        public int id { get; set; }
        public double price { get; set; }
        public int qty { get; set; }
    }

    public class OrderInfo
    {
        public int theCustomer { get; set; }
        public int theProduct { get; set; }
        public int thePayment { get; set; }
        public int orderState { get; set; }
        public string productName { get; set; }
        //public DateTime orderTime { get; set; }
    }

    public class OrderViewModel
    {
        public Customer curCustomer { get; set; }
        
        public List<OrderInfo> orders { get; set; }
        public int orderQty { get; set; }
    }
}
