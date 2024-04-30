using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Orders_Aggregate
{
    public class OrderAddress
    {

        public OrderAddress()
        {
            
        }
        public OrderAddress(string firstName, string lastName, string streat, string city, string country, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Streat = streat;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Streat { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }


    }
}
