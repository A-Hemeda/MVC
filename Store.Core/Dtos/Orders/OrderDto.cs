using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos.Orders
{
    public class OrderDto
    {
        public string basketId { get; set; }

        public int DeliveryMethod { get; set; }

        public AddressDto ShipToAddress { get; set; }


    }

}
