using Rocket.Core.Entities.Identity.OrderAgreggate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Specification
{
    public class OrderByPaymentIntentIdSpecification :BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId):base(o=>o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
