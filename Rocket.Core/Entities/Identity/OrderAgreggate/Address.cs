using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Entities.Identity.OrderAgreggate
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string fName, string lName, string country, string city, string street)
        {
            FName = fName;
            LName = lName;
            Country = country;
            City = city;
            Street = street;
        }

        //عنوان الاوردر مش هيتحول لجدول 
        public string FName { get; set; }
        public string LName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
