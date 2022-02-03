using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Shop_with_Swagger.Models
{
    public class User : IdentityUser
    {
        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }

        public List<CountCarsForUser> Counts { get; set; }
    }
}
