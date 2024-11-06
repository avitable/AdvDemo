using AdvDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace AdvDemo.Pages
{
    public class CustomerManagementModel : PageModel
    {

        private readonly ILogger<CustomerManagementModel> _logger;
        private AdventureWorksContext _context;

        [BindProperty]
        public CustomerBindingModel CustomerBindingModel { get; set; } = new CustomerBindingModel();

        [BindProperty]
        public List<AddressBindingModel> AddressBindingModel { get; set; } = new List<AddressBindingModel>();

        public Customer? Customer { get; set; }

        public CustomerManagementModel(AdventureWorksContext context, ILogger<CustomerManagementModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public PageResult OnGet(int customerId)
        {
            // This should be factored out, but I'm in a smidge of a hurry.
            Customer = _context.Customers.Where(c => c.CustomerId == customerId)
               .Include(c => c.CustomerAddresses)
               .ThenInclude(ca => ca.Address)
               .Include(c => c.SalesOrderHeaders)
               .ThenInclude(soh => soh.SalesOrderDetails).First<Customer>();

            CustomerBindingModel = new CustomerBindingModel
            {
                FirstName = Customer.FirstName,
                LastName = Customer.LastName,
                EmailAddress = Customer.EmailAddress,
                Phone = Customer.Phone,
                CustomerId = customerId
            };
            
            foreach (CustomerAddress addy in Customer.CustomerAddresses)
            {

                AddressBindingModel.Add(
                    new AddressBindingModel
                    {
                        AddressLine1 = addy.Address.AddressLine1,
                        AddressLine2 = addy.Address.AddressLine2,
                        City = addy.Address.City,
                        StateProvince = addy.Address.StateProvince,
                        PostalCode = addy.Address.PostalCode,
                        CountryRegion = addy.Address.CountryRegion
                    }
                );

            }

            return Page();
        }

        public IActionResult OnPost() 
        {
            Customer = _context.Customers.Where(c => c.CustomerId == CustomerBindingModel.CustomerId).First();

            Customer.FirstName = CustomerBindingModel.FirstName;
            Customer.LastName = CustomerBindingModel.LastName;
            Customer.EmailAddress = CustomerBindingModel.EmailAddress;
            Customer.Phone = CustomerBindingModel.Phone;

            _context.SaveChanges();

            return Redirect(Customer.CustomerId.ToString());
        }
    }

    public class CustomerBindingModel
    {
        [Required]
        [HiddenInput]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Phone]
        [Display(Name = "Mobile Phone")]
        public string? Phone { get; set; }

    }

    public class AddressBindingModel
    {
        [Required]
        [StringLength(60)]
        [Display(Name = "Address Line 1")]
        public string? AddressLine1 { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Address Line 2")]
        public string? AddressLine2 { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "State / Province")]
        public string? StateProvince { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Country")]
        public string? CountryRegion { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

    }



}

