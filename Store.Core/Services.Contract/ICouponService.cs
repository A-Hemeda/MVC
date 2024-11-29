using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Dtos;
using Store.Core.Entities.Order;

namespace Store.Core.Services.Contract
{
    public interface ICouponService
    {
        Task<CouponDto> ValidateCouponAsync(string couponCode);
    }
}
