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
    private readonly IPaymentService _paymentService;
    
    public PaymentServiceTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _paymentService = new PaymentService(_accountRepositoryMock.Object);
    }
    
    [TestMethod]
    public void MakePayment_WithAllowedPaymentScheme_UpdatesAccountBalance()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 20, AccountStatus.Live, AllowedPaymentSchemes.Bacs);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = accountNumber,
            PaymentScheme = PaymentScheme.Bacs,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeTrue();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 10)), Times.Once);
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
    
    [TestMethod]
    public void MakePayment_WithDisallowedPaymentScheme_ReturnsUnsuccessfulResult()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 0, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = accountNumber,
            PaymentScheme = PaymentScheme.Chaps,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }
    
    [TestMethod]
    public void MakePayment_WithFasterPaymentScheme_AndWithBalanceGreaterThanRequestAmount_UpdatesAccountBalance()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = accountNumber,
            PaymentScheme = PaymentScheme.FasterPayments,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeTrue();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 90)), Times.Once);    
    }
    
    [TestMethod]
    public void MakePayment_WithFasterPaymentScheme_AndWithBalanceLessThanRequestAmount_ReturnsUnsuccessfulResult()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 0, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = accountNumber,
            PaymentScheme = PaymentScheme.FasterPayments,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never());    
    }
    
    [TestMethod]
    public void MakePayment_WithChapsPaymentScheme_AndLiveAccount_UpdatesAccountBalance()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 100, AccountStatus.Live, AllowedPaymentSchemes.Chaps);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = accountNumber,
            PaymentScheme = PaymentScheme.Chaps,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeTrue();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 90)), Times.Once);    
    }
    
    [TestMethod]
    public void MakePayment_WithChapsPaymentScheme_AndNonLiveAccount_ReturnsUnsuccessfulResult()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 0, AccountStatus.Disabled, AllowedPaymentSchemes.Chaps);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);
        
        var request = new MakePaymentRequest
        {
            Amount = 10,
            CreditorAccountNumber = "creditorAccountNumber",
            DebtorAccountNumber = accountNumber,
            PaymentScheme = PaymentScheme.Chaps,
            PaymentDate = DateTime.Now
        };
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never());    
    }
}
