using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Order
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; }

        public string Description { get; set; }

        public string  DeliveryTime { get; set; }

        public decimal Cost { get; set; }



    }
}
