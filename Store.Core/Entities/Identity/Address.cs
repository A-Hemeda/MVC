using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Identity
{
    public class Address
    {

        public int Id{ get; set; }

        public string  Firstname { get; set; } 


        public string Lastname { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        public string AppUserId { get; set; }  // FK

        public AppUser AppUser { get; set; }

    }
}
