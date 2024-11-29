using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Order
{
    public class OrderO : BaseEntity<int>
    {
        public OrderO()
        {

        }

        public OrderO(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethodId, ICollection<OrderItem> orderItems, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethodId;
            OrderItems = orderItems; 
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public int DeliveryMethodId { get; set; } // FK  

        public DeliveryMethod DeliveryMethod { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public decimal  SubTotal { get; set; }

        public decimal GetTotal() => SubTotal+DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; }

        public object BuyerEmail1 { get; }

        public object Value { get; }
    }
}
