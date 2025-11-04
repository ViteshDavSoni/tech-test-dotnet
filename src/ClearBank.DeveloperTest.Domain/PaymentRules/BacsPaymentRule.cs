using ClearBank.DeveloperTest.Domain.Entities;
using ClearBank.DeveloperTest.Domain.Enums;

namespace ClearBank.DeveloperTest.Domain.PaymentRules;

public class BacsPaymentRule : PaymentRule
{
    public override PaymentScheme Scheme => PaymentScheme.Bacs;
    
    public override bool ValidatePayment(Account account, AccountStatus accountStatus, decimal requestAmount)
        => account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
}