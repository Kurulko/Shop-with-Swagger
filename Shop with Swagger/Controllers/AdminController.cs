using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_with_Swagger.Models;
using System;
using System.Linq;

namespace Shop_with_Swagger.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        StoreContext db;
        public AdminController(StoreContext context)
        {
            db = context;
        }

        [HttpPut]
        public string Add(Car car)
        {
            string resultStr = string.Empty;
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                resultStr = $"Car with carId == {car.Id} was added to the database";
            }
            else
            {
                resultStr = $"Car with carId == {car.Id} wasn't added to the database : ";
                foreach (var error in ModelState)
                    resultStr += $" {error.Key} - {error.Value} \n";
            }
            return resultStr;
        }

        [HttpDelete]
        public string Delete(int? carId)
        {
            string resultStr = string.Empty;
            if (carId != null)
            {
                Car car = db.Cars.FirstOrDefault(c => c.Id == carId);
                if (car != null)
                {
                    db.Cars.Remove(car);
                    db.SaveChanges();
                    resultStr = $"Car with carId == {car.Id} was deleted";
                }
                else
                    resultStr = $"Car with carId == {carId} wasn't deleted";
            }
            else
                resultStr = $"Car with carId == {carId} wasn't found : ";
            return resultStr;
        }

        [HttpPost]
        public string Update(Car car)
        {
            string resultStr = string.Empty;
            if (ModelState.IsValid)
            {
                db.Cars.Update(car);
                db.SaveChanges();
                resultStr = $"Car with carId == {car.Id} was updated";
            }
            else
            {
                resultStr = $"Car with carId == {car.Id} wasn't updated : ";
                foreach (var error in ModelState)
                    resultStr += $" {error.Key} - {error.Value} \n";
            }
            return resultStr;
        }
    }
}
