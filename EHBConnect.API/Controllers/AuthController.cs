using Microsoft.AspNetCore.Mvc;

namespace EHBConnect.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
