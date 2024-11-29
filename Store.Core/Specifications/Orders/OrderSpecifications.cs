using Store.Core.Entities;
using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecification<OrderO,int>
    {

        //public OrderSpecifications(string buyerEmail)
        //{
        //    BuyerEmail = buyerEmail;
        //}

        public OrderSpecifications(string buyerEmail , int orderId) :base(O => O.BuyerEmail == buyerEmail && O.Id == orderId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.OrderItems);
        }
        public OrderSpecifications(string buyerEmail) :base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod); 
            Includes.Add(O => O.OrderItems);
        }

    }
} 
