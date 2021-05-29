using CustomerWebsite.Service.Modals;
using System.Collections.Generic;
using System.Linq;

namespace CustomerWebsite.Service.Services
{
    public class CustomerService : ICustomerService
    {
        WideWorldImportersContext _context;

        public CustomerService(WideWorldImportersContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<CustomerInformationValue> GetCustomerDetailsByCustomerCategoryName(string customerCategoryName)
        {
            var results = _context.Customers
                .Where(x => x.CustomerCategoryName == customerCategoryName);

            var selectResult = (from result in results
                           select new
                           {
                               result.CustomerName,
                               result.PrimaryContact,
                               result.PhoneNumber,
                               result.CityName
                           });

            List<CustomerInformationValue> customerInfo = selectResult.Select(x => new CustomerInformationValue() 
            { 
                CustomerName = x.CustomerName,
                PrimaryContact = x.PrimaryContact,
                PhoneNumber = x.PhoneNumber,
                CityName = x.CityName
            }).ToList();

            return customerInfo;
        }
    }
}
