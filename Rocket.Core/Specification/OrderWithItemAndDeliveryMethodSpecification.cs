using Rocket.Core.Entities.Identity.OrderAgreggate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Specification
{
    public class OrderWithItemAndDeliveryMethodSpecification:BaseSpecification<Order>
    {
        // get all order for specific user
        public OrderWithItemAndDeliveryMethodSpecification(string buyerEmail):base(o=>o.ByerEmail==buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderWithItemAndDeliveryMethodSpecification(int orderid, string buyeremail)
            : base(m => m.ByerEmail == buyeremail && m.Id == orderid)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o=> o.Items);
        }

    }
}
