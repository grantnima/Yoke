using System;
using System.Collections.Generic;

namespace YoKe.Models
{
    public partial class TakeOrder
    {
        public int ObjId { get; set; }
        public int? ThePlaceOrder { get; set; }
        public int? TheCustomer { get; set; }

        public virtual Customer TheCustomerNavigation { get; set; }
        public virtual PlaceOrder ThePlaceOrderNavigation { get; set; }
    }
}
