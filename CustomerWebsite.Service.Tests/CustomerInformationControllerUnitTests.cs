using CustomerWebsite.Service.Controllers;
using CustomerWebsite.Service.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerWebsite.Service.Tests
{
    [Trait("Category", "Unit")]
    public class CustomerInformationControllerUnitTests
    {
        private readonly Mock<ILogger<CustomerInformationController>> _logger;
        private readonly Mock<ICustomerService> _orderStatusService;
        private readonly CustomerInformationController _subject;

        public CustomerInformationControllerUnitTests()
        {
            _logger = new Mock<ILogger<CustomerInformationController>>();
            _orderStatusService = new Mock<ICustomerService>();

            _subject = new CustomerInformationController(
                _logger.Object,
                _orderStatusService.Object);
        }

        [Fact]
        public void GetCustomerDetails_GivenCustomerCategoryName_ShouldReturnCorrectdetails()
        {
            // Given
            const string customerCategoryName = "Gift Store";

            var customerInfo = new List<CustomerInformationValue>()
            {
                new CustomerInformationValue()
                {
                    CustomerName = "Abhra Ganguly",
                    PrimaryContact = "Abhra Ganguly",
                    PhoneNumber = "(212) 555-0100",
                    CityName = "Walker Valley"
                }
            };

            var expectedCustomerInfo = new List<CustomerInformationValue>()
            {
                new CustomerInformationValue()
                {
                    CustomerName = "Abhra Ganguly",
                    PrimaryContact = "Abhra Ganguly",
                    PhoneNumber = "(212) 555-0100",
                    CityName = "Walker Valley"
                }
            };

            _orderStatusService.Setup(x => x.GetCustomerDetailsByCustomerCategoryName(customerCategoryName)).Returns(customerInfo);

            // When
            var result = _subject.GetCustomerDetailsByCustomerCategoryName(customerCategoryName);
            var okResult = result.Result as OkObjectResult;

            // Then
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().BeEquivalentTo(expectedCustomerInfo);
        }

        [Fact]
        public void GetCustomerDetails_GivenCustomerCategoryName_AndDetailsNotFound_ShouldReturn404NotFound()
        {
            // Given
            const string customerCategoryName = "Some Category";

            List<CustomerInformationValue> customerInfo = null;

            _orderStatusService.Setup(x => x.GetCustomerDetailsByCustomerCategoryName(customerCategoryName)).Returns(customerInfo);

            // When
            var result = _subject.GetCustomerDetailsByCustomerCategoryName(customerCategoryName);
            var notFoundResult = result.Result as NotFoundObjectResult;

            // Then
            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            notFoundResult.Value.Should().Be($"No Customer found with CustomerCategoryName [{customerCategoryName}]");
        }

        [Fact]
        public void GetCustomerDetails_GivenCustomerCategoryName_AndExceptionIsThrown_ShouldReturn500InternalServerError()
        {
            // Given
            const string customerCategoryName = "Some Category";

            _orderStatusService.Setup(x => x.GetCustomerDetailsByCustomerCategoryName(customerCategoryName)).Throws(new Exception());

            // When
            var result = _subject.GetCustomerDetailsByCustomerCategoryName(customerCategoryName);
            var errorResult = result.Result as StatusCodeResult;

            // Then
            errorResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
