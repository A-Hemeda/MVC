using Store.Core.Dtos.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IBasketService
    {
        Task<CustomerBasketDto?> GetBasketAsync(string BasketId);

        Task <CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto);

        Task<bool?> DeleteBasketAsync(string basketId);

    }
}
