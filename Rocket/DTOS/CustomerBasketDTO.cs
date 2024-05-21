using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rocket.DTOS
{
    public class CustomerBasketDTO
    {

        [Required]
        public string Id { get; set; }
        public List <BasketItemDTO> Items { get; set; }=new List<BasketItemDTO>();

        public decimal shippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string PaymentIntend { get; set; }
        public string ClientSecret { get; set; }
    }
}
