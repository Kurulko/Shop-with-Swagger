using Microsoft.AspNetCore.Mvc;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhoController : ControllerBase
    {
        [HttpGet]
        public string Who()
        {
            bool isAdmin = User.IsInRole("Admin");
            bool isBuyer = User.IsInRole("Buyer");
            return $"Admin? {isAdmin} - Buyer? {isBuyer}";
        }
    }
}
