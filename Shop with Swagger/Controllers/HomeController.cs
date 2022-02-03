using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop_with_Swagger.Models;
using Shop_with_Swagger.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Shop_with_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        StoreContext db;
        public HomeController(StoreContext context)
        => db = context;

        [HttpGet]
        public List<Car> Index()
            => db.Cars.ToList();
    }
}
