using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoKe.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
            PlaceOrder = new HashSet<PlaceOrder>();
            TakeOrder = new HashSet<TakeOrder>();
        }

        public int ObjId { get; set; }

        
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int? UserId { get; set; }
        public int? TheCustomerType { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? RegistDate { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<PlaceOrder> PlaceOrder { get; set; }
        public virtual ICollection<TakeOrder> TakeOrder { get; set; }
        public virtual CustomerType TheCustomerTypeNavigation { get; set; }
    }
}
