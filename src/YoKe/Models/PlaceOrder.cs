using System;
using System.Collections.Generic;

namespace YoKe.Models
{
    public partial class PlaceOrder
    {
        public PlaceOrder()
        {
            TakeOrder = new HashSet<TakeOrder>();
        }

        public int ObjId { get; set; }
        public int? TheCustomer { get; set; }
        public string Brand { get; set; }
        public string Producer { get; set; }
        public string TheProductName { get; set; }
        public string BigImg { get; set; }
        public double? Price { get; set; }
        public string Address { get; set; }
        public int? Quantity { get; set; }
        public int? ThePaymentType { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<TakeOrder> TakeOrder { get; set; }
        public virtual Customer TheCustomerNavigation { get; set; }
        public virtual PaymentType ThePaymentTypeNavigation { get; set; }
    }
}
