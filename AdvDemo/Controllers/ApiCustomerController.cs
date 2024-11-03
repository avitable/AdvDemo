using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvDemo.Controllers
{
    [ApiController]
    [Route("/api/v1/customers/")]
    public class ApiCustomerController : Controller
    {

        private readonly AdventureWorksContext _context;

        public ApiCustomerController(AdventureWorksContext context)
        {
            _context = context;
        }

        /*
        // GET: ProductCategories
        [HttpGet("get")]
        [HttpGet("")]
        public List<ProductCategory> ListCustomerss()
        {
           //
        }
        */
    }
}
