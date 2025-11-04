using ClearBank.DeveloperTest.Application.Dtos;
using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Application.PaymentRules;
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

        var paymentRules = new List<IPaymentRule>
        {
            new BacsPaymentRule(),
            new ChapsPaymentRule(),
            new FasterPaymentsRule()
        };

        var paymentRuleFactory = new PaymentRuleFactory(paymentRules);
        _paymentService = new PaymentService(_accountRepositoryMock.Object, paymentRuleFactory);
    }

    [DataTestMethod]
    [DataRow(PaymentScheme.Bacs, AllowedPaymentSchemes.Bacs, true)]
    [DataRow(PaymentScheme.FasterPayments, AllowedPaymentSchemes.FasterPayments, true)]
    [DataRow(PaymentScheme.Chaps, AllowedPaymentSchemes.Chaps, true)]
    [DataRow(PaymentScheme.Bacs, AllowedPaymentSchemes.FasterPayments, false)]
    [DataRow(PaymentScheme.Bacs, AllowedPaymentSchemes.Chaps, false)]
    [DataRow(PaymentScheme.FasterPayments, AllowedPaymentSchemes.Bacs, false)]
    [DataRow(PaymentScheme.FasterPayments, AllowedPaymentSchemes.Chaps, false)]
    [DataRow(PaymentScheme.Chaps, AllowedPaymentSchemes.Bacs, false)]
    [DataRow(PaymentScheme.Chaps, AllowedPaymentSchemes.FasterPayments, false)]
    public void MakePayment_WithVariousPaymentSchemeCombinations_ReturnsExpectedResult(
        PaymentScheme paymentScheme,
        AllowedPaymentSchemes allowedSchemes,
        bool expectedSuccess)
    {
        var accountNumber = "accountNumber";
        
        var account = new Account(accountNumber, 20, AccountStatus.Live, allowedSchemes);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

        var request = new MakePaymentRequest(
            "creditorAccountNumber",
            accountNumber,
            10,
            DateTime.Now,
            paymentScheme);
        
        var result = _paymentService.MakePayment(request);
        result.Success.Should().Be(expectedSuccess);

        if (expectedSuccess)
        {
            _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 10)), Times.Once);
        }
        else
        {
            _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
        }
    }

    [TestMethod]
    public void MakePayment_InvalidAccount_ReturnsUnsuccessful()
    {
        var invalidAccountNumber = "invalidAccountNumber";
        _accountRepositoryMock.Setup(x => x.GetAccount(invalidAccountNumber)).Returns((Account?)null);

        var request = new MakePaymentRequest(
            "creditorAccountNumber", 
            invalidAccountNumber, 
            10, 
            DateTime.Now, 
            PaymentScheme.Chaps);

        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
    }

    [TestMethod]
    public void MakePayment_FasterPayment_WithSufficientBalance_UpdatesBalance()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

        var request = new MakePaymentRequest(
            "creditorAccountNumber", 
            accountNumber, 
            10, 
            DateTime.Now, 
            PaymentScheme.FasterPayments);

        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeTrue();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 90)), Times.Once);
    }

    [TestMethod]
    public void MakePayment_FasterPayment_InsufficientBalance_ReturnsUnsuccessful()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 0, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

        var request = new MakePaymentRequest(
            "creditorAccountNumber", 
            accountNumber, 
            10, 
            DateTime.Now, 
            PaymentScheme.FasterPayments);

        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }

    [TestMethod]
    public void MakePayment_ChapsPayment_LiveAccount_UpdatesBalance()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 100, AccountStatus.Live, AllowedPaymentSchemes.Chaps);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

        var request = new MakePaymentRequest(
            "creditorAccountNumber", 
            accountNumber, 
            10, 
            DateTime.Now, 
            PaymentScheme.Chaps);

        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeTrue();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 90)), Times.Once);
    }

    [TestMethod]
    public void MakePayment_ChapsPayment_NonLiveAccount_ReturnsUnsuccessful()
    {
        var accountNumber = "accountNumber";
        var account = new Account(accountNumber, 0, AccountStatus.Disabled, AllowedPaymentSchemes.Chaps);
        _accountRepositoryMock.Setup(x => x.GetAccount(accountNumber)).Returns(account);

        var request = new MakePaymentRequest(
            "creditorAccountNumber", 
            accountNumber, 
            10, 
            DateTime.Now, 
            PaymentScheme.Chaps);
        
        var result = _paymentService.MakePayment(request);

        result.Success.Should().BeFalse();
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }
}