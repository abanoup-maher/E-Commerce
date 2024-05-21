using Rocket.Core.Entities;
using Rocket.Core.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rocket.Repository
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
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basktId)
        {
            var basket = await _database.StringGetAsync(basktId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> updateBasketAsync(CustomerBasket basket)
        {
            var createORupdate = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if(createORupdate==false) { return null; }
           
            return await GetBasketAsync(basket.Id);
        }
    }
}
