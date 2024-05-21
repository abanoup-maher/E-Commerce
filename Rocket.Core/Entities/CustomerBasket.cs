using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }=new List<BasketItem>();
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public decimal shippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string PaymentIntend { get; set; }
        public string ClientSecret { get; set; }
        

    }
}
