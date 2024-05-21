using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rocket.Core.Services;
using Rocket.DTOS;
using Rocket.Errors;
using System.Threading.Tasks;

namespace Rocket.Controllers
{
    //[Authorize]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentServices _paymentservice;

        public PaymentsController(IPaymentServices paymentservice)
        {
            _paymentservice = paymentservice;
        }
        [HttpPost("{basketId}")] //  /api/Payments/basketId
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentintet(string basketId)
        {
            var basket = await _paymentservice.CreateOrUpdatePaymentIntent(basketId);
            if(basket == null)  return BadRequest(new ApiResponse(400 , "a problem with your basket ")); 
            return Ok(basket);
        }
    }
}
