using AdvDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvDemo.Controllers
{
    public class CustomerController : Controller
    {

        private AdventureWorksContext _context;
        public CustomerController(AdventureWorksContext ctx) 
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
