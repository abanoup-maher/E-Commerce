using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Entities.Identity.OrderAgreggate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            this.quantity = quantity;
        }

        public ProductItemOrder Product { get; set;}
        public decimal Price { get; set; }
        public int quantity { get; set; }
    }
}
