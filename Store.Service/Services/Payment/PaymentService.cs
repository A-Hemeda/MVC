using Microsoft.Extensions.Configuration;
using Store.Core;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Entities.Order;
using Store.Core.Services.Contract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Service.Services.Coupons;
using Product = Store.Core.Entities.Product;

namespace Store.Service.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork    _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ICouponService _couponService;
        private readonly StripeClient   _stripeClient;

        public PaymentService(IBasketService basketService, IUnitOfWork unitOfWork, IConfiguration configuration, ICouponService couponService)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _couponService = couponService;
            _stripeClient = new StripeClient(_configuration["Stripe:SecretKey"]);
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId, string? couponCode)
        {
            try
            {
                // auth Stripe : 
                StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
                // Get Basket from Database
                var basket = await _basketService.GetBasketAsync(basketId);
                if (basket is null) return null;

                // Calculate Shipping Price if Delivery Method exists
                var shippingPrice = 0m;
                if (basket.DeliveryMethodId.HasValue)
                {
                    var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                    shippingPrice = deliveryMethod?.Cost ?? 0;
                }

                // Ensure item prices are up to date
                if(basket.Items.Count > 0)
                {
                    foreach (var item in basket.Items)
                    {
                        var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);
                        if (item.Price != product.Price)
                        {
                            item.Price = product.Price;
                        }
                    }
                }

                // Calculate total amounts
                var subTotal = basket.Items.Sum(item => item.Price * item.Quantity);

                // coupon : 
                decimal discount = 0;
                if (!string.IsNullOrEmpty(couponCode))
                {
                    var coupon = await _couponService.ValidateCouponAsync(couponCode);
                    if (coupon is not null)
                    {
                        discount = coupon.DiscountAmount;
                    }
                }

                // Calc the total amount after discount if it exist : 
                var totalAmount = (long)((shippingPrice  + subTotal  - discount)* 100);

                var service = new PaymentIntentService(_stripeClient);

                PaymentIntent paymentIntent;

                if (string.IsNullOrEmpty(basket.PaymentIntentId))
                {
                    // Create new PaymentIntent
                    var createOptions = new PaymentIntentCreateOptions
                    {
                        Amount = totalAmount,
                        PaymentMethodTypes = new List<string> { "card" },
                        Currency = "usd"
                    };

                    paymentIntent = await service.CreateAsync(createOptions);
                    basket.PaymentIntentId = paymentIntent.Id;
                    basket.ClientSecret = paymentIntent.ClientSecret;
                }
                else
                {
                    // Update existing PaymentIntent
                    var updateOptions = new PaymentIntentUpdateOptions
                    {
                        Amount = totalAmount
                    };

                    paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
                    //basket.PaymentIntentId = paymentIntent.Id;
                    //basket.ClientSecret = paymentIntent.ClientSecret;
                }

                // Update basket with new PaymentIntent details
                basket = await _basketService.UpdateBasketAsync(basket);

                return basket;
            }
            catch (StripeException ex)
            {
                // Log the error (can be adjusted to your logging implementation)
                throw new ApplicationException($"Stripe error occurred: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                throw new ApplicationException($"An error occurred while processing payment intent: {ex.Message}", ex);
            }
        }

        
    }
}
