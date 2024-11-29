using StackExchange.Redis;
using Store.Core.Entities;
using Store.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var result = await _database.KeyDeleteAsync(basketId);
            return result;
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var result = await _database.StringGetAsync(BasketId);
            
            // if result is null return null -- and if is not null convert from customerBasket to redis.Value
            return result.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(result);
        }   

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize<CustomerBasket>(basket), TimeSpan.FromDays(30));
            if (CreatedOrUpdatedBasket is false) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
