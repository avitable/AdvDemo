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

        public IActionResult ManageCustomer(int id)
        {

            // get customer, address, orders, and order details.

            var cust = _context.Customers.Where(c => c.CustomerId == id)
                           .Include(c => c.CustomerAddresses)
                           .Include(c => c.SalesOrderHeaders)
                           .ThenInclude(soh => soh.SalesOrderDetails);

            return View();
        }

    }
}
