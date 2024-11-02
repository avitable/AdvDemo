using AdvDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvDemo.Controllers
{
    [ApiController]
    [Route("/api/v1/productCategories/")]
    public class ApiProductCategoryController : Controller
    {

        private readonly AdventureWorksContext _context;

        public ApiProductCategoryController(AdventureWorksContext context)
        {
            _context = context;
        }

        // GET: ProductCategories
        [HttpGet("get")]
        [HttpGet("")]
        public List<ProductCategory> ListProductCategories()
        {
            return _context.ProductCategories.Include(p => p.ParentProductCategory).ToList();
        }

    }
}
