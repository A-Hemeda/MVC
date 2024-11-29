using Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos
{
    public class AddressDto
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
