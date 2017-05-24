using System;
using System.Collections.Generic;

namespace YoKe.Models
{
    public partial class Orders
    {
        public int ObjId { get; set; }
        public int? TheCustomer { get; set; }
        public int? TheProduct { get; set; }
        public int? ThePayment { get; set; }
        public int? OrderState { get; set; }
        public DateTime? OrderTime { get; set; }

        public virtual Customer TheCustomerNavigation { get; set; }
        public virtual PaymentType ThePaymentNavigation { get; set; }
        public virtual Product TheProductNavigation { get; set; }
    }
}
