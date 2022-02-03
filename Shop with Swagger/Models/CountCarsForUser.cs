using System;

namespace Shop_with_Swagger.Models
{
    public class CountCarsForUser
    {
        public int Id { get; set; }
        public int CountOfCars { get; set; } = 1;

        public string UserId { get; set; }
        public User User { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
