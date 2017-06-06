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
        public List<PlaceOrder> POrders { get; set; }
    }

    public class ProductList
    {
        public Product p { get; set; }
        public Orders o { get; set; }
        public PlaceOrder po { get; set; }
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
        public Payment payment { get; set; }
        public List<OrderInfo> orders { get; set; }
        public int orderQty { get; set; }
    }
    public class PayRequestInfo
    {
        public string PostUrl { get; set; }
        public string MerId { get; set; }
        public string Amt { get; set; }
        public string PaymentTypeObjId { get; set; }
        public string MerTransId { get; set; }
        public string ReturnUrl { get; set; }
        public string CheckValue { get; set; }

    }

}
