using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Domain.PaymentRules;

public class ChapsPaymentRule : PaymentRule
{
    public override PaymentScheme Scheme => PaymentScheme.Chaps;
    
    public override bool ValidatePayment(Account account, AccountStatus accountStatus, decimal requestAmount)
    => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
            account.Status == AccountStatus.Live;
}