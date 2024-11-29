using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Order
{
    public class OrderItem :BaseEntity<int>
    {

        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder Product {  get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }




    }
}
