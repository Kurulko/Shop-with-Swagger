using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_with_Swagger.Models;
using System.Linq;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DescriptionController : ControllerBase
    {
        StoreContext db;
        public DescriptionController(StoreContext context)
         => db = context;

        [HttpGet]
        public string Description(int carId)
        {
            Car car = db.Cars.FirstOrDefault(c => c.Id == carId);

            if (car != null)
                return car.Description;
            return $"Car with carId == {carId} wasn't found";
        }
    }
}
