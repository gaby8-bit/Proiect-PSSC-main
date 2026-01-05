using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC.Models
{
    
    
        public class Customer
        {
            public Guid Id { get; init; }
            public string Name { get; init; }
            public string Email { get; init; }
            public Address ShippingAddress { get; init; }

            public Customer(Guid id, string name, string email, Address address)
            {
                Id = id;
                Name = name;
                Email = email;
                ShippingAddress = address;
            }
        }
    
}
