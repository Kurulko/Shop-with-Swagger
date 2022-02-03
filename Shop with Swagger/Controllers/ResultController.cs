using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_with_Swagger.Models;
using System;
using System.Linq;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultController : ControllerBase
    {
        StoreContext db;
        UserManager<User> _userManager;
        public ResultController(StoreContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [HttpGet]
        public string Result()
        {
            string userId = _userManager.GetUserId(User);
            User user = db.Users
                .Include(u => u.Cars)
                .Include(u => u.Orders)
                .Include(u => u.Counts)
                .FirstOrDefault(u => u.Id == userId);

            if (user.Counts.Count != 0)
            {
                Order order = new Order { Number = Guid.NewGuid().ToString() };

                user.Orders.Add(order);
                user.Cars.RemoveAll(c => true);
                db.Counts.RemoveRange(db.Counts.Where(c => c.UserId == userId));
                db.SaveChanges();
                return "Your order # " + user.Orders.LastOrDefault().Number;
            }
            return "The cart was empty ...";
        }
    }
}
