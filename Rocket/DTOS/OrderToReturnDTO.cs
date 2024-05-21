using Rocket.Core.Entities.Identity.OrderAgreggate;
using System.Collections.Generic;
using System;

namespace Rocket.DTOS
{
    public class OrderToReturnDTO
    {
        public int Id { get; init; }
        public string ByerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }  //navigation prop
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }  // navigation property
        public string PaymentIntentId { get; set; }
        public decimal Total { get; set; }
    }
}
