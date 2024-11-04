using AdvDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvDemo.Api
{
    public class CustomerHandler
    {

        protected AdventureWorksContext _context;

        public CustomerHandler(AdventureWorksContext ctx) 
        {
            _context = ctx;
        }

        public Task<List<Customer>> GetCustomers()
        {
            return _context.Customers.ToListAsync();
        }

    }
}
