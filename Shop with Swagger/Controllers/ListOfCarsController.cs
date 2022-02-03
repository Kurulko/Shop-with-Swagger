using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_with_Swagger.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ListOfCarsController : ControllerBase
    {
        StoreContext db;
        UserManager<User> _userManager;
        public ListOfCarsController(StoreContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [HttpGet]
        public Cart ListOfCars()
        {
            string userId = _userManager.GetUserId(User);
            User user = db.Users
                .Include(u => u.Cars)
                .Include(u => u.Counts)
                .FirstOrDefault(u => u.Id == userId);

            Cart cart = new();
            cart.Title = "My shopping list";
            cart.Cars = new List<CarForCart>();
            foreach (Car car in user.Cars)
                cart.Cars.Add(new CarForCart { Name = car.Name, Price = car.PriceDollars, Id = car.Id});
            return cart;
        }
    }
    public class CarForCart
    {
        public string Name { get; set; }
        public int? Price { get; set; }
        public int Id { get; set; }
    }
    public class Cart
    {
        public List<CarForCart> Cars { get; set; }
        public string Title { get; set; }
    }
}
