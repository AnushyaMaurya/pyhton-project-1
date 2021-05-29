using CustomerWebsite.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CustomerWebsite.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerInformationController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerInformationController> _logger;

        public CustomerInformationController(ILogger<CustomerInformationController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerCategoryName"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CustomerInformationValue>> GetCustomerDetailsByCustomerCategoryName(string customerCategoryName)
        {
            try
            {
                var result =  _customerService.GetCustomerDetailsByCustomerCategoryName(customerCategoryName);

                if(result == null)
                {
                    string message = $"No Customer found with CustomerCategoryName [{customerCategoryName}]";
                    _logger.LogInformation(message);
                    return NotFound(message);
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the customer details for [{customerCategoryName}]");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }       
        }
    }
}
