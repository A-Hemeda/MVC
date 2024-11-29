using Microsoft.AspNetCore.Http.HttpResults;
using Store.Core;
using Store.Core.Dtos;
using Store.Core.Entities.Order;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CouponDto> ValidateCouponAsync(string couponCode) 
        {
            var coupon = await _unitOfWork.Repository<CouponDto, string>().GetByIdAsync(couponCode);
            if (coupon is null) throw new ApplicationException("Invalid coupon code."); ;
            if (coupon.ExpiaryDate < DateTime.UtcNow) throw new ApplicationException("This coupon has expired.");
            if (!coupon.IsActive)throw new ApplicationException("This coupon is not active.");
            return coupon;
        }
       
    }
}
