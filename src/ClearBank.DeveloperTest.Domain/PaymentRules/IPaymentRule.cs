using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Domain.PaymentRules;

public interface IPaymentRule
{
    PaymentScheme Scheme { get; }
    bool MakePayment(Account account, AccountStatus accountStatus, decimal requestAmount);
}