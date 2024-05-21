using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Rocket.Core.Entities;
using Rocket.Core.Repository;
using Rocket.DTOS;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Rocket.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _BasketRepo;
        private readonly IMapper _map;

        public BasketController(IBasketRepository basketrepo, IMapper map)
        {
            _BasketRepo = basketrepo;
            _map = map;
        }
        [HttpGet] // api/basket?id=3
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _BasketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost] //  api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var MappedBasket = _map.Map<CustomerBasketDTO, CustomerBasket>(basket);

            var createdORupdated=await _BasketRepo.updateBasketAsync(MappedBasket);
            return Ok(createdORupdated);
        }

        [HttpDelete]
        public async Task DeleteBasket(string ID)
        {
            await _BasketRepo.DeleteBasketAsync(ID);
        }
    }
}
