using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreateAt { get; set; }  = DateTime.UtcNow;
    }
}

