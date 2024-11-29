using AutoMapper;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Baskets
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto?> GetBasketAsync(string BasketId)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            if(basket is null )
            {
                return _mapper.Map<CustomerBasketDto>(new CustomerBasket { Id = BasketId });
            }

            return _mapper.Map<CustomerBasketDto>(basket);
        }




        public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto)
        {
            var mapBasket = _mapper.Map<CustomerBasket>(basketDto);
            var basket = await _basketRepository.UpdateBasketAsync(mapBasket);

            if (basket is null) return null;

            return _mapper.Map<CustomerBasketDto>(basket);
        }



        public async Task<bool?> DeleteBasketAsync(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }

       
    }
}
