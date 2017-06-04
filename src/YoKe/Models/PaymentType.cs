using System;
using System.Collections.Generic;

namespace YoKe.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Orders = new HashSet<Orders>();
            PlaceOrder = new HashSet<PlaceOrder>();
        }

        public int ObjId { get; set; }
        public string TypeName { get; set; }
        public string Url { get; set; }
        public string SmallImg { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<PlaceOrder> PlaceOrder { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
