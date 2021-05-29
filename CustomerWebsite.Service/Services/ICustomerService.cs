using System.Collections.Generic;

namespace CustomerWebsite.Service.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerInformationValue> GetCustomerDetailsByCustomerCategoryName(string customerCategoryName);
    }
}
