using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using Rocket.Core.Services;
using Rocket.Core.Specification;
using Rocket.DTOS;
using Rocket.Errors;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rocket.Controllers
{
    //[Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderservices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderservices, IMapper mapper)
        {
            _orderservices = orderservices;
            _mapper = mapper;
        }
        [HttpPost]//  /api  / Orders
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderdto)
         {
            var buyeremail= User.FindFirstValue(ClaimTypes.Email);
            var orderaddress= _mapper.Map<AddressDto,Address>(orderdto.ShippingAddress);
            var order = await _orderservices.CreateOrderAsync(buyeremail, orderdto.BasketId, orderdto.DeliveryMethodId, orderaddress);
            if(order == null)  return BadRequest(new ApiResponse(400)); 
            return Ok(_mapper.Map<Order,OrderToReturnDTO> (order));

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrderForUser()
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderservices.GetOrderForUserAsync(buyeremail);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList< OrderToReturnDTO>> (orders));

        }
        [HttpGet("{id}")] // /api/orders/id
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdForUser(int id)
        {
            var buyeremail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderservices.GetOrderByIdAsync(id, buyeremail);
            if (orders == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDTO>(orders));
        }
        // bayza 

        [HttpGet("deliveryMethods")] // api/Orders/deliverMethod
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var dlev_meth = await _orderservices.GetDeliveryMethodAsync();
            return Ok(dlev_meth);
        }
    }
}
