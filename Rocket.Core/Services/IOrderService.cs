using Rocket.Core.Entities.Identity.OrderAgreggate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deleveryMethodId, Address ShippingSddress);

        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyeremail);
        Task<Order> GetOrderByIdAsync(int orderid , string buyeremail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync(); 
    }
}
