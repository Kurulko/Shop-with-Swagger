using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_with_Swagger.Models;
using Shop_with_Swagger.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChangeShoppingCartController : ControllerBase
    {
        StoreContext db;
        UserManager<User> _userManager;
        public ChangeShoppingCartController(StoreContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [HttpPut]
        public string Add(int carId)
        {
            string resultStr = string.Empty;

            Car car = db.Cars.FirstOrDefault(c => c.Id == carId);

            if (car != null)
            {
                string userId = _userManager.GetUserId(User);
                User user = db.Users
                    .Include(u => u.Cars)
                    .Include(u => u.Counts).FirstOrDefault(u => u.Id == userId);

                if (user.Cars?.Contains(car) ?? false)
                {
                    CountCarsForUser count = user.Counts.FirstOrDefault(c => c.UserId == userId && c.CarId == car.Id);
                    count.CountOfCars++;
                }
                else
                {
                    CountCarsForUser count = new CountCarsForUser
                    {
                        UserId = userId,
                        CarId = car.Id
                    };
                    user.Cars.Add(car);
                    user.Counts.Add(count);
                }
                db.SaveChanges();
                resultStr = $"Car with carId == {car.Id} was added to the cart";
            }
            else
                resultStr = $"Car with carId == {car.Id} wasn't found";
            return resultStr;
        }

        [HttpDelete]
        public string Delete(int carId, bool all = false)
        {
            string resultStr = string.Empty;

            Car car = db.Cars.FirstOrDefault(c => c.Id == carId);

            if (car != null)
            {
                string userId = _userManager.GetUserId(User);
                User user = db.Users
                    .Include(u => u.Cars)
                    .Include(u => u.Counts)
                    .FirstOrDefault(u => u.Id == userId);

                if (!(user.Cars.Contains(car) && user.Counts
                    .FirstOrDefault(c => c.UserId == userId && c.CarId == car.Id).CountOfCars > 1) || all)
                {
                    user.Cars.RemoveAll(c => c.Id == carId);
                    db.Counts.RemoveRange(db.Counts.Where(c => c.UserId == userId && c.CarId == car.Id));
                }
                else
                    user.Counts.FirstOrDefault(c => c.CarId == car.Id).CountOfCars--;
                db.SaveChanges();
                resultStr = $"Car with carId == {car.Id} was deleted to the cart";
            }
            else
                resultStr = $"Car with carId == {car.Id} wasn't found";
            return resultStr;
        }
    }
}




