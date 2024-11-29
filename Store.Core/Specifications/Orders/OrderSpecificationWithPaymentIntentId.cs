using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Orders
{
    public class OrderSpecificationWithPaymentIntentId : BaseSpecification<OrderO,int>
    {

        public OrderSpecificationWithPaymentIntentId(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.OrderItems);
        }



    }
}
