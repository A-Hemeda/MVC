using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Order
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(string firstname, string lastname, string street, string city, string country)
        {
            Firstname = firstname;
            Lastname = lastname;
            Street = street;
            City = city;
            Country = country;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
