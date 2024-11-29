using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Core.Dtos;
using Store.Core.Dtos.Orders;
using Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.AutoMapping.Orders
{
    public class OrderProfile : Profile
    {
        private readonly IConfiguration configuration;

        public OrderProfile(IConfiguration configuration)
        {
            this.configuration = configuration;


            CreateMap<OrderO, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(d => d.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, options => options.MapFrom(d => d.DeliveryMethod.Cost));
                
            CreateMap<Address,AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                                .ForMember(d => d.ProductId, options => options.MapFrom(d => d.Product.ProductId))
                                .ForMember(d => d.ProductName, options => options.MapFrom(d => d.Product.ProductName))
                                .ForMember(d => d.PictureUrl, options => options.MapFrom(d => $"{configuration["BaseURL"]}{d.Product.PictureUrl}"));
        }
    }
}
