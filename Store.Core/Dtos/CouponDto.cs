using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Dtos
{
    public class CouponDto : BaseEntity<string>
    {
        public string Code { get; set; }

        public decimal DiscountAmount { get; set; }

        public DateTime ExpiaryDate { get; set; }

        public bool IsActive { get; set; }
    }
}
