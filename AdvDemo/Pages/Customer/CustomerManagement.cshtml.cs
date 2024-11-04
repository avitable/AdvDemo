using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdvDemo.Pages
{
    public class CustomerManagementModel : PageModel
    {

        private readonly ILogger<CustomerManagementModel> _logger;
        private AdventureWorksContext _context;

        public CustomerManagementModel(AdventureWorksContext context, ILogger<CustomerManagementModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public PageResult OnGet(int customerId)
        {
            // This should be factored out, but I'm in a smidge of a hurry.
            var cust = _context.Customers.Where(c => c.CustomerId == customerId)
               .Include(c => c.CustomerAddresses)
               .Include(c => c.SalesOrderHeaders)
               .ThenInclude(soh => soh.SalesOrderDetails);

            int i = 0;
            return Page();
        }
    }
}
