using StackExchange.Redis;
using Store.Core.Entities.Order; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IOrderService
    {
       Task<OrderO> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress);
         

       Task<IEnumerable<OrderO>?> GetOrderForSpecificUserAsync(string buyerEmail); 


       Task<OrderO?> GetOrderByIdForSpecificUserAsync(string buyerEmail , int orderId);


    } 
}
