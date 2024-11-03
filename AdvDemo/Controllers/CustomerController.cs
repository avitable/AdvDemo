using Microsoft.AspNetCore.Mvc;

namespace AdvDemo.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
