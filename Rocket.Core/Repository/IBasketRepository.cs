using Rocket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Repository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basktId);
        Task<CustomerBasket> updateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
