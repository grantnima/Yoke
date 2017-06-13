using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YoKe.Models;
using Microsoft.AspNetCore.Http;

namespace YoKe.Models
{
  
    public class HomeIndexViewModel
    {
        public List<ProductList> Products { get; set; }
        public List<PlaceOrder> POrders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class ProductList
    {
        public int ObjId { get; set; }
        public Product p { get; set; }
        public List<Product> Catproduct{ get; set; }
        public Orders o { get; set; }
        public List<Product> PProducts { get; set; }
        public PlaceOrder po { get; set; }
        public List<PlaceOrder> POrders { get; set; }
        //public List<PlacceOrderList> PlaceOrders { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public string img { get; set; }
        public IFormFile PImg { get; set; }
        public IFormFile OImg { get; set; }
        public PagingInfo PagingInfoOrd { get; set; }
        public PagingInfo PagingInfoPro { get; set; }
        public MemberHomeModel MemberHome { get; set; }
        public SOrder so { get; set; }

    }
    public class PlacceOrderList
    {
        public PlaceOrder plo { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public string Remarks { get; set; }
        public double? Price { get; set; }
        public int? theCustomer { get; set; }
        public string img { get; set; }
        
    }

    public class CartItem
    {
        public string productName { get; set; }
        public int id { get; set; }
        public double price { get; set; }
        public int qty { get; set; }
        public string img { get; set; }
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
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
    public class MemberHomeModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class SOrder
    {
        public int o_no { get; set; }
        public string p_name { get; set; }
        public double price { get; set; }
        public string img { get; set; }
    }
    

}
