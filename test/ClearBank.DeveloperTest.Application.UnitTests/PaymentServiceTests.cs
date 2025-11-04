using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Application.Services;
using ClearBank.DeveloperTest.Domain.Enums;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Application.UnitTests;

[TestClass]
public class PaymentServiceTests
{
    [TestMethod]
    public void MakePayment_WithValidRequest_ReturnsSuccessfulResult()
    {
        var service = new PaymentService();
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = "debtorAccountNumber",
            PaymentScheme = PaymentScheme.Bacs,
            PaymentDate = DateTime.Now
        };
        var result = service.MakePayment(request);

        result.Success.Should().BeTrue();
    }
    
    [TestMethod]
    public void MakePayment_WithInvalidRequest_ReturnsUnsuccessfulResult()
    {
        var service = new PaymentService();
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = "debtorAccountNumber",
            PaymentScheme = PaymentScheme.Bacs,
            PaymentDate = DateTime.Now
        };
        var result = service.MakePayment(request);

        result.Success.Should().BeFalse();
    }
}
