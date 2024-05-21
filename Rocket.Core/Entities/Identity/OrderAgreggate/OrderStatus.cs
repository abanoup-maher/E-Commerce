using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Entities.Identity.OrderAgreggate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        pending,
        [EnumMember(Value ="Paument Recieved")]
        PaymentRecieved,
        [EnumMember(Value ="Payment Failed")]
        PaymentFailed
    }
}
