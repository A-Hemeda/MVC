using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Basket;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;

namespace Store.Controllers
{
    public class BasketController : BaseApiController
    {

        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet] // Get : /api/basket?id=...
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            if (id is null) return BadRequest();
            var resukt = await _basketRepository.GetBasketAsync(id);
            if (resukt is null)
            {
                new CustomerBasket() { Id = id };
            }
            return Ok(resukt);
        }

        [HttpPost] // post : api/basket
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto model)
        {
           var result = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model));
           if(result is null)
           {
               return BadRequest();
           }
            return Ok(result);
        }



        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
