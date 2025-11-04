using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Application.Services;
using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;
using ClearBank.DeveloperTest.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace ClearBank.DeveloperTest.Application.UnitTests;

[TestClass]
public class PaymentServiceTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly PaymentService _paymentService;
    
    public PaymentServiceTests()
    {
        _paymentService = new PaymentService();
        _accountRepositoryMock = new Mock<IAccountRepository>();
    }
    
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
    public void MakePayment_WithInvalidDebtorAccountRequest_ReturnsUnsuccessfulResult()
    {
        var invalidAccountNumber = "invalidAccountNumber";
        _accountRepositoryMock.Setup(x => x.GetAccount(invalidAccountNumber)).Returns((Account?)null);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = invalidAccountNumber,
            PaymentScheme = PaymentScheme.Bacs,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
    }
}
