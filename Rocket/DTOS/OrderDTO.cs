using Rocket.Core.Entities.Identity.OrderAgreggate;

namespace Rocket.DTOS
{
    //
    public class OrderDTO
    {
      
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }// of user
    }
}
 